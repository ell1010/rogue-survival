using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour {
	public Playerinformation pinfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadbutton()
	{
		SceneManager.LoadScene(2);
	}

	public void newbutton()
	{
		pinfo.name = "";
		pinfo.PlayerLevel = 0;
		SceneManager.LoadScene(2);
	}
}
