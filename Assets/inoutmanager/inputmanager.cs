using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputmanager : MonoBehaviour {
	
	public static inputmanager instance
	{
		get { return im; }
	}
	private static inputmanager im = null;

	[SerializeField]
	private Keybindings keybinding;

	void Awake () {
		if (im != null) 
		{
			DestroyImmediate (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);
		im = this;
	}

//	public KeyCode GetKeyforaction(KeybindActions keybindingaction)
//	{
//		foreach (Keybindings.KeybindingCheck keybindingcheck in keybinding.keybinds) 
//		{
//			if (keybindingcheck.keybindingaction == keybindingaction)
//				return keybindingcheck.keycode;
//		}
//		return KeyCode.None;
//	}
//
//	public bool GetKeyDown(KeybindActions key)
//	{
//		foreach (Keybindings.KeybindingCheck keybindingcheck in keybinding.keybinds)
//			if(keybindingcheck.keybindingaction == key)
//			return Input.GetKeyDown (keybindingcheck.keycode);
//		return false;
//	}
//
//	public bool GetKey(KeybindActions key)
//	{
//		foreach (Keybindings.KeybindingCheck keybindingcheck in keybinding.keybinds)
//			if(keybindingcheck.keybindingaction == key)
//				return Input.GetKey (keybindingcheck.keycode);
//		return false;
//	}
//
//	public bool GetKeyUp(KeybindActions key)
//	{
//		foreach (Keybindings.KeybindingCheck keybindingcheck in keybinding.keybinds)
//			if(keybindingcheck.keybindingaction == key)
//				return Input.GetKeyUp (keybindingcheck.keycode);
//		return false;
//	}
}
