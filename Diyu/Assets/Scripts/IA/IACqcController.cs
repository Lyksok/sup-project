using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AiCqcController : MonoBehaviour
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
    private float HealTime = 0.0f;

    [SerializeField]
    private NavMeshAgent ai = null;
    //sphere collider with trigger on
    [SerializeField]
    public SightZone sightZone = null;

    [SerializeField]
    private Life life = null;

    [SerializeField]
    public GameObject Redkey = null;

    [SerializeField]
    public bool Spotted = false;

    [SerializeField]
    public bool AtSpawn = true;
    public Animator animator;

    public LayerMask projectileMask;

    void FixedUpdate()
    {

        TimeBetweenAttacks += Time.deltaTime;
        float distanceWithSpawn = Vector3.Distance(transform.position, spawn.transform.position);
        if (!Spotted)
        {
            HealTime += Time.deltaTime;
            if (distanceWithSpawn < 3)
            {
                AtSpawn = true;
            }
        }
        else
        {
            AtSpawn = false;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        //had to put it in parent to make the sightzone still
        sightZone = GetComponentInParent<SightZone>();
        sightZone.onStay += OnEnemySpotted;
        sightZone.onExit += OnEnemyLeft;
        life.onEmpty += Die;
    }

    void Update()
    {



        if (AtSpawn)
        {
            animator.SetBool("IsRunning", false);
        }
        if (Spotted)
        {
            Debug.Log("running");
            animator.SetBool("IsRunning", true);
        }
        
        
        
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
        };
        return false;
    }
    void OnEnemySpotted(GameObject enemy)
    {
        if (CanSeeObject(enemy))
        {
            Spotted = true;
            //ai follows player until it leaves
            ai.SetDestination(enemy.transform.position);
            Life life = enemy.gameObject.GetComponent<Life>();
            float distanceWithEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceWithEnemy <= 3 && TimeBetweenAttacks >= AttackCD)
            {
                
                animator.SetBool("IsRunning", false);
                Debug.Log("attack");
                animator.SetBool("IsAttacking", true);
                life.ChangeHP(-1.0f);
                TimeBetweenAttacks = 0.0f;
            }
            else
            { 
                animator.SetBool("IsAttacking", false);
            }
        }
        else
        {
            ai.SetDestination(spawn.transform.position);
            if (!CanSeeObject(enemy))
            {
                Spotted = false;
                if (HealTime >= 0.5)
                {
                    life.ChangeHP(1.0f);
                    HealTime = 0;
                }
            }
        }

    }
    private void OnEnemyLeft(GameObject enemy)
    {
        //when player not in sightzone -> return to spawn
        ai.SetDestination(spawn.transform.position);
        Spotted = false;
        if (HealTime >= 0.5)
        {
            life.ChangeHP(1.0f);
            HealTime = 0;
        }
    }

    private void Die()
    {
        Debug.Log("matteo");
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);
        ParticleSystem particleSystem = Instantiate(ded, transform.position, transform.rotation);
        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        Instantiate(Redkey, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}