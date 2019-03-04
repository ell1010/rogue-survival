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
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(finishedtyping && page == tbcsplit.Length-1)
		{
			showbuttons();
			finishedtyping = false;
		}
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
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playergetitem();
		base.skiptext();
	}
	public void nobutton()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerignoreitem();
		base.skiptext();
	}
}
