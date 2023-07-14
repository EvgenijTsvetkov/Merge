using System;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public interface IInputService
    {
        Vector3 MousePosition { get; }

        event Action OnLeftMouseDown;
        event Action OnLeftMouseUp;
    }
}