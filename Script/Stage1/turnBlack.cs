using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class turnBlack : MonoBehaviour {
	public AudioControl audioControl;
	public GameObject eye;
	public void OpenEye(){
		audioControl.OpenEye ();
		eye.SetActive (true);
	}
	public void BossEnd(){
		SceneManager.LoadScene ("StageEnd");
	}
	public void turn2Start(){
		SceneManager.LoadScene ("Start");
	}
	public void turn2Boss(){
		SceneManager.LoadScene ("Boss1");
	}
	public void BossCom(){
		SceneManager.LoadScene ("StageCom");
	}

}
