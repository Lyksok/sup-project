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

        if (collider.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }

        Life life = collider.gameObject.GetComponent<Life>();

        if (life != null)
        {
            life.ChangeHP(damage);
        }

        if (!rb && !life)
            return;

        Destroy(gameObject);
    }

}