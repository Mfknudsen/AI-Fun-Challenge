#region Libraries

using Runtime.HardwareMimic;
using Runtime.Mind;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Editor
{
    [DefaultExecutionOrder(2)]
    public class VirtualEnvironmentVisualiser : MonoBehaviour
    {
        #region Values

        [SerializeField] [Required] private BoardMimic boardMimic;
        [SerializeField] private Vector3 initialOffset;

        private VirtualEnvironment environment;

        #endregion

        #region Build In States

        private void Start()
        {
            this.environment = this.boardMimic.GetCrawlerFromBoard().GetBrain().GetEnvironment();

            Mesh finalMesh = new();

            this.GetComponent<MeshFilter>().mesh = finalMesh;
        }

        #endregion
    }
}