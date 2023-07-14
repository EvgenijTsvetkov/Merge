using System.Collections.Generic;
using System.Threading.Tasks;
using Merge2D.Source.Utils;

namespace Merge2D.Source
{
    public class ItemPool : IItemPool
    {
        private readonly IWorldObjectsCollector _worldObjectsCollector;
        private readonly IItemFactory _itemFactory;

        private readonly Dictionary<ItemType, Queue<IItem>> _pool = new Dictionary<ItemType, Queue<IItem>>();

        private const int DefaultSize = 5;

        public ItemPool(IWorldObjectsCollector worldObjectsCollector, IItemFactory itemFactory)
        {
            _worldObjectsCollector = worldObjectsCollector;
            _itemFactory = itemFactory;
        }

        public async Task<IItem> Get(ItemType type)
        {
            if (_pool[type].Count > 0)
                return _pool[type].Dequeue();
            
            return await _itemFactory.Create(_worldObjectsCollector.ContainerForItems, type);
        }

        public void Remove(IItem item)
        {
            item.Deactivate();
            
            _pool[item.Type].Enqueue(item);
        }

        public async Task CreatePool()
        {
            foreach (var itemType in EnumUtil.GetValues<ItemType>())
            {
                Queue<IItem> queue = new Queue<IItem>(DefaultSize);
                for (int i = 0; i < DefaultSize; i++)
                {
                    IItem item = await _itemFactory.Create(_worldObjectsCollector.ContainerForItems, itemType);
                    item.Deactivate();
                    
                    queue.Enqueue(item);
                }
                
                _pool.Add(itemType, queue);
            }
        }
    }
}