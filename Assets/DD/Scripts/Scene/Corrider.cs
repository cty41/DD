using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Corrider : MonoBehaviour
{
// Properties:
	
	/* public*/
	public List<string> Cells;
	public float height { get; private set; }
	public float width { get; private set; }
	/* private*/
	
// Functions
	/* Use this for awake class*/
	void Awake()
	{
		
	}
	/* Use this for initialization*/
	void Start ()
	{
	
	}
	/* Update is called once per frame*/
	void Update ()
	{
	
	}
	/* Update fixed time*/
	void FixedUpdate()
	{
	}
	/* Use this when destroy*/
	void OnDestroy()
	{
	}
	/* Init*/
	public void Init()
	{
		Cells = new List<string>();
		
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
			wall.Init (i, path, -1, 0);
			if (i == 0)
			{
				/*cellPosY = wall.transform.position.y;
				cellWidth = wall.cellSprite.sprite.bounds.size.x;
				cellHeight = wall.cellSprite.sprite.bounds.size.y;
				sceneStartX = wall.transform.position.x - cellWidth * 0.5f;*/
				width = Cells.Count * wall.cellSprite.sprite.bounds.size.x;
				height = wall.cellSprite.sprite.bounds.size.y;
			}
			else if (i == Cells.Count - 1)
			{
				//sceneEndX = wall.transform.position.x + cellWidth * 0.5f;
			}
			
			i++;
		}
		
		for (i = -1; i < Cells.Count ; ++i)
		{
			Cell midGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			midGround.Init(i, "Scene/weald.corridor_mid", -2, 0.1f);
			
			Cell backGround = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			backGround.Init(i, "Scene/weald.corridor_bg", -3, 0.2f);
		}
	}
}
