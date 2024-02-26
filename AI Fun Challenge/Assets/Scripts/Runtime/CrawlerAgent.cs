#region Libraries

using Runtime.Body;
using Runtime.Extensions;
using Sirenix.OdinInspector;
using Unity.AI.Navigation;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.AI;

#endregion

namespace Runtime
{
    [DefaultExecutionOrder(1)]
    public sealed class CrawlerAgent : Agent
    {
        #region Values

        [SerializeField] [RequiredIn(PrefabKind.PrefabAsset)]
        private LegAgent legAgentForwardLeft,
            legAgentForwardRight,
            legAgentMiddleLeft,
            legAgentMiddleRight,
            legAgentBackLeft,
            legAgentBackRight;

        [SerializeField] [RequiredIn(PrefabKind.PrefabInstance)]
        private NavMeshSurface navMeshSurface;

        [SerializeField] [RequiredIn(PrefabKind.PrefabAsset)]
        private NavMeshAgent navMeshAgent;

        private float lastDistanceToGoal = -1;

        #endregion

        #region Build in States

        private void Update() =>
            this.UpdateRewardsForBody();

        private void FixedUpdate() =>
            this.navMeshSurface.BuildNavMesh();

        #endregion

        #region In

        public override void OnActionReceived(ActionBuffers actions)
        {
            //Continuous actions 0-11 to create the next designated standing point for each individual leg.
            //Continuous actions 0-11 are grouped in groups of 2 being x and z offset.
            Vector2 forwardLeft = new(actions.ContinuousActions[0], actions.ContinuousActions[1]);
            Vector2 forwardRight = new(actions.ContinuousActions[2], actions.ContinuousActions[3]);
            Vector2 middleLeft = new(actions.ContinuousActions[4], actions.ContinuousActions[5]);
            Vector2 middleRight = new(actions.ContinuousActions[6], actions.ContinuousActions[7]);
            Vector2 backLeft = new(actions.ContinuousActions[8], actions.ContinuousActions[9]);
            Vector2 backRight = new(actions.ContinuousActions[10], actions.ContinuousActions[11]);

            //Discrete actions 0-5 to set which leg is allowed to be of the ground.
            bool forwardLeftAllowed = actions.DiscreteActions[0] == 1,
                forwardRightAllowed = actions.DiscreteActions[1] == 1,
                middleLeftAllowed = actions.DiscreteActions[2] == 1,
                middleRightAllowed = actions.DiscreteActions[3] == 1,
                backLeftAllowed = actions.DiscreteActions[4] == 1,
                backRightAllowed = actions.DiscreteActions[5] == 1;

            this.legAgentForwardLeft.SetMustBeGrounded(!forwardLeftAllowed);
            this.legAgentForwardRight.SetMustBeGrounded(!forwardRightAllowed);
            this.legAgentMiddleLeft.SetMustBeGrounded(!middleLeftAllowed);
            this.legAgentMiddleRight.SetMustBeGrounded(!middleRightAllowed);
            this.legAgentBackLeft.SetMustBeGrounded(!backLeftAllowed);
            this.legAgentBackRight.SetMustBeGrounded(!backRightAllowed);

            //The leg agent will return false if leg must be grounded.
            this.legAgentForwardLeft.SetNewPoint(forwardLeft);
            this.legAgentForwardRight.SetNewPoint(forwardRight);
            this.legAgentMiddleLeft.SetNewPoint(middleLeft);
            this.legAgentMiddleRight.SetNewPoint(middleRight);
            this.legAgentBackLeft.SetNewPoint(backLeft);
            this.legAgentBackRight.SetNewPoint(backRight);
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            //Individual leg information.
            sensor.AddObservation(this.legAgentForwardLeft.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentForwardLeft.GetHipPosition());
            sensor.AddObservation(this.legAgentForwardLeft.GetEstimatedGroundPointPosition());

            sensor.AddObservation(this.legAgentForwardRight.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentForwardRight.GetHipPosition());
            sensor.AddObservation(this.legAgentForwardRight.GetEstimatedGroundPointPosition());

            sensor.AddObservation(this.legAgentMiddleLeft.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentMiddleLeft.GetHipPosition());
            sensor.AddObservation(this.legAgentMiddleLeft.GetEstimatedGroundPointPosition());

            sensor.AddObservation(this.legAgentMiddleRight.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentMiddleRight.GetHipPosition());
            sensor.AddObservation(this.legAgentMiddleRight.GetEstimatedGroundPointPosition());

            sensor.AddObservation(this.legAgentBackLeft.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentBackLeft.GetHipPosition());
            sensor.AddObservation(this.legAgentBackLeft.GetEstimatedGroundPointPosition());

            sensor.AddObservation(this.legAgentBackRight.GetCurrentFootPosition());
            sensor.AddObservation(this.legAgentBackRight.GetHipPosition());
            sensor.AddObservation(this.legAgentBackRight.GetEstimatedGroundPointPosition());

            //Current position of the crawler
            sensor.AddObservation(this.transform.position);

            //Desired walk direction
            sensor.AddObservation(this.navMeshAgent.steeringTarget);
        }

        #endregion

        #region Internal

        private void UpdateRewardsForBody()
        {
            //The closer the crawler gets to its final destination the more rewarded it will be.
            //Going further from the final destination will reduce the reward
            if (this.navMeshAgent.hasPath)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (this.lastDistanceToGoal == -1)
                    this.lastDistanceToGoal = this.navMeshAgent.remainingDistance;

                this.AddReward(this.lastDistanceToGoal - this.navMeshAgent.remainingDistance);

                this.lastDistanceToGoal = this.navMeshAgent.remainingDistance;
            }

            //The crawlers up direction of the torso should always face upwards.
            //The greater the angle the greater the punish
            this.AddReward(-Vector3.Angle(Vector3.up, this.transform.up) + 5);

            //The crawler should attempt to face the direction its walking.
            //The greater the angle the greater the punish
            this.AddReward(-Vector3.Angle(
                               Quaternion.LookRotation(
                                   this.navMeshAgent.steeringTarget.VecMulti(1f, 0f, 1f) -
                                   this.transform.position.VecMulti(1, 0, 1),
                                   Vector3.up).eulerAngles,
                               this.transform.forward)
                           + 5);
        }

        #endregion
    }
}