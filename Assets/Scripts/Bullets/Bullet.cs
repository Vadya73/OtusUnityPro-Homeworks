using System;
using Common;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PhysicsLayer _physicsLayerType;

        private int _damage;
        public int Damage => _damage;

        private void Awake()
        {
            if (_rigidbody2D == null)
                _rigidbody2D = GetComponent<Rigidbody2D>();

            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Vector3 position,Color color, int damage, PhysicsLayer physicsLayerType, Vector2 velocity)
        {
            transform.position = position;
            _spriteRenderer.color = color;
            _damage = damage;
            _physicsLayerType = physicsLayerType;
            _rigidbody2D.velocity = velocity;
        }
        private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEntered?.Invoke(this, collision);
    }
}