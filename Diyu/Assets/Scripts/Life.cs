using System;
using System.Collections;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 10.0f;

    [SerializeField]
    private float currentHp = 10.0f;

    public event Action<Life> onChanged = null;
    public event Action onEmpty = null;

    private void Start()
    {
        ResetLife();
    }

    public void ChangeHP (float hpDifference)
    {
        if (IsDead() && hpDifference < 0)
        {
            return;
        }

        currentHp += hpDifference; 

        if (currentHp <= 0.0f)
        {
            onEmpty?.Invoke();
            currentHp = 0.0f;
        }

        onChanged?.Invoke(this);
    }

    public bool IsDead()
    {
        return Mathf.Approximately(currentHp, 0.0f);
    }

    public void ResetLife()
    {
        currentHp = maxHP;
        onChanged?.Invoke(this);
    }
}
