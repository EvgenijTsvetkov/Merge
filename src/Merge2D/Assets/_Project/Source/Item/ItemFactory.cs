using System.Threading.Tasks;
using Merge2D.Source.Services;
using UnityEngine;

namespace Merge2D.Source
{
    public class ItemFactory : IItemFactory
    {
        private readonly IAssetProvider _assetProvider;

        public ItemFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public async Task<IItem> Create(Transform parent, ItemType type)
        {
            IItem instance = await _assetProvider.InstantiateAsync<Item>(parent, type.ToString());

            return instance;
        }
    }
}
