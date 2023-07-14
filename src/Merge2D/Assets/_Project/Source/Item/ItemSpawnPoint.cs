using UnityEngine;

namespace Merge2D.Source
{
    public class ItemSpawnPoint : MonoBehaviour
    {
        [SerializeField] private ItemType _type;

        private Transform _selfTransform;

        public Transform SelfTransform => _selfTransform;
        public ItemType Type => _type;

        private void Awake()
        {
            _selfTransform = transform;
        }
    }
}