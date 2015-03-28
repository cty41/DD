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

        //0-3, 0->melee, 3->range
        public int position;
        public bool IsSelectingTarget { get; private set; } 

        public GameObject[] CombatSkills;
        private Skill  CurrentSkill;
        public SpriteRenderer TargetIndicator; 

		void Start()
		{
            IsSelectingTarget = false;
            if (TargetIndicator != null)
            {
                TargetIndicator.enabled = false;
            }
			Debug.Log("Pawn Start");
		}

		void Update()
		{
            if (PlayerMgr.instance.IsChoosingTarget() && IsValidTarget() && Input.GetMouseButtonDown(0))
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    //TODO, use skill -ty.cheng
                    Debug.Log("hit the enemy, cancel selectable state");
                    PlayerMgr.instance.UseSkill(this);
                }
            }
            
		}

		void TakeDamage(int damage)
		{
			health -= damage;
            Debug.Log(gameObject + " Take Damage " + damage + " Current Health " + health);
			if (health <= 0)
			{
				health = 0;
				//die
                Debug.Log(gameObject + " Died ! ");
			}
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
            //TODO -ty.cheng
            return !IsPlayer;
        }

        public void SelectSkill(int idx)
        {
            if (CombatSkills.Length > 0)
            {
                IsSelectingTarget = true;
                CurrentSkill = CombatSkills[idx].GetComponent<Skill>();
                for (int i = 0; i < CurrentSkill.AttackPosition.Length; ++i)
                {
                    if (CurrentSkill.AttackPosition[i])
                    {
                        foreach ( GameObject monster in CombatMgr.instance.Monsters)
                        {
                            var p = monster.GetComponent<Pawn>();
                            if(p.position == i)
                            {
                                p.SetSelectable();
                                break;
                            }
                        }
                        
                    }
                }
            }
        }

        //TODO -ty.cheng
        public void CancelSkill()
        {
            IsSelectingTarget = false;
        }

        public void SetSelectable()
        {
            //
            if (TargetIndicator != null)
            {
                Debug.Log("Show Target indicator pawn gameobj " + this.gameObject + " indicator gameobj " + TargetIndicator.gameObject);
                TargetIndicator.enabled = true;
            }
        }

        public void UseSkill(Pawn target)
        {
            if (CurrentSkill.DamageMod > 0)
            {
                target.TakeDamage(Convert.ToInt32(attack * CurrentSkill.DamageMod));
            }
        }
	}
}

