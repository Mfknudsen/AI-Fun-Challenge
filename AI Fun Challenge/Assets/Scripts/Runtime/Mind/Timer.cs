#region Libraries

using UnityEngine.Events;

#endregion

namespace Runtime.Mind
{
    public struct Timer
    {
        #region Values

        private float currentTimeInSeconds;
        private readonly float totalTimeInSeconds;

        private readonly UnityEvent onDoneEvent;

        #endregion

        #region Build In States

        public Timer(float totalTimeInSeconds, UnityAction onDoneAction) : this()
        {
            this.currentTimeInSeconds = 0;
            this.totalTimeInSeconds = totalTimeInSeconds;
            this.onDoneEvent = new UnityEvent();
            this.onDoneEvent.AddListener(onDoneAction);
        }

        #endregion


        #region In

        public bool Update(float deltaTime)
        {
            this.currentTimeInSeconds += deltaTime;

            if (this.currentTimeInSeconds < this.totalTimeInSeconds)
                return false;

            this.onDoneEvent.Invoke();
            return true;
        }

        #endregion
    }
}