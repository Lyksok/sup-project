using Abilities;
using Entities;
using Mirror;
using UnityEngine;

namespace Weapons
{
    //Abstract class used for managing Weapons
    public abstract class Weapon
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public abstract int id { get; }
        public Entity User;
        protected Rarities Rarity; //Rarity of the weapon, changes stats
        
        protected DamageType type; //type of damage dealt
        public float damagePercent; //% of AD/AP of the user taken for damage calculations
        public float attackSpeedPercent; //% of attack speed of the user taken to calculate speed of the weapon
        public float baseASPD; //flat base attack speed
        public float baseDamage; //flat base damage dealt
        public abstract void SetRarity(Rarities rarity); //Sets the Rarity of the weapon to the input Rarity, changing stats
        public void ChangeRarity(int change) //Changes the Rarity of the weapon by 1 tier, up or down (1 -> 1 tier up, -1 -> 1 tier down)
        {
            if (change == 1)
            {
                if (Rarity == Rarities.COMMON)
                {
                    SetRarity(Rarities.UNCOMMON);
                } else if (Rarity == Rarities.UNCOMMON)
                {
                    SetRarity(Rarities.RARE);
                } else if (Rarity == Rarities.RARE)
                {
                    SetRarity(Rarities.EPIC);
                } else if (Rarity == Rarities.EPIC)
                {
                    SetRarity(Rarities.LEGENDARY);
                }
            }
            else if (change == -1)
            {
                if (Rarity == Rarities.UNCOMMON)
                {
                    SetRarity(Rarities.COMMON);
                } else if (Rarity == Rarities.EPIC)
                {
                    SetRarity(Rarities.RARE);
                } else if (Rarity == Rarities.LEGENDARY)
                {
                    SetRarity(Rarities.EPIC);
                } else if (Rarity == Rarities.RARE)
                {
                    SetRarity(Rarities.UNCOMMON);
                }
            }
        }
        protected float CurrentCooldown { get; set; } = 0; //current cooldown, can attack if <= 0
        protected float Cooldown { get; set; } //base cooldown
        public bool CanAttack => CurrentCooldown <= 0;

        public abstract void Attack();

        //public abstract void CmdAttack(Transform source); tf is that ?
        //public abstract void RpcAttack(Transform source);
        public void Tick(float delta)
        {
            if (!CanAttack)
            {
                CurrentCooldown -= delta;
            }
        }
    }
}