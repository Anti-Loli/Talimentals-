using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerStation;
    public Transform enemyStation;

    public Talimental player;
    public Talimental enemy;

    public Text dialogue;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    //Sets up the battle ui and components
    IEnumerator SetupBattle()
    {

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        yield return null;
    }

   
    //auto runs the enemy turn
    IEnumerator EnemyTurn()
    {
        yield return null;
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogue.text = "Congrats you won!!";
        }
        else if (state == BattleState.LOST)
        {
            dialogue.text = "Sorry, you lost";
        }
    }

    //The next 3 methods tell you whos turn it is
    void PlayerTurn()
    {
        dialogue.text = "Your turn";
    }


    //next 3 methods call healing for different characters
    IEnumerator PlayerHeal()
    {
        yield return null;
    }

    //connects the attack button to the attack action
    public void OnAttackButton()
    {
        if(state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack());
        else return;

    }

    //connects heal button to heal action
    public void OnHealButton()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerHeal());
        else return;
    }
}
