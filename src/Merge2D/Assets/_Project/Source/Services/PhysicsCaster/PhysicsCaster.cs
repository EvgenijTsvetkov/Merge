using System.Linq;
using UnityEngine;

namespace Merge2D.Source
{
    public class PhysicsCaster : IPhysicsCaster
    {
        private readonly RaycastHit2D[] _raycastHits = new RaycastHit2D[20];
            
        private const float Distance = 10f;
        
        public RaycastResult Raycast(Vector2 origin, LayerMask layerMask)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(origin, Vector2.zero, Distance, layerMask);
            bool hasHit = rayHit.collider != null;
            
            return new RaycastResult(hasHit, rayHit);
        }

        public RaycastAllResult RaycastAll(Vector2 origin, LayerMask layerMask)
        {
            int size = Physics2D.RaycastNonAlloc(origin, Vector2.zero, _raycastHits, Distance, layerMask);
            bool hasHit = size > 0;

            return new RaycastAllResult(hasHit, _raycastHits.Take(size).ToArray());
        }
    }
}