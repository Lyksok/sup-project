using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    // basic attributes declaration
    private int _teamId;
    public int Id { get; private set; }
    private string _teamName;
    public string Name { get; private set; }

    // team color
    private Color _teamColor;
    public Color TeamColor { get; private set; }
    private List<PlayerIdentity> _teamMembers = new List<PlayerIdentity>();
    public List<PlayerIdentity> Members { get; private set; }

    // check if player is in team
    public bool isInTeam(PlayerIdentity playerIdentity)
    {
        return _teamMembers.Contains(playerIdentity);
    }

    // add or remove player from team
    // return false if player is already in team or cannot be added
    public bool addPlayer(PlayerIdentity playerIdentity)
    {
        if (_teamMembers.Contains(playerIdentity) || playerIdentity.Team != null)
        {
            return false;
        }
        else
        {
            _teamMembers.Add(playerIdentity);
            return true;
        }
    }
    public bool removePlayer(PlayerIdentity playerIdentity)
    {
        if (_teamMembers.Contains(playerIdentity))
        {
            _teamMembers.Remove(playerIdentity);
            playerIdentity.ResetTeam();
            return true;
        }
        else
        {
            return false;
        }
    }

    // constructor for team class object
    public Team(Color color, int id, string name)
    {
        _teamId = id;
        _teamName = name;
        _teamColor = color;
    }
}