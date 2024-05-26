using Mirror;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : NetworkBehaviour
    {
        public abstract string Name { get; }
        protected float ActualCooldown { get; set; } = 0;
        protected abstract float Cooldown { get; }
        protected bool CanAttack => ActualCooldown >= Cooldown;

        public abstract void CmdAttack(Transform source);
        public abstract void RpcAttack(Transform source);
        protected void UpdateCooldown(float delta)
        {
            ActualCooldown += delta;
        }
    }
}
