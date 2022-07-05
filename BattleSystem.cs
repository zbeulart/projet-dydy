using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN,ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour


{
	public int diceplayer = 0;
	public int diceunit = 0;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator Pay()
	{
		playerUnit.TakeGold (2);
		yield return new WaitForSeconds(1f);
	}

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerLuck()
	{

		 diceplayer = (Random.Range(1, 7)+(playerUnit.damage));
		 diceunit = (Random.Range(1, 7)+(enemyUnit.damage));

 		if (diceplayer > diceunit)
		{		
			playerUnit.TakeLuck(2);
			enemyUnit.TakeDamage(4);
			playerHUD.SetLuck(playerUnit.luck);
			enemyHUD.SetHP(enemyUnit.currentHP);
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
		else 
		{		
			playerUnit.TakeLuck(2);
			playerUnit.TakeDamage(2);
			playerHUD.SetHP(playerUnit.currentHP);
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	IEnumerator PlayerAttack()
	{

		 diceplayer = (Random.Range(1, 7)+(playerUnit.damage));
		 diceunit = (Random.Range(1, 7)+(enemyUnit.damage));

 		if (diceplayer > diceunit)
		{		
			enemyUnit.TakeDamage(2);
			enemyHUD.SetHP(enemyUnit.currentHP);
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
		else 
		{
			playerUnit.TakeDamage(2);
			playerHUD.SetHP(playerUnit.currentHP);
			yield return new WaitForSeconds(1f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		if (enemyUnit.currentHP <= 0)
		{	
			yield return new WaitForSeconds(1f);
			state = BattleState.WON;
			EndBattle();
		} 
		else if(playerUnit.currentHP <= 0)
		{
			yield return new WaitForSeconds(1f);
			state = BattleState.LOST;
			EndBattle();
		}
		else 
		{
			yield return new WaitForSeconds(1f);
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

		public void OnLuckButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerLuck());
	}
	
    	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}
	
    public void OnInventoryButton()
     {
         StartCoroutine(Pay());

     }
}

	
