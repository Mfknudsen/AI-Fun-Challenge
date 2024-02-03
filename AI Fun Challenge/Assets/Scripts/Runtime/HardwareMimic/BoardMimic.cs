#region Libraries

using System.Collections.Generic;
using Runtime.HardwareWrapper;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Runtime.HardwareMimic
{
    public sealed class BoardMimic : MonoBehaviour
    {
        #region Values

        private List<UnityEvent<float>> pins;

        #endregion

        #region Build in States

        private void Start()
        {
            new BoardWrapper(this);
        }

        #endregion
    }
}