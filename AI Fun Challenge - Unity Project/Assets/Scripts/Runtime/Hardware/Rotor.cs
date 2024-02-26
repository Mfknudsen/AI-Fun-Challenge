#region Libraries

using System.Collections.Generic;
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

        private void OnDrawGizmos()
        {
            Vector3 right = this.transform.right;

            List<Vector3> directions = new();

            for (int i = 0; i < 90; i++)
            {
                directions.Add((this.transform.forward - right +
                                right / 90 * i * 2).normalized * 2);
            }

            Vector3 position = this.transform.position;
            for (int i = 0; i < directions.Count - 1; i++)
                Debug.DrawLine(position + directions[i], position + directions[i + 1], Color.red);

            Debug.DrawRay(position, this.rotorHead.forward * 2, Color.green);
            Debug.
        }

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