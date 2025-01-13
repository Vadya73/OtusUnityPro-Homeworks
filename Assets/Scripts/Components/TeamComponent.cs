using Common;
using UnityEngine;

namespace Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [SerializeField] private PhysicsLayer _teamPhysicsLayer;
        public PhysicsLayer TeamPhysicsLayer => _teamPhysicsLayer;
    }
}