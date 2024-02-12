#region Libraries

using UnityEngine;

#endregion

namespace Runtime.Mind
{
    [CreateAssetMenu]
    public class BlockChunkGameObjectConstructor : ScriptableObject
    {
        #region Values

        [SerializeField] private GameObject plane, cube;

        #endregion

        #region Getters

        public GameObject CreatePlane(Vector3 position) =>
            Instantiate(this.plane, position, Quaternion.identity, null);

        public GameObject CreateCube(Vector3 position, Transform planeParent) =>
            Instantiate(this.cube, position, Quaternion.identity, planeParent);

        #endregion
    }
}