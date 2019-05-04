using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryUI : MonoBehaviour {
    public PlayerInventory inv;
	public List<inventoryslot> islots = new List<inventoryslot>();
	public List<inventoryslot> eslots = new List<inventoryslot>();
	// Use this for initialization
	public void Start () {
        
	}
	public void setupInvUI()
	{
		inv = PlayerInventory.instance;
		inv.onItemChangedCallback += UpdateUI;
		setupUseItem();
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
	
	public void setupUseItem()
	{
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			int index = i;
			islots[i].slotGO.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { useitem(index); });
		}
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
	void useitem(int index)
	{
		print(index);
		if(inv.invItems[index].Item.itemtype != Item.type.usable)
		{
			equip(index);
		}
		inv.invItems[index].Item.useItem();
	}
    
	void equip(int index)
	{
		print((int)inv.invItems[inv.getindex(index)].Item.itemtype);
		eslots[(int)inv.invItems[inv.getindex(index)].Item.itemtype].slotIcon.sprite = islots[index].slotIcon.sprite;
		eslots[(int)inv.invItems[inv.getindex(index)].Item.itemtype].slotIcon.enabled = true;
		eslots[(int)inv.invItems[inv.getindex(index)].Item.itemtype].occupied = true;
		islots[index].slotIcon.sprite = null;
		islots[index].slotIcon.enabled = false;
		islots[index].deleteSlot.GetComponent<Button>().interactable = false;
		islots[index].slotAmount.text = " ";
		islots[index].occupied = false;
		inv.invItems[index].uislot = islots.Count + (int)inv.invItems[inv.getindex(index)].Item.itemtype;

	}

    void UpdateSlot(GameObject slot)
    {

    }
	public void initslot()
	{
		for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
		{
			islots[i].init(transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
			//islots[i].deleteSlot.GetComponent<Button>().onClick.AddListener(itemDeleted);
		}
		for (int i = 0; i < transform.GetChild(0).GetChild(1).childCount; i++)
		{
			eslots[i].init(transform.GetChild(0).GetChild(1).GetChild(i).gameObject);
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
