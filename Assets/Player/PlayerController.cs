using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour 
{
	public List<Pathfinding.node> currentpath = null;
	public Pathfinding pf;
	int movespeed = 2;
    public Playerinformation playerinfo;

    private void Awake()
    {
        gameObject.name = playerinfo.PlayerName;
    }

    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentpath != null) 
		{
			int currnode = 0;
			while (currnode < currentpath.Count - 1) 
			{
				Vector3 start = new Vector3(currentpath[currnode].x + 0.5f,currentpath[currnode].y + 0.5f);
				Vector3 end =new Vector3(currentpath[currnode+1].x + 0.5f,currentpath[currnode+1].y + 0.5f);
				Debug.DrawLine (start, end,Color.blue);	
				currnode++;
			}
		}
	}
	public void movetotile()
	{
		float remainingmovement = movespeed;

		while (remainingmovement > 0) {
			if (currentpath == null)
				return;
			
			remainingmovement -= pf.costtotile(currentpath[0].x,currentpath[0].y,currentpath[1].x,currentpath[1].y);

			transform.position = new Vector3 (currentpath [1].x, currentpath [1].y, 0);

			currentpath.RemoveAt (0);

			if (currentpath.Count == 1)
				currentpath = null;
		}
	}
	public void playerpickup()
	{
		
	}
}

