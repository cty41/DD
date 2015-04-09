using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    public Pawn SelfPawn;
    public Pawn CurrentTarget;
    // Use this for initialization
    void Start()
    {
        SelfPawn = gameObject.GetComponent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AIStart()
    {
        SelectSkill();
    }

    public void SelectSkill()
    {
        Debug.Log("AI Select Skill");
        StartCoroutine(DelaySelectSkill());
    }

    public IEnumerator DelaySelectSkill()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("start DelaySelectSkill");
        //by default select randomly
        if (SelfPawn.CombatSkills.Length > 0 )
            SelfPawn.SelectSkill(Random.Range(0, SelfPawn.CombatSkills.Length - 1));

        SelectTarget();
        Debug.Log("stop DelaySelectSkill");
        StopCoroutine(DelaySelectSkill());
        //StartCoroutine(DelaySelectTarget());
    }

    public void SelectTarget()
    {
        List<Pawn> selectableHeroes = new List<Pawn>();
        for (int i = 0; i < SelfPawn.CurrentSkill.AttackPosition.Length; ++i)
        {
            foreach (GameObject hero in CombatMgr.instance.Heroes)
            {
                Pawn p = hero.GetComponent<Pawn>();
                if (p.position == i)
                {
                    selectableHeroes.Add(p);
                    //p.SetSelectable();
                    //break;
                }
            }
        }

        int idx = Random.Range(0, selectableHeroes.Count - 1);
        CurrentTarget = selectableHeroes[idx];
        CurrentTarget.SetSelectable();
        Debug.Log("AI SelectTarget");

        StartCoroutine(DelayAIUseSkill());
    }

    public IEnumerator DelayAIUseSkill()
    {
        yield return new WaitForSeconds(1);
        if (SelfPawn.CurrentSkill != null)
        {
            SelfPawn.UseSkill(CurrentTarget);
            Debug.Log("DelayAIUseSkill skill used");
        }
        else
        {
            StopCoroutine(DelayAIUseSkill());
            Debug.Log("StopCoroutine DelayAIUseSkill");
        }

    }
}
