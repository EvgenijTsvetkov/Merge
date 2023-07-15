using Merge2D.Source.Data;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IMainCameraProvider _cameraProvider;
        private readonly IInputService _inputService;
        private readonly IPhysicsCaster _physicsCaster;
        private readonly IDragAndDropService _dragAndDropService;

        public InteractionService(IMainCameraProvider cameraProvider, IInputService inputService, IPhysicsCaster physicsCaster,
            IDragAndDropService dragAndDropService)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
            _physicsCaster = physicsCaster;
            _dragAndDropService = dragAndDropService;
        }
        
        public void Initialize()
        {
            SubscribeOnEvents();
        }
        
        private void SubscribeOnEvents()
        {
            _inputService.OnLeftMouseDown += LeftLeftMouseClicked;
        }

        private void LeftLeftMouseClicked()
        {
            if (TryGetAvailableObjectToDraggable(out IDraggable draggable) == false)
                return;

            if (draggable.CanDrag) 
                _dragAndDropService.BeganDragFor(draggable);
        }

        private bool TryGetAvailableObjectToDraggable(out IDraggable draggable)
        {
            draggable = null;

            Vector2 origin = _cameraProvider.Value.ScreenToWorldPoint(_inputService.MousePosition);
            RaycastResult result = _physicsCaster.Raycast(origin, Constants.InteractiveObjectsLayerValue);

            return result.HasHit && result.HitInfo.collider.TryGetComponent(out draggable);
        }
    }
}