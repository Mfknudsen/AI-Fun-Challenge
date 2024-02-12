#region Libraries

using Runtime.Mind;

#endregion

namespace Runtime.HardwareWrapper
{
    public sealed class RotorWrapper : ComponentWrapper
    {
        #region Values

        private Pin pin;

        private bool onCooldown;

        #endregion

        #region Build In States

        public RotorWrapper(int pinNumber, BoardWrapper boardWrapper) : base(boardWrapper)
        {
            this.pin = new Pin(pinNumber, boardWrapper);
        }

        #endregion

        #region In

        public void SetRotorAngle(float angle)
        {
            if (this.onCooldown)
                return;

            this.pin.SetValue(1);

            this.boardWrapper.GetCrawler().AddTimer(new Timer(1, () =>
            {
                this.onCooldown = false;
                this.pin.SetValue(0);
            }));

            this.onCooldown = true;
        }

        #endregion
    }
}