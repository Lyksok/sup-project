using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play2T()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void Play4T()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void TeamSelection()
    {
        SceneManager.LoadScene("MenuTeam2");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player has use the back button");
    }

    public void Join()
    {
        SceneManager.LoadScene("JoinMenu");
    }

    public void Host()
    {
        SceneManager.LoadScene("HostMenu");
    }
}
