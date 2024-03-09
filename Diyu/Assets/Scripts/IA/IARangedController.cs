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
    private Transform eyeTransform = null;

    void FixedUpdate()
    {
        timeBetweenShots += Time.deltaTime;
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
        float distanceWithEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        if (distanceWithEnemy <= 17)
        {
            if (CanSeeObject(enemy))
            {
                transform.LookAt(enemy.transform);
                ai.SetDestination(transform.position);
                Debug.LogError("Close");
                if (timeBetweenShots >= shootCD)
                {
                    firespell.Fire();
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
    private void OnEnemyLeft(GameObject enemy)
    {
        //when player not in sightzone -> return to spawn
        ai.SetDestination(spawn.transform.position);
    }

    private void Die()
    {
        StartCoroutine(DestroAIRoutine());
    }

    IEnumerator DestroAIRoutine()
    {
        yield return new WaitForSeconds(0.0f);

        //Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
