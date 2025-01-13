using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position { get { return _firePoint.position; } }

        public Quaternion Rotation { get { return _firePoint.rotation; } }
        
        [SerializeField] private Transform _firePoint;
    }
}