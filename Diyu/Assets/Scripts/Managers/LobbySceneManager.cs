using System;
using System.Net;
using System.Net.Sockets;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : NetworkBehaviour
{
    /*
     LobbySceneManager is responsible for managing the lobby scene.
     It contains methods that are called by the buttons in the scene.

     */

    private NetworkManager networkManager;
    public TMP_Text localIp;

    // On start find network manager and set all menus to false exept waiting screen


    public void Start()
    {
        // Get the network manager
        networkManager = NetworkManager.singleton;
        // Debug.Log(networkManager.name);

        if (isServer)
        {
            localIp.text = GetLocalIPAddress();
        }
    }

    // Method to exit the waiting screen (exit the lobby)
    public void ExitLobby()
    {
        // Stop the client
        networkManager.gameObject.GetComponent<MyNetworkRoomManager>().StopClient();
        //NetworkManager.singleton.StopHost();
        
        networkManager.StopServer();
        // Load offline scene
    
        //SceneManager.LoadScene(networkManager.gameObject.GetComponent<MyNetworkRoomManager>().offlineScene);
    }
    
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    
    // Temporary method to start the game as host (for testing)
    public void TempHostGame()
    {
        networkManager.gameObject.GetComponent<MyNetworkRoomManager>().StartServer();
    }
}
