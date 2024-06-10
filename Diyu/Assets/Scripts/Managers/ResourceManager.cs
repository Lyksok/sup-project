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
                    return Rarities.MYTHIC;
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
                default:
                    return new AbilityNone_0();
            }
        }
    }
}
