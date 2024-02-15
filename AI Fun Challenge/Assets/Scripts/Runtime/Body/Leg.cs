#region Libraries

using Runtime.Hardware;
using Sirenix.OdinInspector;
using Unity.MLAgents.Policies;
using UnityEngine;

#endregion

namespace Runtime.Body
{
    public sealed class Leg : MonoBehaviour
    {
        #region Values

        [SerializeField] [RequiredIn(PrefabKind.PrefabInstance)]
        private Board board;

        [SerializeField] [Required] private BehaviorParameters behaviourParameters;

        [SerializeField] [Required] private Transform groundCheckTransform;

        [SerializeField] [Min(0)] private float groundCheckRadius;

        [SerializeField] private LayerMask groundLayerMask;

        private bool isGrounded, mustBeGrounded;

        [SerializeField] private Rotor hip, knee, angle;

        private Vector3 estimatedBodyConnectionPoint,
            newEstimatedBodyConnectionPoint,
            estimatedGroundPoint;

        #endregion

        #region Build In States

        private void Update()
        {
            this.isGrounded = Physics.CheckSphere(this.groundCheckTransform.position, this.groundCheckRadius,
                this.groundLayerMask);
        }

        #endregion

        #region Getters

        public int GetIsGrounded() => this.isGrounded ? 1 : 0;

        #endregion

        #region Setter

        public void SetMustBeGrounded(bool set)
        {
            this.mustBeGrounded = set;
        }

        #endregion
    }
}