using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartClient()
    {
        // Add custom code to execute when the client starts
        Debug.Log("Client started!");
    }
    public override void OnStartServer()
    {
        // Add custom code to execute when the server starts
        Debug.Log("Server started!");
    }
}
