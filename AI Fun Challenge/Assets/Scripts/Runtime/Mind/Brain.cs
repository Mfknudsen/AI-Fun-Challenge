namespace Runtime.Mind
{
    public sealed class Brain
    {
        #region Values

        private VirtualBody virtualBody;

        private VirtualEnvironment virtualEnvironment;

        private bool isProperlyGrounded;

        #endregion

        #region Setters

        public void SetIsProperlyGrounded(bool set) => this.isProperlyGrounded = set;

        #endregion
    }
}