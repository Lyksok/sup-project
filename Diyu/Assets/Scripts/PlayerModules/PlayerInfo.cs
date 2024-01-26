using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField] private GameObject player;


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

    // Set empty team 
    public void ResetTeam()
    {
        _team = null;
    }
}
