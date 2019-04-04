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
	TurnStates currentstate;
	public List<GameObject> enemies = new List<GameObject>();
	public TurnStates CurrentState
	{
		get
		{
			return currentstate;
		}
		set
		{
			currentstate = value;
		}
	}
	GameObject player;
	int enemycount;
	// Use this for initialization
	void Awake () {
		if (instance != null)
		{
			Debug.LogWarning("DUPLICATE TURNMANAGER");
		}
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player");
		Changestate(TurnStates.PlayerTurn);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void changeturn()
	{
		if (enemies.Count <= 0)
		{
			Changestate(TurnStates.NoCombat);
		}
		if (CurrentState == TurnStates.PlayerTurn)
			Changestate(TurnStates.EnemyTurn);
		else if (CurrentState == TurnStates.EnemyTurn)
			Changestate(TurnStates.PlayerTurn);

	}

	public void Changestate(TurnStates NewState)
	{
		currentstate = NewState;
		switch (NewState)
		{
			case TurnStates.PlayerTurn:
				{
					player.GetComponent<PlayerController>().playerturn();
				}
			break;
			case TurnStates.EnemyTurn:
				{
					//if(enemydostuff != null)
					//enemydostuff();
					enemyturn();
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

	public void nextenemy()
	{
		enemycount++;
		enemyturn();
	}

	void enemyturn()
	{
		//print(enemies.Count);
		if (enemycount < enemies.Count)
		{
			enemies[enemycount].GetComponent<EnemyBase>().startturn();
		}
		else
		{
			print("endenemys");
			enemycount = 0;
			changeturn();
		}
		
	}

}
