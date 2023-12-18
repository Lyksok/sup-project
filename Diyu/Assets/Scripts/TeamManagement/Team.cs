using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    // basic attributes declaration
    private int _teamId;
    public int Id { get; private set; }
    private string _teamName;
    public string Name { get; private set; }
    private Color _teamPrivateColor = Color.blue;
    private Color _teamPublicColor;
    public Color PublicColor { get; private set; }
    private List<Player> _teamMembers = new List<Player>();
    public List<Player> Members { get; private set; }

    // check if player is in team
    public isInTeam(Player player)
    {
        return _teamMembers.Contains(player);
    }

    // add or remove player from team
    public addPlayer(Player player)
    {
        _teamMembers.Add(player);
    }
    public removePlayer(Player player)
    {
        _teamMembers.Remove(player);
    }

    // constructor for team class object
    public void Team(Color color, int id, string name)
    {
        _teamId = id;
        _teamName = name;
        _teamPublicColor = color;
    }
}