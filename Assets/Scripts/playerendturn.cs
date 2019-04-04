using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerendturn : MonoBehaviour {
	Button but;
	bool buttondisabled = false;

	// Use this for initialization
	void Start ()
	{
		but = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (!buttondisabled && turnmanager.instance.CurrentState != turnmanager.TurnStates.PlayerTurn)
		{
			but.interactable = false;
			buttondisabled = true;
		}
		else if (buttondisabled)
		{
			buttondisabled = false;
			but.interactable = true;
		}

	}
	public void endturn()
	{
		turnmanager.instance.changeturn();
	}
}
