using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer))]
public class LineGenerator : MonoBehaviour {


	Mesh mesh;
	GameObject player;

	public List<Vector3> vertices;
	public List<int> triangles;

	public float linewidth;
	public Color32 canwalk;
	public Color32 cantwalk;
	public Color32 canattack;

	private void Awake()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		player = GameObject.FindGameObjectWithTag("Player");

	}

	public void createline(List<Pathfinding.node> line)
	{
		vertices = new List<Vector3>();
		for (int i = 0; i < line.Count; i++)
		{
			if (i == 0)
			{
				checkpos12(line[i] , line[i + 1]);
				
			}
			else if (i == player.GetComponent<PlayerController>().startmovement && i != line.Count-1)
			{
				checkpos123(line[i] , line[i + 1] , line[i - 1]);
				checkpos123(line[i] , line[i + 1] , line[i - 1]);
			}
			else if (i != line.Count - 1)
			{
				checkpos123(line[i] , line[i + 1] , line[i - 1]);
			}
			else
			{
				checkpos12(line[i] , line[i - 1]);
			}
		}
		triangles = new List<int>();

		print(line.Count);
		for (int i = 0; i < line.Count-1; i++)
		{
			if (i < 3)
			{
				print("new rect");
				triangles.Add((i * 2) + 0);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 3);
			}
			else
			{
				print("new rect");
				triangles.Add(((i * 2) + 2) + 0);
				triangles.Add(((i * 2) + 2) + 1);
				triangles.Add(((i * 2) + 2) + 2);
				triangles.Add(((i * 2) + 2) + 2);
				triangles.Add(((i * 2) + 2) + 1);
				triangles.Add(((i * 2) + 2) + 3);
			}
		}
		UpdateMesh();
	}

	void checkpos12(Pathfinding.node pos1, Pathfinding.node pos2)
	{
		if (pos1.x == pos2.x)
		{
			vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
			vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
		}
		else if (pos1.y == pos2.y)
		{
			vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
			vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
		}
	}

	void checkpos123(Pathfinding.node pos1 , Pathfinding.node pos2, Pathfinding.node pos3)
	{
		if (pos1.x == pos2.x && pos1.x == pos3.x)
		{
			vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
			vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
		}
		else if (pos1.y == pos2.y && pos1.y == pos3.y)
		{
			vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
			vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
		}
		else
		{
			vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
			vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
		}
	}
	void UpdateMesh()
	{
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		List<Color32> colors = new List<Color32>();
		for (int i = 0; i < vertices.Count; i++)
		{
			if (i < 8)
			{
				colors.Add(canwalk);
			}
			else
			{
				colors.Add(cantwalk);
			}
		}
		mesh.SetColors(colors);
		mesh.RecalculateNormals();
	}
}
