using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
     This class contains all methods and variables to manage players.
    Managing players includes registering players, removing players, and updating player information
    regarding their team, health, and other attributes.
     */

    // Add player to the main list
    public void AddPlayer(Player player)
    {
        // Get player id
        int id = player.GetInstanceID();

        // Check if player already exists
        if (playerList.ContainsKey(id))
        {
            Debug.LogError("Player already exists in the list.");
            return;
        } 
        else
        {
            playerList.Add(id, player);
        }  
    }

    // Player list
    public Dictionary<int,Player> playerList = new Dictionary<int,Player>();

}

