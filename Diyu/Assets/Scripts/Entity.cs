using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    /*
    An entity is a game object that has some 
    attributs that are common to all game objects
    It manages the health and team of the game object
    while storing the type of the game object
    */

    public Entity(TypesEnum type, int health)
    {
        Type = type;
        Health = health;
        MaxHealth = health;
    }

    public float Health { get; set; }

    public float MaxHealth { get; set; }

    public TypesEnum Type { get; }

    public event Action<Entity> onChanged = null;
    public event Action onEmpty = null;

    public void AddHealth(float value)
    {
        if (IsDead() && value < 0)
        {
            return;
        }
        Health += value;

        if (value < 0)
        {
            //emit damage particle
        }
        if (value > 0)
        {
            //emit healing particle
        }

        onChanged?.Invoke(this);
    }

    public bool IsDead()
    {
        return Mathf.Approximately(Health, 0.0f);
    }

    public abstract void Die();
    //this behaviour will change depending on wanted interactions
}