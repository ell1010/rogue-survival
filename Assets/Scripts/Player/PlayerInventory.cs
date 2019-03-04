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
			print("newItem");
		invItems.Add(new InvItems {amount = 1, Item = Iitem });
		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
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
			onItemChangedCallback.Invoke();
	}

	[System.Serializable]
    public class InvItems //: IEquatable<InvItems>
    {

		public int amount;
		public Item Item;
    }
}
