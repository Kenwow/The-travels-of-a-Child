using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Function List
//get_Random_Ent():row a random entrance of monster

public class Scene : MonoBehaviour {
	private bool isStop = false;
	public GameObject[] stopUI; 
	public GameObject stopBG;
	public SubtitleControl subtitle;
	public AudioControl audioConrtol;
	public GameObject fog;
	public GameObject black;
	public GameObject eye;
	private bool isEnd = false;

	public GameObject monsterN;
	public GameObject monsterP;
	public GameObject monsterF;
	//use to set object direction(reverse the object if at right)
	private GameObject tmpMonster;

	private float size = 2.3f;//2.3->2.7
	private float fogSpeed = 1;
	private float fogTrp = 150;//the transparent(A) value of fog
	private float colorCS = 1;

	//!!!!!!
	//look here!!!!!!!!
	//To IOIOI
	public float timeCounter = 0f;
	public int waveC = 0;
	public float[] sec1Time = new float[7]{0,4,7.5f,10,15,20,22.5f};
	public float[] sec2Time = new float[7]{1,5,7.5f,10,15,20,22.5f};
	public float[] sec3Time = new float[8]{1,3,5,7.5f,10,15,20,22.5f};


	//use in [get_Random_ent()]
	//0-Left+Up clockwise
	public int monsterEnt = -1;

	//monsterType
	private enum mType
	{
		mNorm,
		mPlus,
		mFat
	}


	// Use this for initialization
	void Start () {
		subtitle = GameObject.Find ("SubtitleControl").GetComponent<SubtitleControl>();
		fogSpeed *= Time.deltaTime;
		//colorCS *= Time.deltaTime;
	}
	void Update(){
		if (isEnd) {
			audioConrtol.nightDown ();
			return;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isStop) {
				isStop = false;
				Time.timeScale = 1;
				foreach (GameObject tmpGO in stopUI) {
					tmpGO.SetActive (false);
				}

			} else {
				isStop = true;
				stopBG.SetActive(true);
				stopBG.GetComponent<Animator> ().Rebind ();
				Time.timeScale = 0;
//				foreach (GameObject tmpGO in stopUI) {
//					tmpGO.SetActive (true);
//				}
			}
		}
		if (Input.GetKeyDown ("0")) {
			isEnd = true;
			black.SetActive (true);
		}

	}
	// Update is called once per frame
	void FixedUpdate () {

		if (isStop || isEnd)
			return;
		timeCounter += Time.deltaTime;
		WhatsHappen ();
	}
	public void setRecover(){
		isStop = false;
		Time.timeScale = 1;
		foreach (GameObject tmpGO in stopUI) {
			tmpGO.SetActive (false);
		}
	}
	//gene monster
	private void gene_mNorm(int wave){
		int ent;//monster entrance 
		for (int i = 0; i < wave; i++) {
			ent = get_Random_Ent ();
			gene_A_Monster (ent, mType.mNorm);
		}
	}

	private void gene_mPlus(int wave){
		int ent;//monster entrance 
		for (int i = 0; i < wave; i++) {
			ent = get_Random_Ent ();
			gene_A_Monster (ent, mType.mPlus);
		}
	}

	private void gene_mFat(int wave){
		int ent;//monster entrance 
		for (int i = 0; i < wave; i++) {
			ent = get_Random_Ent ();
			gene_A_Monster (ent, mType.mFat);
		}
	}
	//!!!!!!!
	//!!!!!!!
	//To IOIOI!!!!!!!!!
	//!!!!!!!
	//gene_mNorm(X) & gene_mFat(X) & gene_mPlus(x) can generate X monster
	//change X & time to get your expected effect
	private void WhatsHappen(){
		//optimizable using binary tree
		//Section 1
		int wave = 0;
		if (isEnd) {
//			audioConrtol.nightDown ();
			return;
		}
		if (timeCounter > 93 && !isEnd) {
			if (GameObject.FindWithTag ("monster") == null) {
				subtitle.EndWarning ();
				black.SetActive (true);
				isEnd = true;
			}

		}
		if (timeCounter < 25 && waveC < 7) {
			wave = waveC;
			if (timeCounter - sec1Time [wave] > 0) {
				switch (wave) {
				case 0:
					subtitle.SecWarning (0, 0);
					break;
				case 4:
					subtitle.SecWarning (0, 1);
					break;
				case 5:
					subtitle.SecWarning (0, 2);
					break;
				case 6:
					subtitle.SecWarning (0, 3);
					break;
				default:
					break;
				}

				SecOne (wave);
				waveC++;
			}
		} else if (timeCounter < 50 && waveC < 14) {
			wave = waveC-7;
			if (timeCounter - 25 - sec2Time [wave] > 0) {
				switch (wave) {
				case 1:
					subtitle.SecWarning (1, 0);
					break;
				case 3:
					subtitle.SecWarning (1, 1);
					break;
				case 4:
					subtitle.SecWarning (1, 2);
					break;
				case 5:
					subtitle.SecWarning (1, 3);
					break;
				default:
					break;
				}
				SecTwo (wave);
				waveC++;
			}
		}
		else if(timeCounter < 75 && waveC < 21){
			
			wave = waveC-14;
			if (timeCounter - 50 - sec3Time [wave] > 0) {
				switch (wave) {
				case 1:
					subtitle.SecWarning (2, 0);
					break;
				case 3:
					subtitle.SecWarning (3, 1);
					break;
				case 4:
					subtitle.SecWarning (2, 2);
					break;
				case 5:
					subtitle.SecWarning (3, 3);
					break;
				default:
					break;
				}
				SecThree (wave);
				waveC++;
				if (wave == 8) {
					Debug.Log ("游戏结束");
				}
			}
		}
	}

	private void SecOne(int wave){
		switch (wave) {
		case 0:
		case 1:
		case 2:
			gene_mNorm (1);
			break;
		case 3:
			gene_mNorm (2);
			break;
		case 4:
		case 5:
			gene_mNorm (2);
			break;
		case 6:
			gene_mNorm (1);
			break;
		default:
			break;
		}
	}
	private void SecTwo(int wave){
		switch (wave) {
		case 0:
		case 1:
			gene_mNorm (1);
			break;
		case 2:
			gene_mPlus (1);
			break;
		case 3:
			gene_mNorm (2);
			gene_mPlus (1);
			break;
		case 4:
			gene_mNorm (1);
			gene_mPlus (1);
			break;
		case 5:
			gene_mNorm (1);
			break;
		case 6:
			gene_mNorm (1);
			gene_mPlus (1);
			break;
		default:
			break;
		}
	}
	private void SecThree(int wave){
		switch (wave) {
		case 0:
			gene_mNorm (1);
			break;
		case 1:
		case 2:
			gene_mPlus (1);
			break;
		case 3:
			gene_mFat (2);
			gene_mPlus (1);
			break;
		case 4:
			gene_mNorm (1);
			gene_mFat (1);
			break;
		case 5:
			gene_mNorm (1);
			gene_mPlus (1);
			break;
		case 6:
			gene_mNorm (2);
			break;
		case 7:
			gene_mNorm (1);
			gene_mFat (1);
			break;
		default:
			break;
		}
	}

	//function: 
	//1.to row a random entrance of monster
	//2.prevent contiguous monsters come from the same entrace
	private int get_Random_Ent(){
		int dice = Random.Range (0, 8);
		while(monsterEnt == dice)
			dice = Random.Range (0, 8);
		monsterEnt = dice;
		if (dice == 8) {
			dice = 7;
		}
		return dice;
	}
	//function: 
	//generate a monster from the ent
	private void gene_A_Monster(int ent, mType type){
//		ent = 5;
		Vector3 positon = new Vector3 (-7.5f, -5, 0);
		Vector3 Scale;
		float tmpX, tmpY;
		if (type == mType.mNorm) {
			tmpMonster = Instantiate (monsterN, positon, Quaternion.identity);
		} else if (type == mType.mPlus) {
			tmpMonster = Instantiate (monsterP, positon, Quaternion.identity);
		} else if (type == mType.mFat) {
			tmpMonster = Instantiate (monsterF, positon, Quaternion.identity);
		}
		Scale = tmpMonster.transform.localScale; 
		//the original monster is face left
		//0-Left+Up clockwise
		switch(ent){
		case 0://Mirror! No rotation 左上
			positon.x = Random.Range (-6.5f, -7f);
			positon.y = Random.Range (0f, 5.2f);
			Scale.x = -Scale.x;
			tmpMonster.transform.position = positon;
			tmpMonster.transform.localScale = Scale;
			break;
		case 1://Mirror! Z rotate: mNorm -30˚ mPlus -15˚上左
			positon.x = Random.Range(-8f,0f);
			positon.y = Random.Range (8f, 10f);
			Scale.x = -Scale.x;
			tmpMonster.transform.position = positon;
			tmpMonster.transform.localScale = Scale;
			if (type == mType.mNorm) {
				tmpMonster.transform.Rotate(0, 0, -30);
			} else if (type == mType.mPlus) {
				tmpMonster.transform.Rotate (0, 0, -15);
			}
			break;
		case 2://Not Mirror! Z rotate: mNorm 30˚ mPlus 15˚ 上右
			positon.x = Random.Range(0f,8f);
			positon.y = Random.Range (5.3f, 6f);
			tmpMonster.transform.position = positon;
			if (type == mType.mNorm) {
				tmpMonster.transform.Rotate(0, 0, 30);
			} else if (type == mType.mPlus) {
				tmpMonster.transform.Rotate (0, 0, 15);
			}
			break;
		case 3://Not Mirror! No rotation 右上
			positon.x = Random.Range(6.5f,7f);
			positon.y = Random.Range (0f, 5.2f);
			tmpMonster.transform.position = positon;
			break;
		case 4://Not Mirror! No rotation 右下
			positon.x = Random.Range(8f,10f);
			positon.y = Random.Range (-5.2f,0);
			tmpMonster.transform.position = positon;
			break;
		case 5://Not Mirror! Z rotate: mNorm -30˚ mPlus -15˚下右
			positon.x = Random.Range(0f,8f);
			positon.y = Random.Range (-5f,-4f);
			tmpMonster.transform.position = positon;
			if (type == mType.mNorm) {
				tmpMonster.transform.Rotate(0, 0, -30);
			} else if (type == mType.mPlus) {
				tmpMonster.transform.Rotate (0, 0, -15);
			}
			break;
		case 6://Mirror! Z rotate: mNorm 30˚ mPlus 15˚
			positon.x = Random.Range(-8f,0f);
			positon.y = Random.Range (-5f,-4f);
			Scale.x = -Scale.x;
			tmpMonster.transform.position = positon;
			tmpMonster.transform.localScale = Scale;
			if (type == mType.mNorm) {
				tmpMonster.transform.Rotate(0, 0, 30);
			} else if (type == mType.mPlus) {
				tmpMonster.transform.Rotate (0, 0, 15);
			}
			break;
		case 7://Mirror! No rotation
			positon.x = Random.Range(-6.5f,-7f);
			positon.y = Random.Range (-5.2f,0);
			Scale.x = -Scale.x;
			tmpMonster.transform.position = positon;
			tmpMonster.transform.localScale = Scale;
			break;
		default:
			break;
		}
	}
	public void OpenEye(){
		isEnd = true;
		eye.SetActive (true);
	}
	public void turn2Boss(){
		SceneManager.LoadScene ("Boss1");
	}
}


//废弃代码

//		//fog
//		size = (size + fogSpeed);
//		if (size > 2.7 || size < 2.3)
//			fogSpeed = -fogSpeed;
//		fogTrp += colorCS;
//		if (fogTrp > 255 || fogTrp < 150)
//			colorCS = -colorCS;
//		fog.transform.localScale = new Vector3 (size, size, size);
//		fog.GetComponent<SpriteRenderer> ().color = new Vector4(1f,1f,1f, fogTrp/255); 

//void G_Monsterplus(){
//	//8 direction 
//	float MPsize = 0.2f;
//	for (int i = 0; i < mstN; i++) {
//		float tmpX, tmpY;
//		//left up
//		tmpX = Random.Range(-10f,-8.5f);
//		tmpY = Random.Range (0f, 5.2f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.localScale = new Vector3 (-MPsize, MPsize, MPsize);
//		tmpMonster.transform.Rotate (0, 0, 30);
//		//left down
//		tmpX = Random.Range(-16f,-14.5f);
//		tmpY = Random.Range (-5.2f,0);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.localScale = new Vector3 (-MPsize, MPsize, MPsize);
//		tmpMonster.transform.Rotate (0, 0, 30);
//		//right up
//		tmpX = Random.Range(9.5f,11f);
//		tmpY = Random.Range (0f, 5.2f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.Rotate (0, 0, 30);
//		//right down
//		tmpX = Random.Range(13.5f,15f);
//		tmpY = Random.Range (-5.2f,0);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.Rotate (0, 0, 30);
//		//up left
//		tmpX = Random.Range(-8f,0f);
//		tmpY = Random.Range (7.3f, 8f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.localScale = new Vector3 (-MPsize, MPsize, MPsize);
//		//up right
//		tmpX = Random.Range(0f,8f);
//		tmpY = Random.Range (5.3f, 6f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		//down left
//		tmpX = Random.Range(-8f,0f);
//		tmpY = Random.Range (-9f,-8.3f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//		tmpMonster.transform.localScale = new Vector3 (-MPsize, MPsize, MPsize);
//		//down Right
//		tmpX = Random.Range(0f,8f);
//		tmpY = Random.Range (-6f,-5.3f);
//		tmpMonster = Instantiate (monsterP, new Vector3 (tmpX, tmpY, 0), Quaternion.identity);
//
//	}
//}

////every mstTime occur 8*mstN monster
//private int mstN = 1;
//private float mstTime = 20;
//private float mstTCount = 18;
//
////every mstPlusTime occur 8*mstPN monsterplus
//private int mstPN = 1;
//private float mstPlusTime = 25;
//private float mstPlusTCount = 16;

//switch (wave) {
//case 1://generate one monster
//	ent = get_Random_Ent ();
//	gene_A_Monster (ent, mType.mNorm);
//	break;
//case 2:
//	ent = get_Random_Ent ();
//	gene_A_Monster (ent, mType.mNorm);
//	ent = get_Random_Ent ();
//	gene_A_Monster (ent, mType.mNorm);
//	break;
//case 3:
//	ent = get_Random_Ent ();
//	gene_A_Monster (ent, mType.mNorm);
//	ent = get_Random_Ent ();
//	gene_A_Monster (ent, mType.mNorm);
//	break;
//case 4:
//	for (int i = 0; i < 5; i++) {
//		ent = get_Random_Ent ();
//		gene_A_Monster (ent, mType.mNorm);
//	}
//	break;
//default:
//	break;
//}
//section one
//if (timeNow < 25.0f) {
//	if (1.0f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (5.0f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (7.5f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (10.0f == timeNow) {
//		gene_mNorm (2);
//		return;
//	}
//	if (15.0f == timeNow) {
//		gene_mNorm (2);
//		return;
//	}
//	if (20.0f == timeNow) {
//		gene_mNorm (2);
//		return;
//	}
//	if (22.5f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//
//}
////section two
//if (timeNow < 50.0f) {
//	if (1.0f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (5.0f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (7.5f == timeNow) {
//		gene_mPlus (1);
//		return;
//	}
//	if (10.0f == timeNow) {
//		gene_mNorm (2);
//		gene_mPlus (1);
//		return;
//	}
//	if (15.0f == timeNow) {
//		gene_mNorm (1);
//		gene_mPlus (1);
//		return;
//	}
//	if (20.0f == timeNow) {
//		gene_mNorm (2);
//		return;
//	}
//	if (22.5f == timeNow) {
//		gene_mPlus (1);
//		return;
//	}
//}
////section three
//if (timeNow < 75.0f) {
//	if (1.0f == timeNow) {
//		gene_mNorm (1);
//		return;
//	}
//	if (3.0f == timeNow) {
//		gene_mPlus (1);
//		return;
//	}
//	if (5.0f == timeNow) {
//		gene_mPlus (1);
//		return;
//	}
//	if (7.5f == timeNow) {
//		gene_mFat (1);
//		return;
//	}
//	if (10.0f == timeNow) {
//		gene_mNorm (1);
//		gene_mPlus (1);
//		gene_mFat (1);
//		return;
//	}
//	if (15.0f == timeNow) {
//		gene_mNorm (1);
//		gene_mFat (1);
//		return;
//	}
//	if (20.0f == timeNow) {
//		gene_mPlus (1);
//		gene_mFat (1);
//		return;
//	}
//	if (22.5f == timeNow) {
//		gene_mNorm (2);
//		return;
//	}
//}