using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, IGNUMTURN, MOLOMTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject ignumPrefab;
    public GameObject molomPrefab;

    public Transform playerStation;
    public Transform enemyStation;
    public Transform ignumStation;
    public Transform molomStation;

    Unit playerUnit;
    Unit enemyUnit;
    Unit ignumUnit;
    Unit molomUnit;

    public Text dialogue;

    public BattleHud playerHud;
    public BattleHud enemyHud;
    public BattleHud ignumHud;
    public BattleHud molomHud;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    //Sets up the battle ui and components
    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        GameObject ignumGo = Instantiate(ignumPrefab, ignumStation);
        ignumUnit = ignumGo.GetComponent<Unit>();

        GameObject molomGo = Instantiate(molomPrefab, molomStation);
        molomUnit = molomGo.GetComponent<Unit>();

        dialogue.text = "A wild " + enemyUnit.unitName + " appeared";

        playerHud.SetHud(playerUnit);
        enemyHud.SetHud(enemyUnit);
        ignumHud.SetHud(ignumUnit);
        molomHud.SetHud(molomUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //deals damage
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogue.text = "Attack hit";

        yield return new WaitForSeconds(2f);

        //checks if the enemy is dead otherwise passes to the next turn
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (ignumUnit.currentHP > 0)
        {
            state = BattleState.IGNUMTURN;
           IgnumTurn();
        }
        else if (molomUnit.currentHP > 0)
        {
            state = BattleState.MOLOMTURN;
            MolomTurn();
        }else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator IgnumAttack()
    {
        //deals damage
        bool isDead = enemyUnit.TakeDamage(ignumUnit.damage * 2);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogue.text = "Its Super effective";

        yield return new WaitForSeconds(2f);

        //checks if the enemy is dead otherwise passes to the next turn
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if(molomUnit.currentHP > 0)
        {
            state = BattleState.MOLOMTURN;
            MolomTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator MolomAttack()
    {
        //deals damage
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogue.text = "Attack hit";

        yield return new WaitForSeconds(2f);
        
        //checks if the enemy is dead otherwise passes to the next turn
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //auto runs the enemy turn
    IEnumerator EnemyTurn()
    {
        dialogue.text = enemyUnit.unitName + " attacks";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        ignumUnit.TakeDamage(enemyUnit.damage);
        molomUnit.TakeDamage(enemyUnit.damage * 2);

        playerHud.SetHP(playerUnit.currentHP);
        molomHud.SetHP(molomUnit.currentHP);
        ignumHud.SetHP(ignumUnit.currentHP);


        yield return new WaitForSeconds(1f);

        //checks if the PC is dead otherwise passes to the next turn
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogue.text = "Congrats you won!!";
        } else if (state == BattleState.LOST)
        {
            dialogue.text = "Sorry, you lost";
        }
    }

    //The next 3 methods tell you whos turn it is
    void PlayerTurn()
    {
        dialogue.text = "Dante's turn";
    }

    void IgnumTurn()
    {
        dialogue.text = "Ignum's turn";
    }

    void MolomTurn()
    {
        dialogue.text = "Molom's turn";
    }

    //next 3 methods call healing for different characters
    IEnumerator PlayerHeal()
    {
        playerUnit.PlayerHeal(5);
        playerHud.SetHP(playerUnit.currentHP);
        dialogue.text = "You feel better!";

        yield return new WaitForSeconds(2f);

        state = BattleState.IGNUMTURN;
        IgnumTurn();
    }

    IEnumerator IgnumHeal()
    {
        ignumUnit.PlayerHeal(5);
        ignumHud.SetHP(ignumUnit.currentHP);
        dialogue.text = "You feel better!";

        yield return new WaitForSeconds(2f);

        state = BattleState.MOLOMTURN;
        MolomTurn();
    }

    IEnumerator MolomHeal()
    {
        molomUnit.PlayerHeal(5);
        molomHud.SetHP(molomUnit.currentHP);
        dialogue.text = "You feel better!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //connects the attack button to the attack action
    public void OnAttackButton()
    {
        
            
        if(state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack());
        else if(state == BattleState.IGNUMTURN)
            StartCoroutine(IgnumAttack());
        else if(state == BattleState.MOLOMTURN)
            StartCoroutine(MolomAttack());
        else return;

    }

    //connects heal button to heal action
    public void OnHealButton()
    {
           
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerHeal());
        else if (state == BattleState.IGNUMTURN)
            StartCoroutine(IgnumHeal());
        else if (state == BattleState.MOLOMTURN)
            StartCoroutine(MolomHeal()); 
        else return;
    }
}
