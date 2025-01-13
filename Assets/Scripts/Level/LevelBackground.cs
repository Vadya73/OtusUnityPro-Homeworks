using System;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private Params _params;
        
        private float startPositionY;
        private float endPositionY;
        private float movingSpeedY;
        private float positionX;
        private float positionZ;
        private Transform myTransform;
        
        private void Awake()
        {
            startPositionY = _params._startPositionY;
            endPositionY = _params._endPositionY;
            movingSpeedY = _params._movingSpeedY;
            myTransform = transform;
            var position = myTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void FixedUpdate()
        {
            if (myTransform.position.y <= endPositionY)
            {
                myTransform.position = new Vector3(
                    positionX,
                    startPositionY,
                    positionZ
                );
            }

            myTransform.position -= new Vector3(
                positionX,
                movingSpeedY * Time.fixedDeltaTime,
                positionZ
            );
        }

        [Serializable]
        public struct Params
        {
            [SerializeField] public float _startPositionY;
            [SerializeField] public float _endPositionY;
            [SerializeField] public float _movingSpeedY;
        }
    }
}