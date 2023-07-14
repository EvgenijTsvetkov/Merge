using UnityEngine;

namespace Merge2D.Source.Services
{
    public struct WorldCell
    {
        public ICell Cell;
        public Vector3Int LocalPlace;
        public Vector3 WorldLocation;

        public WorldCell(ICell cell, Vector3Int localPlace, Vector3 worldLocation)
        {
            Cell = cell;
            LocalPlace = localPlace;
            WorldLocation = worldLocation;
        }
    }
}