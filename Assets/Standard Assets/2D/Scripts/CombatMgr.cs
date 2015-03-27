using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;

public class CombatMgr : MonoBehaviour
{
	public static CombatMgr instance { get; private set;}

	public int CurrentRound = 0;

	public Pawn CurrentAttacker = null;

	public GameObject[] PawnArray;
	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void StartCombat()
	{
		CurrentRound = 0;
		NextRound ();
	}

	void NextRound()
	{
		//loop 
		PawnArray = GameObject.FindGameObjectsWithTag ("Pawn");

		for(int i = 0 ; i < PawnArray.Length; ++i)
		{
			Pawn p = PawnArray[i].GetComponent<Pawn>();
			if( p != null )
			{	
				CurrentAttacker = p;
				break;
			}
		}

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

