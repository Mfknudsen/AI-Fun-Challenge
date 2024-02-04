namespace Runtime.HardwareWrapper
{
    public sealed class RotorWrapper
    {
        #region Values

        private Pin pin;

        #endregion

        #region Build In States

        public RotorWrapper(int pinNumber, BoardWrapper boardWrapper) => 
            this.pin = new Pin(pinNumber, boardWrapper);

        #endregion
    }
}