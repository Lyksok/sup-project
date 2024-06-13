using UnityEngine;
using UnityEngine.UIElements;

public class CharacterCard : MonoBehaviour
{
    public GameObject prefab;
    public GameObject panel;

    public bool IsSelected
    {
        get => panel.activeSelf;
        set => panel.SetActive(value);
    }
}
