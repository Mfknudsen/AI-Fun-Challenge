#region Libraries

using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.HardwareMimic
{
    public sealed class DistanceSensorMimic : ComponentMimic
    {
        #region Values

        [SerializeField] [Min(0)] private int pinNumber;

        [SerializeField] private float angleBetweenRays = 1f, shootAngle = 90f, distance = 5;
        [SerializeField] [Required] private Transform rayShootTransform;
        [SerializeField] private LayerMask hitMask;

        private List<Vector3> hits = new();

        #endregion

        #region Build In States

        private void FixedUpdate()
        {
            for (float x = -this.shootAngle; x <= this.shootAngle; x += this.angleBetweenRays)
            {
                for (float y = -this.shootAngle; y <= this.shootAngle; y += this.angleBetweenRays)
                {
                    if (!Physics.Raycast(
                            this.rayShootTransform.position,
                            this.rayShootTransform.forward + new Vector3(x, y, 0).normalized,
                            out RaycastHit hit,
                            this.distance,
                            this.hitMask))
                        continue;

                    this.hits.Add(hit.point - this.rayShootTransform.position);
                }
            }
        }

        #endregion

        #region Getters

#if UNITY_EDITOR
        public int GetPinNumber() => this.pinNumber;
#endif

        #endregion

        #region Out

        public Vector3[] GetRayHits()
        {
            Vector3[] result = this.hits.ToArray();
            this.hits.Clear();
            return result;
        }

        #endregion
    }
}