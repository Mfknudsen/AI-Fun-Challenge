#region Libraries

using Runtime.Body;
using Runtime.HardwareMimic;

#endregion

namespace Runtime.HardwareWrapper
{
    public sealed class BoardWrapper
    {
        #region Values

        private BoardMimic hardware;

        private Crawler crawler;

        #endregion

        #region Build In States

        public BoardWrapper(BoardMimic hardware)
        {
            this.hardware = hardware;

            this.crawler = new Crawler(this);
        }

        #endregion

        #region Getters

#if UNITY_EDITOR
        public Crawler GetCrawler()
        {
            return this.crawler;
        }
#endif

        #endregion

        #region In

        public void Update()
        {
            this.crawler.Update();
        }

        #endregion
    }

    public struct Pin
    {
        #region Values

        private int pinNumber;

        private BoardWrapper boardWrapper;

        #endregion

        #region Build In States

        public Pin(int pinNumber, BoardWrapper boardWrapper)
        {
            this.pinNumber = pinNumber;
            this.boardWrapper = boardWrapper;
        }

        #endregion

        #region Getters

        public int GetNumber() => this.pinNumber;

        public BoardWrapper GetBoard() => this.boardWrapper;

        #endregion

        #region Setter

        public void SetValue(float set)
        {
        }

        #endregion
    }
}