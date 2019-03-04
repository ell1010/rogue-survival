using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "Keybinding")]
public class Keybindings : ScriptableObject 
{
	[System.Serializable]
	public class KeybindingCheck
	{
		public KeybindActions keyAction;
		public KeyCode keyCode;
	}

	public KeybindingCheck[] keybinds;

}
