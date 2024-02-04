#region Libraries

using System.Collections.Generic;
using Runtime.HardwareWrapper;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Runtime.HardwareMimic
{
    [DefaultExecutionOrder(1)]
    public sealed class BoardMimic : MonoBehaviour
    {
        #region Values

        private Dictionary<int, UnityEvent<float>> pins;

        private BoardWrapper boardWrapper;

        #endregion

        #region Build in States

        private void Start() =>
            this.boardWrapper = new BoardWrapper(this);

        private void Update() =>
            this.boardWrapper.Update();

        #endregion

        #region In

        public void AddComponentToPin(int pinNumber, UnityAction<float> action)
        {
            if (this.pins.TryGetValue(pinNumber, out UnityEvent<float> result))
                result.AddListener(action);
            else
            {
                result = new UnityEvent<float>();
                result.AddListener(action);
                this.pins.Add(pinNumber, result);
            }
        }

        #endregion
    }
}