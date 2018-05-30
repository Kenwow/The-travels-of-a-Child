using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Control : MonoBehaviour {
	
	public SubtitleControl subtitle;
	private Animator bossAnimator;
	AnimatorOverrideController overrideController;
	private int aStartID = -1;
	private int aEndID = -1;

	public GameObject[] stopUI = new GameObject[6];
	private bool isStop = false;

	public GameObject hair;
	public GameObject fire;
	//hair drop control
	private int whichH = 1;
	private int whichS = 1;
	//hair drop mode
	private Vector3[] hairPosi1 = new Vector3[6]{new Vector3(5,0,0),new Vector3(-8,9,0),new Vector3(-5,9,0),new Vector3(-2,9,0),new Vector3(3,9,0),new Vector3(8,9,0)};
	private Vector3[] hairPosi2 = new Vector3[7]{new Vector3(6,0,0),new Vector3(-7,9,0),new Vector3(-5.5f,9,0),new Vector3(1,9,0),new Vector3(5.5f,9,0),new Vector3(7.5f,9,0),new Vector3(8.5f,9,0)};
	private Vector3[] hairPosi3 = new Vector3[7]{new Vector3(6,0,0),new Vector3(8.5f,9,0),new Vector3(7.5f,9,0),new Vector3(5.5f,9,0),new Vector3(1f,9,0),new Vector3(-5.5f,9,0),new Vector3(-7f,9,0)};
	private Vector3[] hairPosi4 = new Vector3[7]{new Vector3(6,0,0),new Vector3(-5.9f,9,0),new Vector3(5.6f,9,0),new Vector3(0f,9,0),new Vector3(2f,9,0),new Vector3(-2f,9,0),new Vector3(6.5f,9,0)};
	private int[] hairNumPerS1 = new int[6]{5,1,1,1,1,1};
	private int[] hairNumPerS2 = new int[7]{6,1,1,1,1,1,1};
	private int[] hairNumPerS3 = new int[7]{6,1,1,1,1,1,1};
	private int[] hairNumPerS4 = new int[5]{4,2,1,2,1};
	private int hairInterval = 6;

	//attack state
	private enum aState{
		idle,prepare, dropHair1, spitFire,dropHair2,dropHair3,dropHair4
	}
	private aState attackState = aState.idle;
	private aState[] attackList = new aState[]{aState.dropHair1,aState.dropHair2,aState.dropHair1,aState.dropHair3,aState.dropHair1,aState.dropHair2,aState.dropHair4};

	private int attackInterval =10;

	private float interCount = 5;
	private int attackC = 0;

	private int attackF = 0;

	private float fireTime = 0;
	void Awake(){
		aStartID = Animator.StringToHash ("AttackStart");
		aEndID = Animator.StringToHash ("AttackEnd");
	}
	// Use this for initialization
	void Start () {
		subtitle = GameObject.Find ("SubtitleControl").GetComponent<SubtitleControl>();
		bossAnimator = gameObject.GetComponent<Animator> ();
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isStop) {
				isStop = false;
				Time.timeScale = 1;
				foreach (GameObject tmpGO in stopUI) {
					tmpGO.SetActive (false);
				}
			} else {
				isStop = true;
				Time.timeScale = 0;
				stopUI [0].SetActive (true);
//				foreach (GameObject tmpGO in stopUI) {
//					tmpGO.SetActive (true);
//				}
			}
		}
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		
		if (isStop)
			return;
		switch (attackState) {
		case aState.idle:
			interCount += Time.deltaTime;
			if (interCount > attackInterval) {
				interCount = 0;
				bossAnimator.SetTrigger (aStartID);
				subtitle.attackPre ();
				if (attackInterval < 6 || attackInterval > 10)
					attackInterval = 8;
				attackInterval = Random.Range (attackInterval-2, attackInterval+2);
			}
			break;
		case aState.dropHair1:
			if(attackF == hairInterval ){
				attackF = 0;
				attack_DropHair (hairPosi1, hairNumPerS1);
			}else
				attackF++;
			break;
		case aState.dropHair2:
			if(attackF == hairInterval ){
				attackF = 0;
				attack_DropHair (hairPosi2, hairNumPerS2);
			}else
				attackF++;
			break;
		case aState.dropHair3:
			if(attackF == hairInterval ){
				attackF = 0;
				attack_DropHair (hairPosi3, hairNumPerS3);
			}else
				attackF++;
			break;
		case aState.dropHair4:
			if(attackF == hairInterval ){
				attackF = 0;
				attack_DropHair (hairPosi4, hairNumPerS4);
			}else
				attackF++;
			break;
		case aState.spitFire:
			fireTime += Time.deltaTime;
			if (fireTime > 1.5) {
				fireTime = 0;
				attackState = aState.idle;
				fire.SetActive (false);
			}
			
			break;
		case aState.prepare:
		default:
			break;
		}
	}

	void attack_DropHair(Vector3[] positoin,int[] numPerS){
		for (int i = 0; i < numPerS [whichS]; i++) {
			Instantiate (hair, positoin [whichH], Quaternion.identity);
			whichH++;
		}
		whichS++;
		//attack end
		if (whichS > numPerS [0] || whichH>hairPosi1[0].x) {
			whichH = 1;
			whichS = 1;
			attackState = aState.idle;
		}
	}
	public void setRecover(){
		isStop = false;
		Time.timeScale = 1;
		foreach (GameObject tmpGO in stopUI) {
			tmpGO.SetActive (false);
		}
	}
	public void Attack(){
		attackState = attackList [attackC];
		subtitle.attackWarning ();
//		if (attackState == aState.spitFire)
//			fire.SetActive (true);
		attackC++;
		if (attackC >= attackList.Length)
			attackC = 0;
	}
	public void AttackEnd(){

		bossAnimator.SetTrigger (aEndID);
	}
}
