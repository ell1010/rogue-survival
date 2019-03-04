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
	public Item itemAtFeet;
	GameObject tempitem;

    private void Awake()
    {
        //gameObject.name = playerinfo.PlayerName;
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
		if (settingsmanager.instance.RightMouseButtonDown() && settingsmanager.instance.clicked.tag == "Enemy")
		{
			print("enemy");
		}
	}
	public void moveplayer()
	{
		if(!movingPaused)
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
		Pathfinding.instance.RemoveLinePosition();
		yield break;
	}
	public void playerpickup(Item pickup, GameObject toucheditem)
	{
		itemAtFeet = pickup;
		tempitem = toucheditem;
		UI.transform.GetChild (1).gameObject.SetActive (true);
		movingPaused = true;
	}
	public void playergetitem()
	{
		movingPaused = false;
		bool pickedUp = PlayerInventory.instance.Add(itemAtFeet);
		if (pickedUp)
			Destroy(tempitem);
		else
		{
			string test = "Inventory full!";
			UI.transform.GetChild(2).GetComponent<basictextbox>().starttext(test);
		}
	}
	public void playerignoreitem()
	{
		movingPaused = false;
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		print ("hello");
	}
}

