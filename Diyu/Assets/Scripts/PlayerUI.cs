using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    /*
    This class is responsible for managing the player UI.
    Its purpose is to save the player name and id waiting for the game to start.
    */
    // Initialize playerUI serial fields
    [SerializeField] private TMP_Text nameText;

    // Initialize playerUI variables
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

    // Method to set player id
    public void SetId(int id)
    {
        _playerId = id;
        Debug.Log("Player id : " + _playerId);
    }

    // Method to set team id
    public void SetTeamId(int id)
    {
        _teamId = id;
    }
}
