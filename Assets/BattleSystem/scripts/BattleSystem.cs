﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD1 playerHUD;
	public BattleHUD1 enemyHUD;

	public BattleState state;

	public GameObject attackMenu;

	// Start is called before the first frame update
	void Start()
	{
		state = BattleState.START;
		StartCoroutine(SetupBattle());
	}

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		
		bool isDead = enemyUnit.TakeDamage(playerUnit);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else if(playerUnit.speed < enemyUnit.speed)
        {
			PlayerTurn();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else if(playerUnit.speed < enemyUnit.speed)
        {
			StartCoroutine(PlayerAttack());
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
			dialogueText.text = "You won the battle!";
			SceneManager.LoadScene("Hunters Dev Room");
		}
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
			SceneManager.LoadScene("Hunters Dev Room");
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
		attackMenu.SetActive(true);
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		attackMenu.SetActive(false);

		//compares the speeds of the player and enemy and decides which one goes first 
		if (playerUnit.speed > enemyUnit.speed)
        {
			StartCoroutine(PlayerAttack());
		}
		else if(playerUnit.speed < enemyUnit.speed)
        {
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;
		attackMenu.SetActive(false);
		StartCoroutine(PlayerHeal());
	}

}
