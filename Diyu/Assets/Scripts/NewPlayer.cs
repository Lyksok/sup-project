using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

public class NewPlayer : NetworkBehaviour
{
    [Header("Weapons")] 
    // public GameObject weaponHolder;
    public GameObject primaryWeapon;
    public KeyCode primaryWeaponAttackKey = KeyCode.Mouse0;
    public GameObject secondaryWeapon;
    public KeyCode secondaryWeaponAttackKey = KeyCode.Mouse1;

    [Header("Health")] [SyncVar] public float health;

    [Header("Player characteristics")] 
    public Rigidbody playerRigidbody;
    public GameObject targetDirection;

    public Camera playerCamera;
    public float movementSpeed = 5f;
    private LayerMask layerMask;

    private void Start()
    {
        playerRigidbody = transform.GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("groundMask");
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
            HandleAttack();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void HandleAttack()
    {
        if (Input.GetKeyDown(primaryWeaponAttackKey))
        {
            primaryWeapon.GetComponent<Weapon>().CmdAttack(targetDirection.transform);
        }
    }
    private void HandleMovement()
    {
        // check if the player is owned by the local player
        if (!isOwned) { return; }

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        Vector3 moveBy = transform.forward * x + transform.right * z;

        // If the player is not moving, stop the player else let the player move in the direction of the input x and z
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Stopping");
            playerRigidbody.velocity = Vector3.zero;
        }
        else
        {
            playerRigidbody.MovePosition(transform.position + moveBy.normalized * (movementSpeed * Time.fixedDeltaTime));
        }
    }
    
    private void Aim()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);
            Vector3 lookRotation = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
            targetDirection.transform.LookAt(lookRotation);
        }
    }

    private Vector3? AimAt()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);
            return new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
        return null;
    }
}
