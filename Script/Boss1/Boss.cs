using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {
	public GameObject black;
	public SubtitleControl subtitle;
	private bool isHit;
	private int HitTC = 0;
	//public int Blood = 100;
	private float Harm = 0;
	public GameObject bloodMask;
	private int[] bloodState = new int[4]{100,125,160,204};
	private int whichState = 0;

	public AudioControl audioControl;

	// Use this for initialization
	void Start () {
		subtitle = GameObject.Find ("SubtitleControl").GetComponent<SubtitleControl>();
	}
	void Update(){
		if (Input.GetKeyDown ("9")) {
			black.SetActive (true);
//			SceneManager.LoadScene ("StageCom");
		}
		if (Input.GetKeyDown ("8")) {
			SceneManager.LoadScene ("StageEnd");
		}

	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isHit) {
			HitTC++;
			if (HitTC == 4) {
				gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1,1,1,1);
				isHit = false;
			}
		}
		if (whichState<4 &&  Harm > bloodState [whichState] ) {
			subtitle.BossWaring (whichState);
			whichState++;
			if (whichState > 3) {
				//应有毛发爆炸
				black.SetActive (true);
			}
		}
	

	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "bullet") {
			
			isHit = true;
			HitTC = 0;
			gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0.8f,0.8f,0.8f,1);
			//Blood--;
			Hit_By_Bullet ();
//			Hit_By_EX();
			//Destroy (coll.gameObject);

		}
	}

	public void Hit_By_Bullet(){
		bloodMask.transform.Translate (-0.1f,0, 0);
		Harm += 1;
		audioControl.MonsterHit ();
	}

	public void Hit_By_EX(){
		bloodMask.transform.Translate (-3f,0, 0);
		Harm += 30;
	}

}
