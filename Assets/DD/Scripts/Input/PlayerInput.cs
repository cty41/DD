using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;

public class PlayerInput : MonoBehaviour
{
	private float h;
	private float v;

	void Start ()
	{
	
	}

	void Update ()
	{
		DebugInput ();
	}

	void DebugInput()
	{
		GameObject hero = GameInfo.instance.heroTeam.Heroes [0];
		Vector3 heroPosition = GameInfo.instance.heroTeam.Heroes[0].transform.position;
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Pawn p = hero.GetComponent<Pawn>();
        //if (Input.GetMouseButton (0))
        //{
        //    if (Mathf.Abs (mousePosition.x - heroPosition.x) < 2)
        //    {
        //        hero.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //    }
        //    else if (mousePosition.x > heroPosition.x)
        //    {
        //        hero.GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0);
        //    }
        //    else 
        //    {
        //        hero.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
        //    }
        //}
        //else
        //{
        //    hero.GetComponent<Rigidbody2D>().velocity = new Vector2(0, hero.GetComponent<Rigidbody2D>().velocity.y);
        //}

        if (Input.GetKey(KeyCode.A))
        {
            p.SetVelocity(new Vector2(-3, 0));

        }
        else if (Input.GetKey(KeyCode.D))
        {
            p.SetVelocity(new Vector2(4, 0));
        }
        else
        {
            p.SetVelocity(Vector2.zero);
        }
	}

	void MobileInput()
	{
	}

	void FixedUpdate ()
	{

	}
}