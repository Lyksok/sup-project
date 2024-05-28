using System;
using Mirror;
using UnityEngine;

namespace Weapons
{
    public class Firespell : Shooter
    {
        public override string Name => "firespell";
        protected override float Cooldown => 0.5f;
        [Command]
        public override void CmdAttack(Transform source)
        {
            if (!isLocalPlayer)
                return;
            GameObject newProjectile = Instantiate(projectile, source.position, source.rotation);
            RpcAttack(source);
        }
        [ClientRpc]
        public override void RpcAttack(Transform source)
        {
            throw new NotImplementedException();
        }

        public override int? Ammo => null;
        
    }
}