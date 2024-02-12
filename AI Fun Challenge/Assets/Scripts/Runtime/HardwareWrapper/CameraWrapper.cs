namespace Runtime.HardwareWrapper
{
    public sealed class CameraWrapper : ComponentWrapper
    {
        #region Build In States

        public CameraWrapper(Pin pin) : base(pin.GetBoard())
        {
        }

        #endregion
    }
}