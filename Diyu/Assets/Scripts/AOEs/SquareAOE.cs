using System.Collections.Generic;
using Buffs;
using Entities;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;

namespace AOEs
{
    public class SquareAOE
    {
        public Vector3 size;
        public Vector3 center;
        public Entity user;
        public float damage;
        [CanBeNull] public Buff buff;
        public bool canAffectSelf;
        public DamageType damageType;
        public bool isBuff;
        public Quaternion orientation;

        public GameObject visual;

        public SquareAOE(Vector3 _center, Vector3 _size, Quaternion _orientation, Entity _user, float _damage, [CanBeNull] Buff _buff, bool _isBuff, bool _canAffectSelf, DamageType _damageType)
        {
            center = _center;
            size = _size;
            user = _user;
            damage = _damage;
            buff = _buff;
            canAffectSelf = _canAffectSelf;
            damageType = _damageType;
            isBuff = _isBuff;
            orientation = _orientation;
        }

        public void Effect(List<Entity> targets) //applies the effect to the targets, use FindTargets() to get the targets
        {
            foreach (var target in targets)
            {
                target.TakeDamageRpc(damage,damageType);
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
            Collider[] colliders = Physics.OverlapBox(center,size,orientation);
            foreach (var c in colliders)
            { 
                Debug.LogError(c.gameObject.name);
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