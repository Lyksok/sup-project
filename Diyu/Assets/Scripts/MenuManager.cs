using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_Text incorrectInputText;


    public void JoinGame()
    {
        string inputText = joinInput.text;
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

        if (valid)
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
        Application.Quit();
        Debug.Log("Player has use the back button");
    }
}
