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

    public void SelectSkill(int idx)
    {
        CurrentHero.GetComponent<Pawn>().SelectSkill(idx);
    }

    public void CancelSelectSkill()
    {
        CurrentHero.GetComponent<Pawn>().CancelSkill();
    }

    public bool IsChoosingTarget()
    {
        return CurrentHero != null && CurrentHero.GetComponent<Pawn>().IsSelectingTarget;
    }

    public void UseSkill(Pawn target)
    {
        CurrentHero.GetComponent<Pawn>().UseSkill(target);
    }
}

