using System.Threading.Tasks;
using UnityEngine;

namespace Merge2D.Source.Services
{
    public interface IAssetProvider
    {
        Task<T> LoadAssetAsync<T>(string assetName) where T: class;
        Task<T> InstantiateAsync<T>(Transform parent, string addressableName) where T: Component;
        void ReleaseAsset(string assetName);
    }
}