using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : BaseAttack
{
    public Slash()
    {
        attackName = "Slash";
        attackDescription = "Swing sword for " + attackDamage + " damage.";
        attackDamage = 10f;
        attackCost = 0;
    }
}
