﻿using Abilities;
using Entities;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Weapons
{
    public class DaggerAttack : RangedWeapon
    {
        private readonly GameObject _dagger;
        private readonly ParticleSystem _firelaunch;
        private const float Speed = 30.0f;

        //public override string Name => "firespell";
        public override int id => 8;

        public DaggerAttack(Rarities rarity,Entity user)
        {
            Name = "Daggers";
            switch (rarity)
            {
                case Rarities.COMMON:
                    baseDamage = 10;
                    baseASPD = 0.75f;
                    attackSpeedPercent =  0.5f;
                    damagePercent = 0.5f;
                    break;
                case Rarities.UNCOMMON:
                    baseDamage = 12;
                    baseASPD = 0.8f;
                    attackSpeedPercent =  0.55f;
                    damagePercent = 0.6f;
                    break;
                case Rarities.RARE:
                    baseDamage = 14;
                    baseASPD = 0.85f;
                    attackSpeedPercent =  0.60f;
                    damagePercent = 0.7f;
                    break;
                case Rarities.EPIC:
                    baseDamage = 16;
                    baseASPD = 0.9f;
                    attackSpeedPercent =  0.65f;
                    damagePercent = 0.8f;
                    break;
                case Rarities.LEGENDARY:
                    baseDamage = 18;
                    baseASPD = 0.95f;
                    attackSpeedPercent =  0.70f;
                    damagePercent = 0.9f;
                    break;
            }
            type = DamageType.MAGICAL;
            Rarity = rarity;
            User = user;
            Cooldown = 1 / (baseASPD * (attackSpeedPercent * User.attackSpeed));
            CurrentCooldown = 0;
            _dagger = User.resources.projectileList[6];
            _firelaunch = User.resources.particleList[0];
            anchor = User.anchor;
        }

        public override void SetRarity(Rarities rarity)
        {
            switch (rarity)
            {
                case Rarities.COMMON:
                    break;
                case Rarities.UNCOMMON:
                    break;
                case Rarities.RARE:
                    break;
                case Rarities.EPIC:
                    break;
                case Rarities.LEGENDARY:
                    break;
            }
        }
        
        [Command]
        public override void CmdAttack()
        {
            timeSinceLastAttack = 0;
            if (CanAttack)
            {
                Cooldown = 1 / (baseASPD * (attackSpeedPercent * User.attackSpeed));
                CurrentCooldown = Cooldown;
                AttackRpc();
            }
        }
        [ClientRpc]
        public override void AttackRpc()
        {
            timeSinceLastAttack = 0;
            var position = anchor.transform.position;
            GameObject newFireball = Object.Instantiate(_dagger, position, Quaternion.identity);
            newFireball.GetComponent<ThrownDagger>().damage = (baseDamage + damagePercent * User.abilityPower);
            newFireball.GetComponent<ThrownDagger>().attacker = User;
            Rigidbody rb = newFireball.GetComponent<Rigidbody>();

            rb.AddForce(Speed * anchor.transform.forward, ForceMode.VelocityChange);
            _firelaunch.transform.position = position;
            _firelaunch.Play();
        }
        /*[Command]
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
        }*/

        public override int? Ammo => null;
        
    }
}