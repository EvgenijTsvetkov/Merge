using System;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public interface ICell
    {
        public Transform SelfTransform { get; }
        public Vector3Int LocalPlace { get; set; }

        public event Action<Vector3Int> OnEnterInteractiveObject;
        public event Action OnExitInteractiveObject;
    }
}