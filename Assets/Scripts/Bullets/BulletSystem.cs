using System.Collections.Generic;
using Components;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();
        
        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(_prefab, _container);
                m_bulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            m_cache.Clear();
            m_cache.AddRange(m_activeBullets);

            for (int i = 0, count = m_cache.Count; i < count; i++)
            {
                var bullet = m_cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletArguments bulletArguments)
        {
            if (m_bulletPool.TryDequeue(out var bullet))
                bullet.transform.SetParent(_worldTransform);
            else
                bullet = Instantiate(_prefab, _worldTransform);

            bullet.Initialize(bulletArguments.Position, bulletArguments.Color, 
                bulletArguments.Damage, bulletArguments.PhysicsLayerType, bulletArguments.Velocity);
            
            if (m_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollision;
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(bullet.Damage);
            
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_container);
                m_bulletPool.Enqueue(bullet);
            }
        }
    }
}