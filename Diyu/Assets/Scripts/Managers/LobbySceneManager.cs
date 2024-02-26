using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    /*
     LobbySceneManager is responsible for managing the lobby scene.
     It contains methods that are called by the buttons in the scene.
     */

    public GameObject waitingScreen;

    public GameObject networkManager;

    // On start find network manager and set all menus to false exept waiting screen


    public void Start()
    {
        waitingScreen.SetActive(true);

        // Get the network manager
        networkManager = GameObject.Find("NetworkManager");
        // Debug.Log(networkManager.name);
    }

    // Method to exit the waiting screen (exit the lobby)
    public void ExitWaitingScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        // TODO : Add code to disconnect from the Lobby (idk if it disconnects automatically)
    }

    // TEMPORARY
    // Temporary method to start the game as host (for testing)
    public void TempHostGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
