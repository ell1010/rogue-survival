using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class settingsmanager : MonoBehaviour {
    public static settingsmanager instance
    {
        get { return sm; }
    }
    private static settingsmanager sm = null;
    //public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
	[SerializeField]
	private Keybindings keybinds;
	public bool click = false;
	public GameObject clicked;
	public GameObject Canvas;
	public GraphicRaycaster GraphicsRaycaster;
	public PointerEventData pointerevent;
	public EventSystem eventsystem;

	// Use this for initialization
	void Awake () {
        if (sm != null) 
		{
			DestroyImmediate (gameObject);
			return;
		}
        DontDestroyOnLoad (gameObject);
		sm = this;
//        keys.Add ("select",KeyCode.Mouse0);
//		keys.Add ("Up", KeyCode.W);
//		keys.Add ("Down", KeyCode.S);
//		keys.Add ("Left", KeyCode.A);
//		keys.Add ("Right",KeyCode.D);
		GraphicsRaycaster = Canvas.GetComponent<GraphicRaycaster> ();
		print(keybinds.keybinds[(int)KeybindActions.jump].keyCode);
	}
	public bool jumppressed()
	{ 
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.jump].keyCode);
	}

	public bool leftmouseclick()
	{
		return Input.GetMouseButtonDown (int.Parse(keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString().Substring(5)));
	}
	public bool uppressed()
	{
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.up].keyCode);
	}
	// Update is called once per frame
	void Update ()
    {
		//click = Input.GetMouseButtonDown (int.Parse (keys ["select"].ToString ().Substring (5)));
		clicked = null;
		if (leftmouseclick()) 
		{
			print ("click");
			pointerevent = new PointerEventData (eventsystem);
			pointerevent.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult> ();
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);
			//GraphicsRaycaster.Raycast
			if (hit.transform != null) 
			{
				print ("clicked" + hit);
				clicked = hit.transform.gameObject;
			} 
			else if(hit.transform == null)
			{
				GraphicsRaycaster.Raycast (pointerevent, results);
				if (results.Count > 0) 
				{
					clicked = results [0].gameObject;
				}
			}else
			{
				clicked = null;
			}
		}

	}
	public bool GetKeyDown()
	{
		return false;
	}
}





