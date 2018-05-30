using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S1Ani : MonoBehaviour {
	public GameObject[] subtitle;
	public GameObject arrow;
	public GameObject black;
	private Animator SceneAnimator;
	AnimatorOverrideController overrideController;
	private int page = 1;
	private bool canNext = true;
	void Awake(){
		//gameObject.GetComponent<Animator> ().Rebind ();
	}
	// Use this for initialization
	void Start () {
		SceneAnimator = gameObject.GetComponent<Animator> ();
		SceneAnimator.Rebind ();
		Debug.Log ("Hey nextPage");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return") && canNext) {
			if (page == 7) {
				PlayerPrefs.SetInt ("isRead", 1);
				turnToS1 ();
			}
			if(page>1)
				subtitle [page - 2].SetActive(false);
			if(page < 7)
				GetComponent<AudioSource> ().Play ();
			page++;
	        SceneAnimator.SetInteger ("whichPage", page);
			canNext = false;
//			arrow.SetActive (false);

		}
		if (Input.GetKeyDown ("c")) {
			PlayerPrefs.SetInt ("isRead", 0);
		}
		if (Input.GetKeyDown ("q") && PlayerPrefs.GetInt("isRead") == 1) {
			black.GetComponent<Animator> ().SetTrigger ("turnBlack");
		}
	}
	public void enableText(){
		subtitle [page - 2].SetActive(true);
	}
	public void nextOk(){
//		arrow.SetActive (true);
		canNext = true;
	}
	void turnToS1(){
		black.GetComponent<Animator> ().SetTrigger ("turnBlack");
	}
}
