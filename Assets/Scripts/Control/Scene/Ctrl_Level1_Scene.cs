//控制层，第一关卡的场景控制处理

//负责第一关敌人的动态加载

using UnityEngine;
using System.Collections;

using Global;
using Kernel;
using Model;

namespace Control {
	public class Ctrl_Level1_Scene : BaseControl {

		public AudioClip acBackground;				//背景音乐剪辑，拖拽赋值，不放入缓存

		public Transform traSpawnPosition_1;        //敌人出现的位置
		public Transform traSpawnPosition_2;		//对应一个没有渲染器和碰撞体的立方体
		public Transform traSpawnPosition_3;		//以此确定生成位置
		public Transform traSpawnPosition_4;
		public Transform traSpawnPosition_5;
		public Transform traSpawnPosition_6;
		public Transform traSpawnPosition_7;
		public Transform traSpawnPosition_8;
		public Transform traSpawnPosition_9;
		public Transform traSpawnPosition_10;

		/* 对象缓冲池，复杂对象（敌人预设）*/

		public GameObject goSkeletonWarrior;        //骷髅战士
		public GameObject goSkeletonWarrior_Etite;		//骷髅战士（精英）

		void Start() {
			//背景音乐的播放
			AudioManager.SetAudioBackgroundVolumns(0.5f);
			AudioManager.SetAudioEffectVolumns(1f);
			AudioManager.PlayBackground(acBackground);

			//敌人的动态加载
			StartCoroutine(SpawnEnemies(5));
			//......
		}


		/// <summary>
		/// 生成敌人（传统方式）
		/// </summary>
		/// <param name="spawnNum">生成敌人的数量</param>
		/// <returns></returns>
		//IEnumerator SpawnEnemies(int spawnNum) {
		//	yield return new WaitForSeconds(0.5f);

		//	for(int i = 1;i<= spawnNum;i++) {
		//		yield return new WaitForSeconds(2.5f);

		//		//动态调用资源（敌人的预设）
		//		//GameObject goEnemy = Resources.Load("Prefabs/Enemy/SkeletonWarrior", typeof(GameObject)) as GameObject;
		//		//GameObject goEnemyClone = GameObject.Instantiate(goEnemy);

		//		//使用动态缓冲池技术

		//		GameObject goEnemyClone = ResourceMgr.GetInstance().LoadAsset(GetRandomEnemyType() , true);
		//		//播放敌人出现的特效
		//		EnemySpawnParticalEffect(goEnemyClone);

		//		//定义克隆体随机出现的位置
		//		Transform traEnemySpawnPosition = GetRandomEnemySpawnPosition();
		//		//克隆的位置
		//		goEnemyClone.transform.position = traEnemySpawnPosition.position;
		//		//克隆的旋转
		//		//克隆在层级视图中的位置
		//		goEnemyClone.transform.parent = traSpawnPosition_1.parent;
		//	}		
		//}


		/// <summary>
		/// 生成敌人（加入对象缓冲池技术）
		/// </summary>
		/// <param name="spawnNum">生成敌人的数量</param>
		/// <returns></returns>
		IEnumerator SpawnEnemies(int spawnNum) {
			yield return new WaitForSeconds(0.5f);

			for (int i = 1; i <= spawnNum; i++) {
				yield return new WaitForSeconds(2.5f);

			
				//定义克隆体随机出现的位置
				Transform traEnemySpawnPosition = GetRandomEnemySpawnPosition();
				//在“对象缓冲池“”中激活指定的对象
				PoolManager.PoolsArray["_Enemies"].GetGameObject(goSkeletonWarrior, traEnemySpawnPosition.position, Quaternion.identity);
				//克隆敌人出现的特效
				
			}
		}


		/// <summary>
		/// 得到敌人的多出生点位置
		/// </summary>
		/// <returns></returns>
		public Transform GetRandomEnemySpawnPosition() {
			Transform traReturnSpawnposition ; //返回的敌人位置
			int intRandomNum = UnityHelper.GetInstance().GetRandomNum(1, 10);
			if(intRandomNum == 1) {
				traReturnSpawnposition = traSpawnPosition_1;
			}
			else if (intRandomNum == 2) {
				traReturnSpawnposition = traSpawnPosition_2;
			}
			else if (intRandomNum == 3) {
				traReturnSpawnposition = traSpawnPosition_3;
			}
			else if (intRandomNum == 4) {
				traReturnSpawnposition = traSpawnPosition_4;
			}
			else if (intRandomNum == 5) {
				traReturnSpawnposition = traSpawnPosition_5;
			}
			else if (intRandomNum == 6) {
				traReturnSpawnposition = traSpawnPosition_6;
			}
			else if (intRandomNum == 7) {
				traReturnSpawnposition = traSpawnPosition_7;
			}
			else if (intRandomNum == 8) {
				traReturnSpawnposition = traSpawnPosition_8;
			}
			else if (intRandomNum == 9) {
				traReturnSpawnposition = traSpawnPosition_9;
			}
			else if (intRandomNum == 10) {
				traReturnSpawnposition = traSpawnPosition_10;
			}
			else {
				traReturnSpawnposition = traSpawnPosition_1;
			}
			return traReturnSpawnposition;
		}

		/// <summary>
		/// 得到敌人的随机种类
		/// </summary>
		/// <returns></returns>
		public string GetRandomEnemyType() {

			string strEnemyTypePath = "Prefabs/Enemy/SkeletonWarrior";

			int intRandomNum = UnityHelper.GetInstance().GetRandomNum(1, 10);
			//20%的可能性
			if (intRandomNum <=2 ) {
				strEnemyTypePath = "Prefabs/Enemy/SkeletonWarrior";
			}
			//80%的可能性
			else if(intRandomNum >= 3) {
				strEnemyTypePath = "Prefabs/Enemy/SkeletonWarrior_Etite";
			}
			return strEnemyTypePath;
		}



		/// <summary>
		/// 敌人出现的粒子特效
		/// 不该在这个脚本中
		/// </summary>
		/// <param name="goEnemy"></param>
		private void EnemySpawnParticalEffect(GameObject goEnemy) {

			StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Show2", goEnemy.transform, new Vector3(0f, 0f, 0f), 6f, strAudioEffect: "EnemyDisplayEffect"));
		}

		
	}
}