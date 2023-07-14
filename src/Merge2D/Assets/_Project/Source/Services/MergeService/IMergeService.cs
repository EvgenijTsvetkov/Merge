using System.Threading.Tasks;

namespace Merge2D.Source.Services
{
    public interface IMergeService
    {
        bool CanMerge { get; }
        
        void Initialize();
        void SetItemToMerge(IItem item);
        void Merge();
        void Reset();
    }
}