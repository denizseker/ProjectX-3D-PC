using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHandler : MonoBehaviour
{

    private List<Character> party1 = new List<Character>();
    private List<Character> party2 = new List<Character>();
    public bool isBattleStarted = false;
    public float pastTime;
    public float startTime;
    public string pastTimeString;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBattleStarted)
        {
            pastTime = Time.time - startTime;
            //Debug.Log(pastTime.ToString("F0"));
            pastTimeString = Mathf.Floor(pastTime / 60).ToString("00") + ":" + Mathf.FloorToInt(pastTime % 60).ToString("00");
        }
    }


    public void StartFight(Character _character1,Character _character2)
    {
        startTime = Time.time;
        isBattleStarted = true;
    }


}
