using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	public int health = 20;
	public int attack;
	public int range;

	// Use this for initialization
	void Start () {
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

	}
}
