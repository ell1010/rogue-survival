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
	public GameObject UI;
	bool movingPaused;
	public GameObject itemAtFeet;

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
	public void moveplayer()
	{
		StartCoroutine (movetotile ());
	}
	public IEnumerator movetotile()
	{
		float remainingmovement = movespeed;

		while (remainingmovement > 0) {
			while (movingPaused)
				yield return null;
			if (currentpath == null)
				yield break;
			
			remainingmovement -= pf.costtotile(currentpath[0].x,currentpath[0].y,currentpath[1].x,currentpath[1].y);

			transform.position = new Vector3 (currentpath [1].x, currentpath [1].y, 0);

			currentpath.RemoveAt (0);
			print ("move");

			if (currentpath.Count == 1)
				currentpath = null;
			yield return new WaitForSeconds (0.02f);
		}
		yield break;
	}
	public void playerpickup()
	{
		UI.transform.GetChild (1).gameObject.SetActive (true);
		movingPaused = true;
	}
	public void playergetitem()
	{
		movingPaused = false;
		Destroy (itemAtFeet);
	}
	void playerignoreitem()
	{
		movingPaused = false;
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		print ("hello");
	}
}

