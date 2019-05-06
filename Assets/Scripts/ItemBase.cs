using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {
	public Item item;
	// Use this for initialization
	public virtual void Start () {
		GetComponent<SpriteRenderer>().sprite = item.icon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void use()
    {

    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().playerpickup(item, this.gameObject);
    }
}
