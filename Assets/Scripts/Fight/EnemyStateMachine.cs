using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine bsm;
    public EnemyStats enemy;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }
    public TurnState currentState;
    private float curr_progress = 0f;
    private float progress_bar_charge_time = 1f;
    private float animationSpeed = 450f;

    private Vector2 startPosition;
    public GameObject targetObj;
    private bool actionStarted = false;
    public GameObject selector;

    //alive
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        selector.SetActive(false);
        currentState = TurnState.PROCESSING;
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpdateProgressBar();
                break;
            case (TurnState.CHOOSEACTION):
                    ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):
                //idle
                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    //change tag
                    gameObject.tag = "DeadEnemy";
                    //not attackable
                    bsm.enemiesInBattle.Remove(this.gameObject);

                    //deactivate selector
                    selector.SetActive(false);

                    //remove all inputs of enemy attacks
                    if (bsm.enemiesInBattle.Count > 0)
                    {
                        for (int i = 0; i < bsm.performList.Count; i++)
                        {
                            if (bsm.performList[i].attackerObj = gameObject)
                            {
                                bsm.performList.Remove(bsm.performList[i]);
                            }
                            if (bsm.performList[i].targetObj == this.gameObject)
                            {
                                bsm.performList[i].targetObj = bsm.enemiesInBattle[Random.Range(0, bsm.enemiesInBattle.Count)];
                            }
                        }
                    }
                    //change color to gray
                    gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    //set alive false
                    alive = false;
                    //reset enemy buttons
                    bsm.EnemyButtons();

                    bsm.battleState = BattleStateMachine.PerformAction.CHECKALIVE;
                }
                break;
        }
    }
    /*
     * Uzkrauna health bar
     */
    void UpdateProgressBar()
    {
        curr_progress += Time.deltaTime;
        if (curr_progress >= progress_bar_charge_time && bsm.hasAttacked == true)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }
    /*
     * 
     */
    void ChooseAction()
    {
        TurnHandler myAttack = new TurnHandler();
        myAttack.attacker = enemy.theName;
        myAttack.type = "Enemy";
        myAttack.attackerObj = this.gameObject;
        myAttack.targetObj = bsm.charsInBattle[Random.Range(0, bsm.charsInBattle.Count)];

        int num = Random.Range(0, enemy.attackList.Count);
        myAttack.chosenAttack = enemy.attackList[num];
        Debug.Log(this.gameObject.name + " has chosen " + myAttack.chosenAttack.attackName + " and does " + myAttack.chosenAttack.attackDamage + " dmg.");
        bsm.CollectActions(myAttack);
    }
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
            
        }

        actionStarted = true;

        //enemy animation attack
        Vector2 targetPosition = new Vector2(450, gameObject.transform.position.y);
        while (MoveTowardsTarget(targetPosition))
        {
            yield return null;
        }

        //wait 
        yield return new WaitForSeconds(1f);

        //do dmg
        DoDamage();
        //animate back pos
        Vector2 firstPos = startPosition;
        while (MoveTowardsStart(firstPos))
        {
            yield return null;
        }

        //remove this perform from list in bsm
        bsm.performList.RemoveAt(0);

        //reset bsm to wait
        bsm.battleState = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;

        //reset enemy state
        bsm.hasAttacked = false;
        currentState = TurnState.PROCESSING;
    }
    private bool MoveTowardsTarget(Vector2 target)
    {
        return !target.Equals(transform.position = Vector2.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector2 target)
    {
        return !target.Equals(transform.position = Vector2.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }
    void DoDamage()
    {
        float calc_damage = enemy.currATK + bsm.performList[0].chosenAttack.attackDamage;
        targetObj.GetComponent<CharStateMachine>().TakeDamage(calc_damage);
    }
    public void TakeDamage(float damage)
    {
        enemy.currHP -= damage;
        if(enemy.currHP <= 0)
        {
            enemy.currHP = 0;
            currentState = TurnState.DEAD;
        }
    }
}
