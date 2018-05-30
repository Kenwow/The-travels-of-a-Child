using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class village : MonoBehaviour {
	public SubtitleControl subtitle;
	private int blood;
	public bool test_I_dont_wt_die = false;
	public Text T;
	private Animator VillageAnimator;
	AnimatorOverrideController overrideController;
	private int stateID = -1;

	// Use this for initialization
	void Start () {
		VillageAnimator = gameObject.GetComponent<Animator> ();
		subtitle = GameObject.Find ("SubtitleControl").GetComponent<SubtitleControl>();
		stateID = Animator.StringToHash ("state");
		blood = 15;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown ("2")) {
			test_I_dont_wt_die = false;
			T.text = "正常模式";
		}
		if (Input.GetKeyDown ("1")) {
			test_I_dont_wt_die = true;
			T.text = "无敌模式";
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		//monsters reach village
		if (coll.gameObject.tag == "monster"|| coll.gameObject.tag == "monsterplus" ) {
			blood -= 1;
			switch(15-blood){
			case 1:
				subtitle.villageWarning (0);

				break;
			case 3:
				subtitle.villageWarning (1);
				VillageAnimator.SetInteger (stateID, 1);
				break;
			case 5:
//				GetComponent<Animator> ().SetTrigger ("Die1");
//				VillageAnimator.SetInteger (stateID, 2);
				break;
			case 9:
				subtitle.villageWarning (2);
				VillageAnimator.SetInteger (stateID, 2);
				break;
			case 14:
				subtitle.villageWarning (3);
				VillageAnimator.SetInteger (stateID, 3);
				break;

			default:
				break;
			}
				
			Debug.Log(blood);
			if (blood <= 0 && !test_I_dont_wt_die) {
				VillageAnimator.SetInteger (stateID, 4);
//				GetComponent<Animator> ().SetTrigger ("Die2");

			}
		}
	}
	void End(){
		SceneManager.LoadScene ("Scene/StageEnd");
	}
}
