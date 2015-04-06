using UnityEngine;
using System.Collections;
using AssemblyCSharpfirstpass;

public class Cell : MonoBehaviour
{
	// public
    public Pawn followTarget;
    public float velScale = 0.0f;
	// private
	public SpriteRenderer cellSprite;
	
	void Start ()
	{
        followTarget = GameInfo.instance.heroTeam.Heroes[0].GetComponent<Pawn>();
	}

	void Update ()
	{

	}


    void FixedUpdate()
    {
        if (followTarget.velocity.x != 0)
        {
            Camera2DFollow followCam = Camera.main.GetComponent<Camera2DFollow>();
            Vector3 vecDelta = transform.position;
            vecDelta.x = (followCam.transform.position.x - followCam.oldPos.x) * velScale;

            transform.position += vecDelta;
        }
    }

	public void Init(int index, string path, int layerOrder, float vel)
	{
		// string
		//TODO change the transform position for test - gt.jiang
		float imgW;

        velScale = vel;
		cellSprite = GetComponent<SpriteRenderer> ();
        cellSprite.sortingOrder = layerOrder;
		
		//this.transform.localScale += new Vector3(.5f, .5f, .5f);

		imgW = cellSprite.bounds.size.x;
        this.transform.position = new Vector3(index * imgW, 0, 0);
		
		cellSprite.sprite = Resources.Load (path, typeof(Sprite)) as Sprite;
	}
}
