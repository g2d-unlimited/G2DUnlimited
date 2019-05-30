using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public PerformAction battleState;

    public List<TurnHandler> performList = new List<TurnHandler>();
    public List<GameObject> charsInBattle = new List<GameObject>();
    public List<GameObject> enemiesInBattle = new List<GameObject>();

    public enum CharGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE

    }

    public CharGUI charInput;
    public List<GameObject> charManager = new List<GameObject>();
    private TurnHandler charChoice;
    public GameObject enemyButton;
    public Transform spacer;

    public GameObject selectionPanel;
    public GameObject enemySelectPanel;
    public GameObject abilityPanel;

    //attacks of chars
    public Transform actionSpacer;
    public Transform abilitySpacer;
    public GameObject actionButton;
    public GameObject abilityButton;

    private List<GameObject> actionButtons = new List<GameObject>();

    //enemy buttons
    private List<GameObject> enemyButtons = new List<GameObject>();


    public bool hasAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        battleState = PerformAction.WAIT;
        enemiesInBattle.Add(GameObject.FindGameObjectWithTag("Enemy"));
        charsInBattle.Add(GameObject.FindGameObjectWithTag("Character"));
        charInput = CharGUI.ACTIVATE;

        selectionPanel.SetActive(false);
        enemySelectPanel.SetActive(false);
        abilityPanel.SetActive(false);

        EnemyButtons();
    }
    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        switch (battleState)
        {
            case (PerformAction.WAIT):
                if (performList.Count > 0)
                {
                    battleState = PerformAction.TAKEACTION;
                }
                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(performList[0].attacker);
                if (performList[0].type == "Character")
                {
                    CharStateMachine csm = performer.GetComponent<CharStateMachine>();
                    csm.enemyToAttack = performList[0].targetObj;
                    csm.currentState = CharStateMachine.TurnState.ACTION;
                }

                if (performList[0].type == "Enemy")
                {
                    EnemyStateMachine esm = performer.GetComponent<EnemyStateMachine>();
                    for (int i = 0; i < charsInBattle.Count; i++)
                    {
                        if (performList[0].targetObj == charsInBattle[i])
                        {
                            esm.targetObj = performList[0].targetObj;
                            esm.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        else
                        {
                            performList[0].targetObj = charsInBattle[Random.Range(0, charsInBattle.Count)];
                            esm.targetObj = performList[0].targetObj;
                            esm.currentState = EnemyStateMachine.TurnState.ACTION;

                        }
                    }
                }


                battleState = PerformAction.PERFORMACTION;
                break;

            case (PerformAction.PERFORMACTION):
                //idle
                break;

            case (PerformAction.CHECKALIVE):
                if (charsInBattle.Count < 1)
                {
                    battleState = PerformAction.LOSE;
                    //lose game
                }
                else if (enemiesInBattle.Count < 1)
                {
                    battleState = PerformAction.WIN;
                    //win battle
                }
                else
                {
                    //call function
                    ClearAttackPanel();
                    charInput = CharGUI.ACTIVATE;
                }
                break;

            case (PerformAction.LOSE):
                {
                    Debug.Log("Battle lost");
                }
                break;
            case (PerformAction.WIN):
                {
                    Debug.Log("Battle won");
                    for (int i = 0; i < charsInBattle.Count; i++)
                    {
                        charsInBattle[i].GetComponent<CharStateMachine>().currentState = CharStateMachine.TurnState.WAITING;
                    }
                }
                break;
        }

        switch (charInput)
        {
            case (CharGUI.ACTIVATE):
                if (charManager.Count > 0)
                {
                    charManager[0].transform.FindChild("Selector").gameObject.SetActive(true);
                    charChoice = new TurnHandler();
                    selectionPanel.SetActive(true);
                    CreateAttackButtons();
                    charInput = CharGUI.WAITING;
                }
                break;
            case (CharGUI.WAITING):
                break;
            case (CharGUI.DONE):
                CharInputDone();
                break;
        }
    }
    public void CollectActions(TurnHandler turn)
    {
        performList.Add(turn);
    }

    //create/delete enemy select buttons
    public void EnemyButtons()
    {
        //clean-up
        foreach (GameObject enemyButton in enemyButtons)
        {
            Destroy(enemyButton);
        }
        enemyButtons.Clear();
        //create buttons
        foreach (GameObject enemy in enemiesInBattle)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelect button = newButton.GetComponent<EnemySelect>();

            EnemyStateMachine curr_enemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = curr_enemy.enemy.theName;

            button.enemyObj = enemy;
            newButton.transform.SetParent(spacer, false);
            enemyButtons.Add(newButton);
        }
    }

    /// <summary>
    /// attack button
    /// </summary>
    public void Input1()
    {
        charChoice.attacker = charManager[0].name;
        charChoice.attackerObj = charManager[0];
        charChoice.type = "Character";
        charChoice.chosenAttack = charManager[0].GetComponent<CharStateMachine>().player.attackList[0];

        selectionPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }
    /// <summary>
    /// enemy selection
    /// </summary>
    public void Input2(GameObject chosenTarget)
    {
        charChoice.targetObj = chosenTarget;
        charInput = CharGUI.DONE;
    }

    [System.Obsolete]
    void CharInputDone()
    {
        performList.Add(charChoice);
        //clean selectionpanel
        ClearAttackPanel();

        charManager[0].transform.FindChild("Selector").gameObject.SetActive(false);
        charManager.RemoveAt(0);
        charInput = CharGUI.ACTIVATE;
    }

    //clears selectionpanel
    void ClearAttackPanel()
    {
        enemySelectPanel.SetActive(false);
        selectionPanel.SetActive(false);
        abilityPanel.SetActive(false);


        foreach (GameObject button in actionButtons)
        {
            Destroy(button);
        }
        actionButtons.Clear();
    }

    [System.Obsolete]
    void CreateAttackButtons()
    {
        GameObject attackButton = Instantiate(actionButton) as GameObject;
        Text attackButtonText = attackButton.transform.FindChild("Text").gameObject.GetComponent<Text>();
        attackButtonText.text = "Attack";
        attackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        attackButton.transform.SetParent(actionSpacer, false);
        actionButtons.Add(attackButton);

        GameObject abilityButton1 = Instantiate(actionButton) as GameObject;
        Text abilityButtonText = abilityButton1.transform.FindChild("Text").gameObject.GetComponent<Text>();
        abilityButtonText.text = "Ability";
        abilityButton1.GetComponent<Button>().onClick.AddListener(() => Input3());
        abilityButton1.transform.SetParent(actionSpacer, false);
        actionButtons.Add(abilityButton1);

        //Jei turi ability
        if (charManager[0].GetComponent<CharStateMachine>().player.abilities.Count > 0)
        {
            foreach (BaseAttack ability in charManager[0].GetComponent<CharStateMachine>().player.abilities)
            {
                GameObject button = Instantiate(abilityButton) as GameObject;
                Text buttonText = button.transform.FindChild("Text").gameObject.GetComponent<Text>();
                buttonText.text = ability.attackName;
                AttackButton atb = button.GetComponent<AttackButton>();
                atb.abilityToPerform = ability;
                button.transform.SetParent(abilitySpacer, false);
                actionButtons.Add(button);
            }
        }
        else
        {
            abilityButton.GetComponent<Button>().interactable = false;
        }
    }

    //pasirinktas ability
    public void Input4(BaseAttack ability)
    {
        charChoice.attacker = charManager[0].name;
        charChoice.attackerObj = charManager[0];
        charChoice.type = "Character";

        charChoice.chosenAttack = ability;
        abilityPanel.SetActive(false);
        enemySelectPanel.SetActive(true);
    }
    //switch i ability
    public void Input3()
    {
        selectionPanel.SetActive(false);
        abilityPanel.SetActive(true);
    }
}
