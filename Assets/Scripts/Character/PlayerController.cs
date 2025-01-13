using Bullets;
using Common;
using Components;
using UnityEngine;

namespace Character
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _character; 
        [SerializeField] private GameManager.GameManager _gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private WeaponComponent _weaponComponent;
        
        [SerializeField] private float _reloadTime;
        private bool _canShoot = true;
        private float _reloadTimer = 0f;

        private void Update()
        {
            if (!_canShoot)
            {
                _reloadTimer += Time.deltaTime;

                if (_reloadTimer >= _reloadTime)
                {
                    _reloadTimer = 0f;
                    ShootAvailableSwitch();
                }
            }
        }

        public void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }

        public void Shoot()
        {
            if (!_canShoot)
                return;

            _bulletSystem.FlyBulletByArgs(new BulletArguments
            {
                PhysicsLayerType = PhysicsLayer.PLAYER_BULLET,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.Position,
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
            ShootAvailableSwitch();
        }

        private void ShootAvailableSwitch() => _canShoot = !_canShoot;
    }
}