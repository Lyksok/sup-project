using Abilities;
using Entities;
using Gems;
using UnityEngine;
using UnityEngine.UIElements;
using Weapons;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        public GameObject[] projectileList; //handles projectile spawning
        public ParticleSystem[] particleList; //handles particle spawning
        public GameObject[] indicatorList; //used to preview abilities
        public GameObject[] lootList;
        public Sprite[] debuffIconList;
        public Sprite[] buffIconList;
        public Sprite[] abilityIconList;
        public Sprite[] passiveAbilityIconList; //class passives icons
        
        public int abilityCount
        {
            get => 10;
        }

        public void SetClass(NewPlayer player, int id)
        {
            switch (id)
            {
                case 1:
                    player.PickupWeapon(new Firespell(Rarities.COMMON,player));
                    player.classPassive = new AbilityArcanist_04(Rarities.COMMON,player);
                    break;
                case 2:
                    player.PickupWeapon(new SwordAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityBulwark_01(Rarities.COMMON,player);
                    break;
                case 3:
                    player.PickupWeapon(new AxeAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityBerserk_07(Rarities.COMMON,player);
                    break;
                case 4:
                    player.PickupWeapon(new ScytheAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityLifesteal_08(Rarities.COMMON,player);
                    break;
                case 5:
                    player.PickupWeapon(new ScepterAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityMagicMastery_06(Rarities.COMMON,player);
                    break;
                case 6:
                    player.PickupWeapon(new ConjurationAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityElemental_03(Rarities.COMMON,player);
                    break;
                case 7:
                    player.PickupWeapon(new BowAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityAttackMS_02(Rarities.COMMON,player);
                    break;
                case 8:
                    player.PickupWeapon(new DaggerAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityAssassin_05(Rarities.COMMON,player);
                    break;
                default:
                    player.PickupWeapon(new SwordAttack(Rarities.COMMON,player));
                    player.classPassive = new AbilityBulwark_01(Rarities.COMMON,player);
                    break;
            }
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
                    return new AbilityArrowRain_5(rarity,target);
                case 6:
                    return new AbilityShockwave_6(rarity,target);
                case 7:
                    return new AbilityVolley_7(rarity,target);
                case 8:
                    return new AbilityEarthSpike_8(rarity,target);
                case 9:
                    return new AbilityLastStand_9(rarity,target);
                case 10:
                    return new AbilityThunder_10(rarity,target);
                default:
                    return new AbilityNone_0();
            }
        }

        public Gem GetGem(int id,Rarities rarity, NewPlayer target)
        {
            switch (id)
            {
                case 1:
                    return new GemAD(rarity,target);
                case 2:
                    return new GemAR(rarity,target);
                case 3:
                    return new GemMR(rarity,target);
                case 4:
                    return new GemLifeSteal(rarity,target);
                case 5:
                    return new GemHealPower(rarity,target);
                case 6:
                    return new GemHP(rarity,target);
                case 7:
                    return new GemAS(rarity,target);
                case 8:
                    return new GemAP(rarity,target);
                default:
                    return new GemAD(rarity,target);
            }
        }
        
        public string GetGemName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Attack Damage Gem";
                case 2:
                    return "Armor Gem";
                case 3:
                    return "Magic Resist Gem";
                case 4:
                    return "Lifesteal Gem";
                case 5:
                    return "Healing Power Gem";
                case 6:
                    return "Health Gem";
                case 7:
                    return "Attack Speed Gem";
                case 8:
                    return "Ability Power Gem";
                default:
                    return "No Gem";
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
                case 8:
                    return "Lifesteal";
                case 9:
                    return "Last Stand";
                case 10:
                    return "Thunder";
                default:
                    return "No Abilities";
            }
        }
        
        public string GetWeaponName(int id)
        {
            switch (id)
            {
                case 1:
                    return "Fireball Spell Book";
                case 2:
                    return "Sword";
                case 3:
                    return "Axe";
                case 4:
                    return "Scythe";
                case 5:
                    return "Magic Scepter";
                case 6:
                    return "Conjuration Catalyst";
                case 7:
                    return "Bow";
                case 8:
                    return "Throwing Daggers";
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