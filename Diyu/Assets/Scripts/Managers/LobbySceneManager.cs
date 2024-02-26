using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    // On start find network manager and set all menus to false exept waiting screen
    public GameObject waitingScreen;

    public NetworkManager networkManager;

    public void Start()
    {
        waitingScreen.SetActive(true);

        // Get the network manager
        GameObject tmp = GameObject.FindGameObjectWithTag("NetworkManager");
        if (tmp != null)
        {
            networkManager = tmp.GetComponent<NetworkManager>();
        }
    }

    // Method to exit the waiting screen (exit the lobby)
    public void ExitWaitingScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // TODO : Add code to disconnect from the Lobby (idk if it disconnects automatically)
    }

    // TEMPORARY
    // Temporary method to start the game as host (for testing)
    public void TempHostGame()
    {
        networkManager.StartHost();
    }
}
