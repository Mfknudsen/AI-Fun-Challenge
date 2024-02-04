#region Libraries

using Runtime.HardwareWrapper;

#endregion

namespace Runtime.Body
{
    public sealed class Leg
    {
        #region Values

        private bool isGrounded;

        private RotorWrapper jointUpper, jointMiddle, jointLower;

        #endregion

        #region Build In States

        public Leg(int upper, int middle, int lower, BoardWrapper boardWrapper)
        {
            this.jointUpper = new RotorWrapper(upper, boardWrapper);
            this.jointMiddle = new RotorWrapper(middle, boardWrapper);
            this.jointLower = new RotorWrapper(lower, boardWrapper);
        }

        #endregion

        #region Getters

        public int GetIsGrounded() => this.isGrounded ? 1 : 0;

        #endregion
    }
}