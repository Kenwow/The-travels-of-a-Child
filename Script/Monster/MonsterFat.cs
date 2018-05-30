using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFat : MonoBehaviour {
	public AudioControl soundControl;
	//move toward the character
	private GameObject chara;
	private Vector3 collPosi = new Vector3(1,0,0);

	public float speed = 0.15f;
	private float hitTC = 0.8f;
	private int blood = 5;
	private Vector3 nowDir;//the direction of this monster
	// Use this for initialization
	private enum m_AI{findV = 0,isHit = 1,hitV =2,isDie = 3};
	m_AI state = 0;//0-find village 1-is hit 2-hit village
	//Animator
	private Animator m_Animator;
	private int isDieID = -1;
	//collider
	private CapsuleCollider2D m_collider;
	void Start () {
		m_Animator = gameObject.GetComponent<Animator>();
		m_collider =  gameObject.GetComponent<CapsuleCollider2D>();
		isDieID = Animator.StringToHash ("isDie");
		soundControl = GameObject.Find ("AudioControl").GetComponent<AudioControl>();

	}

	// Update is called once per frame
	void FixedUpdate () {
		nowDir = new Vector3(0,0,0)- gameObject.transform.position;
		nowDir.Normalize ();
		if (state == m_AI.findV) {

			gameObject.transform.Translate (nowDir*speed*Time.deltaTime);
		}
		if (state == m_AI.isHit) {
			hitTC -= Time.deltaTime;
			if (hitTC < 0)
				state = m_AI.isDie;
		}
		if (state == m_AI.isDie) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		
		Vector3 hitBack =  new Vector3(0,0,0);
		if (coll.gameObject.tag == "bullet") {
			soundControl.MonsterHit ();
			blood--;
			Debug.Log (blood);
			if (blood == 0) {
				state = m_AI.isHit;
				m_Animator.SetBool (isDieID, true);
				//m_collider.enabled =false;
				return;
			}
			if (coll.gameObject.name == "bulletDown(Clone)") {
				hitBack = new Vector3(0,-1,0);
				Debug.Log ("this");
			}
			else if (coll.gameObject.name == "bulletUp(Clone)") {
				hitBack = new Vector3(0,1,0);
				Debug.Log ("this");
			}
			else if (coll.gameObject.name == "bulletRight(Clone)") {
				hitBack = new Vector3(1,0,0);
				Debug.Log ("this");
			}
			else if (coll.gameObject.name == "bulletLeft(Clone)") {
				hitBack = new Vector3(-1,0,0);
				Debug.Log ("this");
			}
			coll.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
			gameObject.transform.position = gameObject.transform.position + hitBack * 0.2f;
		}

	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "village") {
			Debug.Log ("village coll");
			//state = m_AI.hitV;
			gameObject.transform.position = collPosi;
		}
		Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "villageOut") {
			collPosi = gameObject.transform.position;
			Debug.Log (collPosi);
		}
	}
}