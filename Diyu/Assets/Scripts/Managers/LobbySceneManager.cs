using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    /*
     LobbySceneManager is responsible for managing the lobby scene.
     It contains methods that are called by the buttons in the scene.

     */

    private GameObject networkManager;

    // On start find network manager and set all menus to false exept waiting screen


    public void Start()
    {
        // Get the network manager
        networkManager = GameObject.FindWithTag("NetworkManager");
        // Debug.Log(networkManager.name);
    }

    // Method to exit the waiting screen (exit the lobby)
    public void ExitLobby()
    {
        // Stop the client
        networkManager.gameObject.GetComponent<MyNetworkRoomManager>().StopClient();

        // Load offline scene
        SceneManager.LoadScene(networkManager.gameObject.GetComponent<MyNetworkRoomManager>().offlineScene);
    }

    // TEMPORARY
    // Temporary method to start the game as host (for testing)
    public void TempHostGame()
    {
        networkManager.gameObject.GetComponent<MyNetworkRoomManager>().StartServer();
    }
}
