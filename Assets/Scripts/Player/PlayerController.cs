using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour 
{
	public Playerinformation pinfo;
	public List<Pathfinding.node> currentpath = null;
	public Pathfinding pf;
	int Movement = 3;
	public int currentmovement
	{
		get
		{
			return Movement;
		}
	}
	public bool turn;
	public GameObject UI;
	bool movingPaused;
	public Item itemAtFeet;
	GameObject tempItem;
	bool canAttack;
	Vector2Int targetpos;
	public GameObject Linegen;

    void Start ()
    {
		canAttack = true;

		targetpos = new Vector2Int(-1 , -1);

		Pathfinding.node currentnode = Pathfinding.instance.GetNode(transform.position);
		currentnode.occupied = true;
		checkplayerinfo();
	}

	void checkplayerinfo()
	{
		if (pinfo.PlayerLevel == 0)
		{
			pinfo.PlayerLevel = 1;
			pinfo.experience = 0;
			pinfo.Health = 20;
			pinfo.maxhealth = 20;
			pinfo.Attack = 2;
			pinfo.Defence = 1;
			pinfo.Range = 1;
			pinfo.movement = 3;
		}
	}
	public void playerturn()
	{
		Movement = pinfo.movement;
		canAttack = true;
	}
	
	void Update () 
	{
		if (turnmanager.instance.CurrentState == turnmanager.TurnStates.PlayerTurn)
		{
			if (currentpath != null)
			{
				int currnode = 0;
				while (currnode < currentpath.Count - 1)
				{
					Vector3 start = new Vector3(currentpath[currnode].x + 0.5f , currentpath[currnode].y + 0.5f);
					Vector3 end = new Vector3(currentpath[currnode + 1].x + 0.5f , currentpath[currnode + 1].y + 0.5f);
					Debug.DrawLine(start , end , Color.blue);
					currnode++;
				}
			}

			if (settingsmanager.instance.LeftMouseButton() && settingsmanager.instance.Clicked() == Pathfinding.instance.tilemap.gameObject)
			{
				//print("test");
				if (targetpos == Pathfinding.instance.getttileatmouse())
				{
					//print("same");
				}
				else
				{
					targetpos = Pathfinding.instance.getttileatmouse();
					//print("different");
					// call pathfinding function
					Pathfinding.instance.playerpath(targetpos.x , targetpos.y);
				}
			}
			if (settingsmanager.instance.LeftMouseButtonUp())
			{
				moveplayer();
			}

			if (settingsmanager.instance.RightMouseButtonDown() && settingsmanager.instance.Clicked().CompareTag("Enemy"))
			{
				print("attack");
				playerAttack();
			}
		}
	}
	public void moveplayer()
	{
		if(!movingPaused)
		StartCoroutine (movetotile ());
		//print("pos" + transform.position);
	}
	public IEnumerator movetotile()
	{
		float moveper = 0;
		while (Movement > 0) {
			//print("movement" + Movement);
			moveper = 0;
			while (movingPaused)
				yield return null;
			if (currentpath == null || currentpath.Count < 1)
			{
				//print("test");
				//Pathfinding.instance.RemoveLinePosition();
				Pathfinding.instance.updateplayerpos();
				yield break;
			}

			if (checknexttile(currentpath[1]))
			{
				currentpath = null;
				yield break;
			}
			currentpath[0].occupied = false;
			while (moveper < 1 && Movement > 0)
			{
				transform.position = Vector2.Lerp(new Vector2(currentpath[0].x,currentpath[0].y),new Vector2(currentpath[1].x,currentpath[1].y),moveper);
				moveper += Time.deltaTime * 1.5f;
				//print("moving");
				yield return null;
			}
			moveper = 1;
            
            Movement -= (int)pf.costtotile(currentpath[1].x,currentpath[1].y);
			transform.position = new Vector3(currentpath[1].x , currentpath[1].y , 0);
			currentpath.RemoveAt (0);
			currentpath[0].occupied = true;

			if(currentpath.Count > 1)
			Linegen.GetComponent<LineGenerator>().createline(currentpath);

			//Linegen.GetComponent<LineGenerator>().updateline(Movement , currentpath.Count);
			if (currentpath.Count == 1)
			{
				//print("curr = null");
				currentpath = null;
			}
			Pathfinding.instance.updateplayerpos();
			yield return new WaitForSeconds (0.02f);
		}
		//Pathfinding.instance.RemoveLinePosition();
		Pathfinding.instance.updateplayerpos();
		//print("test");
		yield break;
	}

	bool checknexttile(Pathfinding.node tile)
	{
		if (tile.occupied)
			return true;
		else
			return false;
	}
	void playerAttack()
	{
		if (canAttack && Movement > 0)
		{
			Vector2Int clickedtile = Pathfinding.instance.getttileatmouse();
			print(clickedtile.x + " " + clickedtile.y);
			float distance = Pathfinding.instance.getTileDistance(clickedtile.x , clickedtile.y);
			if (distance <= pinfo.Range)
			{
				settingsmanager.instance.Clicked().GetComponent<EnemyBase>().takeDamage(pinfo.Attack);
				canAttack = false;
				Movement--;
			}
			else
				print(distance);
		}
	}
	public void playerpickup(Item pickup, GameObject toucheditem)
	{
		itemAtFeet = pickup;
		tempItem = toucheditem;
		UI.transform.GetChild (1).GetComponent<ItemTextbox>().starttext("Would you like to pick up " + itemAtFeet.name + "?");
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

	void experiencegain(int xp)
	{
		int xptonext = pinfo.PlayerLevel * 12;
		pinfo.experience += xp;
		if (pinfo.experience >= xptonext)
		{
			pinfo.experience -= xptonext;
			pinfo.levelup();
		}
	}
    public void playertakedamage(int damage)
    {
		pinfo.Health = (int)Mathf.Floor((damage / pinfo.Defence) + 1);
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		//print ("hello");
	}
}

