#region Libraries

using System;
using Runtime.HardwareMimic;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Runtime.HardwareWrapper
{
    public sealed class AcceleratorWrapper
    {
        #region Values

#if UNITY_EDITOR
        private readonly AcceleratorMimic mimic;
#endif

        #endregion

        #region Build In States

        public AcceleratorWrapper(Pin pin)
        {
#if UNITY_EDITOR
            foreach (AcceleratorMimic acceleratorMimic in Object.FindObjectsOfType<AcceleratorMimic>())
                if (acceleratorMimic.GetPinNumber() == pin.GetNumber())
                    this.mimic = acceleratorMimic;

            if (this.mimic == null)
                throw new Exception($"No Accelerator Mimic found with matching pin number: {pin.GetNumber()}");
#endif
        }

        #endregion

        #region Getters

        public Vector3 GetInput()
        {
#if UNITY_EDITOR
            return this.mimic.ReadValue();
#endif
        }

        #endregion
    }
}