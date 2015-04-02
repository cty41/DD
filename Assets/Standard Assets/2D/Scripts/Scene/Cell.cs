using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
	// public

	// private
	private SpriteRenderer sceneRenderer;
	
	void Start ()
	{

	}

	void Update ()
	{
	
	}

	public void Init(int index)
	{
		// string
		//TODO change the transform position for test - gt.jiang
		sceneRenderer = GetComponent<SpriteRenderer> ();
		float imgW = sceneRenderer.bounds.size.x;
		
		Vector3 newPosition = new Vector3 (index * imgW, 0f, 0f);
		this.transform.position = newPosition;

		//sceneRenderer.sprite = .
	}
}
