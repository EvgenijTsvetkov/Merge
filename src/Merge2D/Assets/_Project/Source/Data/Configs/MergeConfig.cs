using System.Collections.Generic;
using System.Linq;
using Merge2D.Source.Services;
using UnityEngine;
using static Merge2D.Source.Data.Constants;

namespace Merge2D.Source.Data
{
    [CreateAssetMenu(fileName = "MergeConfig", menuName = ProjectName + "/Merge Config")]
    public class MergeConfig : ScriptableObject, IMergeConfig
    {
        [Range(0.3f, 2f)]
        [SerializeField] private float _mergeTime = 1f;
        [SerializeField] private List<MergeChain> _chains;

        public float MergeTime => _mergeTime;

        public List<ItemType> Chain(ChainType type)
        {
            return _chains.First(x => x.Type == type).Chain;
        }
    }
}
