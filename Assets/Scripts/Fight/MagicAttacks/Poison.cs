using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BaseAttack
{
    public Poison()
    {
        attackName = "Posion";
        attackDescription = "Magical poison";
        attackDamage = 10f;
        attackCost = 0;
    }
}
