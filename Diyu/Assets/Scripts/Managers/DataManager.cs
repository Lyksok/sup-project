using System;
using System.Collections.Generic;
using System.Linq;
using Managers.DataSets;
using Mirror;
using UnityEngine;

public class DataManager : NetworkBehaviour
{
    public List<(NetworkIdentity,GameObject)> Players = new List<(NetworkIdentity, GameObject)>();

    public void AddPlayer(NetworkIdentity identity, GameObject player)
    {
        Players.Add((identity,player));
    }

    public void RemovePlayer(NetworkIdentity identity)
    {
        for (var i = 0; i < Players.Count; i++)
            if (Players[i].Item1 == identity) Players.RemoveAt(i);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
