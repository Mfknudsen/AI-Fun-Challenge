#region Libraries

using UnityEngine;

#endregion

namespace Runtime.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 VecMulti(this Vector3 v1, Vector3 v2) => new(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);

        public static Vector3 VecMulti(this Vector3 v1, float f1, float f2, float f3) =>
            new(v1.x * f1, v1.y * f2, v1.z * f3);
    }
}