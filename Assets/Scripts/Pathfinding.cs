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
	List<node> currentPath = new List<node>();
	public GameObject gnode;
	public LineRenderer line;
	public Vector3[] points;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerController> ().pf = this;
		tilemap = GetComponent<Tilemap> ();
		line = GetComponent<LineRenderer>();
		createnodes ();
	}
	
	void Update () {
		if(settingsmanager.instance.LeftMouseClickDown() && settingsmanager.instance.Clicked() == tilemap.gameObject )
		{
            //get the tile that was clicked
			Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3Int coord = tilemap.WorldToCell (mouseworldpos);
            // call pathfinding funftion
			genpathto (
				((int)Math.Floor(tilemap.GetCellCenterWorld (coord).x) + Mathf.Abs(tilemap.origin.x)) , 
				((int)Math.Floor(tilemap.GetCellCenterWorld(coord).y) + Mathf.Abs(tilemap.origin.y))
			);
		}
		if (settingsmanager.instance.LeftMouseClickUp())
		{
			setPath();
		}
		if (settingsmanager.instance.RightMouseButtonDown())
		{

		}
	}
	public Vector2Int getttile()
	{
		Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3Int coord = tilemap.WorldToCell(mouseworldpos);
		return new Vector2Int (coord.x, coord.y);
	}
	
	public float getTileDistance(int endx, int endy)
	{
		//print((tilemap.WorldToCell(player.transform.position).x + tilemap.origin.x) + " " + (tilemap.WorldToCell(player.transform.position).y + tilemap.origin.y));
		return nodegraph[tilemap.WorldToCell(player.transform.position).x - tilemap.origin.x ,
			tilemap.WorldToCell(player.transform.position).y - tilemap.origin.y].distanceto(nodegraph[endx - tilemap.origin.x , endy - tilemap.origin.y]);
	}

	public void createnodes()
	{

		Vector3Int startpos = tilemap.origin;
		Vector3Int size = tilemap.size;

		Vector3Int endpoint = ((startpos +
			new Vector3Int(Mathf.Abs(startpos.x), Mathf.Abs(startpos.y),0))
			+ size)-Vector3Int.one;
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

	void genpathto(int x, int y)
	{
		//gets the players current path and nulls it out
		currentPath = new List<node>();
		player.GetComponent<PlayerController> ().currentpath = null;
		// creates 2 dictionarys, one for calculating shorted distance, one for all previous shortest nodes (path in reverse)
		Dictionary<node,float> dist = new Dictionary<node, float>();
		Dictionary<node,node> prev = new Dictionary<node, node> ();

		//creates a list for all unvisted nodes
		List<node> unvisited = new List<node> ();

		//caches the players postion
		Vector3 playerpos = player.transform.position;

		//gets the players current position
		int playerposx =tilemap.WorldToCell (playerpos).x+Mathf.Abs(tilemap.origin.x);
		int playerposy = tilemap.WorldToCell (playerpos).y+Mathf.Abs(tilemap.origin.y);

		// sets the source and target nodes for the player (start and end points)
		node source = nodegraph [playerposx,playerposy];
		node target = nodegraph [x, y];
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
		}
		//error checking
		if (curr == null) 
			print ("null node");
		//reverses current path to get correct order
		currentPath.Reverse ();
		linerenderer();
	}
	void setPath()
	{
		player.GetComponent<PlayerController>().currentpath = currentPath;
	}

	public void linerenderer()
	{
		line.positionCount = 0;
		line.material = new Material(Shader.Find("Sprites/Default"));
		line.startWidth = 0.05f;
		line.endWidth = 0.05f;
		line.startColor = Color.blue;
		line.endColor = Color.blue;
		line.positionCount = currentPath.Count;
		points = new Vector3[currentPath.Count];
		for (int i = 0; i < currentPath.Count; i++)
		{
			points[i] = new Vector3(currentPath[i].x + 0.5f , currentPath[i].y + 0.5f , -0.1f);
		}
		points = points.Reverse().ToArray();
		line.SetPositions(points);
		line.alignment = LineAlignment.View;
	}

	public void RemoveLinePosition()
	{
		line.positionCount = currentPath.Count;
		print("hello");
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
			//calculates distance to the next node node
			return Vector2.Distance (new Vector2 (x, y), new Vector2 (n.x, n.y));
		}
	}
}