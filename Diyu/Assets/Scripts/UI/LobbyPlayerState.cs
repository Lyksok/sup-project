using UnityEngine;

public class LobbyPlayerState
{
    public ulong ClientId;
    public string PlayerName;
    public bool IsReady;
    
    public LobbyPlayerState(ulong clientId, string playerName, bool isReady)
    {
        ClientId = clientId;
        PlayerName = playerName;
        IsReady = isReady;
    }
}
