using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryUI : MonoBehaviour {
    public PlayerInventory inv;
	public List<inventoryslot> islots = new List<inventoryslot>();
	// Use this for initialization
	public void Start () {
        inv = PlayerInventory.instance;
        inv.onItemChangedCallback += UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateUI(bool added)
    {
        Debug.Log("UPDATING UI");
		if (added)
			itemAdded();
		else
		{
			//itemDeleted();
		}
    }

    public void itemAdded()
    {
		int i = inv.invItems.Count - 1;
		islots[i].slotIcon.sprite = inv.invItems[i].Item.icon;
		islots[i].slotIcon.enabled = true;
		islots[i].slotAmount.text = inv.invItems[i].amount.ToString();
		islots[i].occupied = true;
		islots[i].deleteSlot.GetComponent<Button>().interactable = true;
		inv.invItems[i].uislot = i;
		
    }

	public void itemDeleted(int slotno)
	{
		inv.Removeslot(slotno);
		islots[slotno].deleteSlot.GetComponent<Button>().interactable = false;
		islots[slotno].slotIcon.enabled = false;
		if( int.Parse(islots[slotno].slotAmount.text) > 1)
		{
			islots[slotno].slotAmount.text = (int.Parse(islots[slotno].slotAmount.text) - 1).ToString();
		} else if(int.Parse(islots[slotno].slotAmount.text) <= 1)
		{
			islots[slotno].slotAmount.text = "";
		}
		//int invindex = inv.invItems.FindIndex(x => x.uislot == slotno);
		//inv.Remove(inv.invItems[invindex].Item);
		//print("deleted");
	}

    
    void UpdateSlot(GameObject slot)
    {

    }
	public void initslot()
	{
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			islots[i].init(transform.GetChild(0).GetChild(i).gameObject);
			//islots[i].deleteSlot.GetComponent<Button>().onClick.AddListener(itemDeleted);
		}
		
	}
	[System.Serializable]
    public class inventoryslot
    {
        public GameObject slotGO;
        public Image slotIcon;
        public Text slotAmount;
        public GameObject deleteSlot;
        public bool occupied;

        public void init(GameObject slot)
        {
            slotGO = slot;
            slotIcon = slot.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            slotAmount = slot.transform.GetChild(2).GetComponent<Text>();
			deleteSlot = slot.transform.GetChild(1).gameObject;
			deleteSlot.GetComponent<Button>().targetGraphic = deleteSlot.GetComponent<Image>();
			occupied = false;
        }
    }
}
