using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Tools
{
    [MenuItem("Merge2D/Tools/Restore Tilemaps borders")]
    public static void RestoreTileMapsBorders()
    {
        var tilemaps = Object.FindObjectsOfType<Tilemap>();
        foreach (var tilemap in tilemaps) 
            tilemap.CompressBounds();
    }
}
