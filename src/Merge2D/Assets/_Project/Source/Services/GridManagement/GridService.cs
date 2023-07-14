using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merge2D.Source.Data;
using Merge2D.Source.Data.Constant;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Merge2D.Source.Services
{
    public class GridService : IGridService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPhysicsCaster _physicsCaster;
        
        private readonly Dictionary<Vector3Int, WorldCell> _worldCells = new Dictionary<Vector3Int, WorldCell>();

        public Tilemap Tilemap { get; set; }
        public List<WorldCell> WorldCells => _worldCells.Values.ToList();

        public GridService(IAssetProvider assetProvider, IPhysicsCaster physicsCaster)
        {
            _assetProvider = assetProvider;
            _physicsCaster = physicsCaster;
        }

        public WorldCell GetCell(Vector3Int localPlace)
        {
            return _worldCells[localPlace];
        }

        public WorldCell? TryGetCell(Vector3 position)
        {
            RaycastAllResult raycastAllResult = _physicsCaster.RaycastAll(position, Constants.CellLayerValue);
            if (raycastAllResult.HasHit == false)
                return null;

            foreach (RaycastHit2D hitInfo in raycastAllResult.HitsInfos)
            {
                if (hitInfo.collider.TryGetComponent(out ICell cell))
                    return _worldCells[cell.LocalPlace];
            }

            return null;
        }

        public async Task CreateGrid()
        {
            foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
            {
                var localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                if (Tilemap.HasTile(localPlace) == false)
                    continue;

                await CreateCell(localPlace);
            }
        }

        private async Task CreateCell(Vector3Int localPlace)
        {
            Vector3 worldPosition = Tilemap.CellToWorld(localPlace);
            
            ICell cell = await _assetProvider.InstantiateAsync<Cell>(Tilemap.transform, AddressableNames.Cell);
                
            cell.SelfTransform.position = worldPosition;
            cell.LocalPlace = localPlace;

            var worldCell = new WorldCell
            {
                Cell = cell,
                LocalPlace = localPlace,
                WorldLocation = worldPosition,
            };

            _worldCells.Add(localPlace, worldCell);
        }
    }
}