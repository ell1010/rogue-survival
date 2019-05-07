using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public Playerinformation pinfo;
    public GameObject pausepanel;
    public GameObject invpanel;
    settingsmanager set = settingsmanager.instance;
	public GameObject nameinput;
	public GameObject hpbar;
	public GameObject xpbar;
	// Use this for initialization
	void Start () {
        set = settingsmanager.instance;
		invpanel.GetComponent<InventoryUI>().setupInvUI();
		//PlayerInventory.instance.onItemChangedCallback += invpanel.GetComponent<InventoryUI>().UpdateUI;
		checkpinfo();
	}

	private void checkpinfo()
	{
		if (pinfo.PlayerName.Length == 0 )
		{
			nameinput.SetActive(true);
		}
	}

	public void nameInput(string input)
	{
		pinfo.PlayerName = input;
		print(input);
		nameinput.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
        if (set.pausepressed())
            pausepanel.SetActive(!pausepanel.activeInHierarchy);
        if (set.inventorypressed())
            invpanel.SetActive(!invpanel.activeInHierarchy);

		hpbar.GetComponent<Image>().fillAmount = (pinfo.Health / pinfo.maxhealth);
		xpbar.GetComponent<Image>().fillAmount = (pinfo.experience / (pinfo.PlayerLevel * 12));
	}
}
