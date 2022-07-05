using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int damage;
	public int maxHP;
	public int currentHP;
	//here start inventory
	public int gold;
	public int key;
	public int luck;

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}
	public bool TakeLuck(int unluck)
	{
		luck -= unluck;

		if (luck <= 0)
			return true;
		else
			return false;
	}

	public bool TakeGold(int pay)
{
	gold -= pay;
	
	if (gold <= 0)
		return true;
	else
		return false;
}
}