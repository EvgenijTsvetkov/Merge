using System;
using DG.Tweening;
using Merge2D.Source.Data;
using UnityEngine;
using Zenject;

namespace Merge2D.Source
{
    public class ItemAnimator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _highlightSpriteRenderer;

        private Sequence _highlightAnimation;
        private IConfigsProvider _configsProvider;

        [Inject]
        public void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        private void Awake()
        {
            CreateHighlightAnimation();
        }

        public void PlayHighlight()
        {
            _highlightAnimation.Restart();
        }

        public void StopHighlight()
        {
            _highlightAnimation.Pause();

            _highlightSpriteRenderer.DOFade(0, 0f);
        }

        private void CreateHighlightAnimation()
        {
            _highlightAnimation = DOTween.Sequence();

            _highlightAnimation
                .Append(_highlightSpriteRenderer.DOFade(.2f, 0))
                .Append(_highlightSpriteRenderer.DOFade(.6f, 1f))
                .Append(_highlightSpriteRenderer.DOFade(.2f, 1f));

            _highlightAnimation.SetLoops(-1, LoopType.Yoyo);
            _highlightAnimation.Pause();
        }

        public void PlayMergeAnimation(Vector3 position, Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();
            float duration = _configsProvider.MergeConfig.MergeTime;

            sequence
                .Join(transform.DOMove(position, duration))
                .Join(transform.DOScale(Vector3.zero, duration))
                .OnComplete(() => { callback?.Invoke(); });

            sequence.SetAutoKill();
        }

        public void PlayCreationAnimation(Action callback = null)
        {
            Sequence sequence = DOTween.Sequence();
            float duration = _configsProvider.MergeConfig.MergeTime;

            transform.localScale = Vector3.zero;
            sequence
                .Join(transform.DOScale(Vector3.one, duration))
                .OnComplete(() => { callback?.Invoke(); });

            sequence.SetAutoKill();
        }
    }
}