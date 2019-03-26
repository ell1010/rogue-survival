using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class Pathfinding : MonoBehaviour {
	#region Singleton
	public static Pathfinding instance;
	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("DUPLICATE PATHFINDING");
		}
		instance = this;
	}
	#endregion
	public Tilemap tilemap;
	public Grid tgrid;
	node[,] nodegraph;
	GameObject player;
	Vector3 playerpos;
	List<node> currentPath = new List<node>();
	public GameObject gnode;
	public LineRenderer line;
	public Vector3[] points;
	public LineGenerator linegen;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerController> ().pf = this;

		//caches the players postion
		playerpos = player.transform.position;
		tilemap = GetComponent<Tilemap> ();

		createnodes ();
	}
	
	void Update ()
	{

	}
	public Vector2Int getttile()
	{

		Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int coord = tilemap.WorldToCell(mouseworldpos);

		coord.x = (int)Math.Floor(tilemap.GetCellCenterWorld(coord).x) + Mathf.Abs(tilemap.origin.x);
		coord.y = (int)Math.Floor(tilemap.GetCellCenterWorld(coord).y) + Mathf.Abs(tilemap.origin.y);

		return new Vector2Int (coord.x, coord.y);
	}
	public Vector2Int getplayertile()
	{
		return new Vector2Int(tilemap.WorldToCell(playerpos).x - tilemap.origin.x , tilemap.WorldToCell(playerpos).y - tilemap.origin.y);
	}

	public void updateplayerpos()
	{
		playerpos = player.transform.position;
		print(player.transform.position);
	}
	
	public float getTileDistance(int endx, int endy)
	{
		//print((tilemap.WorldToCell(player.transform.position).x) + " " + (tilemap.WorldToCell(player.transform.position).y));

		return nodegraph[tilemap.WorldToCell(player.transform.position).x - tilemap.origin.x ,
			tilemap.WorldToCell(player.transform.position).y - tilemap.origin.y].distanceto(nodegraph[endx , endy]);
	}

	public void createnodes()
	{

		Vector3Int startpos = tilemap.origin;
		Vector3Int size = tilemap.size;

		Vector3Int endpoint = ((startpos + new Vector3Int(Mathf.Abs(startpos.x), Mathf.Abs(startpos.y),0)) + size)-Vector3Int.one;

		nodegraph = new node[size.x, size.y];

		for (int x = 0; x <= endpoint.x; x++)
		{
			for (int y = 0; y <= endpoint.y; y++)
			{
				
				nodegraph [x, y] = new node ();
				nodegraph [x, y].x = x - (Mathf.Abs (startpos.x));
				nodegraph [x, y].y = y - (Mathf.Abs (startpos.y));

				GameObject newnode = GameObject.Instantiate (gnode, new Vector3 (nodegraph [x, y].x, nodegraph [x, y].y, -1), Quaternion.identity);
				newnode.name = (nodegraph [x, y].x.ToString () + nodegraph [x, y].y.ToString ());
				

			}
		}

		for (int x = 0; x <= endpoint.x; x++) 
		{
			for (int y = 0; y <= endpoint.y; y++) 
			{
                //adds neighbours
				if (nodegraph [x, y].walkable(tilemap,new Vector3Int(nodegraph [x, y].x,nodegraph [x, y].y,0))) {
					if ((x + startpos.x) > startpos.x) {
                        //check left
						if (nodegraph [x-1, y].walkable(tilemap,new Vector3Int(nodegraph [x-1, y].x,nodegraph [x-1, y].y,0))) 
						nodegraph [x, y].neighbours.Add (nodegraph [x - 1, y]);
					}
					if ((x + startpos.x) < (endpoint.x + startpos.x)) {
                        //check right
						if (nodegraph [x+1, y].walkable(tilemap,new Vector3Int(nodegraph [x+1, y].x,nodegraph [x+1, y].y,0))) 
						nodegraph [x, y].neighbours.Add (nodegraph [x + 1, y]);
					}
					if ((y + startpos.y) > startpos.y) {
                        //check down
						if (nodegraph [x, y-1].walkable(tilemap,new Vector3Int(nodegraph [x, y-1].x,nodegraph [x, y-1].y,0))) 
						nodegraph [x, y].neighbours.Add (nodegraph [x, y - 1]);
					}
					if ((y + startpos.y) < (endpoint.y + startpos.y)) {
                        //check up
						if (nodegraph [x, y+1].walkable(tilemap,new Vector3Int(nodegraph [x, y+1].x,nodegraph [x, y+1].y,0))) 
						nodegraph [x, y].neighbours.Add (nodegraph [x, y + 1]);
					}
				} else {
				}

			}
		}
	}
	public float costtotile(int sourceX, int sourceY,int targetX, int targetY)
	{
		float cost = nodegraph[targetX + (Mathf.Abs (tilemap.origin.x)),targetY + (Mathf.Abs (tilemap.origin.y))].movecost(tilemap,new Vector3Int(targetX,targetY,0));
		return cost;
	}

	public void playerpath(int endx, int endy)
	{
		//gets the players current path and nulls it out
		currentPath = new List<node>();
		player.GetComponent<PlayerController>().currentpath = null;

		updateplayerpos();

		//gets the players current position
		int playerposx = tilemap.WorldToCell(playerpos).x + Mathf.Abs(tilemap.origin.x);
		int playerposy = tilemap.WorldToCell(playerpos).y + Mathf.Abs(tilemap.origin.y);

        //print("playerpos" + (tilemap.WorldToCell(playerpos).y));

		genpathto(playerposx , playerposy , endx, endy);
		player.GetComponent<PlayerController>().currentpath = currentPath;
	}

	public void enemypath(GameObject enemy)
	{
		currentPath = new List<node>();

		int playerposx = tilemap.WorldToCell(playerpos).x + Mathf.Abs(tilemap.origin.x);
		int playerposy = tilemap.WorldToCell(playerpos).y + Mathf.Abs(tilemap.origin.y);

		genpathto(tilemap.WorldToCell(enemy.transform.position).x + Mathf.Abs(tilemap.origin.x) , tilemap.WorldToCell(enemy.transform.position).y + Mathf.Abs(tilemap.origin.y), playerposx, playerposy);
		enemy.GetComponent<EnemyBase>().currentpath = currentPath;
	}

	void genpathto(int startx, int starty, int endx, int endy)
	{
		
		// creates 2 dictionarys, one for calculating shorted distance, one for all previous shortest nodes (path in reverse)
		Dictionary<node,float> dist = new Dictionary<node, float>();
		Dictionary<node,node> prev = new Dictionary<node, node> ();

		//creates a list for all unvisted nodes
		List<node> unvisited = new List<node> ();
		

		// sets the source and target nodes for the player (start and end points)
		node source = nodegraph [startx,starty];
		node target = nodegraph [endx, endy];
        //print("targetx" + target.x + " targety" + target.y);

		// sets the distance to source node to 0 and nulls it out from the dictionarys
		dist [source] = 0;
		prev [source] = null;

		//sets up each node in dist or have infinite distance cause not all nodes will be reachable and null every node in prev
		foreach (node n in nodegraph) 
		{				
			if (n != source) 
			{
				dist [n] = Mathf.Infinity;
				prev [n] = null;
			}
			unvisited.Add (n);
		}
		//checks each node in unvisited and checks the distance and generates the shortest distance/cost path
		while (unvisited.Count > 0) 
		{
			node u = null;
			foreach (node possibleu in unvisited) 
			{
				if (u == null || dist [possibleu] < dist [u]) 
					u = possibleu;
			}
			if (u == target) 
				break;
			unvisited.Remove (u);
			foreach (node v in u.neighbours) 
			{
				float alt = dist [u] + u.distanceto (v);
				if (alt < dist [v]) 
				{
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}
		//breaks if there isnt a path
		if (prev [target] == null) 
			return;
		node curr = target;
		//go through prev adding each one to the current path
		while (curr != null) 
		{
			currentPath.Add (curr);
			curr = prev [curr];
			//print("hello");
		}
		//error checking
		//if (curr == null) 
		//	print (curr);
        //reverses current path to get correct order
        currentPath.Reverse ();
		linegen.createline(currentPath);
	}
	void setPath()
	{
		
	}

	public	class node 
	{
		public List<node> neighbours;
		public int x;
		public int y;
		public int movecost(Tilemap tilemap, Vector3Int tilepos)
		{
			//get the name of the tile and returns an int depending on what tile it picked
			if (tilemap.HasTile (tilepos)) {
				if (tilemap.GetTile (tilepos).name == "GrassTile") {
					return (1);
				} else
					return(2);
			} else
				return (2);
		}
		public bool walkable(Tilemap tilemap, Vector3Int tilepos)
		{
			//same as movecost but determines if a tile is walkable
			if (tilemap.HasTile (tilepos)) {
				if (tilemap.GetTile (tilepos).name == "GrassTile") {
					return (true);
				} else
					return(false);
			} else
				return (false);
		}
		public node()
		{
			neighbours = new List<node> ();
		}
		public float distanceto(node n)
		{
			//calculates the distance
			Vector2 dir = new Vector2(x , y) - new Vector2(n.x , n.y);
			float length = dir.sqrMagnitude;
			print("distance "+length);
			//returns the distance as a float
			return length;
			
		}
	}
}