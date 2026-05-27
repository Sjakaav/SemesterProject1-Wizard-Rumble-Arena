
using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class ManaShieldBehaviour : MonoBehaviour
    {
        private Transform attackPoint;
        private LayerMask _layerMask;
        public float AttackRange = 1f;
        
        private GameObject _parent;

        public GameObject Parent
        {
            get => _parent;
            set
            {
                if (_parent != value)
                {
                    _parent = value;
                }
            }
        }
        
        private void Start()
        {
            var gobj = gameObject.GetComponent<PolygonCollider2D>();
            attackPoint = gobj.transform;
            _layerMask = LayerMask.GetMask("Player");
        }

        public void HitStuff()
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, _layerMask);
            foreach (var collider in hitPlayer)
            {
                if (collider.gameObject != Parent)
                {
                    PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
                    playerController.TakeDamage(5.0);
                }
            }
        }

        
    }
}
