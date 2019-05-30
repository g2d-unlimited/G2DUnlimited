using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public BaseAttack abilityToPerform;

    public void CastAbility()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(abilityToPerform);
    }
}
