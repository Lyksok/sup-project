using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Abilities;
using Buffs;
using Cinemachine;
using Entities;
using Gems;
using JetBrains.Annotations;
using Mirror;
using TMPro;
using UnityEngine;
using Weapons;
using Object = UnityEngine.Object;

public class NewPlayer : Entity
{
    [Header("Player characteristics")] 
    public Rigidbody playerRigidbody;
    public Camera playerCamera;
    public CinemachineVirtualCamera playerVirtualCamera;
    public Canvas playerHUD;
    public float movementSpeed = 5f;
    public LayerMask layerMask;
    [SyncVar] public Vector3 pos;
    [SyncVar] public Quaternion rot;
    public bool isSpectator;

    public GameObject statsHUD;
    private string statsValue;
    private TextMeshProUGUI statsHUD2;
    
    public GameObject abilitiesHUD;
    private string abilitiesValue;
    private TextMeshProUGUI abilitiesHUD2;
    
    public GameObject buffsHUD;
    private string buffsValue;
    private TextMeshProUGUI buffsHUD2;

    private DataManager _dataManager;

    public GameObject popUp;
    private TextMeshProUGUI _popUpText;
    public int classId;

    public List<Gem> gemList;
    
    public override void OnStartLocalPlayer()
    {
        _dataManager = FindObjectOfType<DataManager>();
        _dataManager.AddPlayer(netIdentity,gameObject);
        
        base.OnStartLocalPlayer();
    }

    public override void OnStopLocalPlayer()
    {
        _dataManager.RemovePlayer(netIdentity);
        
        base.OnStopLocalPlayer();
    }

    private void Start()
    {
        inEvent = false;
        popUp.SetActive(false);
        _popUpText = popUp.GetComponent<TextMeshProUGUI>();
        statsHUD2 = statsHUD.GetComponent<TextMeshProUGUI>();
        buffsHUD2 = buffsHUD.GetComponent<TextMeshProUGUI>();
        abilitiesHUD2 = abilitiesHUD.GetComponent<TextMeshProUGUI>();
        layerMask = LayerMask.GetMask("groundMask");
        buffList = new List<Buff>();
        debuffList = new List<Buff>();
        gemList = new List<Gem>();
        //abilityList[0] = new AbilityRegen_1(Rarities.COMMON, this);
        //abilityList[1] = new AbilityHeal_2(Rarities.LEGENDARY, this);
        //abilityList[2] = new AbilityExplosion_3(Rarities.LEGENDARY, this);
        //primaryWeapon = new Firespell(Rarities.LEGENDARY,this);
        //primaryWeapon = new SwordAttack(Rarities.LEGENDARY, this);
        resources.SetClass(this,classId);
        health = 5;
        maxHealth = 100;
        aspdModifiers = new Dictionary<int, float>();
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
            playerHUD.gameObject.SetActive(false);
        }
        OnRoundStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isFocused)
            return;

        if (isLocalPlayer)
        {
            UpdateHUD();
            HandleMovement();
            Aim();
            HandleBuffs();
            HandleDebuffs();
            HandleAttack(primaryWeapon,primaryWeaponAttackKey);
            HandleAbility(abilityList[0],KeyCode.Alpha1);
            HandleAbility(abilityList[1],KeyCode.Alpha2);
            HandleAbility(abilityList[2],KeyCode.Alpha3);
            HandleAbility(abilityList[3],KeyCode.Alpha4);
            HandleAbility(classPassive,KeyCode.Alpha0);
            CalculateASPD();
            //DebugPickup();
            //DebugPickupWpn();
            DebugOrb();
            DebugWeaponOrb();
            DebugGemOrb();
            //SrvMovement();
            DebugDamage();
            //EventManager();
        }
    }

    /*public async Task<int> EventManager()
    {
        if (inEvent)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inEvent = false;
                return 0;

            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inEvent = false;
                return 1;
            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inEvent = false;
                return 2;
            } else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inEvent = false;
                return 3;
            } 
        }

        return -1;
    }*/
    
    public void DebugDamage()
    {
        if (Input.GetKeyDown(KeyCode.C) && isLocalPlayer)
        {
            TakeDamageRpc(10,DamageType.TRUE_DAMAGE,null);
        }
    }
    
    public override void OnDeath()
    {
        isSpectator = true;
        body.SetActive(false);
    }

    public void OnRevive()
    {
        isSpectator = false;
        body.SetActive(true);
        canAttack = false;
        canCast = false;
        canMove = false;
        health = maxHealth;
        
    }

    public void OnRoundStart()
    {
        canAttack = true;
        canCast = true;
        canMove = true;
    }

    public void OpenPopUp()
    {
        popUp.SetActive(true);
        inEvent = true;
    }
    
    public void ClosePopUp()
    {
        popUp.SetActive(false);
        inEvent = false;
    }
    
    
    public void DebugOrb()
    {
        if (Input.GetKeyDown(KeyCode.E) && isLocalPlayer)
        {
            Vector3 pos = model.transform.position;
            pos.y -= 0.95f;
            pos.x += 3;
            Object.Instantiate(resources.lootList[0],pos,Quaternion.identity);
            //Debug.LogError($"{pos}");
        }
    }
    
    public void DebugWeaponOrb()
    {
        if (Input.GetKeyDown(KeyCode.R) && isLocalPlayer)
        {
            Vector3 pos = model.transform.position;
            pos.y -= 0.95f;
            pos.x -= 3;
            Object.Instantiate(resources.lootList[1],pos,Quaternion.identity);
            //Debug.LogError($"{pos}");
        }
    }
    
    public void DebugGemOrb()
    {
        if (Input.GetKeyDown(KeyCode.F) && isLocalPlayer)
        {
            Vector3 pos = model.transform.position;
            pos.y -= 0.95f;
            pos.z -= 3;
            Object.Instantiate(resources.lootList[2],pos,Quaternion.identity);
            //Debug.LogError($"{pos}");
        }
    }
    
    /*public void DebugPickup() //gives the player a random ability
    {
        if (Input.GetKeyDown(KeyCode.R) && isLocalPlayer)
        {
            int rank = RandomNumberGenerator.GetInt32(0, 5);
            int id = RandomNumberGenerator.GetInt32(1, resources.abilityCount + 1);
            Ability ability = resources.GetAbility(id, resources.GetRarity(rank), this);
            PickupAbility(ability);
            //Debug.LogError($"{ability.Rarity} {resources.GetRarity(rank)}");
        }
    }*/
    
    /*public void DebugPickupWpn() //gives the player a random weapon
    {
        if (Input.GetKeyDown(KeyCode.F) && isLocalPlayer)
        {
            Debug.LogError(_dataManager.DebugPlayers());
            int rank = RandomNumberGenerator.GetInt32(0, 5);
            int id = RandomNumberGenerator.GetInt32(1, resources.weaponCount + 1);
            Weapon weapon = resources.GetWeapon(id, resources.GetRarity(rank), this);
            PickupWeapon(weapon);
            //Debug.LogError($"{ability.Rarity} {resources.GetRarity(rank)}");
        }
    }*/

    [CanBeNull]
    public Weapon PickupWeapon(Weapon weapon)
    {
        if (!isLocalPlayer)
        {
            return null;
        }

        Weapon backup = primaryWeapon;
        primaryWeapon = weapon;
        return backup;
    }
    
    public void PickupGem(Gem gem)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        gemList.Add(gem);
    }
    
    public Ability PickupAbility(Ability ability)
    {
        Ability backup;
        if (!isLocalPlayer)
        {
            return new AbilityNone_0();
        }
        if (abilityList[0] is AbilityNone_0 || abilityList[0].GetType() == ability.GetType())
        {
            backup = abilityList[0];
            //abilityList[0].OnEnd();
            abilityList[0] = ability;
        } else if (abilityList[1] is AbilityNone_0 || abilityList[1].GetType() == ability.GetType())
        {
            backup = abilityList[1];
            //abilityList[1].OnEnd();
            abilityList[1] = ability;
        } else if (abilityList[2] is AbilityNone_0 || abilityList[2].GetType() == ability.GetType())
        {
            backup = abilityList[2];
            //abilityList[2].OnEnd();
            abilityList[2] = ability;
        } else if (abilityList[3] is AbilityNone_0 || abilityList[3].GetType() == ability.GetType())
        {
            
            backup = abilityList[3];
            //abilityList[3].OnEnd();
            abilityList[3] = ability;
            if (classId == 6)
            {
                ability.ChangeRarity(1);
                ability.ChangeRarity(1);
                if (backup.Rarity == Rarities.MYTHIC)
                {
                    backup.ChangeRarity(-1);
                }
                else
                {
                    backup.ChangeRarity(-1);
                    backup.ChangeRarity(-1);
                }
                
            }
            else
            {
                ability.ChangeRarity(1);
                backup.ChangeRarity(-1);
            }
        }
        else //WIP
        {
            /*OpenPopUp();
            int abilitySlot = -1;
            while (abilitySlot == -1)
            {
                abilitySlot = await EventManager();
            }*/
            abilityList[0].OnEnd();
            backup = abilityList[0];
            abilityList[0] = ability;
            //ClosePopUp();
        }
        return backup;
    }
    
    private string GetAbilityState(Ability ability) //used for HUD display
    {
        string res = ability.Name + $" {ability.Rarity}";
        if (ability.State == States.READY)
        { 
            res += " : Ready";
        } else if (ability.State == States.COOLDOWN)
        { 
            res += $" : {Math.Round(ability.CurrentCooldown, 2)}";
        }
        else if (ability.State == States.PASSIVE && ability.CurrentCooldown >0)
        {
            res += $" : {Math.Round(ability.CurrentCooldown, 2)}";
        }
        return res;
    }

    private string GetBuffList() //used for HUD display
    {
        string res = "";
        foreach (var buff in buffList)
        {
            if (!buff.permanent)
            {
                if (buff.timer <= 0)
                {
                    if (buff.Duration > 0)
                    {
                        res += buff.Name + $" - {Math.Round((float)buff.Duration,2)}" + "\n";
                    }
                    else
                    {
                        res += buff.Name + "\n";
                    }
                }
                else
                {
                    res += buff.Name + $" - {Math.Round(buff.timer,2)}" + $" - {Math.Round((float)buff.Duration,2)}" + "\n";
                }
            }
            else
            {
                res += buff.Name + $" - {buff.timer}" + "\n";
            }
            
        }

        return res;
    }
    
    private void UpdateHUD() //used for HUD display
    {
        statsValue = $" Health : {health} / {maxHealth}\n Attack Damage : {attackDamage}\n Ability Power : {abilityPower}\n Armor : {armor}\n Magic Resist : {magicResist}\n Movement Speed : {movementSpeed}\n Movement Speed% : {moveSpeed}\n Attack Speed : {attackSpeed}\n Lifesteal% : {lifesteal}\n Heal Power% : {healingPower}";
        statsHUD2.text = statsValue;
        abilitiesValue = $" Key 1 - {GetAbilityState(abilityList[0])}\n Key 2 - {GetAbilityState(abilityList[1])}\n Key 3 - {GetAbilityState(abilityList[2])}\n Key 4 - {GetAbilityState(abilityList[3])}\n\n Class - {GetAbilityState(classPassive)}";
        abilitiesValue += $"\n \n {primaryWeapon.Name} - {primaryWeapon.Rarity}";
        abilitiesValue += $"\n Gem Count : {gemList.Count}";
        abilitiesHUD2.text = abilitiesValue;
        buffsValue = $"\n \n{GetBuffList()}";
        buffsHUD2.text = buffsValue;
    }
    
    private void HandleMovement()
    {
        if (inEvent || !canMove)
        {
            return;
        }
        // check if the player is owned by the local player
        if (!isOwned) { return; }

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        Vector3 moveBy = body.transform.forward * x + body.transform.right * z;

        // If the player is not moving, stop the player else let the player move in the direction of the input x and z
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Stopping");
            playerRigidbody.velocity = Vector3.zero;
        }
        else
        {
            var position = body.transform.position;
            playerRigidbody.MovePosition(position + moveBy.normalized * ((movementSpeed * Time.fixedDeltaTime) * moveSpeed));
        }
    }
    
    private void Aim()
    {
        if (!isOwned) { return; }
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Vector3 lookRotation = new Vector3(hit.point.x, model.transform.position.y, hit.point.z);
            model.transform.LookAt(lookRotation);
        }
    }
}
