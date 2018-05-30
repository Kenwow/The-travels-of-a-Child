using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasRoof : MonoBehaviour {
	public GameObject roof;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "chara") {
			roof.SetActive (true);
		}
	}
}
