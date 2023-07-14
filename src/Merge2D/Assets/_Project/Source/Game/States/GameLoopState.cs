using System.Threading.Tasks;
using Merge2D.Source.Services;
using UnityEngine;

namespace Merge2D.Source.Game
{
    public class GameLoopState : GameState
    {
        private readonly IWorldObjectsCollector _worldObjectsCollector;
        private readonly IItemsCollector _itemsCollector;
        private readonly IGridService _gridService;
        private readonly IItemPool _itemPool;
        private readonly IMergeService _mergeService;

        public GameLoopState(IGameProvider gameProvider, IWorldObjectsCollector worldObjectsCollector, 
            IItemsCollector itemsCollector, IItemPool itemPool, IGridService gridService,
            IMergeService mergeService) : base(gameProvider)
        {
            _worldObjectsCollector = worldObjectsCollector;
            _itemsCollector = itemsCollector;
            _gridService = gridService;
            _itemPool = itemPool;
            _mergeService = mergeService;
        }

        public override async void Enter()
        {
            base.Enter();

            SetOrientation();

            await CreateGrid();
            await Task.Yield();
            await CreateItems();

            InitializeMergeService();
        }

        private void SetOrientation()
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }

        private async Task CreateGrid()
        {
            await _gridService.CreateGrid();
        }

        private async Task CreateItems()
        {
            await _itemPool.CreatePool();
            
            foreach (ItemSpawnPoint spawnPoint in _worldObjectsCollector.ItemsSpawnPoints)
            {
                var worldCell = _gridService.TryGetCell(spawnPoint.SelfTransform.position);
                if (worldCell.HasValue == false)
                    continue;

                if (_itemsCollector.HasItem(worldCell.Value.LocalPlace) == false)
                    await CreateItem(spawnPoint, worldCell.Value);
            }
        }

        private async Task CreateItem(ItemSpawnPoint spawnPoint, WorldCell worldCell)
        {
            IItem item = await _itemPool.Get(spawnPoint.Type);
            item.SelfTransform.position = worldCell.WorldLocation;
            item.Activate();

            _itemsCollector.Add(worldCell.LocalPlace, item);
        }

        private void InitializeMergeService()
        {
            _mergeService.Initialize();
        }
    }
}