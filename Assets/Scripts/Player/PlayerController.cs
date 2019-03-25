using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour 
{
	public List<Pathfinding.node> currentpath = null;
	public Pathfinding pf;
	public int startmovement = 3;
	int Movement = 3;
    public Playerinformation playerinfo;
	public GameObject UI;
	bool movingPaused;
	public Item itemAtFeet;
	GameObject tempItem;
	bool canAttack;
	int attackDamage = 2;
	int attackDistance = 3;
	Vector2Int targetpos;


	private void Awake()
    {

    }
    void Start ()
    {
		canAttack = true;

		targetpos = new Vector2Int(-1 , -1);
	}
	public void playerturn()
	{
		Movement = startmovement;
		canAttack = true;
	}
	
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

		if (settingsmanager.instance.LeftMouseButton() && settingsmanager.instance.Clicked() == Pathfinding.instance.tilemap.gameObject)
		{
            //print("test");
			if (targetpos == Pathfinding.instance.getttile())
			{
				print("same");
			}
			else
			{
				targetpos = Pathfinding.instance.getttile();
				print("different");
                // call pathfinding funftion
                //print(targetpos);
				Pathfinding.instance.playerpath(targetpos.x , targetpos.y);
			}
		}
		if (settingsmanager.instance.LeftMouseButtonUp())
		{
			moveplayer();
		}

		if (settingsmanager.instance.RightMouseButtonDown() && settingsmanager.instance.Clicked().CompareTag("Enemy")) 
		{
			playerAttack();
		}
	}
	public void moveplayer()
	{
		if(!movingPaused)
		StartCoroutine (movetotile ());
		print("pos" + transform.position);
	}
	public IEnumerator movetotile()
	{
		float moveper = 0;
		while (Movement > 0) {
			print("movement" + Movement);
			moveper = 0;
			while (movingPaused)
				yield return null;
			if (currentpath == null || currentpath.Count < 1)
			{
				//print("test");
				Pathfinding.instance.RemoveLinePosition();
				Pathfinding.instance.updateplayerpos();
				yield break;
			}
			while (moveper < 1 && Movement > 0)
			{
				transform.position = Vector3.Slerp(new Vector3(currentpath[0].x,currentpath[0].y,0),new Vector3(currentpath[1].x,currentpath[1].y,0),moveper);
				moveper += Time.deltaTime * 1.5f;
				//print("moving");
				yield return null;
			}
			moveper = 1;
            
            Movement -= (int)pf.costtotile(currentpath[0].x,currentpath[0].y,currentpath[1].x,currentpath[1].y);
			transform.position = new Vector3(currentpath[1].x , currentpath[1].y , 0);
			currentpath.RemoveAt (0);

			if (currentpath.Count == 1)
			{
				//print("curr = null");
				currentpath = null;
			}
			yield return new WaitForSeconds (0.02f);
		}
		Pathfinding.instance.RemoveLinePosition();
		Pathfinding.instance.updateplayerpos();
		//print("test");
		yield break;
	}
	void playerAttack()
	{
		if (canAttack && Movement > 0)
		{
			Vector2Int clickedtile = Pathfinding.instance.getttile();
			print(clickedtile.x + " " + clickedtile.y);
			float distance = Pathfinding.instance.getTileDistance(clickedtile.x , clickedtile.y);
			if (distance <= attackDistance)
			{
				settingsmanager.instance.Clicked().GetComponent<EnemyBase>().takeDamage(attackDamage);
				canAttack = false;
			}
			else
				print(distance);
		}
	}
	public void playerpickup(Item pickup, GameObject toucheditem)
	{
		itemAtFeet = pickup;
		tempItem = toucheditem;
		UI.transform.GetChild (1).gameObject.SetActive (true);
		movingPaused = true;
	}
	public void playergetitem()
	{
		movingPaused = false;
		bool pickedUp = PlayerInventory.instance.Add(itemAtFeet);
		if (pickedUp)
			Destroy(tempItem);
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
		//print ("hello");
	}
}

