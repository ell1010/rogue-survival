using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)),RequireComponent(typeof(MeshRenderer))]
public class LineGenerator : MonoBehaviour {


	Mesh mesh;
	GameObject player;
	PlayerController pc;

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
		pc = player.GetComponent<PlayerController>();

	}

	public void createline(List<Pathfinding.node> line)
	{
		vertices = new List<Vector3>();
		//print(line[1].x + " " + line[1].y);
		for (int i = 0; i < line.Count; i++)
		{
			if (i == 0)
			{
				checkpos12(line[i] , line[i + 1],false);
				
			}
			else if (i == pc.currentmovement && i != line.Count-1)
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
				checkpos12(line[i-1] , line[i],true);
			}
		}
		triangles = new List<int>();

		print(line.Count);
		for (int i = 0; i < line.Count-1; i++)
		{
			if (i < pc.currentmovement || pc.currentmovement <= 0)
			{
				//print("new rect");
				triangles.Add((i * 2) + 0);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 3);
			}
			else
			{
				//print("new rect");
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

	void checkpos12(Pathfinding.node pos1, Pathfinding.node pos2, bool end)
	{
		if (pos1.x == pos2.x)
		{
			if (!end)
			{
				if (pos1.x < pos2.x || pos1.y > pos2.y)
				{
					vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
					vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
				}
				else if (pos1.x > pos2.x || pos1.y < pos2.y)
				{
					vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
					vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
				}
			}
			else
			{
				if (pos1.x < pos2.x || pos1.y > pos2.y)
				{
					vertices.Add(new Vector3((pos2.x + 0.5f) - (linewidth / 2) , (pos2.y + 0.5f) , -1));
					vertices.Add(new Vector3((pos2.x + 0.5f) + (linewidth / 2) , (pos2.y + 0.5f) , -1));
				}
				else if (pos1.x > pos2.x || pos1.y < pos2.y)
				{
					vertices.Add(new Vector3((pos2.x + 0.5f) + (linewidth / 2) , (pos2.y + 0.5f) , -1));
					vertices.Add(new Vector3((pos2.x + 0.5f) - (linewidth / 2) , (pos2.y + 0.5f) , -1));
				}
			}
		}
		else if (pos1.y == pos2.y)
		{
			if (!end)
			{
				if (pos1.x < pos2.x || pos1.y > pos2.y)
				{
					vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
					vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
				}
				else if (pos1.x > pos2.x || pos1.y < pos2.y)
				{
					vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
					vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
				}
			}
			else
			{
				if (pos1.x < pos2.x || pos1.y > pos2.y)
				{
					vertices.Add(new Vector3((pos2.x + 0.5f) , (pos2.y + 0.5f) - (linewidth / 2) , -1));
					vertices.Add(new Vector3((pos2.x + 0.5f) , (pos2.y + 0.5f) + (linewidth / 2) , -1));
				}
				else if (pos1.x > pos2.x || pos1.y < pos2.y)
				{
					vertices.Add(new Vector3((pos2.x + 0.5f) , (pos2.y + 0.5f) + (linewidth / 2) , -1));
					vertices.Add(new Vector3((pos2.x + 0.5f) , (pos2.y + 0.5f) - (linewidth / 2) , -1));
				}
			}
		}
	}

	void checkpos123(Pathfinding.node pos1 , Pathfinding.node pos2, Pathfinding.node pos3)
	{
		if (pos1.x == pos2.x && pos1.x == pos3.x)
		{
			if (pos1.y > pos2.y || pos1.y < pos3.y)
			{
				vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
				vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
				print("normal");
			}
			else if(pos1.y < pos2.y || pos1.y > pos3.y)
			{
				vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) , -1));
				vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) , -1));
				print("flipped");
			}
		}
		else if (pos1.y == pos2.y && pos1.y == pos3.y)
		{
			if (pos1.x < pos2.x || pos1.x > pos3.x)
			{
				vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
				vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
                print("y same going right");
			}
			else if(pos1.x > pos2.x || pos1.x < pos3.x)
			{
				vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
				vertices.Add(new Vector3((pos1.x + 0.5f) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
                print("y same going left");
			}
            print("y same");
		}
		else
		{
            if (pos1.x < pos2.x || pos1.x > pos3.x)
            {
                if (pos1.y < pos2.y | pos1.y > pos3.y)
                {
                    vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2), (pos1.y + 0.5f) - (linewidth / 2), -1));
                    vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2), (pos1.y + 0.5f) + (linewidth / 2), -1));
                    print("x up y up");
                }
                else if(pos1.y > pos2.y | pos1.y < pos3.y)
                {
                    vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2), (pos1.y + 0.5f) - (linewidth / 2), -1));
                    vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2), (pos1.y + 0.5f) + (linewidth / 2), -1));
                    print("x up y down");
                }
            }
            else if(pos1.x > pos2.x || pos1.x < pos3.x)
            {
                if (pos1.y < pos2.y | pos1.y > pos3.y)
                {
                    vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2), (pos1.y + 0.5f) + (linewidth / 2), -1));
                    vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2), (pos1.y + 0.5f) - (linewidth / 2), -1));
                    print("x down y up");
                }
                else if (pos1.y > pos2.y | pos1.y < pos3.y)
                {
                    vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2), (pos1.y + 0.5f) + (linewidth / 2), -1));
                    vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2), (pos1.y + 0.5f) - (linewidth / 2), -1));
                    print("x down y down");
                }
            }
			//if (pos1.x < pos2.x || pos1.y > pos2.y)
			//{
			//	vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
			//	vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
   //             print("what");
			//}
			//else if(pos1.x < pos2.x || pos1.y > pos2.y)
			//{
			//	vertices.Add(new Vector3((pos1.x + 0.5f) + (linewidth / 2) , (pos1.y + 0.5f) + (linewidth / 2) , -1));
			//	vertices.Add(new Vector3((pos1.x + 0.5f) - (linewidth / 2) , (pos1.y + 0.5f) - (linewidth / 2) , -1));
   //             print("huh");
			//}
            Debug.LogError("no vertices set");
		}
	}

	public void updateline(int movement, int pathpointscount)
	{
		vertices.RemoveAt(0);
		vertices.RemoveAt(0);
		triangles.Clear();
		for (int i = 0; i < pathpointscount - 1; i++)
		{
			if (i < movement || pathpointscount <= 0)
			{
				//print("new rect");
				triangles.Add((i * 2) + 0);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 2);
				triangles.Add((i * 2) + 1);
				triangles.Add((i * 2) + 3);
			}
			else if (i > movement )
			{
				//print("new rect");
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

	void UpdateMesh()
	{
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		List<Color32> colors = new List<Color32>();
		for (int i = 0; i < vertices.Count; i++)
		{
			if (i < ((pc.currentmovement * 2) + 2) && pc.currentmovement != 0)
			{
				colors.Add(canwalk);
			}
			else if (i >= (pc.currentmovement * 2) + 2 && pc.currentmovement != 0)
			{
				colors.Add(cantwalk);
			}
			else if (pc.currentmovement == 0)
			{
				//print("hi");
				colors.Add(cantwalk);
			}
			//else
			//	print("exception" + i);
		}
		//print("colors "+colors.Count + "vertices " + vertices.Count);

		mesh.SetColors(colors);
		mesh.RecalculateNormals();
	}
}
