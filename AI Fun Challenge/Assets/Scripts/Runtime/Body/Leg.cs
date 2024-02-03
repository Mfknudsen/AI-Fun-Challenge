using Runtime.HardwareWrapper;

namespace Runtime.Body
{
    public sealed class Leg
    {
        #region Values

        private bool isGrounded;

        private RotorWrapper jointUpper, jointLower;

        #endregion

        #region Getters

        public bool GetIsGrounded() => this.isGrounded;

        #endregion
    }
}