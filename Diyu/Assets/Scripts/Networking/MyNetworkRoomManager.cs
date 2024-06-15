using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyNetworkRoomManager : NetworkRoomManager
{
    public List<(NetworkIdentity, GameObject)> Players = new List<(NetworkIdentity, GameObject)>();
    public MainLoop gameLoop;
    public void AddPlayer(NetworkIdentity identity, GameObject player)
    {
        Players.Add((identity,player));
    }

    public void RemovePlayer(NetworkIdentity identity)
    {
        for (var i = 0; i < Players.Count; i++)
            if (Players[i].Item1 == identity) Players.RemoveAt(i);
    }

    public void Debug()
    {
        UnityEngine.Debug.LogError(Players[0].Item2.GetComponent<NewPlayer>()._name);
    }

    public override void OnRoomServerPlayersReady()
    {
        base.OnRoomServerPlayersReady();
        gameLoop.hasGameStarted = true;
    }
}
