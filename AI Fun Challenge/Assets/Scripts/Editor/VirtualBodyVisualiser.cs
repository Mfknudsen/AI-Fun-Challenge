#region Libraries

using Runtime.Hardware;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Editor
{
    [DefaultExecutionOrder(2)]
    public class VirtualBodyVisualiser : MonoBehaviour
    {
        #region Values

        [SerializeField] [Required] private Board board;

        #endregion
    }
}