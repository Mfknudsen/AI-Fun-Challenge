#region Libaries

using UnityEngine;

#endregion

namespace Runtime.Hardware
{
    public sealed class Accelerator : Component
    {
        #region Values

        [SerializeField] private float randomOffsetMaxPercentage = .1f;
        [SerializeField] private int pinNumber;

        private Vector3 previousPosition;

        #endregion

        #region Build In States

        private void Start() =>
            this.previousPosition = this.transform.position;

        #endregion

        #region Getters

        public int GetPinNumber() =>
            this.pinNumber;

        #endregion

        #region Out

        public Vector3 ReadValue() =>
            this.previousPosition = this.transform.position - this.previousPosition + new Vector3(
                Random.Range(-this.randomOffsetMaxPercentage, this.randomOffsetMaxPercentage),
                Random.Range(-this.randomOffsetMaxPercentage, this.randomOffsetMaxPercentage),
                Random.Range(-this.randomOffsetMaxPercentage, this.randomOffsetMaxPercentage));

        #endregion
    }
}