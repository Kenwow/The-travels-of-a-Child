using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopAni : MonoBehaviour {
	public GameObject[] stopUI; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void showOther(){
		foreach (GameObject tmpGO in stopUI) {
			tmpGO.SetActive (true);
		}
	}

}

