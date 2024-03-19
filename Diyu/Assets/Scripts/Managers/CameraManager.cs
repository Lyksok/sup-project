using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    public CinemachineVirtualCamera CmVirtualCamera;
    public Camera mainCamera;

    private bool vCam = true;
    // Creates a toggle for camera lock (like in LoL) on the "Y" key
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            vCam = !vCam;

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

            if (x < 10)
            {
                mainCamera.transform.position -= Vector3.back * (Time.deltaTime * 25);
            } else if (x > Screen.width - 10)
            {
                mainCamera.transform.position -= Vector3.forward * (Time.deltaTime * 25);
            }
            
            if (y < 10)
            {
                mainCamera.transform.position -= Vector3.right * (Time.deltaTime * 25);
            } else if (y > Screen.height - 10)
            {
                mainCamera.transform.position -= Vector3.left * (Time.deltaTime * 25);
            }
        }
    }
}
