using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoldierLevel
{
    PeasentRecruit,
    SwordsMan,
    HorseMan,
    Cavalary,
    EliteCavalary,
}

public class Soldier
{
    public int health;
    public int attack;
    public int expLimit;
    public int amount;
    public int exp;
    public SoldierLevel soldierLevel;

    //Instantiating at army script with default values
    public Soldier(int _health,int _attack,int _expLimit,int _exp,int _amount,SoldierLevel _soldierLevel)
    {
        health = _health;
        attack = _attack;
        expLimit = _expLimit;
        amount = _amount;
        exp = _exp;
        soldierLevel = _soldierLevel;
    }
}
