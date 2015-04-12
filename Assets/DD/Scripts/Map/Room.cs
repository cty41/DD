using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
// Properties:
	
	/* public*/
	public float height { get; private set; }
	public float width { get; private set; }
	public float sceneStartX { get; private set; }
	public float sceneEndX { get; private set; }
	/* private*/
	private Cell wall;
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
		Debug.Log("Room init");
		wall = Instantiate(Resources.Load("Map/Cell", typeof(Cell))) as Cell;
		wall.Init(0, "Map/weald.room_wall.corruptedcabin", -1, 0);
		width = wall.cellSprite.sprite.bounds.size.x;
		height = wall.cellSprite.sprite.bounds.size.y;
		sceneStartX = wall.transform.position.x - width * 0.5f;
		sceneEndX = wall.transform.position.x + width * 0.5f;
	}
	
	public void CleanMap()
	{
		Destroy(wall.gameObject);
	}
}
