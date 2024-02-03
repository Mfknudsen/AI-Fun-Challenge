#region Libraries

using Runtime.Body;
using Runtime.HardwareMimic;

#endregion

namespace Runtime.HardwareWrapper
{
    #region Enums

    public enum PinNumber
    {
        Pin1,
        Pin2,
        Pin3,
        Pin4,
        Pin5,
        Pin6,
        Pin7,
        Pin8
    }

    #endregion

    public sealed class BoardWrapper
    {
        #region Values

        private BoardMimic hardware;

        #endregion

        #region Build In States

        public BoardWrapper(BoardMimic hardware)
        {
            this.hardware = hardware;

            new Crawler(this);
        }

        #endregion
    }

    public struct Pin
    {
        #region Values

        private PinNumber pinNumber;

        private BoardWrapper boardWrapper;

        #endregion

        #region Build In States

        public Pin(PinNumber pinNumber, BoardWrapper boardWrapper)
        {
            this.pinNumber = pinNumber;
            this.boardWrapper = boardWrapper;
        }

        #endregion

        #region Setter

        public void SetValue(float set)
        {
        }

        #endregion
    }
}