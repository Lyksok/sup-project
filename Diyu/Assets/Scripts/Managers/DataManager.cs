using System.Collections.Generic;
using System.Linq;
using Managers.DataSets;
using Mirror;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private List<DataCharacter> _classCharacters = new List<DataCharacter>();
    public List<GameObject> prefabs = new List<GameObject>(); 
    public List<(uint,GameObject)> Players = new List<(uint, GameObject)>();
    public MainLoop loop;
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
        Players.Add((identity.netId,player));
        loop.players.Add(player.GetComponentInChildren<NewPlayer>());
    }

    public void RemovePlayer(NetworkIdentity identity)
    {
        for (var i = 0; i < Players.Count; i++)
            if (Players[i].Item1 == identity.netId) Players.RemoveAt(i);
    }

    public string DebugPlayers()
    {
        string res = "";
        Players.ForEach(tuple => res+=$"{tuple.Item1}\n");
        return res;
    }
}
