using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float diecounter = 0.0f;

    [SerializeField]
    private float limit = 3.0f;

    [SerializeField]
    private float damage = -1.0f;

    void Update()
    {
        diecounter += Time.deltaTime;
    }

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

    void Die()
    {
        if (diecounter >= limit)
        {
            Destroy(gameObject);
        }
    }
}