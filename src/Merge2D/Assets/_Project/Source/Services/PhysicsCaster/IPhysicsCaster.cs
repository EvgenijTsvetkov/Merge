using UnityEngine;

namespace Merge2D.Source
{
    public interface IPhysicsCaster
    {
        public RaycastResult Raycast(Vector2 origin, LayerMask layerMask);
        public RaycastAllResult RaycastAll(Vector2 origin, LayerMask layerMask);
    }
}
