using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown ("r")) {
			SceneManager.LoadScene("Scene/Stage1");
		}	
	}
}
