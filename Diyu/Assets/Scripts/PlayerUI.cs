using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private string _name;
    public string Name { get; private set; }

    private int _playerId;
    public int Id { get; private set; }

    private int _teamId;
    public int TeamId { get; private set; }

    // Method to set player name
    public void SetName(string name)
    {
        _name = name;
    }

    // Method to set team id
    public void SetTeamId(int id)
    {
        _teamId = id;
    }

    // Class constructor
    public PlayerUI(string name, int id)
    {
        _name = name;
        _playerId = id;

        // Set canvas text to player name
        GameObject.Find("NameUI").GetComponent<UnityEngine.UI.Text>().text = _name;
    }
}
