using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpitem : ItemBase {

	// Use this for initialization
	public override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player") {
			print (collision.gameObject);
			collision.gameObject.GetComponent<PlayerController> ().playerpickup (Item, this.gameObject);
			//Destroy (this);
		}
	}
}
