using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerCard : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject waitingForPlayerPanel;
    [SerializeField] private GameObject playerDataPanel;

    [Header("Data Display")]
    [SerializeField] private TMP_Text playerDisplayNameText;
    [SerializeField] private Toggle isReadyToggle;
}
