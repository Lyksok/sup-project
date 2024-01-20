using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class MenuManager : MonoBehaviour
{
    /*
    This class is responsible for managing the menu scene.
    It contains methods that are called by the buttons in the scene.
    */

    // Initialize login menu variables
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private TMP_InputField nameInput;

    // Initialize main menu variables
    [SerializeField] private GameObject mainMenu;

    // Initialize join game menu variables
    [SerializeField] private TMP_InputField ipInput;
    [SerializeField] private TMP_Text incorrectInputText;

    // Initialize all gameobject prefabs
    [SerializeField] private GameObject playerPrefab;

    // Initialize IdManager object
    private IdManager idManager = new IdManager();


    // Change menu to main menu when player enters his username
    public void EnterName()
    {
        if (nameInput.text != "" && nameInput.text.Length <= 15)
        {

            loginMenu.SetActive(false);
            mainMenu.SetActive(true);
            // Instantiate player prefab
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.GetComponent<PlayerUI>().SetName(nameInput.text);
            newPlayer.GetComponent<PlayerUI>().SetId(idManager.GetNextPlayerId());

            NetworkServer.Spawn(newPlayer);
        }
        // TODO : add else statement to display error message
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
        if (valid)
        {
            if (inputText.Length < 7)
            {
                valid = false;
            }
            else
            {
                string[] splitText = inputText.Split('.');
                if (splitText.Length != 4)
                {
                    valid = false;
                }
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

    // Check if ip correct and then join the game
    public void JoinGame()
    {
        if (IsIpCorrect())
        {
            incorrectInputText.gameObject.SetActive(false);
        }
        else
        {
            incorrectInputText.gameObject.SetActive(true);
        }
    }

    public void Play2T()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Play4T()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayFFA()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Debug.Log("Player has use the back button");

    }
}
