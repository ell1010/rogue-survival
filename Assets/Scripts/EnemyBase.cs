using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {
	public int health = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void takeDamage(int damage)
	{
		health -= damage;
		print(health);
	}
}
