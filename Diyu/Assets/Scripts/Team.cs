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
    public int Id { get;  set; } // Set in constructor
    public string Name { get; set; } // Set in constructor

    // team color
    public Material TeamMaterial { get;  set; } // Set in constructor
    public List<Player> Members { get;  set; } // Initialized in constructor

    // check if player is in team
    public bool isInTeam(Player playerInfo)
    {
        return Members.Contains(playerInfo);
    }

    // add or remove player from team
    // return false if player is already in team or cannot be added
    public bool AddPlayer(Player player)
    {
        if (Members.Contains(player) || player.Team != null)
        {
            return false;
        }
        else
        {
            Members.Add(player);
            player.Identity.SetTeam(this);
            player.Identity.SetTeamMaterial(TeamMaterial);
            return true;
        }
    }
    public bool RemovePlayer(Player player)
    {
        // Set player's team to default team
        if (Members.Contains(player))
        {
            Members.Remove(player);
            player.Identity.ResetTeam();
            player.Identity.SetTeamById(0);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool SetTeamName(string newName)
    {
        Name = newName;
        return true;
    }

    // constructor for team class object
    public Team(Material colorMaterial, int id, string name = "")
    {
        Id = id;
        Name = name;
        TeamMaterial = colorMaterial;
        Members = new List<Player>();
    }
}