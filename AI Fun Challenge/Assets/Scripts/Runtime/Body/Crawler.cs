#region Libraries

using Runtime.HardwareWrapper;
using Runtime.Mind;

#endregion

namespace Runtime.Body
{
    public sealed class Crawler
    {
        #region Values

        private BoardWrapper boardWrapper;

        private Leg legForwardLeft,
            legForwardRight,
            legMiddleLeft,
            legMiddleRight,
            legBackLeft,
            legBackRight;

        private Brain brain;

        #endregion

        #region Build In States

        public Crawler(BoardWrapper boardWrapper)
        {
            this.boardWrapper = boardWrapper;

            this.legForwardLeft = new Leg(0, 1, 2, boardWrapper);
            this.legForwardRight = new Leg(3, 4, 5, boardWrapper);
            this.legMiddleLeft = new Leg(6, 7, 8, boardWrapper);
            this.legMiddleRight = new Leg(9, 10, 11, boardWrapper);
            this.legBackLeft = new Leg(12, 13, 14, boardWrapper);
            this.legBackRight = new Leg(15, 16, 17, boardWrapper);

            this.brain = new Brain();
        }

        #endregion

        #region In

        public void Update()
        {
        }

        #endregion

        #region Out

        /// <summary>
        ///     The brain should always aim to have at least three legs on the.
        ///     To be properly grounded it must have one leg on one side and two on the other or an combine total of more then
        ///     three legs.
        /// </summary>
        public bool IsProperlyGrounded()
        {
            int left = this.legForwardLeft.GetIsGrounded() + this.legBackLeft.GetIsGrounded() +
                       this.legMiddleLeft.GetIsGrounded(),
                right = this.legBackRight.GetIsGrounded() + this.legForwardRight.GetIsGrounded() +
                        this.legMiddleRight.GetIsGrounded();

            if (left + right >= 4)
                return true;

            if (left == 1 && right == 2)
                return true;

            return right == 1 && left == 2;
        }

        #endregion
    }
}