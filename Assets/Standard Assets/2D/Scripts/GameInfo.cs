using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour
{
    public static GameInfo instance { get; private set; }

    public HeroTeam heroTeam { get; private set; }

	public Scene backGround { get; private set; }

    void Awake()
    {
        instance = this;
        heroTeam = gameObject.GetComponent<HeroTeam>();
		backGround = gameObject.GetComponent<Scene> ();

        //SpawnMonster();
		SpawnScene();
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("GameInfo Start");
        SpawnHero();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnHero()
    {
        GameObject hero = Instantiate(Resources.Load("Heroes/crusader", typeof(GameObject))) as GameObject;
        //hero.transform.position = Vector3()
        //refactor here?, ty.cheng
        heroTeam.Init();
        heroTeam.Heroes.Add(hero);
        Debug.Log("");
        CombatMgr.instance.AddPawn(hero);
    }

    public void SpawnMonster()
    {
        GameObject monster = Instantiate(Resources.Load("Monsters/brigand", typeof(GameObject))) as GameObject;
        Debug.Log("GameInfo SpawnMonster" + monster);
        CombatMgr.instance.AddPawn(monster);
    }

	public void SpawnScene()
	{
		backGround.Init();
		//backGround.Cells.Add (cell);
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
}
