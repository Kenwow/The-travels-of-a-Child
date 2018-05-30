using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class s_volume : MonoBehaviour {
	public GameObject mask;
	private int nowVolume;
	public AudioSource BGM;
	// Use this for initialization
	void Start () {
		nowVolume = PlayerPrefs.GetInt ("volume");
		mask.transform.Translate (0, 0.18f*nowVolume, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseOver(){
		gameObject.GetComponent<Animator> ().enabled = true;
		if (Input.GetMouseButtonDown (0) && nowVolume <5) {
			mask.transform.Translate (0, 0.18f, 0);
			nowVolume++;
			PlayerPrefs.SetInt ("volume", nowVolume);
			BGM.volume = 0.2f * nowVolume;
		}
		if(Input.GetMouseButtonUp(1) && nowVolume>0) {

			mask.transform.Translate (0, -0.18f, 0);
			nowVolume--;
			PlayerPrefs.SetInt ("volume", nowVolume);
			BGM.volume = 0.2f * nowVolume;
		}
	}
	void OnMouseExit(){
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<Animator> ().Rebind ();

	}

}
