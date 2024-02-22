using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material yellow;
    [SerializeField] private GameObject idManager;
    [SerializeField] public Material _defaultMaterial;
    

    private List<Team> teams = new List<Team>();
    public List<Team> Teams { get; private set; }

    // Start is called before the first frame update
    public void Start()
    {
        IdManager myIdManager = idManager.GetComponent<IdManager>();
        
        // create teams and set them unique ids and materials
        Team redTeam = new Team(red, myIdManager.GetNextTeamId());
        Team blueTeam = new Team(blue, myIdManager.GetNextTeamId());
        Team greenTeam = new Team(green, myIdManager.GetNextTeamId());
        Team yellowTeam = new Team(yellow, myIdManager.GetNextTeamId());

        // Default team
        Team defaultTeam = new Team(_defaultMaterial, 0);
    }
}
