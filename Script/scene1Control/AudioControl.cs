using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

	public AudioClip Hit;
	public AudioClip walk;
	public AudioClip run;
	public AudioClip jump;
	public AudioClip die;
	public AudioClip recovery;
	public AudioClip openEye;

	public AudioSource chara_SL;
	public AudioSource chara_SO;
	public AudioSource shooter;
	public AudioSource getHit;
	public AudioSource monsterHit;
	public AudioSource MP_change;
	public AudioSource night;
//	public AudioSource chara_walk;
	private AudioSource soundControl;


	// Use this for initialization
	void Start () {
		soundControl = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlayHit(){
		getHit.Play();
//		soundControl.clip = Hit;
//		soundControl.Play ();
	}
	public void MonsterHit(){
		monsterHit.Play ();
	}

	public void Play(AudioClip sound){
		soundControl.clip = sound;
		soundControl.Play ();
	}
	public void PlayRunFootstep(){
		chara_SL.clip = run;
		chara_SL.Play ();
	}

	public void PlayWalkFootstep(){
		chara_SL.clip = walk;
		chara_SL.Play ();
	}
	public void CharaStop(){
		chara_SL.Stop ();
	}
	public void CharaShoot(){
		shooter.Play ();
	}
	public void CharaJump(){
		chara_SO.clip = jump;
		chara_SO.Play ();
	}

	public void CharaDie(){
		chara_SO.clip = die;
		chara_SO.Play ();
	}
	public void CharaRecovery(){
		chara_SO.clip = recovery;
		chara_SO.Play ();
	}
	public void PlayChange(){
		MP_change.Play ();
	}
	public void OpenEye(){
		//night.Stop ();
		soundControl.clip = openEye;
		soundControl.Play ();
	}
	public void nightDown(){
		night.volume -= 0.04f;
//		if (night.volume > 0.2f) {
//			night.volume -= 0.04f;
//		}
	}

}
