public class IdManager
{
    // declare variables
    private int _teamIdCounter;
    private int _playerIdCounter;

    // constructor for IdManager class object
    public IdManager()
    {
        _teamIdCounter = 0;
        _playerIdCounter = 0;
    }

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
