using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn2 : MonoBehaviour {
	public AudioSource BGM;
	public AudioClip change;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void soundChange(){
		BGM.Play ();
	}
	void turn2S1(){
		Application.LoadLevel("Scene/Stage1");
	}
}
