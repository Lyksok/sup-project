using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject teamManager;


    // declare variables
    private Team _team = null;
    public Team Team { get; private set; }

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
        _team = team;
    }

    public void SetTeamById(int teamId)
    {
        teamManager.GetComponent<TeamManager>().Teams.ForEach(team =>
        {
            if (team.Id == teamId)
            {
                _team = team;
                return;
            }
        });
    }

    // Set empty team 
    public void ResetTeam()
    {
        _team = null;
    }
}
