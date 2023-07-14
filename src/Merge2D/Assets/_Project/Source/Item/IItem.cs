using System;
using Merge2D.Source.Services;
using UnityEngine;

namespace Merge2D.Source
{
    public interface IItem : IPoolObject, IDraggable
    {
        ItemType Type { get; }
        ChainType ChainType { get; }

        void PlayHighlight();
        void StopHighlight();
        void PlayMergeAnimation(Vector3 position, Action callback);
        void PlayCreationAnimation();
    }
}