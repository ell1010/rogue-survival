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
	public List<Item> items = new List<Item>();
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;
	public int space = 20;
	public bool Add(Item item)
	{
		if (items.Count >= space)
		{
			Debug.Log("not enough room");
			return false;
		}
		items.Add(item);
		if(onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
		return true;
	}
	public void Remove(Item item)
	{

		items.Remove(item);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
