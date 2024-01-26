using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
    PlayerManager is a singleton class that manages the player's Identity.
    */

    // PlayerManager instance
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
                if (instance == null)
                {
                    Debug.LogError("PlayerManager instance not found in the scene. Make sure it's added to a GameObject.");
                }
            }
            return instance;
        }
    }

    // Player Identity list
    public List<PlayerInfo> playerIdentityList = new List<PlayerInfo>();

}

