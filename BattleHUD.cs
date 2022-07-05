using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text maxHPText;
	public Text currentHPText;
	public Text damageText;
	public Text luckText;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		maxHPText.text = "" + unit.maxHP;
		currentHPText.text = "" + unit.currentHP;
		damageText.text = "" + unit.damage;
		luckText.text = "" +unit.luck;
	}
	public void SetHP(int hp)
	{
		currentHPText.text = "" + hp;
	}
	public void SetLuck(int luck)
	{
		luckText.text = "" + luck;
	}

}
