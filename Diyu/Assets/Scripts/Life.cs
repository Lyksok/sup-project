using System;
using System.Collections;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField]
    public float maxHP = 10.0f;

    [SerializeField]
    public float currentHp = 10.0f;

    [SerializeField]
    private ParticleSystem damage = null;

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

        if (hpDifference < 0)
        {
            ParticleSystem particleSystem = Instantiate(damage, transform.position, transform.rotation);
        }
        if (hpDifference > 0)
        {

        }

        currentHp += hpDifference; 

        if (currentHp <= 0.0f)
        {
            onEmpty?.Invoke();
            currentHp = 0.0f;
        }
        if (currentHp > maxHP)
        {
            currentHp = maxHP;
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
