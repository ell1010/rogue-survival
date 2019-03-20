using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour {
	public GameObject player;
	public int health = 20;
	public int attack;
	public int range;
	public int movement;
	public enum enemystates {idle,move,attack,dead };
	public enemystates CurrentState;
	public List<Pathfinding.node> currentpath = null;

	// Use this for initialization
	public virtual void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		turnmanager.instance.enemydostuff += startturn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void takeDamage(int damage)
	{
		health -= damage;
		print(health);
	}
	void startturn()
	{
		changestate(enemystates.idle);
	}
	void changestate(enemystates NewState)
	{
		CurrentState = NewState;
		switch (NewState)
		{
			case enemystates.idle:
				{
					enemyidle();
				}
			break;
			case enemystates.move:
				{
					enemymove();
				}
			break;
			case enemystates.attack:
				{
					enemyattack();
				}
			break;
			case enemystates.dead:
				{
					enemydead();
				}
			break;
			default:
			break;
		}
	}
	public virtual void enemyidle()
	{
		//Vector2Int playerpos = Pathfinding.instance.getttile();
		float distance = Pathfinding.instance.getTileDistance((int)Math.Floor(transform.position.x),(int)Math.Floor(transform.position.y));
		if (distance > range)
		{
			enemymove();
		}
		else if (distance < range)
		{
			enemyattack();
		}
	}

	public virtual void enemymove()
	{
		Pathfinding.instance.enemypath(this.gameObject);
	}

	public virtual void enemyattack()
	{

	}

	public virtual void enemydead()
	{
		turnmanager.instance.enemydostuff -= startturn;
	}
}
