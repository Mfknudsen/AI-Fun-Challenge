namespace Runtime.Mind
{
    public class VirtualEnvironment
    {
        #region Values

        private MapChunk[,] chunks;

        #endregion

        public VirtualEnvironment()
        {
            this.chunks = new MapChunk[20, 20];
        }
    }

    internal class MapChunk
    {
        #region Values

        private readonly float[,] heightMap = new float[100, 100];

        #endregion
    }
}