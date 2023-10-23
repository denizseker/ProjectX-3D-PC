using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public enum CurrentState
    {
        Idle,
        Chasing,
        Patroling,
        Fleeing,
        InInteraction,
        Free,
        Defeated,
        InSettlement,
        Recruiting,
        GoingToWar,
        Following,
    }

    //Soldiers current state.
    [HideInInspector] public CurrentState currentState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Army army;
    [HideInInspector] public float speed;
    [HideInInspector] public GameObject town;
    public Character interactedCharacter;
    public string characterName;
    
    //Soldier values
    public ClanManager.ENUM_Clan enumClan; //enum clan for dropdown list at inspector.
    [HideInInspector] public Clan clan; //real clan value

    public void Setup()
    {
        army = GetComponent<Army>();
        agent = GetComponent<NavMeshAgent>();
        GetClanWithEnum();
        gameObject.name = (string.Format("[{0}] [{1}]", clan.clanName, characterName));
        clan.AddMember(gameObject);
        speed = agent.speed;
    }

    private void GetClanWithEnum()
    {
        if (enumClan == ClanManager.ENUM_Clan.APHALUX) clan = ClanManager.Instance.Aphalux;
        else if (enumClan == ClanManager.ENUM_Clan.DARTRONG) clan = ClanManager.Instance.Dartrong;
        else if (enumClan == ClanManager.ENUM_Clan.SHUNEM) clan = ClanManager.Instance.Shunem;
        else if (enumClan == ClanManager.ENUM_Clan.SOLVENNA) clan = ClanManager.Instance.Solvenna;
        else if (enumClan == ClanManager.ENUM_Clan.VALANDOR) clan = ClanManager.Instance.Valandor;
        else if (enumClan == ClanManager.ENUM_Clan.WUTANG) clan = ClanManager.Instance.Wutang;
        else if (enumClan == ClanManager.ENUM_Clan.BARBARIAN) clan = ClanManager.Instance.Barbarian;
        else if (enumClan == ClanManager.ENUM_Clan.NONE) clan = ClanManager.Instance.None;
    }

    //Setting colliders to opposite
    public void ChangeColliderState()
    {
        gameObject.GetComponentInChildren<DetectArea>().OnOffCollider();
        gameObject.GetComponentInChildren<InteractArea>().OnOffCollider();
        gameObject.GetComponentInChildren<MouseInteraction>().OnOffCollider();
    }


    public void LeaveSettlement()
    {
        OnOffCharacterComponentForTown(true);
        town.GetComponent<Settlement>().RemoveCharacter(gameObject);
        town = null;
        currentState = CurrentState.Patroling;
    }

    public void ResetTarget()
    {
        interactedCharacter = null;
    }

    //This function make ghost that character but important scripts (NPCAI,NPC,PLAYER) is still working while ghost.
    public void OnOffCharacterComponentForTown(bool isActive)
    {
        if (!isActive)
        {
            GetComponent<MouseInteraction>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            GetComponent<MouseInteraction>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

}