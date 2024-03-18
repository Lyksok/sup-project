using UnityEngine;
using Mirror;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{

    // Handle client when entering the lobby
    public override void OnStartClient()
    {
        gameObject.transform.GetChild(0).SetParent(GameObject.FindWithTag("Lobby").transform);
    }
}
