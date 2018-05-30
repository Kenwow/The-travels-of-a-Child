using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openEye : MonoBehaviour {
	public GameObject black;
	public void turn2Boss(){
		black.SetActive (true);
		//SceneManager.LoadScene ("Boss1");
	}
}
