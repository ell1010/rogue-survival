using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public GameObject[] enemies = new GameObject[2];
	public float spawnchance;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnenemy()
	{
		foreach (Pathfinding.node node in Pathfinding.instance.nodegraph)
		{
			if(Random.Range(0,100) < spawnchance)
			{
				GameObject e = (GameObject)Instantiate(enemies[Random.Range(0, 2)], new Vector3(node.x, node.y, -1), Quaternion.identity);
				//print("spawn enemy");
			}
		}
	}
}
