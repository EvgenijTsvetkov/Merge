using UnityEngine;
using UnityEngine.Tilemaps;

namespace Merge2D.Source
{
    public class WorldObjectsCollector : IWorldObjectsCollector
    {
        public Transform ContainerForItems { get; set; }
        public Tilemap Tilemap { get; set; }
        public ItemSpawnPoint[] ItemsSpawnPoints => Object.FindObjectsOfType<ItemSpawnPoint>();
    }
}