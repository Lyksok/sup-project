using Cinemachine;
using UnityEngine;
using Mirror;

public class PlayerBody : NetworkBehaviour
{
    // General Unity serial fields
    [SerializeField] private Transform playerBody;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] public Rigidbody rigidBody;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform targetRay;
    [SerializeField] private float movementSpeed = 5.0f;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    [SerializeField] public GameObject launcher;
    [SerializeField] private float ShootCD = 0.5f;
    [SerializeField] private float CurrShoot = 0.0f;
    [SerializeField] private AutoFramework AutoAttack;

    [SerializeField]
    private Life life = null;
    [SerializeField] public bool GetRedKey = false;
    [SerializeField] public bool GetGreenKey = false;

    // This method is called when the local player object is set up
    private void Start()
    {
        Life life = GetComponent<Life>();
        CurrShoot = 0.0f;
        // check if the player is owned by the local player
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
        else
        {
            rigidBody.isKinematic = false;
        }
        life.onEmpty += Die;
        //playerCamera = GetComponentInChildren<Camera>();
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
            //Debug.Log("Stopping");
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
    public Vector3 Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            //calculate the direction
            var direction = position - transform.position;

            direction.y = 0;

            //cake the transform of player look in the direction.
            targetRay.forward = direction;
            return direction;
        }

        return Vector3.zero;
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
            CurrShoot = CurrShoot + Time.deltaTime;
            Aim();
            Attack();
            //UpdateCameraPosition();
            DrawRays();
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CurrShoot >= ShootCD)
            {
                if (life.IsDead())
                {
                    return;
                }
                else
                {
                    AutoAttack.Attack();
                    CurrShoot = 0.0f;
                }
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
