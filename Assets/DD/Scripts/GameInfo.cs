using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharpfirstpass;

public class GameInfo : MonoBehaviour
{
    public static GameInfo instance { get; private set; }

    public HeroTeam heroTeam { get; private set; }

	public Map backGround { get; private set; }

    void Awake()
    {
        instance = this;
        heroTeam = gameObject.GetComponent<HeroTeam>();
		backGround = gameObject.GetComponent<Map> ();

        //SpawnMonster();
		SpawnScene();
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("GameInfo Start");
        SpawnHero();
		InitCamera ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnHero()
    {
        GameObject hero = Instantiate(Resources.Load("Heroes/leper", typeof(GameObject))) as GameObject;
        //hero.transform.position = Vector3()
        //refactor here?, ty.cheng
        heroTeam.Init();
        heroTeam.Heroes.Add(hero);
        
        CombatMgr.instance.AddPawn(hero);
    }

    public void SpawnMonster()
    {
        GameObject monster = Instantiate(Resources.Load("Monsters/brigand_cutthroat", typeof(GameObject))) as GameObject;
        Debug.Log("GameInfo SpawnMonster" + monster);
        CombatMgr.instance.AddPawn(monster);
        Vector3 pos = Camera.main.transform.position;
        pos.x += Camera2DFollow.instance.cameraXOffset;
        pos.z = 0;
        monster.transform.position = pos;
        Debug.Log("SpawnMonster loc x " + pos.x + " y " + pos.y + " z " + pos.z);
        monster.transform.localScale = new Vector3(-monster.transform.localScale.x, monster.transform.localScale.y, monster.transform.localScale.z);
    }

	public void SpawnScene()
	{
		backGround.Init(Map.MapType.MT_Corrider);
		//backGround.Cells.Add (cell);
	}
	
	public void SpawnRoom()
	{
		StartCoroutine(backGround.CleanMap());
		//backGround.Cells.Add (cell);
	}

	public void InitCamera()
	{
		//GameObject camera = Instantiate(Resources.Load("Scene/Main Camera", typeof(GameObject))) as GameObject;
		Camera2DFollow.instance.Init ();
	}

    public void StartCombat()
    {
        if (!IsInCombat())
        {
            heroTeam.ChangeState(HeroTeam.TeamState.TS_Combat);
            SpawnMonster();
            CombatMgr.instance.StartCombat();
        }
    }

    public void ExitCombat()
    {
        if (IsInCombat())
        {
            heroTeam.ChangeState(HeroTeam.TeamState.TS_Explore);
        }
    }

    public bool IsInCombat()
    {
        return heroTeam.teamState == HeroTeam.TeamState.TS_Combat;
    }

    public void ClampInScene(Pawn p)
    {
        Vector3 oldPos = p.transform.position;
        //only clamp X now 

        oldPos.x = Mathf.Clamp(oldPos.x, backGround.sceneStartX + p.boundBox.size.x * 0.5f,
            backGround.sceneEndX - p.boundBox.size.x * 0.5f);
        p.transform.position = oldPos;
    }

    public bool IsReachedSceneBound(Pawn p)
    {
        if (p.velocity.x > 0.0f)
        {
            return p.transform.position.x >= backGround.sceneEndX - p.boundBox.size.x * 0.5f;
        }
        else if (p.velocity.x < 0.0f)
        {
            return p.transform.position.x <= backGround.sceneStartX + p.boundBox.size.x * 0.5f;
        }

        return false;
    }
}
