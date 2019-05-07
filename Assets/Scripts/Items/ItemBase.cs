using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {
	public Item[] items = new Item[5];
	public Item Item;

	// Use this for initialization
	public virtual void Start () {
		Item = items[Random.Range(0, 5)];
		GetComponent<SpriteRenderer>().sprite = Item.icon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void use()
    {

    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			//print(collision.gameObject);
			collision.gameObject.GetComponent<PlayerController>().playerpickup(Item, this.gameObject);
			//Destroy (this);
		}
	}
}
