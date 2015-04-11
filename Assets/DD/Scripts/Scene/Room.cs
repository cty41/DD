using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
// Properties:
	
	/* public*/
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
		Cell wall = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
		wall.Init(0, "Scene/weald.room_wall.corruptedcabin", -1, 0);
		width = wall.cellSprite.sprite.bounds.size.x;
		height = wall.cellSprite.sprite.bounds.size.y;
	}
}
