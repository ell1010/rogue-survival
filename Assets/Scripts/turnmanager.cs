using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnmanager : MonoBehaviour {
	public static turnmanager instance;
	public enum TurnStates
	{
		PlayerTurn,
		EnemyTurn,
		NoCombat
	}
	public delegate void startenemyturn();
	public startenemyturn enemydostuff;
	GameObject player;

	// Use this for initialization
	void Awake () {
		if (instance != null)
		{
			Debug.LogWarning("DUPLICATE TURNMANAGER");
		}
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void turnstate(TurnStates turn)
	{
		switch (turn)
		{
			case TurnStates.PlayerTurn:
				{
					player.GetComponent<PlayerController>().playerturn();

				}
			break;
			case TurnStates.EnemyTurn:
				{
					if(enemydostuff != null)
					enemydostuff();
				}
			break;
			case TurnStates.NoCombat:
				{
					player.GetComponent<PlayerController>().playerturn();
				}
			break;
			default:
			break;
		}
	}

}
