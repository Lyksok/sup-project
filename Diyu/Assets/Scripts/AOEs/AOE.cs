using System.Collections.Generic;
using Buffs;
using Entities;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;

namespace AOEs
{
    public class AreaOfEffect
    {
        public float radius;
        public Vector3 center;
        public Entity user;
        public float damage;
        [CanBeNull] public Buff buff;
        public bool canAffectSelf;
        public DamageType damageType;
        public bool isBuff;

        public GameObject visual;

        public AreaOfEffect(Vector3 _center, float _radius, Entity _user, float _damage, [CanBeNull] Buff _buff, bool _isBuff, bool _canAffectSelf, DamageType _damageType)
        {
            center = _center;
            radius = _radius;
            user = _user;
            damage = _damage;
            buff = _buff;
            canAffectSelf = _canAffectSelf;
            damageType = _damageType;
            isBuff = _isBuff;
        }

        public void Effect(List<Entity> targets) //applies the effect to the targets, use FindTargets() to get the targets
        {
            foreach (var target in targets)
            {
                target.TakeDamage(damage,damageType);
                if (buff != null)
                {
                    if (isBuff)
                    {
                        target.AddBuff(buff);
                    }
                    else
                    {
                        target.AddDebuff(buff);
                    }
                }
            }
        }

        public List<Entity> FindTargets() //return list of entities hit by the attack
        {
            List<Entity> targets = new List<Entity>();
            Collider[] colliders = Physics.OverlapSphere(center,radius);
            foreach (var c in colliders)
            { 
                //Debug.LogError(c.gameObject.name);
                if (c.gameObject.GetComponentInParent<NetworkIdentity>() && (canAffectSelf || c.gameObject.GetComponentInParent<NetworkIdentity>().netId != user.netId))
                {
                    var componentInParent = c.gameObject.GetComponentInParent<Entity>();
                    if (componentInParent != null)
                    {
                        targets.Add(componentInParent);
                    }
                }
            }
            return targets;
        }
    }
}