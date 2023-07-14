using System;
using UnityEngine;
using Zenject;

namespace Merge2D.Source.Services
{
    public class StandaloneInputService : IInputService, ITickable
    {
        public Vector3 MousePosition => Input.mousePosition;
        
        public event Action OnLeftMouseDown;
        public event Action OnLeftMouseUp;

        public void Tick()
        {
            DetectionLeftMouseClick();
        }

        private void DetectionLeftMouseClick()
        {
            if (Input.GetMouseButtonDown(0)) 
                OnLeftMouseDown?.Invoke();

            if (Input.GetMouseButtonUp(0)) 
                OnLeftMouseUp?.Invoke();
        }
    }
}