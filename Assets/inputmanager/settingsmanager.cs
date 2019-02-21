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

		GraphicsRaycaster = Canvas.GetComponent<GraphicRaycaster> ();
		print(keybinds.keybinds[(int)KeybindActions.jump].keyCode);
	}
	public bool JumpPressed()
	{ 
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.jump].keyCode);
	}

	public bool LeftMouseClick()
	{
		return Input.GetMouseButtonDown (int.Parse(keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString().Substring(5)));
	}
	public bool UpPressed()
	{
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.up].keyCode);
	}
	public bool DownPressed()
	{
		return Input.GetKeyDown (keybinds.keybinds [(int)KeybindActions.down].keyCode);
	}
	void Update ()
    {
		//click = Input.GetMouseButtonDown (int.Parse (keys ["select"].ToString ().Substring (5)));
		clicked = null;
		if (LeftMouseClick()) 
		{
			print ("click");

			pointerevent = new PointerEventData (eventsystem);
			pointerevent.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult> ();


            GraphicsRaycaster.Raycast(pointerevent, results);
            if (results.Count > 0)
            {
                clicked = results[0].gameObject;
            }
            else if (results.Count == 0)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.transform != null)
                {
                    print("clicked" + hit);
                    clicked = hit.transform.gameObject;
                }
            }
            else
            {
                clicked = null;
            }
			
		}

	}

}





