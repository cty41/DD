using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene : MonoBehaviour
{
	// pulbic
	public List<string> Cells;

    public float cellWidth { get; private set; }
    public float cellHeight { get; private set; }
    public float cellPosY { get; private set; }
    public float sceneStartX, sceneEndX;

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
            if (i == 0)
            {
                cellWidth = wall.cellSprite.sprite.bounds.size.x;
                cellHeight = wall.cellSprite.sprite.bounds.size.y;
                sceneStartX = wall.transform.position.x - cellWidth * 0.5f;
            }
            else if (i == Cells.Count - 1)
            {
                sceneEndX = wall.transform.position.x + cellWidth * 0.5f;
            }

            cellPosY = wall.transform.position.y;
			Cell midGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			midGround.Init (i, "Scene/weald.corridor_mid");

			Cell backGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			backGround.Init (i, "Scene/weald.corridor_bg");

			i++;
		}
	}
}