using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TouchOnActionButton(GameObject buttonObj)
    {
        Button button = buttonObj.GetComponent<Button>();
        Debug.Log("GUI TouchOnActionButton", button);
        PlayerMgr.instance.SelectSkill(0);
    }
}
