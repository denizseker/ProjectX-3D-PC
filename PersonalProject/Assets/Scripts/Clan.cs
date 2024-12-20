using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clan
{
    public string clanName;
    public Sprite clanLogo;
    public List<GameObject> members = new List<GameObject>();
    public List<GameObject> settlements = new List<GameObject>();
    public List<Clan> enemies = new List<Clan>();

    public Clan(string _clanName,Sprite _clanLogo)
    {
        clanName = _clanName;
        clanLogo = _clanLogo;
    }

    public void AddMember(GameObject member)
    {
        members.Add(member);
    }
    public void AddSettlement(GameObject settlement)
    {
        settlements.Add(settlement);
    }

    public GameObject FindClosestAllySettlement(Character _character)
    {
        //if there is settlement
        if(settlements.Count > 0)
        {
            float distance;
            GameObject closestSettlement;

            if (_character.town != null)
            {
                distance = Vector3.Distance(_character.town.transform.position, _character.transform.position);
                closestSettlement = _character.town;
            }
            else
            {
                distance = Vector3.Distance(settlements[0].transform.position, _character.transform.position);
                closestSettlement = settlements[0];
            }

            for (int i = 0; i < settlements.Count; i++)
            {
                float tempDistance = Vector3.Distance(settlements[i].transform.position, _character.transform.position);

                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestSettlement = settlements[i];
                }
            }

            return closestSettlement;
            
        }
        //clan dont have any ally town
        else
        {
            _character.SetCharacterState(Character.State.Free);
            _character.GetComponent<NPCAI>().GoToRandomPoint();
            throw new Exception("There is no ally town");
        }
        
    }
}
