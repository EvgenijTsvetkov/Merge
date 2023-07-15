using System;
using UnityEngine;
using Zenject;

namespace Merge2D.Source.Services
{
    public class MobileInputService : IInputService, ITickable
    {
        private Vector3 _touchPosition;
        public Vector3 MousePosition => _touchPosition;
        
        public event Action OnLeftMouseDown;
        public event Action OnLeftMouseUp;

        public void Tick()
        {
            if (HasTouch() == false)
                return;
            
            GetTouchPosition();
            DetectionPhase();
        }

        private bool HasTouch()
        {
            return Input.touchCount > 0;
        }

        private void DetectionPhase()
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnLeftMouseDown?.Invoke();
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnLeftMouseUp?.Invoke();
                    break;
            }
        }

        private void GetTouchPosition()
        {
            _touchPosition = Input.GetTouch(0).position;
        }
    }
}
