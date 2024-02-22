using UnityEngine;
using Mirror;

public class PlayerBody : NetworkBehaviour
{
    // General Unity serial fields
    [SerializeField] private Transform playerBody;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform targetRay;
    [SerializeField] private float movementSpeed = 5.0f;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;


    // This method is called when the local player object is set up
    private void Start()
    {

        // check if the player is owned by the local player
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
        else
        {
            rigidBody.isKinematic = false;
        }

        playerCamera = GetComponentInChildren<Camera>();
        initalOffset = transform.position - playerBody.position;
    }

    /*
    void HandleMovement()
    {

        if(!isLocalPlayer) { return; }

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        //we normalize movements by dircube so player can rotate without the "forward" changing
        Vector3 moveBy = transform.forward * x + transform.right * z;

        rigidBody.MovePosition(transform.position + moveBy.normalized * movementSpeed * Time.fixedDeltaTime);
    }
    */

    // Method to handle player movement
    void HandleMovement()
    {
        // check if the player is owned by the local player
        if (!isOwned) { return; }

        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");

        Vector3 moveBy = transform.forward * x + transform.right * z;

        // If the player is not moving, stop the player else let the player move in the direction of the input x and z
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("Stopping");
            rigidBody.velocity = Vector3.zero;
        }
        else
        {
            rigidBody.MovePosition(transform.position + moveBy.normalized * movementSpeed * Time.fixedDeltaTime);
        }
    }

    // Method to handle player aiming
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

    // Method to handle player aiming
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

    // method to update the camera position
    void UpdateCameraPosition()
    {
        //makes cam follow target
        cameraPosition = playerBody.position + initalOffset;
        transform.position = cameraPosition;
    }

    // debug method to draw rays
    void DrawRays()
    {
        //this draws ray from the front
        var ray = new Ray(playerCamera.transform.position, targetRay.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, targetRay.transform.forward * 100.0f, Color.red);
    }

    // Update is called once per frame with a fixed delta time
    void Update()
    {
        HandleMovement();

        // check if the player is owned by the local player
        if (isLocalPlayer)
        {
            Aim();
            UpdateCameraPosition();
            DrawRays();
        }
    }
}
