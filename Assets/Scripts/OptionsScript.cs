using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class OptionsScript : MonoBehaviour {
    public Dropdown ResolutionD;
    Resolution[] resolutions;
    public Toggle FST;
    public Slider mainv;
    public Slider effv;
    public Slider bgv;
    public Button moveb;
    public Button attackb;
    public Button invb;
    public Button pauseb;
	public saveandload SL;
	public OptionValues options;
	public Keybindings keybinds;
	GameObject currentKey;
	int inputtochange;
	// Use this for initialization
	void Start () {
        popuateOptions();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentKey != null)
		{
			foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(kc))
				{
					keybinds.keybinds[inputtochange].keyCode = kc;
					currentKey.transform.GetChild(0).GetComponent<Text>().text = kc.ToString();
					currentKey = null;
				}
			}

			//Event e = Event.current;
			//if (e.isKey)
			//{
			//	keybinds.keybinds[inputtochange].keyCode = e.keyCode;
			//	currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
			//}
		}
	}

    void popuateOptions()
    {
        resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            ResolutionD.options.Add(new Dropdown.OptionData(res.ToString()));
        }
		FST.isOn = options.fullscreen;
		// keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString();
		pauseb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.pause].keyCode.ToString();
		moveb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString();
		attackb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.attack].keyCode.ToString();
		invb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.inventory].keyCode.ToString();
	}

    public void resolutionDropdown()
    {
        Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, Screen.fullScreen);
    }

    public void fullscreen()
    {
        Screen.fullScreen = FST.isOn;
    }

	public void mainvChanged()
	{

	}

	public void effvChanged()
	{

	}

	public void bgvChanged()
	{

	}

	public void assignMove()
	{
		currentKey = moveb.gameObject;
		inputtochange = (int)KeybindActions.select;
	}

	public void assignAttck()
	{
		currentKey = moveb.gameObject;
		inputtochange = (int)KeybindActions.attack;
	}

	public void assignInventory()
	{
		currentKey = moveb.gameObject;
		inputtochange = (int)KeybindActions.inventory;
	}

	public void assignPause()
	{
		currentKey = moveb.gameObject;
		inputtochange = (int)KeybindActions.pause;
	}

	public void saveOptions()
	{
		options.resolution = new Vector2Int(Screen.width, Screen.height);
		options.fullscreen = Screen.fullScreen;
		options.mainvol = mainv.value;
		options.effvol = effv.value;
		options.bgvol = bgv.value;

		SL.Save();
	}

	public void cancelOptions()
	{
		SL.Load();
		Screen.SetResolution(options.resolution.x, options.resolution.y, options.fullscreen);
		mainv.value = options.mainvol;
		effv.value = options.effvol;
		bgv.value = options.bgvol;
		moveb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.select].keyCode.ToString();
		attackb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.attack].keyCode.ToString();
		invb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.inventory].keyCode.ToString();
		pauseb.transform.GetChild(0).GetComponent<Text>().text = keybinds.keybinds[(int)KeybindActions.pause].keyCode.ToString();
	}
}
