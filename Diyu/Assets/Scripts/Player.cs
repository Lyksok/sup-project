using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform targetRay;

    [SerializeField] private float movementSpeed = 5.0f;

    private Vector3 initalOffset;

    private Vector3 cameraPosition;

    //[SerializeField] private Vector3 cameraOffset = new Vector3(0f, 2f, -5f);

    private void Start()
    {
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
            playerCamera = GetComponentInChildren<Camera>();
            initalOffset = transform.position - playerBody.position;
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        //we normalize movements by dircube so player can rotate without the "forward" changing
        Vector3 moveBy = transform.forward * x + transform.right * z;

        rigidBody.MovePosition(transform.position + moveBy.normalized * movementSpeed * Time.fixedDeltaTime);
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        //this ray gives mouse position 
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            //the Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            //the Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            //calculate the direction
            var direction = position - transform.position;

            direction.y = 0;

            //cake the transform of player look in the direction.
            targetRay.forward = direction;
        }
    }

    void UpdateCameraPosition()
    {
        //makes cam follow target
        cameraPosition = playerBody.position + initalOffset;
        transform.position = cameraPosition;
    }

    void DrawRays()
    {
        //this draws ray from the front
        var ray = new Ray(playerCamera.transform.position, targetRay.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, targetRay.transform.forward * 100.0f, Color.red);
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            HandleMovement();
            Aim();
            UpdateCameraPosition();
            DrawRays();
        }
    }
}
