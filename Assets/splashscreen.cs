using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;



public class splashscreen : MonoBehaviour {
	public VideoPlayer vp;
	public saveandload sl;
	// Use this for initialization
	void Start () {
		vp.loopPointReached += loadmenu;
		loadsetting();
	}

	void loadsetting()
	{
		sl.Load();
	}
	
	// Update is called once per frame
	void loadmenu (VideoPlayer vid) {
		SceneManager.LoadScene(1);
	}
}
