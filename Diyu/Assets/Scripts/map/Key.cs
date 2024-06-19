using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject bouton;
    // Start is called before the first frame update
    void Start()
    {
        bouton = this.gameObject;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        NewPlayer pb = other.GetComponent<NewPlayer>();
        if (pb != null)
        {
            pb.Keys ++;
            Destroy(bouton);
        }
    }
}