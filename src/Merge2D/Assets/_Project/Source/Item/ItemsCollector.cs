using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Merge2D.Source
{
    public class ItemsCollector : IItemsCollector
    {
        private readonly Dictionary<Vector3Int, IItem> _items = new Dictionary<Vector3Int, IItem>();
        
        public bool HasItem(Vector3Int localPlace)
        {
            return _items.ContainsKey(localPlace);
        }

        public void Add(Vector3Int localPlace, IItem item)
        {
            _items.Add(localPlace, item);
        }

        public IItem Get(Vector3Int localPlace)
        {
            return _items[localPlace];
        }

        public Vector3Int GetLocalPlaceFor(IItem item)
        {
            int index = _items.Values.ToList().IndexOf(item);
            Vector3Int localPlace = _items.Keys.ToList()[index];
            
            return localPlace;
        }

        public void Remove(IItem item)
        {
            RemoveAt(GetLocalPlaceFor(item));
        }

        public void RemoveAt(Vector3Int localPlace)
        {
            _items.Remove(localPlace);
        }
    }
}