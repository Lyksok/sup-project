using Entities;
using Mirror;
using UnityEngine;

namespace Weapons
{
    public abstract class Projectile : Entity
    {
        //protected abstract float BulletSpeed { get; }
        //protected abstract float BulletLifeCycle { get; }
        //public abstract void OnTriggerEnter(Collider other);
        public override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}