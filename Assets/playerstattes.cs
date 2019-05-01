using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerstattes : MonoBehaviour {
    public Playerinformation stats;
	// Use this for initialization
	void Start () {
        Text text = GetComponent<Text>();
        text.text = (stats.PlayerName + "\n" + stats.PlayerLevel + "\n" + stats.playerinv.Length);
	}
	
	// Update is called once per frame
	void Update () {
        Text text = GetComponent<Text>();
        text.text = (stats.PlayerName + "\n" + stats.PlayerLevel + "\n" + stats.playerinv.Length);
    }
}
