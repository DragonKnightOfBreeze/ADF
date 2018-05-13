//控制层，主角攻击控制
//包括事件的注册（添加相关方法进去）

//开发思路：
//	把附近的所有敌人放入“敌人数组”
//		得到所有敌人，放入“敌人集合”数组。
//		判断敌人集合，找出最近的敌人。

//	主角在一定范围内，开始自动“关注”最近的敌人（仅在RPG手游中）
//	响应输入攻击信号，对于主角“正面”的敌人予以一定伤害处理
//不使用传统的触发器（绑定在剑上）

//备注：
//在使用技能或普通攻击时，要锁定方向，不能更改

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using System.Reflection;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroAttack : BaseControl {
		public float FloMinFocusDistance = 5f;  //主角的最小自动攻击距离（更紧迫）
		public float FloHeroRotationSpeed = 1f; //主角的旋转速率

		public float FloMagicAtkAMultiple = 12f;  //魔法攻击A的攻击倍率
		public float FloMagicAtkBMultiple = 5f;	//魔法攻击B的攻击倍率

		public float FloRealAtkArea = 3f;       //主角实际有效攻击距离
		public float FloMagicAtkAArea = 5.5f;     //魔法攻击A的有效攻击距离
		public float FloMagicAtkBArea = 9f;     //魔法攻击B的有效攻击距离

		public int MPConsByMagicAtkA = 36;		//魔法攻击A的MP消耗
		public int MPConsByMagicAtkB = 20;		//魔法攻击B的MP消耗

		private List<GameObject> _Lis_Enemies;	//敌人的集合
		private Transform _Tra_NearestEnemy;    //最近的敌人方位
		private float _FloMaxDistance = 20f;    //敌我最大距离（可能攻击到，可能需要进行处理）




		private void Awake() {
			//事件注册：主角攻击输入（技术是多播委托）
			//根据提供的参数来判断调用的方法

			//注意：多输入可能会使脚本效率下降，可以使用预编译指令改善

#if UNITY_STANDALONE_WIN || UNITY_EDITOR

			//键盘输入
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseNormalAtk;
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseMagicAtkA;
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseMagicAtkB;

#endif

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR

			//虚拟按键输入
			Ctrl_HeroAttackByET.Eve_PlayerControl += ResponseNormalAtk;
			Ctrl_HeroAttackByET.Eve_PlayerControl += ResponseMagicAtkA;
			Ctrl_HeroAttackByET.Eve_PlayerControl += ResponseMagicAtkB;
			//Ctrl_HeroAttackByET.Eve_PlayerControl += ResponseMagicAtkC;
			//Ctrl_HeroAttackByET.Eve_PlayerControl += ResponseMagicAtkD;

#endif

		}

		private void Start() {
			//集合类型初始化
			_Lis_Enemies = new List<GameObject>();

			//把附近所有敌人放入“敌人数组”
			StartCoroutine("RecordNearbyEnemiesTOArray");
			//主角在一定范围内，自动注视最近的敌人
			StartCoroutine("HeroRotationEnemy");
		}


#region 自动注视最近敌人的相关处理

		/// <summary>
		/// 得到所有敌人（存活的），放入“敌人集合”
		/// </summary>
		public void GetEnemiesToArray() {
			//清空集合，否则会出现差错
			_Lis_Enemies.Clear();

			GameObject[] GoEnemies = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemy);
			foreach (GameObject goItem in GoEnemies) {
				//判断敌人是否存活
				Ctrl_BaseEnemy_Prop enemy = goItem.GetComponent<Ctrl_BaseEnemy_Prop>();
				//if (enemy && enemy.IsAlive) { 
				if(enemy !=null &&  enemy.CurrentState != EnemyActionState.Dead) {
					_Lis_Enemies.Add(goItem);
				}
			}
		}

		/// <summary>
		/// 把附近的所有敌人放入“敌人数组”
		/// 每隔2s处理一次
		/// </summary>
		IEnumerator RecordNearbyEnemiesTOArray() {
			// // Debug.Log("协程已开始：RecordNearbyEnemiesTOArray");
			while (true) {
				GetEnemiesToArray();
				GetNearestEnemy();
				yield return new WaitForSeconds(2f);   
			}
		}

		/// <summary>
		/// 判断“敌人集合”，然后找出最近的敌人
		/// </summary>
		public void GetNearestEnemy() {
			if ((_Lis_Enemies != null) && (_Lis_Enemies.Count >= 1)) {
				foreach (GameObject goEnemy in _Lis_Enemies) {
					float floDistance = Vector3.Distance(this.gameObject.transform.position, goEnemy.transform.position);
					//如果有敌人与玩家的距离小于敌我最大距离
					if (floDistance < _FloMaxDistance) {
						_FloMaxDistance = floDistance;
						_Tra_NearestEnemy = goEnemy.transform;  //得到最近的敌人
					}
				}

			}
		}

		/// <summary>
		/// 主人公在一定范围内，自动注视最近的敌人
		/// </summary>
		/// <returns></returns>
		IEnumerator HeroRotationEnemy() {
			while (true) {
				yield return new WaitForSeconds(0.5f); //每隔0.5s判断1次
																						//需要增加判断条件，必须是主角停下来时，才会自动注视
				if (_Tra_NearestEnemy != null && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle
					) {
					float floDistance = Vector3.Distance(this.gameObject.transform.position, _Tra_NearestEnemy.position);
					//可以认为是近战攻击时的方向修正
					if (floDistance < FloMinFocusDistance) {
						//应该仅仅按照Y轴进行旋转
						//this.transform.LookAt(_Tra_NearestEnemy);
						//另外的改进：

						//使用四元数，使仅仅头部发生旋转，并使用插值运算使之更加平滑

						//this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
						//	Quaternion.LookRotation(new Vector3(_Tra_NearestEnemy.position.x, 0, _Tra_NearestEnemy.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)),
						//	FloHeroRotationSpeed);


						//重构代码
						UnityHelper.GetInstance().FaceToGoal(gameObject.transform, _Tra_NearestEnemy, FloHeroRotationSpeed);

					}
				}
			}
		}

#endregion

#region 响应攻击输入

		/// <summary>
		/// 响应普通攻击，以下方法依此类推
		/// </summary>
		/// <param name="controlType"></param>
		public void ResponseNormalAtk(string controlType) {

			//bool SingleCtrl = true;

			//是用if还是while？
			if (controlType == GlobalParameter.INPUT_MGR_NormalAtk && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.NormalAtk) {

				//播放攻击动画
				// // Debug.Log("进行了1次普通攻击！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.NormalAtk);

				//固定时间触发1次。（单次伤害判定）
				//目的：为了适应键盘按住不放，控制自动攻击/连续攻击速度
				////这个时间参数很重要，否则会出现很多问题
				//if (UnityHelper.GetInstance().GetSmallTime(GlobalParameter.CHECK_TIME) || SingleCtrl) { 
				// // Debug.Log("给特定敌人以伤害处理");
				AttackEnemyByNormal();
				//if(SingleCtrl == true) {
				//	SingleCtrl = false;
				//}

			//}

			}
		}

		/// <summary>
		/// 响应魔法攻击A
		/// </summary>
		/// <param name="controlType"></param>
		public void ResponseMagicAtkA(string controlType) {
			if (controlType == GlobalParameter.INPUT_MGR_MagicAtkA && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicAtkA) {
				//播放攻击动画
				// // Debug.Log("进行了1次魔法攻击A！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicAtkA);
				//给特定敌人以伤害处理
				StartCoroutine( AttackEnemyByMagicAtkA() );
				//魔法值消耗
				Ctrl_HeroProperty.Instance.DeMana(MPConsByMagicAtkA);
			}
		}

		/// <summary>
		/// 响应魔法攻击B
		/// </summary>
		/// <param name="controlType"></param>
		public void ResponseMagicAtkB(string controlType) {
			if (controlType == GlobalParameter.INPUT_MGR_MagicAtkB && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicAtkB) {
				//播放攻击动画
				// // Debug.Log("进行了1次魔法攻击B！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicAtkB);
				//给特定敌人以伤害处理
				StartCoroutine( AttackEnemyByMagicAtkB() );
				//魔法值消耗
				Ctrl_HeroProperty.Instance.DeMana(MPConsByMagicAtkB);
			}
		}
#endregion

#region 给特定敌人以伤害处理

		private void AttackEnemyByNormal() {

			base.AttackEnemy(_Lis_Enemies,_Tra_NearestEnemy,FloRealAtkArea);

			/*
			
			//参数检查，如果敌人数量小于等于0，则直接跳过
			if(_Lis_Enemies==null || _Lis_Enemies.Count <=0) {
				_Tra_NearestEnemy = null;
				return;
			}

			//对多个敌人进行攻击判定
			foreach (GameObject enemyItem in _Lis_Enemies) {
				//首先判断敌人是否活着
				//前提是该游戏对象存在
				//if (enemyItem && enemyItem.GetComponent<Ctrl_Enemy>().IsAlive) {

				//这里仍然有待优化
				if (enemyItem && enemyItem.GetComponent<Ctrl_SkeletonWarrior_Prop>().CurrentState != EnemyActionState.Dead) {
					//敌我距离
					float floDistance = Vector3.Distance(this.gameObject.transform.position, enemyItem.gameObject.transform.position);
					//定义敌我方向（使用向量减法）
					Vector3 dir = (enemyItem.transform.position - this.transform.position).normalized;  //归一化，不要长度
																										//定义敌我夹角（使用向量点乘）
					float floDiretion = Vector3.Dot(dir, this.gameObject.transform.forward);

					//如果主角和敌人在同一方向，且在有效攻击范围内，则对敌人进行伤害处理。
					if (floDiretion > 0 && floDistance <= FloRealAtkArea) {
						//不需要返回值，更好的办法是使用委托事件
						//参数：方法名，角色当前攻击力
						// // Debug.Log("OnHurt!");
						enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurATK(), SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			*/
		}

		/// <summary>
		/// 使用魔法攻击A，攻击敌人
		/// 攻击范围：在主角周边范围都造成伤害
		/// 更好的方法：将伤害判定挂载到合适的帧上
		/// </summary>
		IEnumerator AttackEnemyByMagicAtkA() {
			yield return new WaitForSeconds(1f);
			base.AttackEnemy(_Lis_Enemies, _Tra_NearestEnemy, FloMagicAtkAArea, FloMagicAtkAMultiple, false);
		}


		/// <summary>
		/// 使用魔法攻击B，攻击敌人
		/// 攻击范围：主角的正对面方向，造成较大伤害
		/// 更好的方法：将伤害判定挂载到合适的帧上
		/// </summary>
		IEnumerator AttackEnemyByMagicAtkB() {
			yield return new WaitForSeconds(1f);
			base.AttackEnemy(_Lis_Enemies, _Tra_NearestEnemy, FloMagicAtkBArea, FloMagicAtkBMultiple);
		}

#endregion

	}//class_end
}