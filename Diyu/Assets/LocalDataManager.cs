using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Mirror;
using TMPro;
using UnityEngine;

public class LocalDataManager : MonoBehaviour
{
    public GameObject playerClassPrefab;
    public string playerName;
    public TMP_InputField nameInput;
    public List<CharacterCard> characterCards = new List<CharacterCard>();
    [CanBeNull] private CharacterCard _current;
    
    public TMP_Text incorrectNameText;
    public TMP_Text incorrectClassText;
    
    public void SetPlayerName()
    {
        playerName = nameInput.text;
    }

    public void SetPlayerClassPrefab()
    {
        foreach (CharacterCard card in characterCards)
        {
            if (card.IsSelected)
            {
                playerClassPrefab = card.prefab;
            }

            break;
        }
    }

    public void SelectCharacterCard(CharacterCard card)
    {
        characterCards.ForEach(charCard => charCard.IsSelected = false);
        card.IsSelected = true;
        NetworkManager.singleton.playerPrefab = card.prefab;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        characterCards.ForEach(charCard => charCard.IsSelected = false);
    }

    public bool CheckInputs()
    {
        bool name = nameInput.text == "";
        bool charClass = characterCards.All(card => card.IsSelected == false);
        
        incorrectNameText.gameObject.SetActive(name);
        incorrectClassText.gameObject.SetActive(charClass);

        return name || charClass; // Return true if there is a problem
    }
    
    public void CheckInputsNoReturn()
    {
        bool name = nameInput.text == "";
        bool charClass = characterCards.All(card => card.IsSelected == false);
        
        incorrectNameText.gameObject.SetActive(name);
        incorrectClassText.gameObject.SetActive(charClass);
    }
}
