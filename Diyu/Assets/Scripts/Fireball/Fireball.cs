using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField]
    private float damage = -1.0f;

    private void OnTriggerEnter(Collider collider)
    {
        Rigidbody rb = collider.GetComponent<Rigidbody>();
        if (rb)
        {
            //rien
        }

        if (collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        Entity Entity = collider.gameObject.GetComponent<Entity>();

        if (Entity.Health != null)
        {
            Entity.AddHealth(damage);
        }

        if (!rb && !Entity)
            return;

        Destroy(gameObject);
    }

}