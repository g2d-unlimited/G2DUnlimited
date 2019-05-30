using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : MonoBehaviour
{
    public GameObject enemyObj;
    private bool showSelector;

    public void SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input2(enemyObj);
    }

    public void HideSelector()
    {

        GameObject.FindWithTag("EnemySelector").gameObject.SetActive(false);

    }
    public void ShowSelector()
    {

        GameObject.FindWithTag("EnemySelector").gameObject.SetActive(true);

    }
}
