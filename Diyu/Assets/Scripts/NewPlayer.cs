using System;
using System.Collections.Generic;
using Abilities;
using Buffs;
using Entities;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

public class NewPlayer : Entity
{
    [Header("Player characteristics")] 
    public Rigidbody playerRigidbody;
    public GameObject body;  //part of the player that moves
    public GameObject model;  //part of the player that turns
    public Camera playerCamera;
    public float movementSpeed = 5f;
    private LayerMask layerMask;
    [SyncVar] public Vector3 pos;
    [SyncVar] public Quaternion rot;

    private void Start()
    {
        //playerRigidbody = transform.GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("groundMask");
        buffList = new List<Buff>();
        debuffList = new List<Buff>();
        abilityList[0] = new AbilityRegen_1(Rarities.COMMON, this);
        abilityList[1] = new AbilityHeal_2(Rarities.LEGENDARY, this);
        abilityList[2] = new AbilityExplosion_3(Rarities.LEGENDARY, this);
        primaryWeapon = new Firespell(Rarities.RARE,this);
        health = 20;
        maxHealth = 100;
        pos = body.transform.position;
        rot = model.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isFocused)
            return;

        if (isLocalPlayer)
        {
            HandleMovement();
            Aim();
            HandleBuffs();
            HandleDebuffs();
            HandleAttack(primaryWeapon,primaryWeaponAttackKey);
            HandleAbility(abilityList[0],KeyCode.Alpha1);
            HandleAbility(abilityList[1],KeyCode.Alpha2);
            HandleAbility(abilityList[2],KeyCode.Alpha3);
            //SrvMovement();
        }
    }

    [Command(requiresAuthority = false)]
    private void SrvMovement(Vector3 movement)
    {
        playerRigidbody.MovePosition(movement);
    }
    
    [Command(requiresAuthority = false)]
    private void SrvStopMovement()
    {
        playerRigidbody.velocity = Vector3.zero;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    //[Command(requiresAuthority = false)]
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
            SrvStopMovement();
            playerRigidbody.velocity = Vector3.zero;
        }
        else
        {
            var position = body.transform.position;
            SrvMovement(position + moveBy.normalized * ((movementSpeed * Time.fixedDeltaTime) * moveSpeed));
            playerRigidbody.MovePosition(position + moveBy.normalized * ((movementSpeed * Time.fixedDeltaTime) * moveSpeed));
        }
    }
    
    //[Command(requiresAuthority = false)]
    private void Aim()
    {
        if (!isOwned) { return; }
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Vector3 lookRotation = new Vector3(hit.point.x, model.transform.position.y, hit.point.z);
            model.transform.LookAt(lookRotation);
        }
    }
}
