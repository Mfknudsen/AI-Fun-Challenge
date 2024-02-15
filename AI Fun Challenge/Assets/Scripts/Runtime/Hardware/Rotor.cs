#region Libraries

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.Hardware
{
    public sealed class Rotor : Component
    {
        #region Values

        [SerializeField] [Required] private Transform rotorHead;

        private float angle, rotateSpeed;

        #endregion

        #region Build In States

        private void Update()
        {
            this.rotorHead.RotateAround(this.rotorHead.position, this.rotorHead.up,
                (this.angle - this.rotorHead.localRotation.y) * this.rotateSpeed * Time.deltaTime);
        }

        #endregion

        #region Setters

        public void SetAngle(float set) =>
            this.angle = set;

        #endregion
    }
}