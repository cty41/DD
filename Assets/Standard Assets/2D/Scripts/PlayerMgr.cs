using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;

public class PlayerMgr : MonoBehaviour
{
	public static PlayerMgr instance { get; private set;}

	public GameObject CurrentHero;
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
		/*if (CurrentHero != null ) {
			Pawn P = CurrentHero.GetComponent<Pawn>();
			if(P.IsReadyToMakeAction() )
		}*/
	}

	public void PlayerStart(GameObject Pawn)
	{
		CurrentHero = Pawn;
	}

	public void PlayerEnd()
	{
		CurrentHero = null;

	}
}

