using System;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public class Cell : MonoBehaviour, ICell
    {
        [SerializeField] private TriggerObserver _triggerObserver;
       
        private Transform _selfTransform;

        public Transform SelfTransform => _selfTransform;
        public Vector3Int LocalPlace { get; set; }
        
        public event Action<Vector3Int> OnEnterInteractiveObject;
        public event Action OnExitInteractiveObject;

        private void Awake()
        {
            _selfTransform = transform;
        }

        private void Start()
        {
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }
        
        private void OnEnterInteractiveObjectHandler(GameObject interactiveObject)
        {
            OnEnterInteractiveObject?.Invoke(LocalPlace);
        }
        private void OnExitInteractiveObjectHandler(GameObject interactiveObject)
        {
            OnExitInteractiveObject?.Invoke();
        }

        private void SubscribeOnEvents()
        {
            _triggerObserver.TriggerEnter += OnEnterInteractiveObjectHandler;
            _triggerObserver.TriggerExit += OnExitInteractiveObjectHandler;
        }

        private void UnsubscribeOnEvents()
        {
            _triggerObserver.TriggerEnter -= OnEnterInteractiveObjectHandler;
            _triggerObserver.TriggerExit -= OnExitInteractiveObjectHandler;
        }
    }
}