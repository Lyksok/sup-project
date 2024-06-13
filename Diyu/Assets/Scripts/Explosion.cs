using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float lifespan;
    
    // Start is called before the first frame update
    void Start()
    {
        lifespan = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan += Time.deltaTime;
        if (lifespan > 0.75f)
        {
            Destroy(gameObject);
        }
    }
}
