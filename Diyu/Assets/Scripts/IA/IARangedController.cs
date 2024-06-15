using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AiRangedController : MonoBehaviour
{
    //where ai will return without target
    [SerializeField]
    private Transform spawn = null;

    [SerializeField]
    private NavMeshAgent ai = null;
    //sphere collider with trigger on
    [SerializeField]
    public SightZone sightZone = null;

    [SerializeField]
    private Life life = null;

    [SerializeField]
    private Firespell firespell = null;

    [SerializeField]
    private float shootCD = 1.0f;

    [SerializeField]
    private float timeBetweenShots = 0.0f;

    [SerializeField]
    private float HealTime = 0.0f;

    [SerializeField]
    private Transform eyeTransform = null;

    [SerializeField]
    public GameObject Greenkey = null;

    [SerializeField]
    public bool Spotted = false;

    [SerializeField]
    public bool AtSpawn = true;

    public LayerMask projectileMask;

    void FixedUpdate()
    {
        timeBetweenShots += Time.deltaTime;
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
        ai = GetComponent<NavMeshAgent>();
        //had to put it in parent to make the sightzone still
        sightZone = GetComponentInParent<SightZone>();
        sightZone.onStay += OnEnemySpotted;
        sightZone.onExit += OnEnemyLeft;
        life.onEmpty += Die;
        firespell = GetComponentInChildren<Firespell>();
    }

    private bool CanSeeObject(GameObject go)
    {
        RaycastHit hit;

        if (Physics.Raycast(eyeTransform.position, go.transform.position - eyeTransform.position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        else if (Physics.Raycast(eyeTransform.position, (go.transform.position + new Vector3(0.0f, 1.0f, 0.0f)) - eyeTransform.position, out hit))
        {
            return hit.collider.gameObject == go;
        }
        return false;
    }

    private void OnEnemySpotted(GameObject enemy)
    {
        if (CanSeeObject(enemy))
        {
            Spotted = true;
            float distanceWithEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceWithEnemy <= 10)
            {
                if (CanSeeObject(enemy))
                {
                    transform.LookAt(enemy.transform);
                    ai.SetDestination(transform.position);
                    Debug.LogError("Close");
                    if (timeBetweenShots >= shootCD)
                    {
                        firespell.Attack();
                        timeBetweenShots = 0.0f;
                    }
                }
            }
            if (distanceWithEnemy > 10)
            {
                //ai follows player until it leaves
                ai.SetDestination(enemy.transform.position);
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
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);

        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        Instantiate(Greenkey, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}