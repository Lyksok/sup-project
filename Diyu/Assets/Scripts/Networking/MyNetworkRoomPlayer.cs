using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class MyNetworkRoomPlayer : NetworkRoomPlayer
{
    // Handle client when entering the lobby
    public override void OnStartClient()
    {
        Debug.Log("Client started on lobby");
    }

    public override void OnStopClient()
    {   
        SceneManager.LoadScene(this.GetComponent<MyNetworkRoomManager>().offlineScene);
    }

}
