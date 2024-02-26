#region Libraries

using Runtime;
using Runtime.Mind;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Editor
{
    [DefaultExecutionOrder(2)]
    public class VirtualEnvironmentVisualiser : MonoBehaviour
    {
        #region Values

        [FormerlySerializedAs("crawler")]
        [FormerlySerializedAs("board")]
        [FormerlySerializedAs("boardMimic")]
        [SerializeField]
        [Required]
        private CrawlerAgent crawlerAgent;

        [SerializeField] private Vector3 initialOffset;

        private VirtualEnvironment environment;

        #endregion

        #region Build In States

        private void Start()
        {
            Mesh finalMesh = new();

            this.GetComponent<MeshFilter>().mesh = finalMesh;
        }

        #endregion
    }
}