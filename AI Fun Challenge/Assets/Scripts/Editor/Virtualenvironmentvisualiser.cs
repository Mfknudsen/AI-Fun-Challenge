#region Libraries

using Runtime.Hardware;
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

        [FormerlySerializedAs("boardMimic")] [SerializeField] [Required]
        private Board board;

        [SerializeField] private Vector3 initialOffset;

        private VirtualEnvironment environment;

        #endregion

        #region Build In States

        private void Start()
        {
            this.environment = this.board.GetBrain().GetEnvironment();

            Mesh finalMesh = new();

            this.GetComponent<MeshFilter>().mesh = finalMesh;
        }

        #endregion
    }
}