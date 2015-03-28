using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;
using System.Collections.Generic;

public class CombatMgr : MonoBehaviour
{
	public static CombatMgr instance { get; private set;}

	public int CurrentRound = 0;

	public Pawn CurrentAttacker = null;

	private GameObject[] PawnArray;
    public List<GameObject> Heroes { get; private set;}
    public List<GameObject> Monsters { get; private set;}


	void Awake()
	{
		
	}
	// Use this for initialization
	void Start ()
	{
        Debug.Log("CombatMgr Start");
        instance = this;
        Heroes = new List<GameObject>();
        Monsters = new List<GameObject>();
        //TODO, should not call here , change later -ty.cheng
        StartCombat();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void StartCombat()
	{
        Debug.Log("StartCombat");
		CurrentRound = 0;

        PawnArray = GameObject.FindGameObjectsWithTag("Pawn");
        Debug.Log("StartCombat PawnArray length", PawnArray[0]);

        for (int i = 0; i < PawnArray.Length; ++i)
        {
            if (PawnArray[i].GetComponent<Pawn>().IsPlayer)
            {
                Heroes.Add(PawnArray[i]);
            }
            else
            {
                Monsters.Add(PawnArray[i]);
            }
        }

		NextRound ();
	}

	void NextRound()
	{
        //TODO, sort by speed -ty.cheng
		for(int i = 0 ; i < PawnArray.Length; ++i)
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
	bool NextAttacker()
	{
		int j;
		for (int i = 0; i < PawnArray.Length; ++i) {
			if(PawnArray[i] == CurrentAttacker)
			{
				j = i+1;
				if(j < PawnArray.Length)
				{
					CurrentAttacker = PawnArray[j].GetComponent<Pawn>();
					CurrentAttacker.StartAction();
					return true;
				}
			}
		}
		CurrentAttacker = PawnArray[0].GetComponent<Pawn>();
		CurrentAttacker.StartAction();
		return false;
	}
}

