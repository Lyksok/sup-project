using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Abilities;
using Buffs;
using Cinemachine;
using Entities;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weapons;
using Random = System.Random;

public class NewPlayer : Entity
{
    [Header("Player characteristics")] 
    public Rigidbody playerRigidbody;
    public GameObject body;  //part of the player that moves
    public GameObject model;  //part of the player that turns
    public Camera playerCamera;
    public CinemachineVirtualCamera playerVirtualCamera;
    public Canvas playerHUD;
    public float movementSpeed = 5f;
    private LayerMask layerMask;
    [SyncVar] public Vector3 pos;
    [SyncVar] public Quaternion rot;

    public GameObject statsHUD;
    private string statsValue;
    private TextMeshProUGUI statsHUD2;
    
    public GameObject abilitiesHUD;
    private string abilitiesValue;
    private TextMeshProUGUI abilitiesHUD2;
    
    public GameObject buffsHUD;
    private string buffsValue;
    private TextMeshProUGUI buffsHUD2;
    
    private void Start()
    {
        statsHUD2 = statsHUD.GetComponent<TextMeshProUGUI>();
        buffsHUD2 = buffsHUD.GetComponent<TextMeshProUGUI>();
        abilitiesHUD2 = abilitiesHUD.GetComponent<TextMeshProUGUI>();
        layerMask = LayerMask.GetMask("groundMask");
        buffList = new List<Buff>();
        debuffList = new List<Buff>();
        //abilityList[0] = new AbilityRegen_1(Rarities.COMMON, this);
        //abilityList[1] = new AbilityHeal_2(Rarities.LEGENDARY, this);
        //abilityList[2] = new AbilityExplosion_3(Rarities.LEGENDARY, this);
        primaryWeapon = new Firespell(Rarities.RARE,this);
        health = 20;
        maxHealth = 100;
        pos = body.transform.position;
        rot = model.transform.rotation;
        
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
            playerHUD.gameObject.SetActive(false);
        }
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
            DebugPickup();
            //SrvMovement();
        }
    }


    public void DebugPickup() //gives the player a random ability
    {
        if (Input.GetKeyDown(KeyCode.R) && isLocalPlayer)
        {
            int rank = RandomNumberGenerator.GetInt32(0, 5);
            int id = RandomNumberGenerator.GetInt32(1, 4);
            Ability ability = resources.GetAbility(id, resources.GetRarity(rank), this);
            PickupAbility(ability);
            //Debug.LogError($"{ability.Rarity} {resources.GetRarity(rank)}");
        }
    }
    
    public void PickupAbility(Ability ability)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (abilityList[0] is AbilityNone_0)
        {
            abilityList[0] = ability;
        } else if (abilityList[1] is AbilityNone_0)
        {
            abilityList[1] = ability;
        } else if (abilityList[2] is AbilityNone_0)
        {
            abilityList[2] = ability;
        } else if (abilityList[3] is AbilityNone_0)
        {
            abilityList[3] = ability;
            ability.ChangeRarity(1);
        }
        else //WIP
        {
            abilityList[0].OnEnd();
            abilityList[0] = ability;
        }
    }
    
    private string GetAbilityState(Ability ability)
    {
        string res = ability.Name + $"{ ability.Rarity}";
        if (ability.State == States.READY)
        { 
            res += " : Ready";
        } else if (ability.State == States.COOLDOWN)
        { 
            res += $" : {Math.Round(ability.CurrentCooldown, 2)}";
        }
        return res;
    }

    private string GetBuffList()
    {
        string res = "";
        foreach (var buff in buffList)
        {
            res += buff.Name + $" {buff.timer}" + "\n";
        }

        return res;
    }
    
    private void UpdateHUD()
    {
        statsValue = $" Health : {health} / {maxHealth}\n Attack Damage : {attackDamage}\n Ability Power : {abilityPower}\n Armor : {armor}\n Magic Resist : {magicResist}\n Movement Speed : {moveSpeed}\n Attack Speed : {attackSpeed}\n Lifesteal% : {lifesteal}\n Cooldown Reduction% : {cooldownReduction}\n Tenacity% : {tenacity}";
        statsHUD2.text = statsValue;
        abilitiesValue = $" Key 1 - {GetAbilityState(abilityList[0])}\n Key 2 - {GetAbilityState(abilityList[1])}\n Key 3 - {GetAbilityState(abilityList[2])}\n Key 4 - {GetAbilityState(abilityList[3])}";
        abilitiesHUD2.text = abilitiesValue;
        buffsValue = $"{GetBuffList()}";
        buffsHUD2.text = buffsValue;
    }
    
    private void HandleMovement()
    {
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
