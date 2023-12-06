using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private Transform playerBody = null;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CharacterController characterController = null;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float movementSpeed = 5.0f;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponentInChildren<CharacterController>();
    }

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float z = Input.GetAxis("Horizontal");
            float x = Input.GetAxis("Vertical");

            // Utilisez seulement les axes horizontaux (x et z) pour le mouvement
            Vector3 moveBy = new Vector3(x, 0f, -z);

            // Normalize seulement les composantes horizontales
            moveBy.Normalize();

            // Transforme le vecteur de mouvement de l'espace local du joueur à l'espace mondial
            moveBy = transform.TransformDirection(moveBy);

            // Applique le mouvement au CharacterController
            characterController.Move(moveBy * movementSpeed * Time.deltaTime);
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - playerBody.position;
            direction.y = 0;
            playerBody.forward = direction;
        }
    }

    void Update()
    {
        HandleMovement();
        Aim();
    }
}
