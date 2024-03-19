using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public CharDB chars;
    private int curChar = 0;

    public TMP_Text charName;
    public SpriteRenderer charSprite;

    public void NextChar()
    {
        curChar = (curChar + 1) % chars.charCount;
        UpdateChar();
    }
    
    public void PrevChar()
    {
        curChar = (curChar - 1 + chars.charCount) % chars.charCount;
        UpdateChar();
    }

    private void UpdateChar()
    {
        Character character = chars.GetChar(curChar);
        charSprite.sprite = character.charSprite;
        charName.text = character.charName;
    }

    void Start()
    {
        UpdateChar();
    }
    
    
}