using System.Collections.Generic;
using Runtime.HardwareWrapper;

namespace Runtime.Body
{
    public sealed class Crawler
    {
        #region Values

        private BoardWrapper boardWrapper;

        private List<Leg> legs;

        #endregion

        #region Build In States

        public Crawler(BoardWrapper boardWrapper)
        {
            this.boardWrapper = boardWrapper;

            this.legs = new List<Leg>();
        }

        #endregion
    }
}