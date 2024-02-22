using UnityEngine;

public class PlayerTeam
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teamManager;


    // declare variables
    public Team Team { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Set player material based on team 
    public void SetTeamMaterial(Material material)
    {
        player.GetComponent<MeshRenderer>().material = material;
    }

    // Set entity type
    public void SetTeam(Team team)
    {
        Team = team;
    }

    public void SetTeamById(int teamId)
    {
        teamManager.GetComponent<TeamManager>().Teams.ForEach(team =>
        {
            if (team.Id == teamId)
            {
                Team = team;
                return;
            }
        });
    }

    // Set empty team 
    public void ResetTeam()
    {
        Team = null;
    }
}
