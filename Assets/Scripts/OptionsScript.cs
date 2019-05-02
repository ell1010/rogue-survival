using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    int[] defaultRes = new int[2];
    bool startful;
	// Use this for initialization
	void Start () {
        popuateOptions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void popuateOptions()
    {
        resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            ResolutionD.options.Add(new Dropdown.OptionData(res.ToString()));
        }
        defaultRes[0] = Screen.width;
        defaultRes[1] = Screen.height;
        startful = Screen.fullScreen;

    }

    public void resolutionDropdown()
    {
        Screen.SetResolution(resolutions[ResolutionD.value].width, resolutions[ResolutionD.value].height, Screen.fullScreen);
    }

    public void fullscreen()
    {
        Screen.fullScreen = FST.isOn;
    }
}
