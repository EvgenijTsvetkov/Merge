using UnityEngine;
using UnityEngine.Tilemaps;

namespace Merge2D.Source
{
    public interface IWorldObjectsCollector
    {
        Transform ContainerForItems { get; set; }
        ItemSpawnPoint[] ItemsSpawnPoints { get; }
    }
}