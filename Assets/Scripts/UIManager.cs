using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject pausepanel;
    public GameObject invpanel;
    settingsmanager set = settingsmanager.instance;
	// Use this for initialization
	void Start () {
        set = settingsmanager.instance;
		invpanel.GetComponent<InventoryUI>().setupInvUI();
		//PlayerInventory.instance.onItemChangedCallback += invpanel.GetComponent<InventoryUI>().UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
        if (set.pausepressed())
            pausepanel.SetActive(!pausepanel.activeInHierarchy);
        if (set.inventorypressed())
            invpanel.SetActive(!invpanel.activeInHierarchy);
	}
}
