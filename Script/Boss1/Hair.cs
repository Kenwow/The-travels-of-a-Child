using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hair : MonoBehaviour {
	//hey!
	private int goodChoise = 0;
	private bool onRoof = false;
	private enum hairState{
		drop,change,run
	} 
	private hairState hState =hairState.drop;
	private int runDir = 1;
	private Vector3 tmpV = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
		if (isOnRoof()) {
			onRoof = true;
		}
		if (transform.position.x > 4.3f)
			runDir = -1;
		else if(transform.position.x > -2.6f){
			runDir = Random.Range (-1, 1) > 0 ? 1 : -1;
		}
		if (runDir == -1) {
			Vector3 tmpV = transform.localScale;
			tmpV.x = -tmpV.x;
			transform.localScale = tmpV;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (hState) {
		case hairState.drop:
			transform.Translate (new Vector3 (0, -3 * Time.deltaTime, 0));
			if (transform.position.y < -0.7f && onRoof) {
				Destroy (gameObject);
			}
			if (transform.position.y < -3.1f) {
				GetComponent<Animator> ().SetTrigger ("change");
				hState = hairState.change;
			}
			break;
		case hairState.run:
			transform.Translate (new Vector3 (5 * runDir * Time.deltaTime, 0, 0));
			if (transform.position.x < -11 || transform.position.x > 11) {
				Destroy (gameObject);
			}
				
			break;
		case hairState.change:
			break;
		default:
			break;

		}
	}

	bool isOnRoof(){
		float x = transform.position.x;
		if (x < -7.2f && x > -9f)
			return true;
		if (x < -2.2f && x > -4.7f)
			return true;
		if (x < 4.2f && x > 2.5f)
			return true;
		if (x < 9f && x > 7f)
			return true;
		return false;
	}
	void changeEnd(){
		hState = hairState.run;
		GetComponent<Animator> ().SetTrigger ("walk");
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			coll.GetComponent<HoriChara>().isHit();
		}

	}
}
