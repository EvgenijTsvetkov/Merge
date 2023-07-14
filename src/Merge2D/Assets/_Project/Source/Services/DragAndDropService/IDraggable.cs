using UnityEngine;

namespace Merge2D.Source
{
    public interface IDraggable
    {
        Transform SelfTransform { get; }
        bool CanDrag { get; set; }
        void BeganDrag();
        void EndDrag();
    }
}