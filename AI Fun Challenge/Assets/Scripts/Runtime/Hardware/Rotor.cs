#region Libraries

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.Hardware
{
    public sealed class Rotor : Component
    {
        #region Values

        [SerializeField] private float currentAngle;

        [SerializeField] [Required] private Transform rotorHead;

        [SerializeField] [Min(0.1f)] private float rotateSpeed = 0.1f;

        private int angleDirection;

        #endregion

        #region Build In States

        private void OnValidate()
        {
            this.currentAngle = Mathf.Clamp(this.currentAngle, -45f, 45f);
            this.rotorHead.localRotation =
                Quaternion.Euler(new Vector3(0, this.currentAngle, 0));
        }

        private void Update()
        {
            if (Mathf.Abs(this.rotorHead.localRotation.y) > 45)
            {
                this.angleDirection = 0;
                Vector3 rot = this.rotorHead.localRotation.eulerAngles;
                this.rotorHead.localRotation = Quaternion.Euler(new Vector3(rot.x, Mathf.Clamp(rot.y, -45, 45), rot.z));
            }

            this.rotorHead.RotateAround(this.rotorHead.position, this.rotorHead.up,
                this.angleDirection * this.rotateSpeed * Time.deltaTime);
        }

        #endregion

        #region Getters

        public float GetAngle() => this.currentAngle;

        public int GetAngelDirection() => this.angleDirection;

        public Vector3 GetRotorHeadPosition() => this.rotorHead.position;

        #endregion

        #region Setters

        public void SetAngelDirection(int set) =>
            this.angleDirection = set;

        #endregion
    }
}