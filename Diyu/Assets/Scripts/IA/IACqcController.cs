using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Entities;
using Random = UnityEngine.Random;

public class AiCqcController : Entity
{
    //where ai will return without target
    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private ParticleSystem ded = null;

    [SerializeField]
    private float AttackCD = 1.0f;

    [SerializeField]
    private float TimeBetweenAttacks = 0.0f;

    [SerializeField]
    private NavMeshAgent ai = null;
    //sphere collider with trigger on
    [SerializeField]
    public SightZone sightZone = null;

    [SerializeField]
    public GameObject Redkey = null;

    void FixedUpdate()
    {
        TimeBetweenAttacks += Time.deltaTime;
    }

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        //had to put it in parent to make the sightzone still
        sightZone = GetComponentInParent<SightZone>();
        sightZone.onStay += OnEnemySpotted;
        sightZone.onExit += OnEnemyLeft;
        health = 50;
    }

    private void Update()
    {
        if (health <= 0) { Die();}
    }

    private bool CanSeeObject(GameObject go)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, go.transform.position - (transform).position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        else if (Physics.Raycast(transform.position, (go.transform.position + new Vector3(0.0f, 1.0f, 0.0f)) - transform.position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        return false;
    }
    private void OnEnemySpotted(GameObject enemy)
    {
        if (CanSeeObject(enemy))
        {
            //ai follows player until it leaves
            ai.SetDestination(enemy.transform.position);
            Life life = enemy.gameObject.GetComponent<Life>();
            float distanceWithEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceWithEnemy <= 3 && TimeBetweenAttacks >= AttackCD)
            {
                Debug.LogError("ATTAAAAAAAAAAAAAAAACK");
                life.ChangeHP(-1.0f);
                TimeBetweenAttacks = 0.0f;
            }
        }
        else
            ai.SetDestination(spawn.transform.position);

    }
    private void OnEnemyLeft(GameObject enemy)
    {
        //when player not in sightzone -> return to spawn
        ai.SetDestination(spawn.transform.position);
        Heal(1000);
    }

    private void Die()
    {
        //Debug.Log("matteofdp");
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);
        ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        Instantiate(Redkey, transform.position, Quaternion.identity);
        OnDeath();
    }
}
