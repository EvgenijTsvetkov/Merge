using UnityEngine;
using Zenject;

namespace Merge2D.Source.Services
{
    public class DragAndDropService : IDragAndDropService, ITickable
    {
        private readonly IMainCameraProvider _cameraProvider;
        private readonly IInputService _inputService;
        private readonly IGridService _gridService;
        private readonly IItemsCollector _itemsCollector;
        private readonly IMergeService _mergeService;

        private IDraggable _draggableObject;
        private Vector3 _beganPosition;
        private Vector2 _deltaPosition;
 
        public bool HasDraggableObject => _draggableObject != null;
        public IDraggable DraggableObject => _draggableObject;
        
        public DragAndDropService(IMainCameraProvider cameraProvider, IInputService inputService, IGridService gridService,
            IItemsCollector itemsCollector, IMergeService mergeService)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
            _gridService = gridService;
            _itemsCollector = itemsCollector;
            _mergeService = mergeService;
        }

        public void Tick()
        {
            if (HasDraggableObject == false)
                return;

            Drag();
        }

        public void BeganDragFor(IDraggable draggableObject)
        {
            _draggableObject = draggableObject;
            _draggableObject.BeganDrag();
            
            SetupForMerge();    
            CalculationDeltaPosition();
            SubscribeOnEvents();
        }

        private void Drag()
        {
            Vector2 dragPosition = _cameraProvider.Value.ScreenToWorldPoint(_inputService.MousePosition) -
                                   _draggableObject.SelfTransform.position;

            _draggableObject.SelfTransform.Translate(new Vector2(dragPosition.x - _deltaPosition.x,
                dragPosition.y - _deltaPosition.y));
        }

        private void EndDrag()
        {
            if (_mergeService.CanMerge) 
                _mergeService.Merge();
            else
            {
                TryDropOnFreeCell();
                ResetMerge();
            }
            
            ResetDraggableObject();
            UnsubscribeOnEvents();
        }

        private void TryDropOnFreeCell()
        {
            var worldCell =
                _gridService.TryGetCell(_cameraProvider.Value.ScreenToWorldPoint(_inputService.MousePosition));

            if (worldCell.HasValue == false) 
                ReturnToBeganDrag();
            else if (_itemsCollector.HasItem(worldCell.Value.LocalPlace)) 
                ReturnToBeganDrag();
            else
                DropTo(worldCell.Value);
        }

        private void DropTo(WorldCell worldCell)
        {
            if (_draggableObject is IItem item) 
                UpdateLocalPlaceFor(item, worldCell);

            _draggableObject.SelfTransform.position = worldCell.WorldLocation;
        }

        private void UpdateLocalPlaceFor(IItem item, WorldCell endWorldCell)
        {
            var beganWorldCell = _gridService.TryGetCell(_beganPosition);
            _itemsCollector.RemoveAt(beganWorldCell.Value.LocalPlace);
            _itemsCollector.Add(endWorldCell.LocalPlace, item);
        }

        private void ReturnToBeganDrag()
        {
            _draggableObject.SelfTransform.position = _beganPosition;
        }

        private void ResetMerge()
        {
            _mergeService.Reset();
        }
        
        private void ResetDraggableObject()
        {
            _draggableObject.EndDrag();
            _draggableObject = null;
        }

        private void SetupForMerge()
        {
            if (_draggableObject is IItem item)
                _mergeService.SetItemToMerge(item);
        }
        
        private void CalculationDeltaPosition()
        {
            var mousePosition = _cameraProvider.Value.ScreenToWorldPoint(_inputService.MousePosition);
            _beganPosition = _draggableObject.SelfTransform.position;
            _deltaPosition = new Vector2(mousePosition.x - _beganPosition.x, mousePosition.y - _beganPosition.y);
        }
        
        private void SubscribeOnEvents()
        {
            _inputService.OnLeftMouseUp += EndDrag;
        }

        private void UnsubscribeOnEvents()
        {
            _inputService.OnLeftMouseUp -= EndDrag;
        }
    }
}