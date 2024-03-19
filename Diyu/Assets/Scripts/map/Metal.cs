using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Metal : MonoBehaviour
{
    public float Vie = 3;
    public UnityEvent unityEvent = new UnityEvent();
    public GameObject metal;
    [SerializeField] public SightZone sketuve = null;
   

    // Start is called before the first frame update
    void Start()
    {
        metal = this.gameObject;
        sketuve = GetComponentInChildren<SightZone>();
        sketuve.onStay += Open;
        sketuve.onEnter += Open;
    }
    void Open(GameObject player)
    {
        PlayerBody pb = player.gameObject.GetComponent<PlayerBody>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            pb.metal+=1;
            Vie -=1;
            Debug.Log("caillou");
            if (Vie == 0)
                Destroy(metal);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
