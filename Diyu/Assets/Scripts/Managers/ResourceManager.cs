using Abilities;
using Entities;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public GameObject[] projectileList; //handles projectile spawning
        public ParticleSystem[] particleList; //handles particle spawning
        public GameObject[] indicatorList; //used to preview abilities
        public GameObject[] lootList;

        public int abilityCount
        {
            get => 7;
        }
        
        public int weaponCount
        {
            get => 8;
        }
        public Rarities GetRarity(int rank)
        {
            switch (rank)
            {
                case 0:
                    return Rarities.COMMON;
                case 1:
                    return Rarities.UNCOMMON;
                case 2:
                    return Rarities.RARE;
                case 3:
                    return Rarities.EPIC;
                case 4:
                    return Rarities.LEGENDARY;
                default:
                    return Rarities.COMMON;
            }
        }
        public Ability GetAbility(int id,Rarities rarity, Entity target)
        {
            switch (id)
            {
                case 1:
                    return new AbilityRegen_1(rarity,target);
                case 2:
                    return new AbilityHeal_2(rarity,target);
                case 3:
                    return new AbilityExplosion_3(rarity,target);
                case 4:
                    return new AbilityCharge_4(rarity,target);
                case 5:
                    return new AbilityBerserk_5(rarity,target);
                case 6:
                    return new AbilityShockwave_6(rarity,target);
                case 7:
                    return new AbilityVolley_7(rarity,target);
                default:
                    return new AbilityNone_0();
            }
        }

        public string GetAbilityName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Regeneration";
                case 2:
                    return "Heal";
                case 3:
                    return "Explosion";
                case 4:
                    return "Charge";
                case 5:
                    return "Berserk";
                case 6:
                    return "Shockwave";
                case 7:
                    return "Fire Volley";
                default:
                    return "No Abilities";
            }
        }
        
        public Weapon GetWeapon(int id,Rarities rarity, Entity target)
        {
            switch (id)
            {
                case 1:
                    return new Firespell(rarity,target);
                case 2:
                    return new SwordAttack(rarity,target);
                case 3:
                    return new AxeAttack(rarity,target);
                case 4:
                    return new ScytheAttack(rarity,target);
                case 5:
                    return new ScepterAttack(rarity,target);
                case 6:
                    return new ConjurationAttack(rarity,target);
                case 7:
                    return new BowAttack(rarity, target);
                case 8:
                    return new DaggerAttack(rarity, target);
                default:
                    return new SwordAttack(rarity,target);
            }
        }
    }
}