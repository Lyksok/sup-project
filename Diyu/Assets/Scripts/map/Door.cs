using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Door : MonoBehaviour
{
    
    public float speed = 3;
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject door_ ;
    public float smoothTime = 0.5f;
    public bool key = false;
    [SerializeField] public SightZone sketuve;
    public bool boul = false;

    // Start is called before the first frame update
    void Start()
    {
        sketuve.onStay += Open;
        sketuve.onEnter += Open;
    }
    void Open(GameObject player)
    {
        NewPlayer pb = player.gameObject.GetComponent<NewPlayer>();
        if (pb.Keys>0 && Input.GetKeyDown(KeyCode.E))
        { 
            pb.Keys--;
           door_.transform.Translate(-1 * transform.up * speed * Time.deltaTime);
           Debug.Log("test");
        key = true;
        boul = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boul)
        {
            door_.transform.Translate(transform.up * (-1 * speed * Time.deltaTime));
        }
    }
}

