using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;
using System.Collections.Generic;

public class CombatMgr : MonoBehaviour
{
	public static CombatMgr instance { get; private set;}

	public int CurrentRound = 0;

	public Pawn CurrentAttacker = null;

    public List<GameObject> PawnArray { get; private set; }
    public List<GameObject> Heroes { get; private set;}
    public List<GameObject> Monsters { get; private set;}


	void Awake()
	{
        instance = this;
        PawnArray = new List<GameObject>();
        Heroes = new List<GameObject>();
        Monsters = new List<GameObject>();
	}
	// Use this for initialization
	void Start ()
	{
        Debug.Log("CombatMgr Start");
        

        //TODO, should not call here , change later -ty.cheng
        //StartCombat();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    public void AddPawn(GameObject p)
    {
        PawnArray.Add(p);
        if (p.GetComponent<Pawn>().IsPlayer)
        {
            Heroes.Add(p);
        }
        else
        {
            Monsters.Add(p);
        }
    }

	public void StartCombat()
	{
        Debug.Log("StartCombat");
		CurrentRound = 0;

		NextRound ();
	}

	void NextRound()
	{
        //TODO, sort by speed -ty.cheng
		for(int i = 0 ; i < PawnArray.Count; ++i)
		{
			Pawn p = PawnArray[i].GetComponent<Pawn>();
			if( p != null && p.IsPlayer)
			{	
				CurrentAttacker = p;
				break;
			}
		}

        Debug.Log("CurrentAttacker", CurrentAttacker);
		CurrentAttacker.StartAction();
	}

	//return false means we restart again
    public bool NextAttacker()
	{
		int j;
        Debug.Log("NextAttacker CurrentPawnArray Length" + PawnArray.Count);
        for (int i = 0; i < PawnArray.Count; ++i)
        {
			if(PawnArray[i] == CurrentAttacker.gameObject)
			{
				j = i+1;
                if (j < PawnArray.Count)
				{
					CurrentAttacker = PawnArray[j].GetComponent<Pawn>();
					CurrentAttacker.StartAction();
					return true;
				}
			}
		}
        Debug.Log("NextAttacker --> we restart again " + PawnArray[0] );
		CurrentAttacker = PawnArray[0].GetComponent<Pawn>();
		CurrentAttacker.StartAction();
		return false;
	}

    public void HandleCombatEnded(Pawn victim)
    {
        if (!victim.IsPlayer)
        {
            foreach (GameObject monster in Monsters)
            {
                if (monster == victim.gameObject)
                {
                    Monsters.Remove(victim.gameObject);
                    break;
                }
            }
            if (Monsters.Count == 0)
            {
                Debug.Log("Heroes win!");
            }
        }
        else
        {
            foreach (GameObject hero in Heroes)
            {
                if (hero == victim.gameObject)
                {
                    Heroes.Remove(victim.gameObject);
                    break;
                }
            }
            if (Heroes.Count == 0)
            {
                Debug.Log("Heroes lose! game over");
            }
        }
    }
}

