using System;
using Character;
using Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Input
{
    public sealed class InputManager : MonoBehaviour
    {
        public event Action<Vector2> MoveAction;
        public event Action FireAction;
        
        [SerializeField] private MoveComponent _characterMoveComponent;
        [SerializeField] private PlayerController _playerController;
        
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                FireAction?.Invoke();
            }
        }
        
        private void FixedUpdate()
        {
            Vector2 direction = Vector2.zero;

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                direction = Vector2.left;
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                direction = Vector2.right;
            else
                return;

            MoveAction?.Invoke(direction * Time.fixedDeltaTime);
        }
    }
}