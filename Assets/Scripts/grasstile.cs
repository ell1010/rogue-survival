using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class grasstile : Tile {

	[SerializeField] private Sprite[] grasssprites;
	[SerializeField] private Sprite preview;

	public new Sprite sprite;
	public new Color color = Color.white;
	public new Matrix4x4 transform = Matrix4x4.identity;
	public GameObject gameobject = null;
	public new TileFlags flags = TileFlags.LockColor;
	public Tile.ColliderType collidertype = Tile.ColliderType.Sprite;

	public bool obstacle;

	/*public bool obstacle()
	{
		if (true) {
			return (true);
		} else
			return (false);
	}*/
	public override void RefreshTile(Vector3Int location, ITilemap tilemap)
	{
		for (int yd = -1; yd <= 1; yd++)
			for (int xd = -1; xd <= 1; xd++) 
			{
				Vector3Int pos = new Vector3Int (location.x + xd, location.y + yd, location.z);
				if(HasGrassTile(tilemap,pos))
					tilemap.RefreshTile (pos);
			}
		base.RefreshTile (location, tilemap);
	}
	private bool HasGrassTile(ITilemap tilemap, Vector3Int position)
	{
		return tilemap.GetTile (position) == this;
	}
	public override void GetTileData (Vector3Int position, ITilemap tilemap, ref TileData tileData)
	{
		base.GetTileData (position, tilemap, ref tileData);
		//tileData.sprite = this.sprite;
		//tileData.color = this.color;
		//tileData.transform = this.transform;
		//tileData.gameObject = this.gameobject;
		//tileData.flags = this.flags;
		//tileData.colliderType = this.collidertype;

		int mask = 0;
		if (HasGrassTile (tilemap, position + new Vector3Int (0, 1, 0)))
			mask += 1;
		if (HasGrassTile (tilemap, position + new Vector3Int (1, 0, 0)))
			mask += 2;
		if (HasGrassTile (tilemap, position + new Vector3Int (0, -1, 0)))
			mask += 4;
		if (HasGrassTile (tilemap, position + new Vector3Int (-1, 0, 0)))
			mask += 8;
		int index = GetIndex ((byte)mask);
		if (index >= 0 && index <= grasssprites.Length) 
		{
			tileData.sprite = grasssprites [index];
		}
		
	}
	private int GetIndex(byte mask)
	{
		switch (mask) 
		{
		case 0:
		case 2:
		case 8:
		case 10: return 0;
		case 4:
		case 5:
		case 1: return 1;
		case 3: return 7;
		case 6: return 8;
		case 7: return 4;
		case 9:	return 6;
		case 11: return 2;
		case 12: return 1;
		case 13: return 5;
		case 14: return 3;
		case 15: return 0;
		}
		return -1;
	}
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Grasstile")]
	public static void CreateGrassTile()
	{
		string path = EditorUtility.SaveFilePanelInProject ("Save Grass Tile", "GrassTile", "Asset", "Save Grass Tile", "Assets");
		if (path == "")
			return;
		AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<grasstile> (), path);
	}
	#endif
}
