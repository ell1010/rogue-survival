using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public void Remove(Item item)
	{
		int index = invItems.IndexOf(item);
		invItems.Remove(new InvItems { amount = 1, Item = item });
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public class InvItems
    {

		public int amount;
		public Item Item;
    }
}
