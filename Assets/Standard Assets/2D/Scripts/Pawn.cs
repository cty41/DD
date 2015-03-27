using System;
using UnityEngine;


namespace AssemblyCSharpfirstpass
{
	public class Pawn : MonoBehaviour
	{
		public int health = 20;
		public int attack = 5;
		public int speed = 4;
		public bool IsPlayer = false;

        public Skill[] CombatSkills;

		void Start()
		{
			Debug.Log("Pawn Start");
		}

		void Update()
		{
            if (PlayerMgr.instance.IsChoosingTarget() && IsValidTarget() )
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {

                }
            }
            
		}

		void TakeDamage(int damage)
		{
			health -= damage;
			if (health <= 0)
			{
				health = 0;
				//die
			}
		}

		void Attack()
		{

		}

		public void StartAction()
		{
			if (IsPlayer) {
				PlayerMgr.instance.PlayerStart(this.gameObject);
			}
		}

		public bool IsReadyToMakeAction()
		{
			return true;
		}

        public bool IsValidTarget()
        {
            //TODO
            return !IsPlayer;
        }

        public void SelectSkill(int idx)
        {
            if (CombatSkills.Length > 0)
            {
                CombatSkills[idx].ChooseTarget();
            }
        }
	}
}

