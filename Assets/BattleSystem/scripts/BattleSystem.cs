using System.Collections;
using UnityEditorInternal;
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
	public Button attackOne;
	public Button attackTwo;

	//sprites used to represent attacks
	public GameObject tornadoPunch;
	public GameObject charge;
	public GameObject lunapillarAttack;

	private bool continueBattle;

	private void Awake()
    {
		//listeners for when the players attack buttons are clicked
		attackOne.onClick.AddListener(AttackOneClicked);
		attackTwo.onClick.AddListener(AttackTwoClicked);
	}

    void Start()
	{
		continueBattle = false;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			if(state == BattleState.START)
            {

            }
		}
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
		else if (playerUnit.speed < enemyUnit.speed)
		{
			charge.SetActive(false);
			tornadoPunch.SetActive(false);
			PlayerTurn();
		}
		else
		{
			charge.SetActive(false);
			tornadoPunch.SetActive(false);
			
			if(continueBattle)
            {
				Debug.Log("here");
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyTurn());
			}
            
			
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";
		lunapillarAttack.SetActive(true);

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
			lunapillarAttack.SetActive(false);
			StartCoroutine(PlayerAttack());
		}
		else
		{
			lunapillarAttack.SetActive(false);
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

	private void AttackOneClicked()
    {
		playerUnit.currentMove = attackOne.GetComponentInChildren<Text>().text;
		charge.SetActive(true);
		Debug.Log(playerUnit.currentMove);
	}

	private void AttackTwoClicked()
    {
		playerUnit.currentMove = attackTwo.GetComponentInChildren<Text>().text;
		tornadoPunch.SetActive(true);
		Debug.Log(playerUnit.currentMove);
	}
}
