using Mirror;
using UnityEngine;

namespace Weapons
{
    public abstract class Projectile : NetworkBehaviour
    {
        protected abstract float BulletSpeed { get; }
        protected abstract float BulletLifeCycle { get; }
        public abstract void OnTriggerEnter(Collider other);
    }
}