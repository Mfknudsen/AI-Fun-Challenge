#region Libraries

using Runtime.Body;
using Runtime.HardwareMimic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace Editor
{
    [DefaultExecutionOrder(2)]
    public class VirtualBodyVisualiser : MonoBehaviour
    {
        #region Values

        [SerializeField] [Required] private BoardMimic boardMimic;

        private Crawler crawler;

        #endregion

        #region Build In States

        private void Start()
        {
            this.crawler = this.boardMimic.GetCrawlerFromBoard();
        }

        #endregion
    }
}