using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float diecounter = 0.0f;

    [SerializeField]
    private float limit = 1.5f;

    [SerializeField]
    private float damage = -1.0f;

    [SerializeField]
    private ParticleSystem ded = null;

    void Start()
    {

    }

    void Update()
    {
        diecounter += Time.deltaTime;
        Die();
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
            ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        Life life = collider.gameObject.GetComponent<Life>();

        if (life != null)
        {
            life.ChangeHP(damage);
            Destroy(gameObject);
        }

        if (!rb && !life)
            return;

        Destroy(gameObject);
    }

    void Die()
    {
        if (diecounter >= limit)
        {
            Debug.LogError("fireball is dead");
            ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}