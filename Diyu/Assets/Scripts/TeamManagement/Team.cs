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
    private List<Player> _teamMembers = new List<Player>();
    public List<Player> Members { get; private set; }

    // check if player is in team
    public bool isInTeam(Player player)
    {
        return _teamMembers.Contains(player);
    }

    // add or remove player from team
    // return false if player is already in team or cannot be added
    public bool addPlayer(Player player)
    {
        if (_teamMembers.Contains(player) || player.Team != null)
        {
            return false;
        }
        else
        {
            _teamMembers.Add(player);
            return true;
        }
    }
    public bool removePlayer(Player player)
    {
        if (_teamMembers.Contains(player))
        {
            _teamMembers.Remove(player);
            player.ResetTeam();
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