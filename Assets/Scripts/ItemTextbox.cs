using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTextbox : Textboxbase {
	public GameObject skipbutton;
	public GameObject contextbuttons;

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	public void showbuttons()
	{
		skipbutton.SetActive (false);
		contextbuttons.SetActive (true);
	}
	public override void skiptext()
	{
		if (base.istyping)
			canceltyping = true;
		else if (!istyping && page < tbcsplit.Length - 1) 
		{
			page++;
			base.StartCoroutine (base.typewriter (tbcsplit [page]));
		}
		else
		{
			
		}
	}
	public void yesbutton()
	{

	}
	public void nobutton()
	{
		
	}
}
