using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace GUIManagement
{
    public class GUIAnimationBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _originalScale;
        private bool _isSetup;


        private void Start()
        {
            transform.localScale = new Vector3(0.5f, 0.5f,  0.5f);
            startUp();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isSetup)
            {
                Move();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DOTween.KillAll();
            resetAnimation();
        }

        private void startUp()
        {
            _isSetup = false;
            transform.DOScale(1f, 2f).SetEase(Ease.InOutQuint).OnComplete(delegate
            {
                _isSetup = true;
            });

            _originalScale = Vector3.one;
        }


        private void Move()
        {
            transform.DOScale(1.1f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void resetAnimation()
        {
            var position = transform.position;
            transform.DOScale(_originalScale, 1f);
        }
        
    }
}