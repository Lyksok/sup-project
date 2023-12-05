using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //we target our player
    [SerializeField]
    public Transform targetObject;
    //initial offset the diff between cam and target's position
    private Vector3 initalOffset;

    private Vector3 cameraPosition;

    void Start()
    {
        //initial offset the diff between cam and target's position
        initalOffset = transform.position - targetObject.position;
    }

    //could put this in fixed update for + perfs but smoother in update
    void Update()
    {
        //makes cam follow target
        cameraPosition = targetObject.position + initalOffset;
        transform.position = cameraPosition;
    }
}
