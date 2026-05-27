using System.Collections;
using DG.Tweening;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumables.Spawner
{
    public class DespawnTimer : MonoBehaviour
    {
        // float for how many seconds it takes before the pick up despawns
        private readonly float _despawnTimer = 60f; 

        private Rigidbody2D _rb;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            StartCoroutine(Despawn());
            //AnimationRotation();
        }

        private void Update()
        {
        }

        private void AnimationRotation()
        {
            var seq = DOTween.Sequence();
            var position = transform.position;
            seq.Append(transform.DOMoveY(position.y + 5f, 2f, false))
                .Append(transform.DOMoveY(position.y, 2f, false))
                .Append(transform.DORotate(new Vector3(0, 180, 0), 2f, RotateMode.Fast).SetLoops(-1)
                    .SetEase(Ease.Linear)).onComplete();
        }

        IEnumerator Despawn()
        {
            //yield return new WaitForSeconds(2f);
            //_originalY = transform.position.y;
            yield return new WaitForSeconds(_despawnTimer);
            DOTween.Kill(transform);
            Destroy(gameObject);
        }
    }
}