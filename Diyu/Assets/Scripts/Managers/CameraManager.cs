using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public CinemachineVirtualCamera CmVirtualCamera;
    public Camera mainCamera;
    public GameObject defaultPosition;
    //public GameObject body;
    private bool vCam = true;

    public NewPlayer pb;
    // Creates a toggle for camera lock (like in LoL) on the "Y" key
    // Update is called once per frame

    private void Start()
    {
        // Find the player's script
        //pb = defaultPosition.GetComponent<NewPlayer>();
    }

    void Update()
    {
        //Debug.LogError(pb.isOwned);
        if (!pb.isOwned) { return; }
        if (Input.GetKeyDown(KeyCode.Y) || pb.isSpectator)
        {
            if (pb.isSpectator)
            {
                vCam = false;
            }
            else
            {
                vCam = !vCam;
            }
            if (vCam)
            {
                CmVirtualCamera.gameObject.SetActive(true);
                //mainCamera.gameObject.SetActive(false);
            }
            else
            {
                CmVirtualCamera.gameObject.SetActive(false);
                //mainCamera.gameObject.SetActive(true);
            }
        }

        if (!vCam)
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if (y < 30)
            {
                mainCamera.transform.position -= Vector3.forward * (Time.deltaTime * 25);
            } else if (y > Screen.height - 30)
            {
                mainCamera.transform.position -= Vector3.back * (Time.deltaTime * 25);
            }
            
            if (x < 30)
            {
                mainCamera.transform.position -= Vector3.right * (Time.deltaTime * 25);
            } else if (x > Screen.width - 30)
            {
                mainCamera.transform.position -= Vector3.left * (Time.deltaTime * 25);
            }
        }
        else
        {
            mainCamera.transform.position = defaultPosition.transform.position;
        }
    }
}
