using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
	// public

	// private
	public SpriteRenderer cellSprite;
	
	void Start ()
	{

	}

	void Update ()
	{
	
	}

	public void Init(int index, string path)
	{
		// string
		//TODO change the transform position for test - gt.jiang
		float imgW;

		cellSprite = GetComponent<SpriteRenderer> ();
		
		//this.transform.localScale += new Vector3(.5f, .5f, .5f);

		imgW = cellSprite.bounds.size.x;
		this.transform.position = new Vector3 (index * imgW, 0, 0);
		
		cellSprite.sprite = Resources.Load (path, typeof(Sprite)) as Sprite;
	}
}
