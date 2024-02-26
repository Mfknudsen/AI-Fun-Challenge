#region Libraries

using UnityEditor;
using UnityEngine;

#endregion

namespace Runtime.Mind
{
    public class VirtualEnvironment
    {
        #region Values

        public const float chunkSizeCentimeter = 100f;

        private MapChunk[,] chunks;

        private readonly BlockChunkGameObjectConstructor blockConstructor =
            AssetDatabase.LoadAssetAtPath<BlockChunkGameObjectConstructor>(
                "Assets/ScriptableObjects/Block Chunk Game Object Constructor.asset");

        #endregion

        #region Build In States

        public VirtualEnvironment()
        {
            this.chunks = new MapChunk[3, 3];
            for (int x = 0; x < this.chunks.GetLength(0); x++)
            {
                for (int y = 0; y < this.chunks.GetLength(1); y++)
                {
                    this.chunks[x, y] = new MapChunk(
                        new Vector3(
                            -chunkSizeCentimeter / 100f * this.chunks.GetLength(0) + chunkSizeCentimeter / 100f / 2,
                            0,
                            -chunkSizeCentimeter / 100f * this.chunks.GetLength(1) + chunkSizeCentimeter / 100f / 2)
                        + new Vector3(
                            chunkSizeCentimeter / 100f * x,
                            0,
                            chunkSizeCentimeter / 100f * y
                        ), this.blockConstructor);
                }
            }
        }

        #endregion

        #region In

        public void InputDistanceMeasurements(Vector3 crawlerSensorVirtualPosition, Quaternion crawlerVirtualRotation,
            Vector3[] distanceHits)
        {
            foreach (Vector3 distanceHit in distanceHits)
            {
                //Distance sensor will not know the direction of the crawler and the input will be giving from the perspective of the forward vector (0,0,1).
                Vector3 rotatedDistanceHit = crawlerVirtualRotation * distanceHit;
                Vector3 hitPosition = crawlerSensorVirtualPosition + rotatedDistanceHit;

                foreach (MapChunk mapChunk in this.chunks)
                {
                    Vector3 chunkPosition = mapChunk.GetPosition();
                    if (!(hitPosition.x > chunkPosition.x &&
                          hitPosition.x < chunkPosition.x + chunkSizeCentimeter / 100f &&
                          hitPosition.y > chunkPosition.y &&
                          hitPosition.y < chunkPosition.y + chunkSizeCentimeter / 100f &&
                          hitPosition.z > chunkPosition.z &&
                          hitPosition.z < chunkPosition.z + chunkSizeCentimeter / 100f))
                        continue;

                    mapChunk.CreateNewBlock(hitPosition);

                    break;
                }
            }
        }

        #endregion
    }

    public class MapChunk
    {
        #region Values

        /// <summary>
        /// If true then a block is placed at that point
        /// </summary>
        private readonly bool[,,] pointMap = new bool[20, 20, 20];

        private readonly GameObject planeFloor;

        private readonly BlockChunkGameObjectConstructor blockConstructor;

        private readonly Vector3 position;

        #endregion

        #region Build In States

        public MapChunk(Vector3 position, BlockChunkGameObjectConstructor blockConstructor)
        {
            this.position = position - (Vector3.one - Vector3.up) * VirtualEnvironment.chunkSizeCentimeter / 100 / 2;
            this.blockConstructor = blockConstructor;
            this.planeFloor = this.blockConstructor.CreatePlane(position);
        }

        #endregion

        #region Getters

        public Vector3 GetPosition() => this.position;

        #endregion

        #region In

        public void CreateNewBlock(Vector3 hitPosition)
        {
            Vector3 dir = hitPosition - this.position;
            int x = Mathf.FloorToInt(dir.x / 20),
                y = Mathf.FloorToInt(dir.y / 20),
                z = Mathf.FloorToInt(dir.z / 20);

            if (this.pointMap[x, y, z])
                return;

            this.pointMap[x, y, z] = true;

            this.blockConstructor.CreateCube(
                Vector3.one * VirtualEnvironment.chunkSizeCentimeter / 100 / 20 / 2 +
                new Vector3(x, y, z) * VirtualEnvironment.chunkSizeCentimeter / 100 / 20,
                this.planeFloor.transform);
        }

        #endregion
    }
}