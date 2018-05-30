using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX : MonoBehaviour {
	public GameObject Chara;
	public GameObject Fire;
	public Boss boss;
	public AudioSource chara_SO;
	public AudioClip exSound;
	// Use this for initialization
	void Awake(){
		Chara.SetActive (false);
	}
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void EXEnd(){
		Time.timeScale = 1;
		boss.Hit_By_EX ();
		Chara.SetActive (true);
		Chara.transform.position = new Vector3 (-1.54f, -3.9f, 0);
		Chara.GetComponent<HoriChara> ().EXInv ();
		Fire.SetActive (true);
	}
	public void CharaInv(){
		Chara.SetActive (false);
	}
	public void fireSound(){
		chara_SO.clip = exSound;
		chara_SO.Play ();
	}
}
