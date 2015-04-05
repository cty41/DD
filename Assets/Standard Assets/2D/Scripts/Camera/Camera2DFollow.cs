﻿using System;
using UnityEngine;
using AssemblyCSharpfirstpass;

public class Camera2DFollow : MonoBehaviour
{
	public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 4f; // How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 4f; // How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.
	
	private Transform m_Player; // Reference to the player's transform.
    private GameObject targetPawn;
    private float pawnWidth;
    private float pawnHeight;
    private float cellHeight;
	public static Camera2DFollow instance { get; private set; }
	
	void Awake()
	{
		instance = this;
		//m_Player = GameInfo.instance.heroTeam.Heroes [0].transform;
	}

	void Start()
	{
		//m_Player = GameInfo.instance.heroTeam.Heroes[0].transform;
	}

	public void Init()
	{
		Debug.Log ("m_player " + GameInfo.instance.heroTeam.Heroes);
		m_Player = GameInfo.instance.heroTeam.Heroes [0].transform;
        targetPawn = GameInfo.instance.heroTeam.Heroes[0];
        pawnWidth = targetPawn.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        pawnHeight = targetPawn.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        cellHeight = GameInfo.instance.backGround.cellHeight;
	}
	
	
	private bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - m_Player.position.x) > xMargin;
	}
	
	
	private bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - m_Player.position.y) > yMargin;
	}
	
	
	private void Update()
	{

	}

	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	private void TrackPlayer()
	{
		if (!m_Player)
		{
			return;
		}
        float scale = targetPawn.GetComponent<Pawn>().uniformScale;
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

        //camera x follow hero pos, y fixed on cell pos Y, TODO later -ty.cheng
        Scene bg = GameInfo.instance.backGround;
        float camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        targetX = m_Player.position.x + pawnWidth * 0.5f * scale;
        float minX = bg.sceneStartX + camWidth;
        float maxX = bg.sceneEndX - camWidth;
        Debug.Log("[CAM DEBUG] ==> targetX " + targetX + " minX " + minX + " maxX " + maxX + " camWidth " + camWidth);
        targetX = Mathf.Clamp(targetX, minX, maxX);

        //Ycam = Ycellcenter - CellHalfHeight + deltaY,YcellCenter = 0(world)
        //deltaY = half cam height - UIHeight
        //hard code 100 is pixel per unit for other sprites, refactor later
        float cellPosY = GameInfo.instance.backGround.cellPosY;
        targetY = (cellPosY - cellHeight / 2 + Camera.main.orthographicSize - UIMgr.instance.CombatUIHeight / 100.0f);
        //Debug.Log("cellHeight " + cellHeight + " orthographicSize " + Camera.main.orthographicSize);
		// If the player has moved beyond the x margin...
        //if (CheckXMargin())
        //{
        //    // ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
        //    targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth*Time.deltaTime);
        //}
		
        //// If the player has moved beyond the y margin...
        //if (CheckYMargin())
        //{
        //    // ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
        //    targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, ySmooth*Time.deltaTime);
        //}
		
        //// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
        //targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        //targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}