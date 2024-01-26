using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material yellow;

    private List<Team> teams = new List<Team>();
    public List<Team> Teams { get; private set; }

    // Start is called before the first frame update
    public void Start()
    {
    }
}
