using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : MonoBehaviour {
	#region Singleton
	public static PlayerInventory instance;
	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("DUPLICATE INVENTORY");
		}
		instance = this;
	}
	#endregion
    public List<InvItems> invItems = new List<InvItems>();
	public delegate void OnItemChanged(bool added);
	public OnItemChanged onItemChangedCallback;
	public int space = 20;
	public Item[] equipped = new Item[5];
	public Playerinformation pinfo;
	public bool Add(Item Iitem)
	{
		if (invItems.Count >= space)
		{
			Debug.Log("not enough room");
			return false;
		}
		// more compex version of one in remove where it adds a null check.
		var search = invItems.Select((item , i) => new { Item = item , Index = (int?)i });
		int? index = (from pair in search where pair.Item == search select pair.Index).FirstOrDefault();
		if (index == null)
		{
			//print("newItem");
			invItems.Add(new InvItems { amount = 1, Item = Iitem });
		}
		else
		{
			invItems[(int)index].amount++;
		}
		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke(true);
		return true;
	}
	public void Remove(Item Iitem)
	{
		int index = invItems.Select((item , i) => new { Item = item , Index = i }).First(x => x.Item.Item == Iitem).Index;
		print(index);
		if (invItems[index].amount > 1)
			invItems[index].amount--;
		else if (invItems[index].amount == 1)
			invItems.Remove(invItems[index]);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke(false);
	}

	public void Removeslot(int slotno)
	{
		int index = invItems.FindIndex(x => x.uislot == slotno);
		if (invItems[index].amount > 1)
			invItems[index].amount--;
		else if (invItems[index].amount == 1)
			invItems.Remove(invItems[index]);

	}

	public int getindex(int slotno)
	{
		int index = invItems.FindIndex(x => x.uislot == slotno);
		return index;
	}

	public void calcstats()
	{
		for (int i = 0; i < equipped.Length; i++)
		{
			if (equipped[i] != null)
			{
				pinfo.Attack += equipped[i].itemstats[0];
				pinfo.Defence += equipped[i].itemstats[2];
				pinfo.Range += equipped[i].itemstats[1];
				print("calc" + equipped[i].name);
			}
		}
	}

	void uiDelete()
	{

	}

	public void equip(Item e)
	{
		print("equipped" + e);
		equipped[(int)e.itemtype] = e;
	}

	[System.Serializable]
    public class InvItems
    {
		public int amount;
		public Item Item;
		public int uislot = 33;
    }
}
