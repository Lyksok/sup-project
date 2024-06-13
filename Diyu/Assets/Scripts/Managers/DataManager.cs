using System;
using System.Collections.Generic;
using System.Linq;
using Managers.DataSets;
using Mirror;
using UnityEngine;

public class DataManager : NetworkBehaviour
{
    public List<DataCharacter> _classCharacters = new List<DataCharacter>();
    public List<GameObject> prefabs = new List<GameObject>(); 
    public List<(NetworkIdentity,GameObject)> Players = new List<(NetworkIdentity, GameObject)>();
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        for (var i = 0; i < prefabs.Count; i++)
        {
            _classCharacters.Add(new DataCharacter(i,prefabs[i]));
        }
    }

    public void AddPlayer(NetworkIdentity identity, GameObject player)
    {
        Players.Add((identity,player));
    }

    public void RemovePlayer(NetworkIdentity identity)
    {
        for (var i = 0; i < Players.Count; i++)
            if (Players[i].Item1 == identity) Players.RemoveAt(i);
    }

    public GameObject GetPlayerClassPrefab(NetworkIdentity netId)
    {
        // return _classCharacters.Where(((character, i) => character == netId))
        throw new NotImplementedException();
    }

    public string DebugPlayers()
    {
        string res = "";
        Players.ForEach(tuple => res+=$"{tuple.Item1}\n");
        return res;
    }
}
