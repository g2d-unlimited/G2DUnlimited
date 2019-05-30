using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : BaseAttack
{
    public Hack()
    {
        attackName = "Hack";
        attackDescription = "Swing sword for " + attackDamage + " damage.";
        attackDamage = 15f;
        attackCost = 0;
    }
}
