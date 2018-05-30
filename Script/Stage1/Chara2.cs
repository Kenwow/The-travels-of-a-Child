using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara2 : MonoBehaviour {
	//hey!
	private int goodChoise = 0;
	public float speed  = 2;
	public float runSpeed = 4;
	private int life = 3;
	public GameObject[] heart = new GameObject[3];


//	private AudioSource chara_Audio;
//	public AudioClip Audio_die;
//	public AudioClip Audio_recover;

	public AudioControl audioControl;

	private Rigidbody2D chara_Rb;
	private CapsuleCollider2D chara_Coll;
	//--------------------------------------
	//animator
	private Animator charaAnimator;
	AnimatorOverrideController overrideController;
	private int stateID = -1;
	private int idleSID = -1;
	private int moveSID = -1;
	private int runSID = -1;
	private int shotSID = -1;
	private int isHitID = -1;
	private int dieID = -1;
	private int relifeID = -1;
	private int hitRID = -1;
	//--------------------------------------

	//--------------------------------------
	public GameObject bulletL;
	public GameObject bulletR;
	public GameObject bulletU;
	public GameObject bulletD;

	//time for bullet
	private float bullet_time = 0;
	private float b_t_limit = 0.3f;//the internal time between two shoot
	//--------------------------------------

	//--------------------------------------
	//chara state
	private enum State
	{
		Idle,
		move,
		run,
		Die,
	}
	private State charaState = State.Idle;
	//--------------------------------------

	private enum Direction{
		front,
		back,
		left,
		right
	}
	private Direction charaDir = Direction.front;


	//--------------------------------------
	//after hit the charator will have an invincible time
	private float invcTime = 1.5f;
	private bool isInv = false;
	//--------------------------------------


	void Awake(){
		stateID = Animator.StringToHash ("state");
		idleSID = Animator.StringToHash ("idleS");
		moveSID = Animator.StringToHash ("moveS");
		runSID = Animator.StringToHash ("runS");
		shotSID = Animator.StringToHash ("shotS");
		isHitID = Animator.StringToHash ("isHit");
		dieID = Animator.StringToHash ("die");
		relifeID = Animator.StringToHash ("relife");
		hitRID = Animator.StringToHash ("hitR");
	}

	// Use this for initialization
	void Start () {
		chara_Coll = GetComponent<CapsuleCollider2D> ();
		chara_Rb = gameObject.GetComponent<Rigidbody2D> ();
		charaAnimator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Time control
		if(bullet_time < b_t_limit){
			bullet_time += Time.deltaTime;
		}
		pressTime += Time.deltaTime;
		dashTC -= Time.deltaTime;

		if (charaState == State.Die) {
			return;
		}
		if(isInv)
			invincible ();
		
		if (Input.GetKeyUp ("a") || Input.GetKeyUp ("s") || Input.GetKeyUp ("d") || Input.GetKeyUp ("w")) {
			if (!Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("w")) {
				charaState = State.Idle;
				charaAnimator.SetFloat (stateID, 0);
				audioControl.CharaStop ();
				if(Input.GetKeyUp ("d"))
					gameObject.transform.localScale = new Vector3 (0.42f, 0.42f, 0);
			}
		}
//		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right") || Input.GetKeyUp ("down") || Input.GetKeyUp ("up")) {
//			
//			charaAnimator.SetFloat (idleSID, 0);
//
//	
//		}
		if (Input.anyKey) {
			control ();

			if (bullet_time >= b_t_limit && Input.GetKey("j")) {
				shoot ();
			}

		}
		if (Input.GetKeyUp ("j")) {
			charaAnimator.SetFloat (idleSID, 0);
		}

	}

	//private Key lastKey = Key.none;

	private KeyCode lastKey = KeyCode.Space;
	private float pressTime = 0;
	private float dashInterval = 0.5f;
	private float dashTC = 0.8f;
	private float dashT = 0.8f;

	void control(){
		chara_Rb.velocity = new Vector2(0, 0);
		//change state

		if (Input.anyKeyDown) {
			if (Input.GetKeyDown ("a")) {
				charaDir = Direction.left;
				if (lastKey == KeyCode.A && pressTime < dashInterval)
					ct_dashLeft ();
				else {
					charaState = State.move;
					audioControl.PlayWalkFootstep ();
					charaAnimator.SetFloat (stateID, 1);
					charaAnimator.SetFloat (moveSID, 2);
					gameObject.transform.localScale = new Vector3 (0.42f, 0.42f, 0);
				}
			    lastKey = KeyCode.A;
				pressTime = 0;
			
			}
			if (Input.GetKeyDown ("s")) {
				charaDir = Direction.front;
				if (lastKey==KeyCode.S && pressTime<dashInterval)
					ct_dashDown();
				else {
					charaState = State.move;
					audioControl.PlayWalkFootstep ();
					charaAnimator.SetFloat (stateID, 1);
					charaAnimator.SetFloat (moveSID, 0);
				}
				lastKey = KeyCode.S;
				pressTime = 0;

			}
			if (Input.GetKeyDown ("d")) {
				charaDir = Direction.right;
				if (lastKey==KeyCode.D && pressTime<dashInterval)
					ct_dashRight();
				else {
					charaState = State.move;
					audioControl.PlayWalkFootstep ();
					charaAnimator.SetFloat (stateID, 1);
					charaAnimator.SetFloat (moveSID, 2);
					gameObject.transform.localScale = new Vector3 (-0.42f, 0.42f, 0);
				}
				lastKey = KeyCode.D;
				pressTime = 0;

			}
			if (Input.GetKeyDown ("w")) {
				charaDir = Direction.back;
				if (lastKey == KeyCode.W && pressTime < dashInterval)
					ct_dashUp ();
				else {
					charaState = State.move;
					audioControl.PlayWalkFootstep ();
					charaAnimator.SetFloat (stateID, 1);
					charaAnimator.SetFloat (moveSID, 1);
				}
				lastKey = KeyCode.W;
				pressTime = 0;

			}
		}

		if (charaState == State.move)
			chara_Rb.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), speed * Input.GetAxis ("Vertical"));
		else if (charaState == State.run) {
			chara_Rb.velocity = new Vector2 (runSpeed * Input.GetAxis ("Horizontal"), runSpeed * Input.GetAxis ("Vertical"));
			//isDashEnd ();
		}
		

	}
	void ct_dashLeft(){
		charaState = State.run;
		audioControl.PlayRunFootstep ();
		charaAnimator.SetFloat (stateID, 2);
		charaAnimator.SetFloat (runSID, 2);
		gameObject.transform.localScale = new Vector3 (-0.42f, 0.42f, 0);
		dashTC = dashT;
	}
	void ct_dashDown(){
		charaState = State.run;
		audioControl.PlayRunFootstep ();
		charaAnimator.SetFloat (stateID, 2);
		charaAnimator.SetFloat (runSID, 0);
		dashTC = dashT;
	}
	void ct_dashRight(){
		charaState = State.run;
		audioControl.PlayRunFootstep ();
		charaAnimator.SetFloat (stateID, 2);
		charaAnimator.SetFloat (runSID, 2);
		gameObject.transform.localScale = new Vector3 (0.42f, 0.42f, 0);
		dashTC = dashT;
	}
	void ct_dashUp(){
		charaState = State.run;
		audioControl.PlayRunFootstep ();
		charaAnimator.SetFloat (stateID, 2);
		charaAnimator.SetFloat (runSID, 1);
		dashTC = dashT;
	}
	void isDashEnd(){
		if (dashTC < 0) {
			Debug.Log ("hey");
			charaState = State.move;
			charaAnimator.SetFloat (stateID, 1);
			float direction = charaAnimator.GetFloat (runSID);
			charaAnimator.SetFloat (moveSID,direction);
			if (direction == 2) {
				float scale = gameObject.transform.localScale.x;
				gameObject.transform.localScale = new Vector3 (-scale, 0.42f, 0);
			}
		}
	}
	void shoot(){
		//audioControl.CharaShoot();
		if (charaDir==Direction.left) {
			Instantiate (bulletL, gameObject.transform.position + new Vector3 (-0.5f, 0, 0), Quaternion.identity);
			bullet_time = 0;
			ct_shot ();
			if (charaState == State.Idle) {
				charaAnimator.SetFloat (idleSID, 3);
			}

		} else if (charaDir==Direction.right) {
			Instantiate (bulletR, gameObject.transform.position + new Vector3 (0.5f, 0, 0), Quaternion.identity);
			bullet_time = 0;
			ct_shot ();
			if (charaState == State.Idle) {
				charaAnimator.SetFloat (idleSID, 4);
			}
		} else if (charaDir==Direction.back) {
			Instantiate (bulletU, gameObject.transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);
			bullet_time = 0;
			ct_shot ();
			if (charaState == State.Idle) {
				charaAnimator.SetFloat (idleSID, 2);
			}
		} else if (charaDir==Direction.front) {
			Instantiate (bulletD, gameObject.transform.position + new Vector3 (0, -0.5f, 0), Quaternion.identity);
			bullet_time = 0;
			ct_shot ();
			if (charaState == State.Idle) {
				charaAnimator.SetFloat (idleSID, 1);
			}
		} else if (charaState == State.run) {
			charaAnimator.SetFloat (stateID, 2);
		}



	}
	void ct_shot(){
		if (charaState == State.run) {
			charaAnimator.SetFloat (stateID, 3);
			charaAnimator.SetFloat (shotSID, charaAnimator.GetFloat (runSID));
		}

	}
	void dieEnd(){
		
//		audioControl.CharaRecovery ();
//		chara_Audio.clip = Audio_recover;
//		chara_Audio.Play ();
		//GetComponent<AudioSource> ().Play ();
		chara_Coll.enabled = true;
		charaState = State.Idle;
		charaAnimator.SetTrigger (relifeID);
		charaAnimator.SetBool(isHitID,false);
		charaAnimator.SetFloat (stateID, 0);
		setHP (life);
		//gameObject.GetComponent<CircleCollider2D> ().enabled = true;
	}
	void attackedEnd(){
		charaAnimator.SetBool(isHitID,false);

	}
	void invincible(){
		invcTime -= Time.deltaTime;
		int invcCount = (int)((1-invcTime)/0.1f);
		if (invcTime < 0) {
			isInv = false;
			invcTime = 1.5f;
			return;
		}
		if (invcCount%2 == 0) {
			gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,0.3f);
		}
		else
			gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1);
	}
	void OnCollisionEnter2D(Collision2D coll){
		
		if (isInv || charaState==State.Die)
			return;
		if (coll.gameObject.tag == "monster" || coll.gameObject.tag == "monsterplus" ) {
//			chara_Audio.clip = Audio_die;
//			chara_Audio.Play ();
			audioControl.PlayHit();
			isInv = true;
			if (coll.gameObject.tag == "monster") {
				life -= 1;
				setHP (life);
			} else {
				life -= 2;
				setHP (life);
			}
			
			if (life <= 0) {
				Debug.Log ("Die");
				life = 3;
				charaState = State.Die;
				audioControl.CharaStop ();
				audioControl.CharaDie ();
//				chara_Audio.clip = Audio_die;
//				chara_Audio.Play ();
				chara_Coll.enabled = false;
				charaAnimator.SetTrigger (dieID);
				isInv = true;
			
				//gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			
			} else {
				
				Vector3 tmpVect = gameObject.transform.position-coll.gameObject.transform.position;
				charaAnimator.transform.localScale = new Vector3 (0.42f, 0.42f, 0);
				tmpVect.Normalize();
				//gameObject.transform.Translate (0.4f*tmpVect);
				charaAnimator.SetBool(isHitID,true);
				if (tmpVect.x > 0) {
					charaAnimator.SetTrigger (hitRID);
				}
					
			}

		}
	}

	void setHP(int life){
		switch (life) {

		case 1:
			heart [0].SetActive (true);
			heart [1].SetActive (false);
			heart [2].SetActive (false);
			break;
		case 2:
			heart [0].SetActive (true);
			heart [1].SetActive (true);
			heart [2].SetActive (false);
			break;
		case 3:
			heart [0].SetActive (true);
			heart [1].SetActive (true);
			heart [2].SetActive (true);
			break;
		default:
			heart [0].SetActive (false);
			heart [1].SetActive (false);
			heart [2].SetActive (false);
			break;	
		}
	}
}
