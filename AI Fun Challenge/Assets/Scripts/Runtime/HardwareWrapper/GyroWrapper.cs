namespace Runtime.HardwareWrapper
{
    public sealed class GyroWrapper : ComponentWrapper
    {
        #region Build In States

        public GyroWrapper(Pin pin) : base(pin.GetBoard())
        {
        }

        #endregion
    }
}