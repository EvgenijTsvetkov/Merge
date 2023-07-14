using System.Collections.Generic;
using System.Threading.Tasks;
using Merge2D.Source.Data;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public class MergeService : IMergeService
    {
        private readonly IGridService _gridService;
        private readonly IItemsCollector _itemsCollector;
        private readonly IConfigsProvider _configsProvider;
        private readonly IItemPool _itemPool;
        
        private IItem _firsItemToMerge;
        private Vector3Int _mergeLocalPlace;
        private readonly HashSet<IItem> _mergeGroup = new HashSet<IItem>();

        public MergeService(IGridService gridService, IItemsCollector itemsCollector, IConfigsProvider configsProvider,
            IItemPool itemPool)
        {
            _gridService = gridService;
            _itemsCollector = itemsCollector;
            _configsProvider = configsProvider;
            _itemPool = itemPool;
        }

        public bool CanMerge { get; private set; }

        public void Initialize()
        {
            foreach (var worldCell in _gridService.WorldCells)
                worldCell.Cell.OnEnterInteractiveObject += EnterInteractiveObjectToCellHandler;
        }

        public void SetItemToMerge(IItem item)
        {
            _firsItemToMerge = item;
        }

        public async void Merge()
        {
            StopHighlightForMergeGroup();

            List<ItemType> chain = _configsProvider.MergeConfig.Chain(_firsItemToMerge.ChainType);
            List<WorldCell> worldCellsForSpawn = GetWorldCellForSpawn();
            
            ItemType previousType = _firsItemToMerge.Type;
            ItemType nextType = chain[chain.IndexOf(previousType) + 1];
            
            int cont = _mergeGroup.Count;
            int timeDelay = (int) (1000 * _configsProvider.MergeConfig.MergeTime);

            RemoveMergeGroup();

            await Task.Delay(timeDelay);
            
            TryAddBonusItem();

            int countNextItems = cont / 3;
            int countPreviousItems = cont % 3;

            for (int i = 0; i < countNextItems; i++)
            {
                await CreateItem(nextType, worldCellsForSpawn[i]);
            }
            
            for (int i = countNextItems; i < countNextItems + countPreviousItems; i++)
            {
                await CreateItem(_firsItemToMerge.Type, worldCellsForSpawn[i]);
            }

            Reset();
            
            void TryAddBonusItem()
            {
                if ((cont + 1) % 6 == 0)
                    cont += 1;
            }
        }
        
        private async Task CreateItem(ItemType type, WorldCell worldCell)
        {
            IItem item = await _itemPool.Get(type);
            item.SelfTransform.position = worldCell.WorldLocation;
            item.Activate();
            item.PlayCreationAnimation();

            _itemsCollector.Add(worldCell.LocalPlace, item);
        }

        private void RemoveMergeGroup()
        {
            WorldCell worldCell = _gridService.GetCell(_mergeLocalPlace);
            foreach (IItem item in _mergeGroup)
            {
                item.PlayMergeAnimation(worldCell.WorldLocation, () =>
                {
                    _itemsCollector.Remove(item);
                    _itemPool.Remove(item);
                });
            }
        }

        private List<WorldCell> GetWorldCellForSpawn()
        {
            List<WorldCell> worldCells = new List<WorldCell>();

            foreach (IItem item in _mergeGroup)
            {
                var worldCell = _gridService.TryGetCell(item.SelfTransform.position);
                if (worldCell.HasValue) 
                    worldCells.Add(worldCell.Value);
            }
            
            return worldCells;
        }
        
        public void Reset()
        {
            _firsItemToMerge = null;

            ResetMergeGroup();
        }

        private void EnterInteractiveObjectToCellHandler(Vector3Int localPlace)
        {
            ResetMergeGroup();

            if (_firsItemToMerge == null)
                return;

            _mergeLocalPlace = localPlace;
            
            CreteMergeGroup();

            CanMerge = _mergeGroup.Count >= 3;

            if (CanMerge) 
                PlayHighlightForMergeGroup();
        }

        private void CreteMergeGroup()
        {
            List<ItemType> chain = _configsProvider.MergeConfig.Chain(_firsItemToMerge.ChainType);

            if (_firsItemToMerge.Type == chain[chain.Count - 1])
                return;
            
            List<Vector3Int> blackListLocalPlaces = new List<Vector3Int>{_itemsCollector.GetLocalPlaceFor(_firsItemToMerge)};

            FindItemsFor(new List<Vector3Int> {_mergeLocalPlace});

            _mergeGroup.Add(_firsItemToMerge);
            
            void FindItemsFor(List<Vector3Int> localPlaces)
            {
                while (true)
                {
                    var nextLocalPlaces = new List<Vector3Int>();
                    if (localPlaces.Count == 0)
                        break;

                    foreach (Vector3Int place in localPlaces)
                    {
                        if (blackListLocalPlaces.Contains(place))
                            continue;

                        blackListLocalPlaces.Add(place);

                        if (_itemsCollector.HasItem(place))
                        {
                            IItem localItem = _itemsCollector.Get(place);
                            if (localItem.Type == _firsItemToMerge.Type)
                            {
                                _mergeGroup.Add(localItem);

                                FindLocalPlacesFor(place, ref nextLocalPlaces);
                            }
                        }
                    }

                    localPlaces = nextLocalPlaces;
                }
            }
            
            void FindLocalPlacesFor(Vector3Int place, ref List<Vector3Int> resultLocalPlaces)
            {
                var leftLocalPlace = place + Vector3Int.left;
                var rightLocalPlace = place + Vector3Int.right;
                var upLocalPlace = place + Vector3Int.up;
                var downLocalPlace = place + Vector3Int.down;

                TryAddToNextLocalPlaces(leftLocalPlace, resultLocalPlaces);
                TryAddToNextLocalPlaces(rightLocalPlace, resultLocalPlaces);
                TryAddToNextLocalPlaces(upLocalPlace, resultLocalPlaces);
                TryAddToNextLocalPlaces(downLocalPlace, resultLocalPlaces);
            }
            
            void TryAddToNextLocalPlaces(Vector3Int leftLocalPlace, List<Vector3Int> resultLocalPlaces)
            {
                if (blackListLocalPlaces.Contains(leftLocalPlace) == false)
                {
                    if (_itemsCollector.HasItem(leftLocalPlace))
                        resultLocalPlaces.Add(leftLocalPlace);
                }
            }
        }
        
        private void ResetMergeGroup()
        {
            CanMerge = false;

            StopHighlightForMergeGroup();

            _mergeGroup.Clear();
        }

        private void PlayHighlightForMergeGroup()
        {
            foreach (var item in _mergeGroup)
                item.PlayHighlight();
        }
        
        private void StopHighlightForMergeGroup()
        {
            foreach (var item in _mergeGroup)
                item.StopHighlight();
        }
    }
}