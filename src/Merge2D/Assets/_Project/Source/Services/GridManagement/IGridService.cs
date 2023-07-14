using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Merge2D.Source.Services
{
    public interface IGridService
    {
        Tilemap Tilemap { get; set; }
        List<WorldCell> WorldCells { get; }
        
        Task CreateGrid();
        WorldCell GetCell(Vector3Int localPlace);
        WorldCell? TryGetCell(Vector3 position);
    }
}