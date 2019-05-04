using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OptionValues", menuName = "Options")]
public class OptionValues : ScriptableObject {
    public Vector2Int resolution;
    public bool fullscreen;
    public float mainvol;
    public float effvol;
    public float bgvol;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
