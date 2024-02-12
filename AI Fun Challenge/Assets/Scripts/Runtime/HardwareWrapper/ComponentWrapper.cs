namespace Runtime.HardwareWrapper
{
    public class ComponentWrapper
    {
        #region Values

        protected BoardWrapper boardWrapper;

        #endregion

        #region Build In States

        public ComponentWrapper(BoardWrapper boardWrapper)
        {
            this.boardWrapper = boardWrapper;
        }

        #endregion
    }
}