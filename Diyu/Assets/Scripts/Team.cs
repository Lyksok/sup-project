using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    /*
    A team is a group of players that are playing together.
    It has an id, a color, a name and a list of players.
    It contains all the methods to manage the team.
    It also manages entities' team variable.
    */
    // basic attributes declaration
    private int _teamId;
    public int Id { get; private set; }
    private string _teamName;
    public string Name { get; private set; }

    // team color
    private Material _teamMaterial;
    public Material TeamMaterial { get; private set; }
    private List<PlayerInfo> _teamMembers = new List<PlayerInfo>();
    public List<PlayerInfo> Members { get; private set; }

    // check if player is in team
    public bool isInTeam(PlayerInfo playerInfo)
    {
        return _teamMembers.Contains(playerInfo);
    }

    // add or remove player from team
    // return false if player is already in team or cannot be added
    public bool addPlayer(PlayerInfo playerInfo)
    {
        if (_teamMembers.Contains(playerInfo) || playerInfo.Team != null)
        {
            return false;
        }
        else
        {
            _teamMembers.Add(playerInfo);
            playerInfo.SetTeam(this);
            playerInfo.SetTeamMaterial(_teamMaterial);
            return true;
        }
    }
    public bool removePlayer(PlayerInfo playerInfo)
    {
        // Set player's team to default team
        if (_teamMembers.Contains(playerInfo))
        {
            _teamMembers.Remove(playerInfo);
            playerInfo.ResetTeam();
            playerInfo.SetTeamById(0);
            return true;
        }
        else
        {
            return false;
        }
    }

    // constructor for team class object
    public Team(Material colorMaterial, int id, string name = "")
    {
        _teamId = id;
        _teamName = name;
        _teamMaterial = colorMaterial;
    }
}