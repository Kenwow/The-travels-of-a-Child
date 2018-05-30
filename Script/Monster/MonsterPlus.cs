using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlus : MonoBehaviour {
	public AudioControl soundControl;
	//find charactor
	private GameObject chara;
	private Vector3 collPosi = new Vector3(1,0,0);

	public float speed = 0.25f;
	private float hitTC = 2f;
	private int blood = 2;
	private Vector3 nowDir;//the direction of this monster
	// Use this for initialization
	private enum m_AI{findV = 0,isHit = 1,hitV =2,isDie = 3};
	m_AI state = 0;//0-find village 1-is hit 2-hit village
	//Animator
	private Animator m_Animator;
	private int isFissionID = -1;
	//collider
	private CapsuleCollider2D m_collider1;
	private CapsuleCollider2D m_collider2;

	public GameObject monster1;
	private GameObject tmpMonster; 

	void Start () {
		m_Animator = gameObject.GetComponent<Animator>();
		m_collider1 =  gameObject.GetComponents<CapsuleCollider2D>()[0];
		m_collider2 =  gameObject.GetComponents<CapsuleCollider2D>()[1];
		isFissionID = Animator.StringToHash ("isFission");
		chara = GameObject.Find ("character");
		soundControl = GameObject.Find ("AudioControl").GetComponent<AudioControl>();

	}

	// Update is called once per frame
	void FixedUpdate () {
//		Vector3 distance = chara.transform.position - gameObject.transform.position;
//		if (distance.magnitude < 3) {
//			state = m_AI.isHit;
//			m_Animator.SetBool (isFissionID, true);
//			m_collider.enabled =false;
//		}
			
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
			tmpMonster = Instantiate (monster1, gameObject.transform.position, Quaternion.identity);
			if(tmpMonster.transform.position.x>0)
				tmpMonster.transform.localScale = new Vector3 (-0.4f, 0.4f, 0.35f);
			else
				tmpMonster.transform.localScale = new Vector3 (0.4f, 0.4f, 0.35f);
			Destroy (gameObject);

		}
	}
	void OnCollisionEnter2D(Collision2D coll){

		Vector3 hitBack =  new Vector3(0,0,0);
		if (coll.gameObject.tag == "bullet") {
			soundControl.MonsterHit ();
			blood--;
			if (blood == 0  && state == m_AI.findV) {
				state = m_AI.isHit;
				soundControl.PlayChange ();
				m_Animator.SetBool (isFissionID, true);
				m_collider1.enabled =false;
				m_collider2.enabled =false;
				return;
			}
			//Debug.Log ("bullet coll");
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
			gameObject.transform.position = gameObject.transform.position + hitBack * 0.3f;
			coll.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
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
