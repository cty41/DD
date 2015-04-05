using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene : MonoBehaviour
{
	// pulbic
	public List<string> Cells;

    public float cellHeight;

	void Awark()
	{

	}
	
	void Start ()
	{

	}

	void Update ()
	{
		
	}

	public void Init()
	{
		Cells.Add ("Scene/weald.corridor_door.basic");
		Cells.Add ("Scene/weald.corridor_wall.01");
		Cells.Add ("Scene/weald.corridor_wall.02");
		Cells.Add ("Scene/weald.corridor_wall.03");
		Cells.Add ("Scene/weald.corridor_door.basic");

		int i = 0;
		foreach (string path in Cells)
		{
			Debug.Log("");
			Cell wall = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			wall.Init (i, path);
            cellHeight  = wall.cellSprite.sprite.bounds.size.y;

			Cell midGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			midGround.Init (i, "Scene/weald.corridor_mid");

			Cell backGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			backGround.Init (i, "Scene/weald.corridor_bg");

			i++;
		}
	}
}