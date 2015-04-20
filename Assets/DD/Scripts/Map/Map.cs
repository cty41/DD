using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	// enum
	public enum MapType
	{
		MT_None = 0,
		MT_Corrider = 1,
		MT_Room = 2
	};
	// pulbic
	public List<string> Cells;
	public MapType m_eSceneType;

    public float cellWidth { get; private set; }
    public float cellHeight { get; private set; }
    public float cellPosY { get; private set; }
    public float sceneStartX, sceneEndX;
    public int iCurMapIndex{ get; private set; }
    // private
	private Corrider corrider;
	private Room room;
	private IList<MapType> maps;

	public Map()
	{
		maps = new List<MapType>();
		maps.Add(MapType.MT_Room);
		maps.Add(MapType.MT_Corrider);
		
		iCurMapIndex = 0;
	}
	
	void Awake()
	{
	}
	
	void Start ()
	{
		maps = new List<MapType>();
		maps.Add(MapType.MT_Room);
		maps.Add(MapType.MT_Corrider);
		
		iCurMapIndex = 0;
		
		// TODO: for map
		Maze maze = new Maze();
		maze.Init();
	}

	void Update ()
	{
	
	}

	public void Init(MapType type)
	{
		m_eSceneType = type;
		
		switch (m_eSceneType)
		{
			case MapType.MT_Corrider:
			{
				corrider = new Corrider();
				corrider.Init();
				
				cellHeight = corrider.height;
				cellWidth = corrider.width;
				sceneStartX = corrider.sceneStartX;
				sceneEndX = corrider.sceneEndX;
			}	
			break;
			
			case MapType.MT_Room:
			{
				room = new Room();
				room.Init();
				
				cellHeight = room.height;
				cellWidth = room.width;
				sceneStartX = room.sceneStartX;
				sceneEndX = room.sceneEndX;
			}
			break;
		}
	}

	public IEnumerator ChangeMap(int index)
	{
		float fadeTime = GameInfo.instance.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		
		switch (m_eSceneType)
		{
			case MapType.MT_Corrider:
				corrider.CleanMap();
			break;
				
			case MapType.MT_Room:
				room.CleanMap();
			break;
		}
		
		GameInfo.instance.GetComponent<Fading>().BeginFade(-1);
		LoadMap(index);
	}
	
	public void LoadMap(int index)
	{
		iCurMapIndex = index;
		
		Init (maps[iCurMapIndex]);
	}
}