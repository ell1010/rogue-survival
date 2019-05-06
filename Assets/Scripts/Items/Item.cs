using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
	new public string name = "NewItem";
	public Sprite icon = null;
	public enum type
	{
		boots,
		leggings,
		chestplate,
		helmet,
		weapon,
		usable,
	}
	public type itemtype;
	bool equipped;
	[Range (0,3), SerializeField]
	int range;
	[SerializeField]
	int damage;
	[SerializeField]
	int defence;
	[SerializeField]
	int durability;

	public int[] itemstats
	{
		get
		{
			return new int[] {damage, range, defence, durability };
		}
		set
		{

		}
	}

	public virtual void useItem()
	{

	}
}
