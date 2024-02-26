#region Libraries

using Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

namespace Editor
{
    [DefaultExecutionOrder(2)]
    public class VirtualBodyVisualiser : MonoBehaviour
    {
        #region Values

        [FormerlySerializedAs("crawler")] [FormerlySerializedAs("board")] [SerializeField] [Required]
        private CrawlerAgent crawlerAgent;

        #endregion
    }
}