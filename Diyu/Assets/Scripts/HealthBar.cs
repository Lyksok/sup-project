using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Entity body;
    public GameObject model; 
    public Canvas cv;
    public Slider hp; 
    private bool _isBodyNotNull;

    //[Command]
    private void Start()
    {
        _isBodyNotNull = body != null;
    }

    //[Command]
    private void Update()
    {
        hp.value = body.health;
        hp.maxValue = body.maxHealth;

        if (_isBodyNotNull)
        {
            var transform1 = cv.transform;
            transform1.position = new Vector3(model.transform.position.x,transform1.position.y,model.transform.position.z);
        }
    }
}
