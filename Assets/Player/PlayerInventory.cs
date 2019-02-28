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
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;
	public int space = 20;
	public bool Add(Item item)
	{
		if (invItems.Count >= space)
		{
			Debug.Log("not enough room");
			return false;
		}
		invItems.Add(new InvItems {amount = 1, Item = item });
		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		return true;
	}
	public void Remove(Item Iitem)
	{
		//item = invItems.FindIndex(i => i.Contains == item);
		//string index = invItems.First(s => s.Item == item));
		//print(index);
		int index = invItems.Select((item , i) => new { Item = item , Index = i }).First(x => x.Item.Item == Iitem).Index;
		print(index);
		int test = invItems.First(x => x.Item == Iitem).Index;
		if (invItems[index].amount > 1)
			invItems[index].amount--;
		else if (invItems[index].amount == 1)
			invItems.Remove(invItems[index]);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
	bool finditem(InvItems ii, Item item)
	{
		if (ii.Item == item)
			return true;
		else
			return false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	[System.Serializable]
    public class InvItems //: IEquatable<InvItems>
    {

		public int amount;
		public Item Item;
    }
}
