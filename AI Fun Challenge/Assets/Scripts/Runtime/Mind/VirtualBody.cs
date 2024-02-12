#region Libraries

using Runtime.Body;
using UnityEngine;

#endregion

namespace Runtime.Mind
{
    public class VirtualBody
    {
        #region Values

        public const float MaxBodyHeightCentimeter = 20f;

        private Vector3 calculatedPosition = Vector3.zero;

        private Quaternion calculatedRotation = Quaternion.identity;

        #endregion

        #region Build In States

        public VirtualBody(Leg[] legs)
        {
        }

        #endregion

        #region Getters

        public Vector3 GetPosition() => this.calculatedPosition;

        public Quaternion GetRotation() => this.calculatedRotation;

        #endregion

        #region In

        public void UpdatePosition(Vector3 toAdd) =>
            this.calculatedPosition += toAdd;

        public void UpdateRotation(Quaternion toAdd) =>
            this.calculatedRotation *= toAdd;

        #endregion
    }
}