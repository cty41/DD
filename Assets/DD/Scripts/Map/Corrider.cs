using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Corrider : MonoBehaviour
{
// Properties:
	
	/* public*/
	public float height { get; private set; }
	public float width { get; private set; }
	public float sceneStartX { get; private set; }
	public float sceneEndX { get; private set; }
	/* private*/
	private List<string> paths;
	private IList<Cell> cells;
// Functions
	/* construct*/
	public Corrider()
	{
		paths = new List<string>();
		cells = new List <Cell>(); 
	}
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
		paths.Add ("Map/weald.corridor_door.basic");
		paths.Add ("Map/weald.corridor_wall.01");
		paths.Add ("Map/weald.corridor_wall.02");
		paths.Add ("Map/weald.corridor_wall.03");
		paths.Add ("Map/weald.corridor_door.basic");
		
		int i = 0;
		foreach (string path in paths)
		{
			Cell wall = Instantiate(Resources.Load("Map/Cell", typeof(Cell))) as Cell;
			wall.Init (i, path, -1, 0);
			if (i == 0)
			{
				/*cellPosY = wall.transform.position.y;
				cellWidth = wall.cellSprite.sprite.bounds.size.x;
				cellHeight = wall.cellSprite.sprite.bounds.size.y;
				sceneStartX = wall.transform.position.x - cellWidth * 0.5f;*/
				width = paths.Count * wall.cellSprite.sprite.bounds.size.x;
				height = wall.cellSprite.sprite.bounds.size.y;
				sceneStartX = wall.transform.position.x - wall.cellSprite.bounds.size.x * 0.5f;
			}
			else if (i == paths.Count - 1)
			{
				// cell width = wall.cellSprite.bounds.size.x
				sceneEndX = wall.transform.position.x + wall.cellSprite.bounds.size.x * 0.5f;
			}
			
			cells.Add(wall);
			i++;
		}
		
		for (i = -1; i < paths.Count ; ++i)
		{
			Cell midGround = Instantiate(Resources.Load("Map/Cell", typeof(Cell))) as Cell;
			midGround.Init(i, "Map/weald.corridor_mid", -2, 0.1f);
			cells.Add(midGround);
			
			Cell backGround = Instantiate(Resources.Load("Map/Cell", typeof(Cell))) as Cell;
			backGround.Init(i, "Map/weald.corridor_bg", -3, 0.2f);
			
			cells.Add(backGround);
		}
		
	}
	
	public void CleanMap()
	{
		Debug.Log("cells.Count: " + cells.Count);
		for (int i = 0; i < cells.Count; i++)
		{
			Destroy(cells[i].gameObject);
		}
	}
	
}
