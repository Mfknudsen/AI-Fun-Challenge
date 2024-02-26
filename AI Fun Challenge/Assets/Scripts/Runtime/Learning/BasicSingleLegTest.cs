#region Libraries

using Runtime.Body;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Runtime.Learning
{
    public class BasicSingleLegTest : MonoBehaviour
    {
        #region Values

        [SerializeField] [Required] private LegAgent legAgent;

        #endregion

        #region Build In Staes

        private void Start()
        {
        }

        #endregion
    }
}