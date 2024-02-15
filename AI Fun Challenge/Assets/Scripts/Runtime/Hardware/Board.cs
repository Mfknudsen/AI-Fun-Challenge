#region Libraries

using Runtime.Body;
using Runtime.Mind;
using Unity.AI.Navigation;
using UnityEngine;

#endregion

namespace Runtime.Hardware
{
    [DefaultExecutionOrder(1)]
    public sealed class Board : MonoBehaviour
    {
        #region Values

        private Brain brain;

        [SerializeField] private Leg legForwardLeft,
            legForwardRight,
            legMiddleLeft,
            legMiddleRight,
            legBackLeft,
            legBackRight;

        [SerializeField] private NavMeshSurface navMeshSurface;

        #endregion

        #region Build in States

        private void Start()
        {
            this.brain = new Brain(this.legForwardLeft, this.legForwardRight, this.legMiddleLeft,
                this.legMiddleRight,
                this.legBackLeft, this.legBackRight);

            Debug.Log("Crawler constructor");
        }

        private void FixedUpdate() =>
            this.navMeshSurface.BuildNavMesh();

        #endregion

        #region Getters

        public Brain GetBrain() => this.brain;

        #endregion
    }
}