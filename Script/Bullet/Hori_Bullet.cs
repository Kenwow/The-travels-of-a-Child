using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hori_Bullet : MonoBehaviour {

	private float translation;
	public float speed = 20;
	private bool isHit = false; 
	// Use this for initialization
	void Start () {
		translation = Time.deltaTime*speed;
		transform.eulerAngles = new Vector3 (0, 0, 90);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(isHit)
			return;
		//如果在画面内且没碰到怪物，就往左边移动一点距离
		if (IsIn ()) {
			this.transform.Translate (translation, 0,0);
		} else
			Destroy (gameObject);


	}
	private float range = 0;
	bool IsIn(){
		range += translation;
		if (range<9)
			return true;
		return false;
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "monster") {
			isHit = true;
			GetComponent<Animator> ().SetTrigger ("isHit");
		}
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "monster") {
			//gameObject.SetActive (false);
			Debug.Log("hit Boss");
			isHit = true;
			GetComponent<Animator> ().SetTrigger ("isHit");
		}
	}
	void Hit(){
		Destroy (gameObject);
	}
}
