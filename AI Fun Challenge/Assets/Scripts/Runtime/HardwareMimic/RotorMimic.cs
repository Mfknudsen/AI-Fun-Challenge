#region Libraries

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.HardwareMimic
{
    public sealed class RotorMimic : ComponentMimic
    {
        #region Values

        [SerializeField] [Min(0)] private int pinNumber;

        [SerializeField] [Required] private Transform rotorHead;

        #endregion

        #region Build In States

        private void Start() =>
            this.boardMimic.AddComponentToPin(this.pinNumber, this.OnPinUpdate);

        #endregion

        #region Internal

        private void OnPinUpdate(float pin)
        {
        }

        #endregion
    }
}