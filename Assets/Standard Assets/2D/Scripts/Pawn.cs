using System;
using UnityEngine;
using UnityEngine.UI;



namespace AssemblyCSharpfirstpass
{
	public class Pawn : MonoBehaviour
	{
        public int MaxHealth = 20;
		public int health = 0;
		public int attack = 5;
		public int speed = 4;
		public bool IsPlayer = false;
        public Vector2 velocity;
        //0-3, 0->melee, 3->range
        public int position;
        public float uniformScale = 1;
        public bool IsSelectingTarget { get; private set; } 

        public GameObject[] CombatSkills;
        public Skill  CurrentSkill { get; private set; } 
        public SpriteRenderer TargetIndicator;

        public GameObject healthBar;
        public Image healthBarImage;
        public Collider2D collider;
        public SpriteRenderer pawnSprite { get; private set; } 
        public GameObject canvas;

		void Start()
		{
            health = MaxHealth;
            IsSelectingTarget = false;
            if (TargetIndicator != null)
            {
                TargetIndicator.enabled = false;
            }
			Debug.Log("Pawn Start");
            healthBar = Instantiate(Resources.Load("Overlays/healthbar", typeof(GameObject))) as GameObject;

            collider = gameObject.GetComponent<Collider2D>();
            pawnSprite = gameObject.GetComponent<SpriteRenderer>();

            GameObject full = healthBar.transform.Find("healthbar_full").gameObject;
            Debug.Log("find the real health bar " + full);
            healthBarImage = full.GetComponent<Image>();
            healthBarImage.fillAmount = 1.0f;

            transform.localScale = new Vector3(uniformScale, uniformScale, uniformScale);

            float posX = 0;
            float posY = 0 - GameInfo.instance.backGround.cellHeight * 0.5f + 
                gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y * 0.5f * uniformScale;
            float posZ = transform.position.z;
            transform.position = new Vector3( posX, posY, posZ);
		}

        void FixedUpdate()
        {
            transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.smoothDeltaTime;
        }

		void Update()
		{
            if (PlayerMgr.instance.IsChoosingTarget() && IsValidTarget() && Input.GetMouseButtonUp(0))
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
            UpdateHealthBar();

            GameInfo.instance.ClampInScene(this);
		}

        void UpdateHealthBar()
        {
            //update position 
            canvas = GameObject.FindGameObjectWithTag("Canvas");
            healthBar.transform.SetParent(canvas.transform);
            Vector3 offset = new Vector3(0, -collider.bounds.size.y * 0.5f, 0);
            Vector3 screenPos = Camera.main.WorldToViewportPoint(gameObject.transform.position + offset);
            //Debug.Log("Pawn " + this + " ViewportPoint " + screenPos);
            //-10.f hard code coded
            healthBar.transform.position = new Vector3(screenPos.x * Screen.width, screenPos.y * Screen.height - 10.0f, healthBar.transform.position.z);

            healthBarImage.fillAmount = health / (float)MaxHealth;
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
                CombatMgr.instance.HandleCombatEnded(this);
			}
		}

		public void StartAction()
		{
            if (IsPlayer)
            {
                PlayerMgr.instance.PlayerStart(this.gameObject);
            }
            else
            {
                //we are monster, use ai to select skill
                gameObject.GetComponent<AIController>().AIStart();
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
            Debug.Log("Pawn SelectSkill length " + CombatSkills.Length + " CurrentSkillIdx " + idx);
            if (CombatSkills.Length > 0)
            {
                IsSelectingTarget = true;
                CurrentSkill = CombatSkills[idx].GetComponent<Skill>();
                ShowSelectableTarget();
            }
        }

        private void ShowSelectableTarget()
        {
            if (IsPlayer)
            {
                for (int i = 0; i < CurrentSkill.AttackPosition.Length; ++i)
                {
                    if (CurrentSkill.AttackPosition[i])
                    {
                        foreach (GameObject monster in CombatMgr.instance.Monsters)
                        {
                            Pawn p = monster.GetComponent<Pawn>();
                            if (p.position == i)
                            {
                                p.SetSelectable();
                            }
                        }
                    }
                }

            }
            else
            {
                AIController AI = gameObject.GetComponent<AIController>();

                if (AI != null)
                {
                    AI.SelectTarget();
                }
            }

        }

        //TODO -ty.cheng
        public void CancelSkill()
        {
            IsSelectingTarget = false;
            CurrentSkill = null;
        }

        public bool IsSelectable()
        {
            return TargetIndicator.enabled;
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

        public void CancelSelectable()
        {
            if (TargetIndicator != null)
            {
                TargetIndicator.enabled = false;
            }
        }

        public void UseSkill(Pawn target)
        {
            if (CurrentSkill.DamageMod > 0)
            {
                target.TakeDamage(Convert.ToInt32(attack * CurrentSkill.DamageMod));
            }

            CancelSkill();
            target.CancelSelectable();

            CombatMgr.instance.NextAttacker();
         
        }


	}
}

