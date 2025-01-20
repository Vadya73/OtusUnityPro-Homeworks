﻿using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyAttackTimer : MonoBehaviour
    {
        public event Action OnTimeToShoot;
        
        [SerializeField] private float _countdown = 1.0f;
    
        private float _currentTime;

        private void Reset() => _currentTime = _countdown;

        public void TimerCountdown(bool isReached, bool anyHitPoints)
        {
            if (!isReached) 
                return;
            
            if (!anyHitPoints)
                return;
            
            _currentTime -= Time.fixedDeltaTime;
            
            if (_currentTime > 0)
                return;
            
            OnTimeToShoot?.Invoke();
            
            Reset();
        }
    }
}