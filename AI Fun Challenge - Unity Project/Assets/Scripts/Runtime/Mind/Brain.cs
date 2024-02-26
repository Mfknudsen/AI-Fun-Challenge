using Runtime.Body;
using UnityEngine;

namespace Runtime.Mind
{
    public sealed class Brain
    {
        #region Values

        private VirtualBody virtualBody;

        private VirtualEnvironment virtualEnvironment;

        private bool isProperlyGrounded;

        #endregion

        #region Build In States

        public Brain(params LegAgent[] legs)
        {
            this.virtualBody = new VirtualBody(legs);
            this.virtualEnvironment = new VirtualEnvironment();
            Debug.Log("Brain constructor");
        }

        #endregion

        #region Getters

        public VirtualEnvironment GetEnvironment() => this.virtualEnvironment;

        public VirtualBody GetBody() => this.virtualBody;

        #endregion

        #region Setters

        public void SetIsProperlyGrounded(bool set) => this.isProperlyGrounded = set;

        public void SetDistanceMeasurements(Vector3[] input) =>
            this.virtualEnvironment.InputDistanceMeasurements(this.virtualBody.GetPosition(),
                this.virtualBody.GetRotation(), input);

        #endregion
    }
}