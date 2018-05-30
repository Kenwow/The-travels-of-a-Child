using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class night : MonoBehaviour {
	private float timeCount = 0; 
	private Animator nightAnimator;
	private int changeID = -1;
	public Text nightT;
	AnimatorOverrideController overrideController;
	void Awake(){
		changeID = Animator.StringToHash ("isChange");
	}
	// Use this for initialization
	void Start () {
		nightAnimator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeCount += Time.deltaTime;
		if (timeCount > 5) {
			timeCount = 0;
			nightAnimator.SetBool (changeID, true);
			Debug.Log("night");
			nightT.text = "现在开始扩散，但emmm看不大出来";
		}
	}
	void changeEnd(){
		Debug.Log ("night end");
		nightAnimator.SetBool (changeID, false);
		nightT.text = "黑夜扩散结束";
		timeCount = 0;
	}

}
