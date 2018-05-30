using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleControl : MonoBehaviour {
	public Text subtitle;
	string[]  Sec1 = new string[4]{"烟雾把村子包围了，还好村子的灯光照亮了一块地方，黑雾里有妖怪袭来！！来不及了，按下“J”键射击，“W”“A”“S”“D”方向键移动！",
		"第一波结束了，可妖怪越来越多了，双击方向键可以快速赶到远处击杀妖怪！","别停留在原地，妖怪不止在这边出现！","小孩：“哼，这也太简单了。”"};

	string[] Sec2 = new string[4]{"还没完，小怪又过来了",
		"小怪太多了，仿佛数之不尽。小孩：“来一个，我打一个！”",
		"那是什么！？出现了新的妖怪！",
		"新妖怪的出现加大了压力，不过没关系，还是有办法的。"
	};
	string[] Sec3 = new string[4]{"“夕”按捺不住了，加强了兵力，做好准备！",
		"除了之前的合体妖怪，果然又出现了新的妖怪！",
		"好累啊，小孩想。但妖怪还在向村子奔来……",
		"“夕”的小怪们全都出来了，撑住！我们能赢！"
	};
	string[] VillageWaring = new string[4]{"村子被攻击了！不过还好，防御还很坚固！",
		"村子被攻击3次了！村民给周围加强了防御，还能再坚持一会！",
		"村子被攻击9次了！妖怪肆虐，还有村民受伤了，村子岌岌可危！",
		"村子再被攻击一次就会被摧毁了，守护村子！"
	};

	string BossStart = "“夕”的体型巨大，可以按“W”跳上屋子的房檐，更好地躲避“夕”的攻击。";


	string BossPre = "“夕”在观察你。";
	string BossHairAttack = "小心夕的毛发，它们从天而降，瞬息万变。";

	string[] BossBlood = new string[4]{"夕：“你惹毛我了，小孩！”“夕”开始暴躁了起来，他动作变快了。！",
		"“夕”看起来有点疲惫了，动作变慢了，加大火力！我们可以打败它！",
		"“夕”发出了嚎叫声，加快了动作，但明显力不从心了。小孩：“夕不行了，我们快要赢了！”",
		"“嘭”！！！"
	};







	



	string Stage1End = "周围都安静了下来，“呼——”小孩松了一口气，想着应该都结束了吧。这时地面却开始颤抖，沙石飞扬，天空中的雷云还没有散去，天空中露出一线红光，小孩的内心开始不安。红光打开了，一双布满血丝的红色的大眼睛跟小孩的视线对上了……";
	
		
	void Start () {

	// Use this for initialization
		
	}

	void Update () {
		
	}
	public void villageWarning(int i){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = VillageWaring [i];
	}
	public void SecWarning(int Sec,int wave){
		subtitle.GetComponent<Animator> ().Rebind ();
		switch (Sec) {
		case 0:
			subtitle.text = Sec1 [wave];
			break;
		case 1:
			subtitle.text = Sec2 [wave];
			break;
		case 2:
			subtitle.text = Sec3 [wave];
			break;
		default:
			break;
		}
	}
	public void EndWarning(){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = Stage1End;
	}
	public void bossStart(){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = BossStart;
	}

	public void attackPre(){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = BossPre;
	}
	public void attackWarning(){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = BossHairAttack;
	}
	public void BossWaring(int i){
		subtitle.GetComponent<Animator> ().Rebind ();
		subtitle.text = BossBlood [i];
	}
}
