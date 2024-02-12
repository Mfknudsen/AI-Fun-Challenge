#region Libraries

using System.Collections.Generic;
using Runtime.HardwareWrapper;
using Runtime.Mind;
using UnityEngine;

#endregion

namespace Runtime.Body
{
    public sealed class Crawler
    {
        #region Values

        private BoardWrapper boardWrapper;

        private readonly Leg legForwardLeft,
            legForwardRight,
            legMiddleLeft,
            legMiddleRight,
            legBackLeft,
            legBackRight;

        private readonly Brain brain;

        private readonly DistanceSensorWrapper distanceSensorWrapper;

        private readonly CameraWrapper cameraWrapper;

        private readonly AcceleratorWrapper acceleratorWrapper;

        private readonly GyroWrapper gyroWrapper;

        private readonly List<Timer> timers = new();

        #endregion

        #region Build In States

        public Crawler(BoardWrapper boardWrapper)
        {
            this.boardWrapper = boardWrapper;

            this.distanceSensorWrapper = new DistanceSensorWrapper(new Pin(18, boardWrapper));
            this.cameraWrapper = new CameraWrapper(new Pin(19, boardWrapper));
            this.acceleratorWrapper = new AcceleratorWrapper(new Pin(20, boardWrapper));
            this.gyroWrapper = new GyroWrapper(new Pin(21, boardWrapper));

            this.legForwardLeft = new Leg(0, 1, 2, boardWrapper);
            this.legForwardRight = new Leg(3, 4, 5, boardWrapper);
            this.legMiddleLeft = new Leg(6, 7, 8, boardWrapper);
            this.legMiddleRight = new Leg(9, 10, 11, boardWrapper);
            this.legBackLeft = new Leg(12, 13, 14, boardWrapper);
            this.legBackRight = new Leg(15, 16, 17, boardWrapper);

            this.brain = new Brain(this.legForwardLeft, this.legForwardRight, this.legMiddleLeft, this.legMiddleRight,
                this.legBackLeft, this.legBackRight);
            Debug.Log("Crawler constructor");
        }

        #endregion

        #region Getters

        public Brain GetBrain() => this.brain;

        #endregion

        #region In

        public void Update()
        {
            this.brain.SetIsProperlyGrounded(this.IsProperlyGrounded());
            List<Timer> toRemove = new();
            foreach (Timer timer in this.timers)
                if (timer.Update(Time.deltaTime))
                    toRemove.Add(timer);

            foreach (Timer timer in toRemove)
                this.timers.Remove(timer);

            this.brain.SetDistanceMeasurements(this.distanceSensorWrapper.GetInput());
        }

        public void AddTimer(Timer toAdd)
        {
            this.timers.Add(toAdd);
        }

        #endregion

        #region Out

        /// <summary>
        ///     The brain should always aim to have at least three legs on the.
        ///     To be properly grounded it must have one leg on one side and two on the other or an combine total of more then
        ///     three legs.
        /// </summary>
        public bool IsProperlyGrounded()
        {
            int left = this.legForwardLeft.GetIsGrounded() + this.legBackLeft.GetIsGrounded() +
                       this.legMiddleLeft.GetIsGrounded(),
                right = this.legBackRight.GetIsGrounded() + this.legForwardRight.GetIsGrounded() +
                        this.legMiddleRight.GetIsGrounded();

            if (left + right >= 4)
                return true;

            if (left == 1 && right == 2)
                return true;

            return right == 1 && left == 2;
        }

        #endregion
    }
}