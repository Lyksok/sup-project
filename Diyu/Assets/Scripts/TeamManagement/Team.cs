using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private int _teamId;
    public int Id { get; private set; }

    private string _teamName;
    public string Name { get; private set; }

    private Color _teamPrivateColor = Color.blue;

    private Color _teamPublicColor;
    public Color PublicColor { get; private set; }

    private List<Player> _teamMembers = new List<Player>();
    public List<Player> Members { get; private set; }

    /*
    public void Team(Color color, int id, string name)
    {
        _teamPublicColor = color;
    }*/
}