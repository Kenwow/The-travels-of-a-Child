using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_sound : MonoBehaviour {
	public GameObject mask;
//	public AudioSource BGM;
	private int nowVolume;
	// Use this for initialization
	void Start () {
		nowVolume = PlayerPrefs.GetInt ("sound");
		mask.transform.Translate (0, 0.13f*nowVolume, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		gameObject.GetComponent<Animator> ().enabled = true;
		if (Input.GetMouseButtonDown (0) && nowVolume <5) {
			mask.transform.Translate (0, 0.13f, 0);
			nowVolume++;
			PlayerPrefs.SetInt ("sound", nowVolume);
//			BGM.volume = 0.2f * nowVolume;
		}
		if(Input.GetMouseButtonUp(1) && nowVolume>0) {

			mask.transform.Translate (0, -0.13f, 0);
			nowVolume--;
			PlayerPrefs.SetInt ("sound", nowVolume);
//			BGM.volume = 0.2f * nowVolume;
		}
	}
	void OnMouseExit(){
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<Animator> ().Rebind ();

	}
}
