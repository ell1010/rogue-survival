using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour {
	public GameObject player;
	public int health = 20;
	public int attack;
	public int range;
	public int startmovement;
	public int movement;
	public enum enemystates {idle,move,attack,dead };
	public enemystates CurrentState;
	public List<Pathfinding.node> currentpath = null;
	public GameObject item;

	// Use this for initialization
	public virtual void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		Pathfinding.node currentnode = Pathfinding.instance.GetNode(transform.position);
		currentnode.occupied = true;
		turnmanager.instance.enemies.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void takeDamage(int damage)
	{
		health -= damage;
		StartCoroutine(damageflash());
		if (health <= 0)
		{
			enemydead();
		}
	}
	IEnumerator damageflash()
	{
		SpriteRenderer sp = transform.GetChild(0).GetComponent<SpriteRenderer>();
	
		sp.color = Color.black;
		yield return new WaitForSeconds(0.05f);

		sp.color = Color.white;
		yield return new WaitForSeconds(0.05f);
		yield break;
	}
	public void startturn()
	{
		movement = startmovement;
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
					StartCoroutine(enemymove());
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
		float distance = Pathfinding.instance.enemyGetTileDistance((int)Math.Floor(transform.position.x),(int)Math.Floor(transform.position.y));
		if (distance > range)
		{
			changestate(enemystates.move);
		}
		else if (distance <= range)
		{
			changestate(enemystates.attack);
		}
	}

	public virtual IEnumerator enemymove()
	{
		float moveper = 0;
		Pathfinding.instance.enemypath(this.gameObject);
		while (movement > 0)
		{
			moveper = 0;
			if (checknexttile(currentpath[1]))
			{
				currentpath = null;
				yield break;
			}
			currentpath[0].occupied = false;
			while (moveper < 1 && movement > 0)
			{
				transform.position = Vector2.Lerp(new Vector2(currentpath[0].x , currentpath[0].y) , new Vector2(currentpath[1].x , currentpath[1].y) , moveper);
				moveper += Time.deltaTime * 3f;
				yield return null;
			}
			moveper = 1;
			movement -= (int)Pathfinding.instance.costtotile(currentpath[1].x , currentpath[1].y);
			transform.position = new Vector3(currentpath[1].x , currentpath[1].y , 0);
			currentpath.RemoveAt(0);
			currentpath[0].occupied = true;
			if (currentpath.Count == 1)
			{
				currentpath = null;
			}
			if (checkforplayer() && movement != 0)
			{
				changestate(enemystates.attack);
				yield break;
			}
			
			yield return new WaitForSeconds(0.02f);
		}
		endturn();
		yield break;
	}
	bool checknexttile(Pathfinding.node tile)
	{
		if (tile.occupied)
			return true;
		else
			return false;
	}
	public virtual bool checkforplayer()
	{
		float distance = Pathfinding.instance.enemyGetTileDistance((int)Math.Floor(transform.position.x) , (int)Math.Floor(transform.position.y));
		if (distance <= range)
		{
			return true;
		}
		else
			return false;
	}
	public virtual void enemyattack()
	{
        player.GetComponent<PlayerController>().playertakedamage(attack);
        endturn();
	}

	public virtual void enemydead()
	{
		turnmanager.instance.enemies.Remove(this.gameObject);
		Instantiate(item, transform.GetChild(0).position, Quaternion.identity);
		Pathfinding.node currentnode = Pathfinding.instance.GetNode(transform.position);
		currentnode.occupied = false;
		player.GetComponent<PlayerController>().pinfo.experience += 2;
		Destroy(this.gameObject);

	}
	public virtual void endturn()
	{
		turnmanager.instance.nextenemy();
	}
}
