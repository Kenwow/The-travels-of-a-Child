using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoriChara : MonoBehaviour {
	//hey!
	private int goodChoise = 0;
	public AudioControl audioControl;

	public float maxSpeed = 8;
	private Rigidbody2D charaRigid;
	public GameObject roof;
	public GameObject bullet;
	private float bullet_time = 0;
	private float b_t_limit = 0.3f;

	private bool canEX = false;
	public GameObject EXMask;
	public GameObject EXok;
	private float EXenergy = 2.5f;
	private bool isEX = false;
	public GameObject EX;

	private Animator charaAnimator;
	AnimatorOverrideController overrideController;


	private int stateID = -1;
	private int jumpID = -1;
	private int EXpreID = -1;
	private int EXID = -1;
	private int EXendID = -1;
	private int isHitID = -1;

	private bool isHitNow = false;
	private bool isInv = false;
	private float InvTC = 0;
	private int InvC = 0;
	private SpriteRenderer charaSR;

	private bool isJump = false;
	private int jumpState = 0;

	public int life = 3;
	public GameObject[] blood;
	private bool isDie = false;

	public GameObject black;


	void Awake(){
		EX.SetActive (false);
		stateID = Animator.StringToHash ("MoveState");
		EXpreID = Animator.StringToHash ("EXpre");
		EXID = Animator.StringToHash ("EX");
		EXendID = Animator.StringToHash ("EXend");
		jumpID = Animator.StringToHash ("JumpState");
		isHitID = Animator.StringToHash ("isHit");
	}

	// Use this for initialization
	void Start () {
		charaRigid = gameObject.GetComponent<Rigidbody2D> ();
		charaAnimator = gameObject.GetComponent<Animator> ();
		charaSR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isEX) {
			moveToEnd ();
			return;
		}
		if (isDie)
			return;
		if (isInv) {
			InvTC += Time.deltaTime;
			if (InvTC > 2)
				isInv = false;
			InvC++;
			if (InvC>8) {
				gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1f);
				InvC = 0;
			}
			else if(InvC>4){
				gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,0.5f);
			}
		}
		if (isHitNow) {
			
			return;
		}

			
		if(bullet_time < b_t_limit){
			bullet_time += Time.deltaTime;
		}
		if (Input.GetKeyUp ("a") || Input.GetKeyUp("d")) {
			audioControl.CharaStop();
			if (!isJump) {
				if (Input.GetKey ("j")) {
					charaAnimator.SetFloat (stateID, 3);
				}
				else
					charaAnimator.SetFloat (stateID, 0);

				setMirror (false);
			}
			
		}
		if (Input.GetKeyUp ("j")) {
			if (charaAnimator.GetFloat (stateID) == 3) {
				charaAnimator.SetFloat (stateID, 0);
			}
		}
		if (Input.anyKey) {
			Control ();

		}

		if (bullet_time >= b_t_limit && Input.GetKey("j")) {
			if (charaAnimator.GetFloat (stateID) == 0) {
				charaAnimator.SetFloat (stateID, 3);
			}
			Shoot ();
			if (!canEX) {
				EXMask.transform.Translate (0, 0.03f, 0);
				EXenergy += 0.1f;
				if (EXenergy > 4.8) {
					canEX = true;
					EXok.SetActive (true);

				}
			}

		}
		if (isJump) {
			switch (jumpState) {
			case 1:
				if (charaRigid.velocity.y < 0) {
					charaAnimator.SetFloat (jumpID, 2);
					jumpState=2;
				}
				break;
			case 2:
				if (charaRigid.velocity.y < 0.2 && charaRigid.velocity.y > -0.2) {
					charaAnimator.SetFloat (jumpID, 3);
					isJump = false;
					jumpState=3;
					setMirror (false);
				}
				break;
			}

			//charaRigid.d
		}
	}

	void Control(){
		if (Input.GetKeyDown ("m") && canEX) {
//			foreach (GameObject tmpG in GameObject.FindGameObjectsWithTag("hair")) {
//				Destroy(tmpG);
//			}
			roof.SetActive (false);
			charaRigid.velocity = new Vector2(0,-8);

			charaAnimator.SetTrigger (EXpreID);
			XDistance = transform.position.x + 9;
			canEX = false;
		}



		if (Input.GetKeyDown ("w")) {
//			if(transform.position.y<-4.4){
//				roof.SetActive (false);
//				Jump ();
//			}else if (transform.position.y<-2 && charaRigid.velocity.y == 0) {
//				Jump ();
//			}
			if (charaRigid.velocity.y <= 0.1f) {
				if (transform.position.y < -2.5) {
					roof.SetActive (false);
					Jump ();
				} else if (transform.position.y < -0f) {
					Jump ();
				}
			}
		}
		if (Input.GetKeyDown ("s") && !isJump) {
			roof.SetActive (false);
		}

		if (Input.GetKey ("d")) {
			if (Input.GetKeyDown ("d")) {
				if (!isJump) {
					audioControl.PlayRunFootstep ();
					charaAnimator.SetFloat (stateID, 1);
				}
				setMirror (false);
			}
			if (!isJump) {
				charaAnimator.SetFloat (stateID, 1);
			}
//			if(Mathf.Abs( charaRigid.velocity.x)<maxSpeed)
			charaRigid.velocity = new Vector2(5, charaRigid.velocity.y);
		}

		if (Input.GetKey ("a")) {
			if (Input.GetKeyDown ("a")) {
				if (!isJump) {
					audioControl.PlayRunFootstep ();
					charaAnimator.SetFloat (stateID, 1);
				}
				setMirror (true);
			}
			if (!isJump) {
				charaAnimator.SetFloat (stateID, 1);
				setMirror (true);
			}
//			if(Mathf.Abs( charaRigid.velocity.x)<maxSpeed)
			charaRigid.velocity = new Vector2(-5, charaRigid.velocity.y);
		}

			
	}
	void Jump(){
		audioControl.CharaStop ();
		audioControl.CharaJump ();
		roof.SetActive (false);
		isJump = true;
		charaAnimator.SetFloat (stateID, 2);
		charaAnimator.SetFloat (jumpID, 0);
		charaRigid.velocity = new Vector2(0, 18);
	}
	void Shoot(){
//		audioControl.CharaShoot ();
		Instantiate (bullet, gameObject.transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);
		bullet_time = 0;
	}
//	void dieEnd(){
//		charaAnimator.ResetTrigger (EXID);
//		charaAnimator.SetTrigger (EXendID);
//
//	}
	void setMirror(bool isMirror){
		Vector3 scale = transform.localScale;
		scale.x = Mathf.Abs (scale.x);
		if (isMirror) {
			scale.x *= -1;
		} 

		transform.localScale = scale;
	}
	public void isHit(){
		if (!isEX && !isInv) {
			audioControl.PlayHit ();
			isInv = true;
			InvTC = 0;
			InvC = 0;
			isHitNow = true;
			charaAnimator.SetFloat (stateID, 0);
			charaAnimator.SetBool (isHitID, true);
			life--;
			blood [life].SetActive (false);
			if (life == 0) {
				audioControl.CharaDie ();
				roof.SetActive (false);
				charaAnimator.SetTrigger ("isDie");
				isDie = true;
				roof.SetActive (false);
			}
		}
//		Debug.Log ("HitByHair");
	}
	public void isHitEnd(){
		isHitNow = false;
		charaAnimator.SetBool (isHitID, false);
		if (Input.GetKey ("j")) {
			charaAnimator.SetFloat (stateID, 3);
		} else if (Input.GetKey ("a") ) {
			charaAnimator.SetFloat (stateID, 1);
			setMirror (true);
		}
		else if(Input.GetKey ("d")){
			charaAnimator.SetFloat (stateID, 1);
			setMirror (false);
		}else
			charaAnimator.SetFloat (stateID, 0);
	}
	public void jumpEnd(){
		if (Input.GetKey ("a") || Input.GetKey ("d")) {
			charaAnimator.SetFloat (stateID, 1);
		}
		else if(Input.GetKey ("j")) {
			charaAnimator.SetFloat (stateID, 3);
		} else
			charaAnimator.SetFloat (stateID, 0);
		jumpState = 0;
	}

	public void jumpStart(){
		Debug.Log ("hey jumpStart");
		charaAnimator.SetFloat (jumpID, 1);
		jumpState=1;
	}
	public void newJump(){
		Debug.Log ("hey jumpStart");
		charaAnimator.SetFloat (jumpID, 1);
		jumpState=1;
		
	}


	float XDistance = 14;//XDistance*deltaTime
	public void startEx(){
		isEX = true;

	}
	public void moveToEnd(){
//		Vector3 newPosi = transform.position;
//		Debug.Log (newPosi);
//		newPosi.x -= 15 * Time.deltaTime;
//		Debug.Log (newPosi);
//		transform.position = newPosi;
		transform.Translate(-15*Time.deltaTime,-(transform.position.y+4.2f)*Time.deltaTime,0);
	}
	public void EndMove(){
		Time.timeScale = 0.1f;
		charaAnimator.SetTrigger (EXID);
		//this.enabled = false;
		isEX = false;
//		canEX = true;
		//roof.SetActive (false);
		EX.SetActive (true);
		EXMask.transform.localPosition = new Vector3 (0, 0.81f, 0);
		EXenergy = 0;
		EXok.SetActive (false);
	}
	public void EXInv(){
		EX.SetActive (false);
	}
	public void dieEnd(){
		black.SetActive (true);
		//SceneManager.LoadScene ("StageEnd");
	}
}
