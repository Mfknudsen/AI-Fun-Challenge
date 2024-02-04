#region Libraries

using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.HardwareMimic
{
    [DefaultExecutionOrder(0)]
    public class ComponentMimic : MonoBehaviour
    {
        #region Values

        [SerializeField, Required] protected BoardMimic boardMimic;

        #endregion
    }
}
