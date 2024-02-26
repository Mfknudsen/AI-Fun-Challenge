#region Libraries

using Runtime.Learning;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

#endregion

namespace Editor.Tests
{
    public class SetupBasicSingleLegTests : MonoBehaviour
    {
        #region Values

        [SerializeField] [Required] [AssetsOnly] [AssetSelector]
        private BasicSingleLegTest legTestPrefab;

        [SerializeField] [Required] private Transform testTransform;

        [SerializeField] private float distanceBetween;

        [SerializeField] [Min(1)] private int xCount = 1, yCount = 1;

        #endregion

        #region Internal

        [Button]
        private void SetupTests()
        {
            this.RemoveTests();

            for (int x = 0; x < this.xCount; x++)
            {
                for (int y = 0; y < this.yCount; y++)
                {
                    Instantiate(this.legTestPrefab, new Vector3(x * this.distanceBetween, 0, y * this.distanceBetween),
                        Quaternion.identity, this.testTransform);
                }
            }

            Undo.RecordObject(this.testTransform, "Test Transform");
        }

        [Button]
        private void RemoveTests()
        {
            for (int i = this.testTransform.childCount - 1; i >= 0; i--)
                DestroyImmediate(this.testTransform.GetChild(i).gameObject);
        }

        #endregion
    }
}