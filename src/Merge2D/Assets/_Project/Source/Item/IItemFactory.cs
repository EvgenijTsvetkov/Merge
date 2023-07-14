using System.Threading.Tasks;
using UnityEngine;

namespace Merge2D.Source
{
    public interface IItemFactory
    {
        Task<IItem> Create(Transform parent, ItemType type);
    }
}
