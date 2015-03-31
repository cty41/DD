using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIMgr : MonoBehaviour {

    public static UIMgr instance { get; private set; }
    public List<GameObject> SkillButtons;
	// Use this for initialization

    //use for combat debug
    private bool IsInCombat = false;
    void Awake()
    {
        instance = this;
    }
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        bool value = Input.GetKeyUp("f1");
        if (value)
        {
            IsInCombat = !IsInCombat;
            if (IsInCombat)
            {
                GameInfo.instance.StartCombat();
            }
            else
            {
                GameInfo.instance.ExitCombat();
            }
            Debug.Log("release f1");
        }
	}

    public void TouchOnActionButton(GameObject buttonObj)
    {
        Button button = buttonObj.GetComponent<Button>();
        Debug.Log("GUI TouchOnActionButton", button);
        PlayerMgr.instance.SelectSkill(0);
    }

    public void EnableSkillButtons()
    {
        foreach (GameObject button in SkillButtons)
        {
            Debug.Log("EnableSkillButtons" + button);
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void DisableSkillButtons()
    {
        foreach (GameObject button in SkillButtons)
        {
            Debug.Log("DisableSkillButtons" + button);
            button.GetComponent<Button>().interactable = false;
        }
    }
}
