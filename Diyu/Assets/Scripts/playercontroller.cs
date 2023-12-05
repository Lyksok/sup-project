using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField]
    private bool isdashing = true;

    //normalizes the movements of the player to that of this specific cube in the scene
    [SerializeField]
    private Transform dirCube = null;

    //cooldown of dash
    [SerializeField]
    private float dashCD = 3.0f;

    [SerializeField]
    private float dashTime = 3.0f;

    [SerializeField]
    private Rigidbody rb = null;

    [SerializeField]
    private float MovementSpeed = 1.0f;

    //this is required to make targeting ignore walls and enemies -> smoother
    [SerializeField] 
    private LayerMask groundMask;

    private Camera mainCamera;

    [SerializeField]
    private float SprintMultiplier = 1.5f;

    //these two will raycast -> useful in editor
    [SerializeField]
    private Transform Targ = null;

    [SerializeField]
    private Transform Cam = null;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //this draws ray from the front
        var ray = new Ray(Cam.transform.position, Targ.transform.forward);
        Debug.DrawRay(Cam.transform.position, Targ.transform.forward * 100.0f, Color.red);
        //can't move if dashing
        if (isdashing == false)
        {
            MoveCharacter();
        }
        Aim();
        Dash();
        StopDash();
        //needed otherwise movespeed is dependent on frames -> sucks
        dashTime += Time.deltaTime;
    }

    private void MoveCharacter()
    {
        float x = Input.GetAxis("Vertical");

        float z = Input.GetAxis("Horizontal");

        //we normalize movements by dircube so player can rotate without the "forward" changing
        Vector3 moveBy = dirCube.forward * x + dirCube.right * z;

        //sprint
        float ActualSpeed = MovementSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ActualSpeed *= SprintMultiplier;
        }

        rb.MovePosition(transform.position + moveBy.normalized * ActualSpeed * Time.fixedDeltaTime);
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
            transform.forward = direction;
        }
    }
    private (bool success, Vector3 position) GetMousePosition()
    {
        //this ray gives mouse position 
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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
    private void Dash()
    {
        //if pressing space and can dash -> dash
        if (Input.GetKeyDown(KeyCode.Space) && dashTime >= dashCD)
        {
            isdashing = true;
            rb.AddRelativeForce(0.0f, 0.0f, 50.0f, mode: ForceMode.Impulse);
            dashTime = 0.0f;
        }
    }
    private void StopDash()
    {
        //but if isdashing for a second, stop all momentum
        if (dashTime >= 1.0f && isdashing)
        {
            isdashing = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
