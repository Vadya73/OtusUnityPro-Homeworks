using Components;
using Input;
using UnityEngine;

namespace Character
{
    public class PlayerMoveObserver : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private PlayerController _playerController;

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _inputManager.MoveAction += _moveComponent.MoveByRigidbodyVelocity;
            _inputManager.FireAction += _playerController.Shoot;
            _hitPointsComponent.HpEmpty += _playerController.OnCharacterDeath;
        }

        private void Unsubscribe()
        {
            _inputManager.MoveAction -= _moveComponent.MoveByRigidbodyVelocity;
            _inputManager.FireAction -= _playerController.Shoot;
            _hitPointsComponent.HpEmpty -= _playerController.OnCharacterDeath;
        }
    }
}