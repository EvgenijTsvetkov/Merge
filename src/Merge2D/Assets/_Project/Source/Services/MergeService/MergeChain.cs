using System;
using System.Collections.Generic;

namespace Merge2D.Source.Services
{
    [Serializable]
    public struct MergeChain
    {
        public ChainType Type;
        public List<ItemType> Chain;
    }
}   
