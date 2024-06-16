using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadChar : MonoBehaviour
{
    public GameObject[] charPrefab;
    public Transform spawnPoint;
    public TMP_Text label;
    void Start()
    {
        int curChar = PlayerPrefs.GetInt("selectedChar");
        GameObject prefab = charPrefab[curChar];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        label.text = prefab.name;
    }

}
