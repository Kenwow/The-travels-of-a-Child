using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chara : MonoBehaviour {
	//public GameObject chara;
	public float speed  = 4;
	public GameObject[] blood;
	public int bloodC = 3;
	//private bool isDied = false;

	public GameObject bulletL;
	public GameObject bulletR;
	public GameObject bulletU;
	public GameObject bulletD;
	//float translation;
	Vector3 LastMove;
	private Rigidbody2D chara_Rb;

	//after hit the charator will have an invincible time
	//private bool isInvincible;
	private float invcTime = 1;

	//chara dash
	private float dT = 0.4f;
	private float dashTc = 0.4f;
	private int lastDash = 0;//0-no input 1-down 2- left 3-right 4-up

	private bool isStop = true;
	private float stopTc = 0.8f;

	//chara state
	private enum State
	{
		Norm,
		Die,
		Inv
	}
	private State charaState = State.Norm;

	//when chara is die
	private float dieTC = 3;
	public Text dialog;

	//time for bullet
	private float bullet_time = 0;
	private float b_t_limit = 0.3f;//the internal time between two shoot

	//animator
	private Animator charaAnimator;
	private int runID = -1;
	private int blendID = -1;
	private int blend2ID = -1;
	private int attackedID = -1;
	private int isRID = -1;
	private int isReID = -1;
	private int isDieID = -1;
	AnimatorOverrideController overrideController;
	void Awake(){
		runID = Animator.StringToHash ("run");
		blendID = Animator.StringToHash ("Blend");
		blend2ID = Animator.StringToHash ("Blend2");
		attackedID = Animator.StringToHash ("Attacked");
		isRID = Animator.StringToHash ("isRight");
		isReID = Animator.StringToHash ("isRe");
		isDieID = Animator.StringToHash ("isDie");
//		overrideController = new AnimatorOverrideController(); 
//		overrideController.runtimeAnimatorController  = Anitor.runtimeAnimatorController; 
	}

	// Use this for initialization
	void Start () {
		//translation = Time.deltaTime*3;
		chara_Rb = gameObject.GetComponent<Rigidbody2D> ();
		charaAnimator = gameObject.GetComponent<Animator> ();
		//speed *= Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		dashTc -= Time.deltaTime;
		if (dashTc < 0) {
			lastDash = 0;
		}
		if (charaState == State.Die) {
			//whenDie();
			return;
		}
		if(charaState == State.Inv)
			invincible ();
		control ();
		if(bullet_time < 0.5){
			bullet_time += Time.deltaTime;
		}
		if (bullet_time > b_t_limit) {
			shoot ();
		}
			
            
	}
	void shoot(){
		if (Input.GetKey ("left")) {
			//+ new Vector3 (-0.5f, 0.5f, 0)
			Instantiate (bulletL, gameObject.transform.position + new Vector3 (-0.5f, 0, 0), Quaternion.identity);
			bullet_time = 0;
		}
		else if (Input.GetKey ("right")) {
			//+ new Vector3 (-0.5f, 0.5f, 0)
			Instantiate (bulletR, gameObject.transform.position + new Vector3 (0.5f, 0, 0), Quaternion.identity);
			bullet_time = 0;
		}
		else if (Input.GetKey ("up")) {
			//+ new Vector3 (-0.5f, 0.5f, 0)
			Instantiate (bulletU, gameObject.transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);
			bullet_time = 0;
		}
		else if (Input.GetKey ("down")) {
			//+ new Vector3 (-0.5f, 0.5f, 0)
			Instantiate (bulletD, gameObject.transform.position + new Vector3 (0, -0.5f, 0), Quaternion.identity);
			bullet_time = 0;
		}
	}
	void control(){
		float translation = 0.8f;
		if (1 == lastDash && Input.GetKeyDown ("s")) {
			gameObject.transform.Translate (0, -translation, 0);
			lastDash = 0;
			return;
		}
		if (2 == lastDash && Input.GetKeyDown ("a")) {
			gameObject.transform.Translate (-translation, 0,0);
			lastDash = 0;
			return;
		}
		if (3 == lastDash && Input.GetKeyDown ("d")) {
			gameObject.transform.Translate (translation, 0,0);
			lastDash = 0;
			gameObject.transform.localScale = new Vector3 (-0.35f, 0.35f, 0);
			return;
		}
		if (4 == lastDash && Input.GetKeyDown ("w")) {
			gameObject.transform.Translate (0, translation, 0);
			lastDash = 0;
			return;
		}
		chara_Rb.velocity = new Vector2(0, 0);

		if (Input.GetKey ("w")) {
			if (Input.GetKeyDown ("w")) {
				lastDash = 4;
				dashTc = dT;
			}
			//Chara.transform.Translate (0, translation, 0);
			//chara_Rb.AddForce(Vector3.up*20,ForceMode2D.Force);
			chara_Rb.velocity += new Vector2 (0, speed);
			charaAnimator.SetFloat (blendID, -0.8f);
			charaAnimator.SetFloat (blend2ID, 0f);
		}
		//		if (Input.GetKeyUp ("w")) {
		//			charaAnimator.SetFloat (blendID, 0f);
		//			charaAnimator.SetFloat (blend2ID, 0f);
		//			gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
		//		}

		if (Input.GetKey ("s")) {
			if (Input.GetKeyDown ("s")) {
				lastDash = 1;
				dashTc = dT;
			}
			//Chara.transform.Translate (0, -translation, 0);
			chara_Rb.velocity += new Vector2 (0, -speed);
			charaAnimator.SetFloat (blendID, 0.8f);
			charaAnimator.SetFloat (blend2ID, 0f);
		}
		//		if (Input.GetKeyUp ("a") || Input.GetKeyUp ("s") ||Input.GetKeyUp ("d")||Input.GetKeyUp ("w")) {
		//			charaAnimator.SetInteger (runID, 0);
		//			Debug.Log ("hey");
		//			gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
		//		}
		if (Input.GetKey ("a")) {
			if (Input.GetKeyDown ("a")) {
				lastDash = 2;
				dashTc = dT;
				gameObject.transform.localScale = new Vector3 (0.35f, 0.35f, 0);
			}
			//Chara.transform.Translate (-translation, 0, 0);
			chara_Rb.velocity += new Vector2 (-speed, 0);
			charaAnimator.SetFloat (blendID, 0f);
			charaAnimator.SetFloat (blend2ID, 0.8f);
		}
		//		if (Input.GetKeyUp ("a") || Input.GetKeyUp ("s") ||Input.GetKeyUp ("d")||Input.GetKeyUp ("w")) {
		//			charaAnimator.SetInteger (runID, 0);
		//			Debug.Log ("hey");
		//			gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
		//		}
		if (Input.GetKeyDown ("d")) {
			gameObject.transform.localScale = new Vector3 (-0.35f, 0.35f, 0);
			lastDash = 3;
			dashTc = dT;
		}
		if (Input.GetKey ("d")) {
			//Chara.transform.Translate (translation, 0, 0);
			charaAnimator.SetInteger (runID, 2);
			chara_Rb.velocity += new Vector2 (speed,0);
			charaAnimator.SetFloat (blendID, 0f);
			charaAnimator.SetFloat (blend2ID, -0.8f);
			//charaAnimator.SetInteger (runID, 2);
		}
		if (Input.GetKeyUp ("a") || Input.GetKeyUp ("s") ||Input.GetKeyUp ("d")||Input.GetKeyUp ("w")) {
			stopTc = 0.4f;
		}

		if (stopTc > 0) {
			stopTc -= Time.deltaTime;
		} else if(stopTc < 0 && !Input.anyKey) {
			charaAnimator.SetFloat (blendID, 0f);
			charaAnimator.SetFloat (blend2ID, 0f);
			gameObject.transform.localScale = new Vector3 (0.35f, 0.35f, 0);
			stopTc = 0;
		}
		chara_Rb.velocity.Normalize ();

	}
	bool CharaIsIn(){
		Vector3 pos = gameObject.transform.position;
		if (pos.x < 3.5 && pos.x > -3.5 && pos.y < 4.5 && pos.y > -4.5)
			return true;
		return false;
	}
	void OnCollisionEnter2D(Collision2D coll){
		Debug.Log (coll.gameObject.name);
		if (charaState == State.Inv || charaState == State.Die)
			return;
		if (coll.gameObject.tag == "monster" || coll.gameObject.tag == "monsterplus" ) {
			charaState = State.Inv;
			charaAnimator.SetInteger (runID,0);
			if (coll.gameObject.tag == "monster")
				bloodC -= 1;
			else
				bloodC -= 2;
			charaAnimator.SetTrigger (attackedID);
			if (bloodC <= 0) {
				bloodC = 3;
				charaState = State.Die;
				charaAnimator.SetTrigger (isDieID);
				dialog.text = "你死了！3s后复活";
				//Application.LoadLevel("Scene/StageEnd");
			}
			//blood [bloodC].SetActive (false);
			Vector3 tmpVect = gameObject.transform.position-coll.gameObject.transform.position;
			tmpVect.Normalize();
			gameObject.transform.Translate (0.4f*tmpVect);
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log (coll.gameObject.name);


	}
	void whenDie(){
		dieTC -= Time.deltaTime;

		if (dieTC < 0) {
			charaState = State.Norm;
			dieTC = 3;
			charaAnimator.SetTrigger (isReID);
			dialog.text = "你复活了";
			return;
		}
	}
	void dieEnd(){
		charaState = State.Norm;
		charaAnimator.SetTrigger (isReID);
		dialog.text = "你复活了";
	}
	void invincible(){
		invcTime -= Time.deltaTime;
		int invcCount = (int)((1-invcTime)/0.1f);
//		if (invcTime < 0) {
//			charaState = State.Norm;
//			invcTime = 1;
//			return;
//		}
		if (invcCount%2 == 0) {
			gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,0.3f);
		}
		else
			gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1);
	}
	public void attackedEnd(){
		charaAnimator.SetInteger (runID,1);
		charaState = State.Norm;
		//Debug.Log ("受击结束");
	}
}
