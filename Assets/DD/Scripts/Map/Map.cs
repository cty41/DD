using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	// enum
	public enum MapType
	{
		MT_Corrider = 0,
		MT_Room = 1
	};
	// pulbic
	public List<string> Cells;
	public MapType m_eSceneType;

    public float cellWidth { get; private set; }
    public float cellHeight { get; private set; }
    public float cellPosY { get; private set; }
    public float sceneStartX, sceneEndX;
    // private
	private Corrider corrider;
	private Room room;

	void Awake()
	{
	}
	
	void Start ()
	{

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

	public IEnumerator CleanMap()
	{
		float fadeTime = GameInfo.instance.GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		
		switch (m_eSceneType)
		{
		case MapType.MT_Corrider:
			corrider.CleanMap();
			break;
			
		case MapType.MT_Room:
			Debug.Log("room clean map");
			room.CleanMap();
			break;
		}
		// hard code for test
		Init(MapType.MT_Room);
		
		GameInfo.instance.GetComponent<Fading>().BeginFade(-1);
	}
	
	public void LoadMap()
	{
		CleanMap();
		Init (MapType.MT_Room);
	}
	
	public void ChangeMap()
	{
	
	}
}