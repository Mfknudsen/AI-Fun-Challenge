#region Libraries

using Runtime.Hardware;
using Sirenix.OdinInspector;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

#endregion

namespace Runtime.Body
{
    public sealed class LegAgent : Agent
    {
        #region Values

        [SerializeField] [Required] private Transform groundCheckTransform;

        [SerializeField] [Min(0)] private float groundCheckRadius;

        [SerializeField] private LayerMask groundLayerMask;

        private bool isGrounded, mustBeGrounded;

        [SerializeField] private Rotor hip, knee;
        [SerializeField] private Rotor angel;

        private Vector3 estimatedGroundPoint;

        private const float footGroundTolerance = 1;

        #endregion

        #region Build In States

        private void Update()
        {
            if (Physics.CheckSphere(this.groundCheckTransform.position, this.groundCheckRadius,
                    this.groundLayerMask))
            {
                if (!this.isGrounded && this.mustBeGrounded) this.AddReward(50);

                this.isGrounded = true;
            }
            else
            {
                if (this.isGrounded && this.mustBeGrounded) this.AddReward(-100);

                this.isGrounded = false;
            }

            this.UpdateBodyRewards();
        }

        #endregion

        #region Getters

        public Vector3 GetCurrentFootPosition() => this.groundCheckTransform.position;

        public Vector3 GetHipPosition() => this.hip.GetRotorHeadPosition();

        public Vector3 GetEstimatedGroundPointPosition() => this.estimatedGroundPoint;

        #endregion

        #region Setter

        public void SetMustBeGrounded(bool set) => this.mustBeGrounded = set;

        #endregion

        #region In

        /// <summary>
        /// Set a new desired leg ground touch point by offset.
        /// Offset to be multiplied by forward and right of hip transform.
        /// </summary>
        /// <param name="offset">X value to be multiplied by right of hip transform and Y value to be multiplied by forward of hip transform.</param>
        /// <returns>False if the leg must be grounded</returns>
        public void SetNewPoint(Vector2 offset)
        {
            Transform hipTransform = this.hip.transform;
            this.estimatedGroundPoint = hipTransform.position + hipTransform.forward * offset.y +
                                        hipTransform.right * offset.x;
        }


        public override void OnActionReceived(ActionBuffers actionBuffers)
        {
            this.hip.SetAngelDirection(actionBuffers.DiscreteActions[0]);
            this.knee.SetAngelDirection(actionBuffers.DiscreteActions[1]);
            this.angel.SetAngelDirection(actionBuffers.DiscreteActions[2]);
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            //Each individual angle of the rotors in the leg.
            sensor.AddObservation(this.hip.GetAngle());
            sensor.AddObservation(this.knee.GetAngle());
            sensor.AddObservation(this.angel.GetAngle());

            //Each individual angle turning direction of the rotors in the leg.
            sensor.AddObservation(this.hip.GetAngelDirection());
            sensor.AddObservation(this.knee.GetAngelDirection());
            sensor.AddObservation(this.angel.GetAngelDirection());

            //If it should be grounded and if it is currently grounded.
            sensor.AddObservation(this.mustBeGrounded);
            sensor.AddObservation(this.isGrounded);

            //The different positions of each individual rotor head turning point.
            sensor.AddObservation(this.hip.GetRotorHeadPosition());
            sensor.AddObservation(this.knee.GetRotorHeadPosition());
            sensor.AddObservation(this.angel.GetRotorHeadPosition());

            //The current position of the foot which should end at the ground point
            sensor.AddObservation(this.groundCheckTransform.position);

            //The desired point for the leg to stand on.
            sensor.AddObservation(this.estimatedGroundPoint);

            //The direction the foot should travel.
            sensor.AddObservation((this.estimatedGroundPoint - this.GetCurrentFootPosition()).normalized);
        }

        #endregion

        #region Internal

        private void UpdateBodyRewards()
        {
            this.AddReward(!this.isGrounded && this.mustBeGrounded ? -5 : 2);

            if (Vector3.Distance(this.GetCurrentFootPosition(), this.estimatedGroundPoint) < footGroundTolerance &&
                this.mustBeGrounded) this.AddReward(30);

            float groundDistanceWhenMovingLegReward = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Vector3 rayOffset = new Vector3(x, 0, z).normalized;
                    if (Physics.Raycast(this.groundCheckTransform.position + rayOffset, Vector3.down,
                            layerMask: this.groundLayerMask, maxDistance: 0.5f))
                        groundDistanceWhenMovingLegReward -= 5;
                    else
                        groundDistanceWhenMovingLegReward += 2;
                }
            }

            this.AddReward(groundDistanceWhenMovingLegReward);
        }

        #endregion
    }
}