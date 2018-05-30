using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void jump2Level1(){
		Application.LoadLevel("Stage1");  
	}
	public void jump2Boss1(){
		Application.LoadLevel("Boss1");  
	}
}
