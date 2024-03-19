using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;
    [SerializeField] private Material yellow;
    [SerializeField] public Material _defaultMaterial;
    

    private List<Team> teams = new List<Team>();
    public List<Team> Teams { get; private set; }

    // Start is called before the first frame update
    public void Start()
    {
        
        // create teams and set them unique ids and materials
        Team redTeam = new Team(red, (int) TeamIdEnum.Red);
        Team blueTeam = new Team(blue, (int) TeamIdEnum.Blue);
        Team greenTeam = new Team(green, (int) TeamIdEnum.Green);
        Team yellowTeam = new Team(yellow, (int) TeamIdEnum.Yellow);

        // Default team
        Team defaultTeam = new Team(_defaultMaterial, (int)TeamIdEnum.Default);
    }
}
