using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s_again : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		gameObject.GetComponent<Animator> ().enabled = true;
	}
	void OnMouseExit(){
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<Animator> ().Rebind ();
	}
	void OnMouseDown(){
		Time.timeScale = 1;
		SceneManager.LoadScene (Application.loadedLevel);
	}
	void aniEnd(){
		gameObject.GetComponent<Animator> ().enabled = false;
	}
}
