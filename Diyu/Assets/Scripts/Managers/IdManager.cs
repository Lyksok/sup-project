using UnityEngine;

public class IdManager : MonoBehaviour
{
    // declare variables
    private int _teamIdCounter = 1;
    private int _playerIdCounter = 0;


    // get next team and player id
    public int GetNextTeamId()
    {
        _teamIdCounter++;
        return _teamIdCounter;
    }
    public int GetNextPlayerId()
    {
        _playerIdCounter++;
        return _playerIdCounter;
    }
}
