using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RedKey : MonoBehaviour
{
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject bouton;
    // Start is called before the first frame update
    void Start()
    {
        bouton = this.gameObject;
        
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        PlayerBody pb = other.GetComponent<PlayerBody>();
        if (pb != null)
        {
            pb.GetRedKey = true;
            Destroy(gameObject);
        }
    }
}
/*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                if()
                Debug.Log("boutton !");
            } 
        }
        */