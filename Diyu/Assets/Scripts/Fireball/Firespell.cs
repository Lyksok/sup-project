using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firespell : MonoBehaviour
{
    [SerializeField]
    private GameObject Fireballprefab = null;

    [SerializeField]
    private ParticleSystem Firelaunch = null;

    [SerializeField]
    private Transform SpawnTransform = null;

    [SerializeField]
    private float FireSpeed = 50.0f;

    private void Update()
    {

    }

    public void Fire()
    {
        GameObject NewFireball = Instantiate(Fireballprefab, SpawnTransform.position, Quaternion.identity);

        Rigidbody rb = NewFireball.GetComponent<Rigidbody>();

        rb.AddForce(FireSpeed * SpawnTransform.forward, ForceMode.VelocityChange);

        ParticleSystem particleSystem = Instantiate(Firelaunch, SpawnTransform.position, SpawnTransform.rotation);
    }
}