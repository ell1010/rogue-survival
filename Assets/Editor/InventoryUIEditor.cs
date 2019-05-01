using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryUI))]
public class InventoryUIEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		InventoryUI iui = (InventoryUI)target;
		if(GUILayout.Button("Build Inventory"))
		{
			iui.initslot();
		}
	}
}
