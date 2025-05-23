using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    /*
    This class is responsible for managing the menu scene.
    It contains methods that are called by the buttons in the scene.
    */

    

    // Initialize join game menu variables
    [SerializeField] private TMP_InputField ipInput;
    [SerializeField] private TMP_Text incorrectInputText;
    
    public NetworkManager networkManager; 
    public LocalDataManager localDataManager;

    // Initialize all menus as variables
    [Header("All pages")]
    public GameObject mainMenu;
    public GameObject joinPage;
    public GameObject playerInfoPage;
    public GameObject selectClassPage;
    
    // Start is called before the first frame update
    void Start()
    {
        // Insures that only main menu is active at the start
        mainMenu.SetActive(true);
        joinPage.SetActive(false);
        playerInfoPage.SetActive(false);
        selectClassPage.SetActive(false);
    }

    // Verify if the input is a valid IP address
    public bool IsIpCorrect()
    {
        string inputText = ipInput.text;
        bool valid = true;

        foreach (char c in inputText)
        {
            if (!char.IsDigit(c) && c != '.')
            {
                valid = false;
                break;
            }
        }
        if (inputText == "localhost")
            return true;
        if (valid)
        {
            if (inputText.Length < 7)
                valid = false;
            else
            {
                string[] splitText = inputText.Split('.');
                if (splitText.Length != 4)
                    valid = false;
                else
                {
                    foreach (string s in splitText)
                    {
                        if (s.Length > 3)
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            int num = int.Parse(s);
                            if (num < 0 || num > 255)
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        return valid;
    }

    // Set ip on input change
    public void SetIp(string ip)
    {
        networkManager.networkAddress = ip;
    }

    // Check if ip correct and then join the game
    public void JoinGame()
    {
        if (IsIpCorrect())
        {
            networkManager.StartClient();
        }
        else
        {
            incorrectInputText.gameObject.SetActive(true);
        }
    }

    public void ValidateInputs()
    {
        if (localDataManager.CheckInputs())
        {
            // Do nothing because there are problems
        }
        else
        {
            playerInfoPage.SetActive(false);
            joinPage.SetActive(true);
        }
    }

    // Temporary method to start the game as host
    public void TempHostGame()
    {
        networkManager.StartHost();
    }

    // Method to start a server on localhost
    public void StartServerOnlocalhost()
    {
        networkManager.networkAddress = "localhost";
        networkManager.StartServer();
    }

    public void Quit()
    {
#if UNITY_STANDALONE
        // Quitter seulement en mode standalone (build)
        Application.Quit();
#endif

#if UNITY_EDITOR
        // Si vous êtes dans l'éditeur Unity, arrêtez le mode Play
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Player has used the back button");

    }
}

public class OpenDevConsoleScript : MonoBehaviour
{
    void Start()
    {
        Debug.developerConsoleVisible = true;
    }
}