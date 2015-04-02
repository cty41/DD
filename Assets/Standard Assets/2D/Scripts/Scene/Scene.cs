using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scene : MonoBehaviour
{
	// pulbic
	public List<GameObject> Cells;

	void Awark()
	{

	}
	
	void Start ()
	{

	}

	void Update ()
	{
		
	}

	public void Init()
	{
		for (int i = -3; i < 3; i++)
		{
			Cell cell = Instantiate(Resources.Load("Scene/Corridor", typeof(Cell))) as Cell;
			cell.Init (i);
		}
	}
}