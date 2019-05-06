﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player Information")]
public class Playerinformation : ScriptableObject {

    public string PlayerName;
    public int PlayerLevel;
	public int experience;
	public int Health;
	public int Attack;
	public int Defence;
	public int Range;
	public int movement;


	public void levelup()
	{
		int x = Random.Range(0, 1);
		switch (x)
		{
			case 0: Attack++;
				break;
			case 1: Defence++;
				break;
		}
		Health += 2;
		PlayerLevel++;
	}


    public PlayerInventory[] playerinv;

	[System.Serializable]
	public class PlayerInventory
	{
		public GameObject Item;
		public int ItemAmount;
	}
}
