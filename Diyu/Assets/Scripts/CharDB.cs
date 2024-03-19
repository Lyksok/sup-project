using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharDB : ScriptableObject
{
    //Used for character selection screen
    public Character[] character;
    public int charCount
    {
        get => character.Length;
    }

    public Character GetChar(int index)
    {
        return character[index];
    }
}
