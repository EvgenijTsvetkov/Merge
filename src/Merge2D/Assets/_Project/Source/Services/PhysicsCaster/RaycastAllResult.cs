using UnityEngine;

namespace Merge2D.Source
{
    public struct RaycastAllResult
    {
        public bool HasHit;
        public RaycastHit2D[] HitsInfos;

        public RaycastAllResult(bool hasHit, RaycastHit2D[] hitsInfos)
        {
            HasHit = hasHit;
            HitsInfos = hitsInfos;
        }
    }
}