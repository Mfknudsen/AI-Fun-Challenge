#region Libraries

using Runtime.HardwareMimic;
using UnityEngine;

#endregion

namespace Runtime.HardwareWrapper
{
    public sealed class DistanceSensorWrapper : ComponentWrapper
    {
        #region Values

#if UNITY_EDITOR
        private readonly DistanceSensorMimic mimic;
#endif

        #endregion

        #region Build In States

        public DistanceSensorWrapper(Pin pin) : base(pin.GetBoard())
        {
#if UNITY_EDITOR
            foreach (DistanceSensorMimic distanceSensorMimic in Object.FindObjectsOfType<DistanceSensorMimic>())
            {
                if (distanceSensorMimic.GetPinNumber() != pin.GetNumber())
                    continue;

                this.mimic = distanceSensorMimic;
                break;
            }
#endif
        }

        #endregion

        #region Getters

        public Vector3[] GetInput()
        {
#if UNITY_EDITOR
            return this.mimic.GetRayHits();
#endif
        }

        #endregion
    }
}