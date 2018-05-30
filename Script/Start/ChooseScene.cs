using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseScene : MonoBehaviour {
	public Text skip;
	private bool canNext = true;
	private int whichPage = 0;
	private Animator SceneAnimator;
	AnimatorOverrideController overrideController;
	private int PageID1 = -1;
	private int PageID2 = -1;
	private int isCWID = -1;
	private bool isFirst = true;

	public GameObject stage1;

	void Awake(){
		PageID1 = Animator.StringToHash ("whichScene1");
		PageID2 = Animator.StringToHash ("whichScene2");
		isCWID = Animator.StringToHash ("isClockwise");
	}
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("volume", 5);
		PlayerPrefs.SetInt ("sound", 5);
		SceneAnimator = gameObject.GetComponent<Animator> ();
		SceneAnimator.Rebind ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.anyKey) {
			if (isFirst) {
				
				if (Input.GetKeyDown ("w")) {
					isFirst = false;
					SceneAnimator.SetBool (isCWID, true);
				}else if (Input.GetKeyDown ("s")) {
					isFirst = false;
					SceneAnimator.SetBool (isCWID, false);
				}
			}
			if (Input.GetKeyDown ("w")) {
				GetComponent<AudioSource> ().Play ();
				lastPage2 ();
				whichPage++;
				if (whichPage > 3)
					whichPage = 0;
			} else if (Input.GetKeyDown ("s")) {
				GetComponent<AudioSource> ().Play ();
				whichPage--;
				nextPage2 ();
				if (whichPage < 0)
					whichPage = 3;
			}
			if (Input.GetKeyDown ("return")) {
				choose (whichPage);
				if(whichPage == 0)
					Destroy (this);
			}
			if (Input.GetKeyDown ("0")) {
				Application.LoadLevel("Scene/Boss1");
			}
		}
	}
	//2 --- -4
	//3 --- -3
	//4 --- -2
	//1 --- -5
	void choose(int stage){
		if (stage == 0) {
			stage1.SetActive (true);
			if (PlayerPrefs.GetInt ("isRead") == 0)
				skip.enabled = false;
			else
				skip.enabled = true;
		}
	}
	void lastPage2(){
		bool isCW = SceneAnimator.GetBool (isCWID);
		int page ;
		if (isCW) {
			page = SceneAnimator.GetInteger (PageID1);
			page++;
			if (page > 4)
				page = 1;
			SceneAnimator.SetInteger (PageID1, page);
		} else {
			SceneAnimator.SetBool (isCWID, true);
			page = SceneAnimator.GetInteger (PageID2);
			page = 5 - page;
			SceneAnimator.SetInteger (PageID1, page);
		}
	}
	void nextPage2(){
		bool isCW = SceneAnimator.GetBool (isCWID);
		int page ;
		if (isCW) {
			SceneAnimator.SetBool (isCWID, false);
			page = SceneAnimator.GetInteger (PageID1);
			page = 5 - page;
			SceneAnimator.SetInteger (PageID2, page);
		} else {
			page = SceneAnimator.GetInteger (PageID2);
			page++;
			if (page > 4)
				page = 1;
			SceneAnimator.SetInteger (PageID2, page);
		}
	}

//	void lastPage(){
//		SceneAnimator.SetFloat (SpeedID, -1f);
//		float page = SceneAnimator.GetFloat (PageID);
//		if (page < -5)
//			page = -5;
//		if (page == 0) {
//			SceneAnimator.SetFloat (PageID, -1);
//		}else if (page < 0) {
//			page-=1;
//			SceneAnimator.SetFloat (PageID, page);
//		} else {	
//			
//			page = -6+page;
//
//			SceneAnimator.SetFloat (PageID, page);
//		}
//	}
//	void nextPage(){
//		SceneAnimator.SetFloat (SpeedID, 1);
//		float page = SceneAnimator.GetFloat (PageID);
//		if (page > 5) {
//			page = 5;
//		}
//		if (page == 0) {
//			SceneAnimator.SetFloat (PageID, 1);
//		}else if (page < 0) {
//			
//			page = 6 + page;
//
//			//SceneAnimator.speed = Mathf.Abs(SceneAnimator.speed);
//			SceneAnimator.SetFloat (PageID, page);
//		} else {	
//			page += 1;
//			SceneAnimator.SetFloat (PageID, page);
//		}
//	}
//	void aniEnd(){
////		canNext = true;
//		float page = SceneAnimator.GetFloat (PageID);
//		if(page>=4 || page <= -4)
//			SceneAnimator.SetFloat (PageID, 0);
//	}

}
