using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class HeroTeam : MonoBehaviour
{
    public enum TeamState
    {
        TS_Explore = 0,
        TS_Combat = 1,
        TS_Camp = 2,
    };
    public TeamState teamState;
    public List<GameObject> Heroes { get; private set; }

    // Use this for initialization
    void Start()
    {
        Debug.Log("HeroTeam Start");
        teamState = TeamState.TS_Explore;
        UIMgr.instance.DisableSkillButtons();
        //Heroes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        Heroes = new List<GameObject>();
    }

    public void ChangeState(TeamState nextState)
    {
        if (nextState != teamState)
        {
            //TODO, ty.cheng
            if (nextState == TeamState.TS_Combat)
            {
                teamState = nextState;
                UIMgr.instance.EnableSkillButtons();
            }
            else if (nextState == TeamState.TS_Explore)
            {
                UIMgr.instance.DisableSkillButtons();
            }
            else
            {
                //TODO
            }
        }
    }
}
