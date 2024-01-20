using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Entity
{
    /*
    An entity is a game object that has some 
    attributs that are common to all game objects
    */

    private Team _team = null;
    public Team Team { get; private set; }
    private int _health;
    public int Health { get; private set; }

    private string _type;
    public string Type { get; private set; }

    // Class methods to set entity health and team
    public void SetHealth(int health)
    {
        _health = health;
    }
    public void SetTeam(Team team)
    {
        _team = team;
    }

    // Set empty team 
    public void ResetTeam()
    {
        _team = null;
    }

}