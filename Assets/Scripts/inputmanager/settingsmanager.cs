﻿using System.Security.Cryptography;
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

	//public GameObject clicked;
	public GameObject Canvas;

	public GraphicRaycaster GraphicsRaycaster;
	public PointerEventData pointerevent;
	public EventSystem eventsystem;
	int layermask = 1 << 8;

	// Use this for initialization
	void Awake () {
        if (sm != null) 
		{
			DestroyImmediate (gameObject);
			return;
		}
        DontDestroyOnLoad (gameObject);
		sm = this;
		//layermask = ~layermask;
		GraphicsRaycaster = Canvas.GetComponent<GraphicRaycaster> ();
		//print(keybinds.keybinds[(int)KeybindActions.jump].keyCode);
	}

	public bool JumpPressed()
	{ 
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.jump].keyCode);
	}

	public bool LeftMouseClickDown()
	{
		return Input.GetMouseButtonDown (int.Parse(keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString().Substring(5)));
	}
	public bool LeftMouseClickUp()
	{
		return Input.GetMouseButtonUp(int.Parse(keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString().Substring(5)));
	}
	public bool RightMouseButtonDown()
	{
		return Input.GetMouseButtonDown(int.Parse(keybinds.keybinds[(int)KeybindActions.attack].keyCode.ToString().Substring(5)));
	}
	public bool RightMouseButtonUp()
	{
		return Input.GetMouseButtonUp(int.Parse(keybinds.keybinds[(int)KeybindActions.attack].keyCode.ToString().Substring(5)));
	}

	public bool UpPressed()
	{
		return Input.GetKeyDown(keybinds.keybinds [(int)KeybindActions.up].keyCode);
	}
	public bool DownPressed()
	{
		return Input.GetKeyDown (keybinds.keybinds [(int)KeybindActions.down].keyCode);
	}

	public GameObject Clicked()
	{
		if (LeftMouseClickDown() || RightMouseButtonDown())
		{
			print("click");

			pointerevent = new PointerEventData(eventsystem);
			pointerevent.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();


			GraphicsRaycaster.Raycast(pointerevent , results);
			if (results.Count > 0)
			{
				return results[0].gameObject;
			}
			else if (results.Count == 0)
			{

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast(ray.origin , ray.direction,10, layermask);
				if (hit.transform != null)
				{
					print("clicked" + hit);
					return hit.transform.gameObject;
				}
				else
					return null;
			}
			else
			{
				return null;
			}

		}
		else
			return null;
	}
	void Update ()
    {
		//clicked = null;
		//if (LeftMouseClickDown() || RightMouseButtonDown())
		//{
		//	print("click");

		//	pointerevent = new PointerEventData(eventsystem);
		//	pointerevent.position = Input.mousePosition;
		//	List<RaycastResult> results = new List<RaycastResult>();


		//	GraphicsRaycaster.Raycast(pointerevent , results);
		//	if (results.Count > 0)
		//	{
		//		clicked = results[0].gameObject;
		//	}
		//	else if (results.Count == 0)
		//	{

		//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//		RaycastHit2D hit = Physics2D.Raycast(ray.origin , ray.direction);
		//		if (hit.transform != null)
		//		{
		//			print("clicked" + hit);
		//			clicked = hit.transform.gameObject;
		//		}
		//	}
		//	else
		//	{
		//		clicked = null;
		//	}

		//}
		//else
		//	clicked = null;

	}

}





