//控制层，关卡二控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {
	public class Ctrl_Level2_Scene:BaseControl {
		//背景音乐与音效处理
		public AudioClip Auc_Background;
		public AudioClip Auc_BOSSBattle;

		//标签：需要隐藏的对象
		public string[] TagNameByHideObject;

		/* 敌人预设 */

		public GameObject goArcher;
		public GameObject goMage;
		public GameObject goKing;
		public GameObject goWarrior;

		public GameObject goBOSS;


		/* 敌人的产生地点*/
		//区域A
		public Transform[] ST_Area_A_Archer;
		public Transform[] ST_Area_A_Mage;
		//区域B
		public Transform[] ST_Area_B_Mage;
		public Transform[] ST_Area_B_Warrior;
		//区域C
		public Transform[] ST_Area_C_King;
		public Transform[] ST_Area_C_Archer;
	
		//BOSS区域
		public Transform[] ST_BOSS;
		public Transform[] ST_Area_BOSS_Mage;
		//BOSS区域空气墙
		public GameObject[] goWalls;


		/* 敌人的单次出生控制 */
		private bool[] IsSingleCtrl=new bool[] {true,true,true,true};



		private void Awake() {
			//事件注册：升级
			//事件注册：敌人出生触发器
			TriggerCommonEvent.eve_CommonTrigger += SpawnEnemyMgr;
		}



		void Start() {
			//播放背景音乐
			AudioManager.PlayBackground(Auc_Background);
			//默认因此场景中非活动的区域
			//StartCoroutine(HideUnactiveArea());
			//隐藏空气墙
			HideAirWall(goWalls);
		}




		/// <summary>
		/// 统计敌人数量
		/// </summary>
		/// <returns></returns>
		private int CountEnemy() {
			GameObject[] goEnemys = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemy);
			if (goEnemys != null) {
				return goEnemys.Length;
			}
			return 0;
		}







		/// <summary>
		/// 注册用私有方法：利用触发器生成敌人的总管理器
		/// </summary>
		/// <param name="ctt"></param>
		private void SpawnEnemyMgr(CommonTriggerType ctt) {
			switch (ctt) {

				case CommonTriggerType.Enemy1_Spawn:
					//动态加载，单次控制
					if (IsSingleCtrl[0]) {
						SpawnEnemy_Area_A();
						IsSingleCtrl[0] = false;
					}
					break;
				case CommonTriggerType.Enemy2_Spawn:
					//动态加载，单次控制
					if (IsSingleCtrl[1]) {
						SpawnEnemy_Area_B();
						IsSingleCtrl[1] = false;
					}
					break;
				case CommonTriggerType.Enemy3_Spawn:
					//动态加载，单次控制
					if (IsSingleCtrl[2]) {
						SpawnEnemy_Area_C();
						IsSingleCtrl[2] = false;
					}
					break;

				case CommonTriggerType.EnemyBOSS_Spawn:
					//动态加载，单次控制
					if (IsSingleCtrl[3]) {
						//设置透明墙
						SetAirWall(goWalls);
						//设置背景音乐
						AudioManager.PlayBackground(Auc_BOSSBattle);
						//生成敌人
						SpawnEnemy_Area_BOSS();
						IsSingleCtrl[2] = false;
					}
					break;

				default:
					break;
			}
		}






		/// <summary>
		/// 场景优化管理：默认隐藏非活动的区域
		/// </summary>
		/// <returns></returns>
		IEnumerator HideUnactiveArea() {
			yield return new WaitForEndOfFrame();
			foreach (string  tagName in TagNameByHideObject) {
				//得到需要隐藏的游戏对象
				GameObject[] goHideObjArray = GameObject.FindGameObjectsWithTag(tagName);
				foreach (GameObject item in goHideObjArray) {
					item.SetActive(false);
				}

			}
		}




		#region 【产生敌人的方法】

		/// <summary>
		/// 生成敌人：A区域
		/// （在这里调整）
		/// </summary>
		private void SpawnEnemy_Area_A() {
			if(goArcher && goMage) {
				/********常量设置*******/
				const int COUNT_Archer = 3;	//产生3个弓箭手
				const int COUNT_Mage = 2;     //2个法师

				StartCoroutine(SpawnEnemy(goArcher,COUNT_Archer, ST_Area_A_Archer));
				StartCoroutine(SpawnEnemy(goMage,COUNT_Mage, ST_Area_A_Mage));
			}
		}

		/// <summary>
		/// 生成敌人：B区域
		/// （在这里调整）
		/// </summary>
		private void SpawnEnemy_Area_B() {
			if (goMage && goWarrior) {
				/********常量设置*******/
				const int COUNT_Mage = 4;			//产生4个法师
				const int COUNT_Warrior = 2;      //产生2个战士

				StartCoroutine(SpawnEnemy(goMage, COUNT_Mage, ST_Area_B_Mage));
				StartCoroutine(SpawnEnemy(goWarrior, COUNT_Warrior, ST_Area_B_Warrior));
			}
			
			
		}

		/// <summary>
		/// 生成敌人：C区域
		/// </summary>
		private void SpawnEnemy_Area_C() {
			if (goKing && goArcher) {
				/********常量设置*******/
				const int COUNT_King = 1;          //产生1个国王
				const int COUNT_Archer = 4;         //产生3个弓箭手

				StartCoroutine(SpawnEnemy(goKing, COUNT_King, ST_Area_C_King));
				StartCoroutine(SpawnEnemy(goArcher, COUNT_Archer, ST_Area_C_Archer));
			}
		}

		/// <summary>
		/// 生成敌人：BOSS区域
		/// </summary>
		private IEnumerator SpawnEnemy_Area_BOSS() {
			if (goKing && goArcher) {
				/********常量设置*******/
				const float TIME_Area_BOSS_LoopSpawn = 5f;	//循环生成敌人时间
				const int COUNT_MaxEnemy = 4;				//最大敌人数量
				
				StartCoroutine(SpawnEnemy(goBOSS, 1, ST_BOSS));

				while (true) {
					//如果敌人数量少于一定数值
					if (CountEnemy() < COUNT_MaxEnemy) {
						StartCoroutine(SpawnEnemy(goMage, 1, ST_Area_BOSS_Mage));
					}
					yield return new WaitForSeconds(TIME_Area_BOSS_LoopSpawn);

				}

			}
		}

		#endregion



		#region 【空气墙处理】

		/// <summary>
		/// 隐藏空气墙
		/// </summary>
		/// <param name="walls">空气墙组</param>
		private void HideAirWall(GameObject[] walls) {
			for (int i = 0; i < walls.Length; i++) {
				walls[i].SetActive(false);
			}
		}

		/// <summary>
		/// 设置空气墙
		/// </summary>
		/// <param name="walls">空气墙组</param>
		private void SetAirWall(GameObject[] walls ) {
			for (int i = 0; i < walls.Length; i++) {
				walls[i].SetActive(true); }	
		}

		#endregion






	}
}
