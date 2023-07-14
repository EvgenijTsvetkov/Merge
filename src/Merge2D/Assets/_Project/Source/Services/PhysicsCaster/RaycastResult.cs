using UnityEngine;

namespace Merge2D.Source
{
    public struct RaycastResult
    {
        public bool HasHit;
        public RaycastHit2D HitInfo;

        public RaycastResult(bool hasHit, RaycastHit2D hitInfo)
        {
            HasHit = hasHit;
            HitInfo = hitInfo;
        }
    }
}