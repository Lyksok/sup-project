using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject body;
    public Canvas cv;
    public Slider HP;
    public Life playerHP;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        HP.value = playerHP.currentHp;
        HP.maxValue = playerHP.maxHP;
        if (playerHP.IsDead())
        {
            HP.gameObject.SetActive(false);
        }

        if (body != null)
        {
            cv.transform.position = body.transform.position;
        }
    }
}
