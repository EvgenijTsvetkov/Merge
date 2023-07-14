using System;
using Merge2D.Source.Services;
using UnityEngine;

namespace Merge2D.Source
{
    [RequireComponent(typeof(ItemAnimator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private ChainType _chainType;
        [SerializeField] private SpriteRenderer _itemRender;

        private Transform _selfTransform;
        private GameObject _selfGameObject;
        private ItemAnimator _animator;

        private PolygonCollider2D _collider;

        public Transform SelfTransform => _selfTransform;
        public ItemType Type => _type;
        public ChainType ChainType => _chainType;
        public bool CanDrag { get; set; }

        private void Awake()
        {
            _selfTransform = transform;
            _selfGameObject = gameObject;
            
            _animator = GetComponent<ItemAnimator>();
            _collider = GetComponent<PolygonCollider2D>();
        }

        public void BeganDrag()
        {
            _itemRender.sortingOrder += 1;
        }

        public void EndDrag()
        {
            _itemRender.sortingOrder -= 1;
        }
        
        public void PlayHighlight()
        {
            _animator.PlayHighlight();
        }

        public void StopHighlight()
        {
            _animator.StopHighlight();
        }

        public void PlayMergeAnimation(Vector3 position, Action callback = null)
        {
            CanDrag = false;
            _collider.enabled = false;
            
            _animator.PlayMergeAnimation(position, callback);
        }

        public void PlayCreationAnimation()
        {
            _animator.PlayCreationAnimation(() =>
            {
                CanDrag = true;
                _collider.enabled = true;
            });
        }

        public void Activate()
        {
            CanDrag = true;
            
            _selfGameObject.SetActive(true);
        }

        public void Deactivate()
        {
            CanDrag = false;
            
            _selfGameObject.SetActive(false);
        }
    }
}