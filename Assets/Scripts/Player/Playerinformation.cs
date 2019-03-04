using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player Information")]
public class Playerinformation : ScriptableObject {

    public string PlayerName;
    public int PlayerLevel;
    [System.Serializable]
    public class PlayerInventory
    {
        public GameObject Item;
        public int ItemAmount;
    }
    public PlayerInventory[] playerinv;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
