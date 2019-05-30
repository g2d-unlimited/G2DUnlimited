using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnHandler
{
    public string attacker;      // name of char who is attacking
    public string type;
    public GameObject attackerObj;   //GameObject of attacker
    public GameObject targetObj;     //Target of attacker

    //Figure out which attack is done
    public BaseAttack chosenAttack;

}
