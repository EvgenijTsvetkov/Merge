using System.Collections.Generic;
using Merge2D.Source.Services;

namespace Merge2D.Source.Data
{
    public interface IMergeConfig
    {
        float MergeTime { get; }
        List<ItemType> Chain(ChainType type);
    }
}