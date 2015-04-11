using System;
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
    public Vector3 oldPos { get; private set; }
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
		//Debug.Log ("m_player " + GameInfo.instance.heroTeam.Heroes);
		m_Player = GameInfo.instance.heroTeam.Heroes [0].transform;
        targetPawn = GameInfo.instance.heroTeam.Heroes[0];
        BoxCollider2D pBox = targetPawn.GetComponent<BoxCollider2D>();
        pawnWidth = pBox.size.x;
        pawnHeight = pBox.size.y;
        Debug.Log("m_player " + GameInfo.instance.heroTeam.Heroes + " pawnWidth " + pawnWidth + " pawnHeight " + pawnHeight);
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
        /*float scale = targetPawn.GetComponent<Pawn>().uniformScale;
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

        //camera x follow hero pos, y fixed on cell pos Y, TODO later -ty.cheng
        Scene bg = GameInfo.instance.backGround;
        float camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        //targetX = m_Player.position.x + pawnWidth * 0.5f * scale;
        targetX = m_Player.position.x;
        
        //float minX = bg.sceneStartX + camWidth;
        //float maxX = bg.sceneEndX - camWidth;
        
        //targetX = Mathf.Clamp(targetX, minX, maxX);

        //hard code 100 is pixel per unit for other sprites, refactor later
        float cellPosY = GameInfo.instance.backGround.cellPosY;
        targetY = (cellPosY - cellHeight / 2 + Camera.main.orthographicSize - UIMgr.instance.CombatUIHeight / 100.0f);
		
		// Set the camera's position to the target position with the same z component.
        oldPos = transform.position;
		transform.position = new Vector3(targetX, targetY, transform.position.z);*/
		float targetX = m_Player.transform.position.x;
		float targetY;
		float cellHeight = GameInfo.instance.backGround.cellHeight;
		float cellPosY = GameInfo.instance.backGround.cellPosY;
		// y = screenH - canvasH
		targetY = cellPosY -  cellHeight / 2 + Camera.main.orthographicSize - UIMgr.instance.CombatUIHeight / 100.0f;
		
		Scene bg = GameInfo.instance.backGround;
		float minX = bg.sceneStartX - bg.cellWidth;
		float maxX = bg.sceneStartX + bg.cellWidth;
		targetX = Mathf.Clamp(targetX, minX, maxX);
		
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
