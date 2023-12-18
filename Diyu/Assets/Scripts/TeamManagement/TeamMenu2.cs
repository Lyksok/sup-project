using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamMenu2 : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void Back()
    {
        Application.Quit();
        Debug.Log("Player has use the back button");
    }
}
