using System.Collections.Generic;
using UnityEngine;

namespace Merge2D.Source
{
    public interface IItemsCollector
    {
        bool HasItem(Vector3Int localPlace);
        void Add(Vector3Int localPlace, IItem item);
        
        IItem Get(Vector3Int localPlace);
        Vector3Int GetLocalPlaceFor(IItem item);

        void Remove(IItem item);
        void RemoveAt(Vector3Int localPlace);
    }
}