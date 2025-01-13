using Common;
using UnityEngine;

namespace Bullets
{
    public struct BulletArguments
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public Color Color;
        public int Damage;
        public PhysicsLayer PhysicsLayerType;
    }
}