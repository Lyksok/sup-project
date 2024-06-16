using System;
using System.Collections.Generic;
using Abilities;
using Buffs;
using Managers;
using Mirror;
using UnityEngine;
using Weapons;

namespace Entities
{
    public abstract class Entity : NetworkBehaviour
    {
        
        public ResourceManager resources; //handles projectile spawning
        public bool isAttacking;
        public bool isCasting;
        public ParticleSystem heal;
        
        [Header("Weapons")] public Weapon primaryWeapon;
        public KeyCode primaryWeaponAttackKey = KeyCode.Mouse0;

        [Header("Abilities")] 
        public Ability[] abilityList = {new AbilityNone_0(),new AbilityNone_0(),new AbilityNone_0(),new AbilityNone_0()};
        public Ability classPassive;

        public GameObject anchor;
    
        [Header("Buffs")] public List<Buff> buffList;
    
        [Header("Debuffs")] public List<Buff> debuffList;

        public void AddBuff(Buff buff)
        {
            bool isnew = true;
            for (int i = 0; i < buffList.Count; i++)
            {
                if (buffList[i].Id == buff.Id && isnew)
                {
                    isnew = false;
                    //b.Refresh(buff);
                    //buffList[i].OnEnd();
                    //buffList[i] = buff;
                    //buff.OnAdd();
                    buffList[i].Refresh(buff);
                }
            }

            if (isnew)
            {
                buffList.Add(buff);
                buff.OnAdd();
            }
        }

        public void RemoveBuff(Buff buff)
        {
            int i = 0;
            while (i < buffList.Count)
            {
                if (buffList[i].Id == buff.Id)
                {
                    buffList[i].OnEnd();
                    buffList.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public void AddDebuff(Buff buff)
        {
            bool isnew = true;
            foreach (var b in debuffList)
            {
                if (b.Id == buff.Id)
                {
                    isnew = false;
                    b.Refresh(buff);
                }
            }

            if (isnew)
            {
                debuffList.Add(buff);
                buff.OnAdd();
            }
        }

        public void RemoveDebuff(Buff buff)
        {
            int i = 0;
            while (i < debuffList.Count)
            {
                if (debuffList[i].Id == buff.Id)
                {
                    debuffList.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        protected void HandleBuffs()
        {
            foreach (var b in buffList)
            {
                b.Effect();
                b.Tick(Time.deltaTime);
                if (!b.permanent)
                {
                    if (b.Duration <= 0)
                    {
                        b.OnEnd();
                        buffList.Remove(b);
                    }
                }
            }
        }
    
        protected void HandleDebuffs()
        {
            foreach (var b in debuffList)
            {
                b.Effect();
                b.Tick(Time.deltaTime);
                if (!b.permanent)
                {
                    if (b.Duration <= 0)
                    {
                        b.OnEnd();
                        debuffList.Remove(b);
                    }
                }
            }
        }

        protected void HandleAbility(Ability ability, KeyCode key)
        {
            if (inEvent || !canCast)
            {
                return;
            }
            ability.Tick(Time.deltaTime);
            if (Input.GetKey(key))
            {
                ability.SetupEffect();
                isCasting = true;
            }
            if (Input.GetKeyUp(key))
            {
                ability.ActiveEffect();
                isCasting = false;
            }
            ability.PassiveEffect();
        }
        
        protected void HandleAttack(Weapon weapon, KeyCode key)
        {
            if (inEvent || !canAttack)
            {
                return;
            }
            weapon.Tick(Time.deltaTime);
            if (Input.GetKeyDown(key))
            {
                weapon.CmdAttack();
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
        }

        public abstract void OnDeath();
        
        [Command(requiresAuthority = false)]
        public void CmdTakeDamage(float damage, DamageType damageType,Entity attacker)
        {
            TakeDamageRpc(damage, damageType, attacker);
            //resources.GenerateLoot(model.transform.position);
        }

        [ClientRpc]
        public void TakeDamageRpc(float damage, DamageType damageType,Entity attacker)
        {
            
            if (damage < 0)
            {
                return;
            }
            float actualDamage = damage;
            if (damageType == DamageType.PHYSICAL) //reduce damage using defense stats
            {
                actualDamage -= armor;
            } else if (damageType == DamageType.MAGICAL)
            {
                actualDamage -= magicResist;
            }

            if ((damage / 10) > actualDamage) //max damage reduction from defense stats is 90%
            {
                actualDamage = damage / 10;
            }
            
            health -= (float)(Math.Round(actualDamage) + 1); //min damage is 1
            if (attacker != null && attacker.lifesteal > 0)
            {
                attacker.CmdHeal(actualDamage * lifesteal);
            }
            if (health <= 0) //trigger death if HP reaches 0
            {
                OnDeath();
                health = 0;
            }
        }
        
        [Command(requiresAuthority = false)]
        public void CmdHeal(float healing)
        {
            HealRpc(healing);
        }

        [ClientRpc]
        public void HealRpc(float healing)
        {
            if (healing < 0)
            {
                return;
            }
            heal.Play();
            healing *= healingPower;
            health += healing;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
        
        
        public void CalculateASPD()
        {
            attackSpeed = 1;
            foreach (var mod in aspdModifiers.Values)
            {
                attackSpeed += mod;
            }
        }
        
        [Header("Base Stats")] 
        [SyncVar] public float health; //current HP
        [SyncVar] public float maxHealth; //flat base HP
        [SyncVar] public float attackDamage; //flat base AD, boosts physical attacks and abilities
        [SyncVar] public float abilityPower; //flat base AP, boosts magical attacks and abilities
        [SyncVar] public float armor; //flat reduction to physical damage received, cant reduce more than 90% of original damage
        [SyncVar] public float magicResist; //flat reduction to magical damage received, cant reduce more than 90% of original damage
        [SyncVar] public float moveSpeed; //movement speed multiplier, 1 = 100%, 2 = 200%
        [SyncVar] public float attackSpeed; //# of attacks per second, 1 = 1 sec between each attack, 2 = 0.5 sec per attack
        [SyncVar] public float healingPower; //healing received multiplier, 1 = 100%, 2 = 200%
        //[SyncVar] public float cooldownReduction; //cooldown multiplier, 1 = 100% of original cooldown, hard cap at 50% CDR
        //[SyncVar] public float tenacity; //reduces debuffs & CCs duration
        [SyncVar] public float lifesteal; //health gained from damage, 1 = 100%, reduced by 50% to abilities
        
        public Dictionary<int,float> aspdModifiers;
        
        public GameObject body;  //part of the player that moves
        public GameObject model;  //part of the player that turns
        
        public bool canMove; //crowd control checks
        public bool canAttack;
        public bool canCast;
        public bool inEvent;
    }
}