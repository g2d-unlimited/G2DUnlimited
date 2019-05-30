using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStateMachine : MonoBehaviour
{
    public PlayerStats player;
    private BattleStateMachine bsm;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }
    public TurnState currentState;
    //progress bar
    private float curr_progress = 0f;
    private float progress_bar_charge_time = 2f;
    public Image progressBarHP;
    public Image progressBarArmor;
    public GameObject selector;

    //IEnumerator
    public GameObject enemyToAttack;
    public bool actionStarted = false;
    private Vector2 startPosition;
    private float animationSpeed = 450f;

    //death
    private bool alive = true;

    //charpanel
    private CharPanelStats stats;
    public GameObject CharPanel;

    // Start is called before the first frame update
    void Start()
    {

        CreateCharPanel();


        startPosition = transform.position;
        curr_progress = Random.Range(0, 2.5f);
        selector.SetActive(false);
        bsm = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;      
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                UpdateProgressBar(progressBarHP);
                UpdateProgressBar(progressBarArmor);
                break;
            case (TurnState.ADDTOLIST):
                bsm.charManager.Add(gameObject);
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):
                break;
            case (TurnState.ACTION):
                bsm.hasAttacked = true;
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
                    this.gameObject.tag = "DeadChar";
                    //not attackable
                    bsm.charsInBattle.Remove(gameObject);
                    //not managable
                    bsm.charManager.Remove(gameObject);
                    //deactivate selector
                    selector.SetActive(false);
                    //reset gui
                    bsm.selectionPanel.SetActive(false);
                    bsm.enemySelectPanel.SetActive(false);
                    //remove item from performlist
                    if (bsm.charsInBattle.Count > 0)
                    {
                        for (int i = 0; i < bsm.performList.Count; i++)
                        {
                            if (bsm.performList[i].targetObj == this.gameObject)
                            {
                                bsm.performList.Remove(bsm.performList[i]);
                            }
                            if (bsm.performList[i].targetObj == this.gameObject)
                            {
                                bsm.performList[i].targetObj = bsm.charsInBattle[Random.Range(0, bsm.charsInBattle.Count)];
                            }
                        }
                    }
                    //change anmimation
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    //reset heroinput
                    bsm.battleState = BattleStateMachine.PerformAction.CHECKALIVE;

                    alive = false;

                    
                }
                break;
            
        }
    }
    /*Uzkrauna health bar*/
    void UpdateProgressBar(Image progressBar)
    {
        curr_progress += Time.deltaTime;
        float calc_progress = curr_progress / progress_bar_charge_time;
        progressBar.transform.localScale = new Vector2(Mathf.Clamp(calc_progress, 0, 1), progressBar.transform.localScale.y);
        if(curr_progress >= progress_bar_charge_time)
        {
            currentState = TurnState.ADDTOLIST;
        }
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
        yield return new WaitForSeconds(0.5f);

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
        if (bsm.battleState != BattleStateMachine.PerformAction.WIN && bsm.battleState != BattleStateMachine.PerformAction.LOSE)
        {

            bsm.battleState = BattleStateMachine.PerformAction.WAIT;
            //reset enemy state
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState = TurnState.WAITING;
        }

        //end coroutine
        actionStarted = false;


    }
    private bool MoveTowardsTarget(Vector2 target)
    {
        return !target.Equals(transform.position = Vector2.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }
    private bool MoveTowardsStart(Vector2 target)
    {
        return !target.Equals(transform.position = Vector2.MoveTowards(transform.position, target, animationSpeed * Time.deltaTime));
    }
    public void TakeDamage(float getDamage)
    {
        player.currHP -= getDamage;
        if(player.currHP <= 0)
        {
            player.currHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdateCharPanel();
    }
    //create hero panel
    private void CreateCharPanel()
    {

        CharPanel.GetComponent<RectTransform>().localPosition = new Vector2(960, -540);
        stats = CharPanel.GetComponent<CharPanelStats>();
        stats.charName.text = player.theName;
        stats.charHP.text = "Health: " + player.currHP + "/" + player.MaxValueHP;
        stats.charARM.text = "Armor: " + player.currARM + "/" + player.MaxValueArmor;

        progressBarHP = stats.progressBarHP;
        progressBarArmor = stats.progressBarARM;
    }

    private void UpdateCharPanel()
    {
        stats.charHP.text = "Health: " + player.currHP + "/" + player.MaxValueHP;
        stats.charARM.text = "Armor: " + player.currARM + "/" + player.MaxValueArmor;
        //progressBarHP.GetComponent<RectTransform>().localScale.Set(player.cu)
        //progressBarArmor = stats.progressBarARM;
    }

    private void DoDamage()
    {
        float calc_damage = player.currATK + bsm.performList[0].chosenAttack.attackDamage;
        enemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage);
    }
}
