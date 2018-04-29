//控制层，主角攻击控制
//包括事件的注册（添加相关方法进去）

//开发思路：
//	把附近的所有敌人放入“敌人数组”
//		得到所有敌人，放入“敌人集合”数组。
//		判断敌人集合，找出最近的敌人。

//	主角在一定范围内，开始自动“关注”最近的敌人（仅在RPG手游中）
//	响应输入攻击信号，对于主角“正面”的敌人予以一定伤害处理
//不使用传统的触发器（绑定在剑上）

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
		public float FloRealAtkArea = 2f;		//主角实际有效攻击距离

		private List<GameObject> _Lis_Enemies;	//敌人的集合
		private Transform _Tra_NearestEnemy;    //最近的敌人方位
		private float _FloMaxDistance = 20f;    //敌我最大距离（可能攻击到，可能需要进行处理）



		private void Awake() {
			//事件注册：主角攻击输入（技术是多播委托）
			//根据提供的参数来判断调用的方法
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseNormalAtk;
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseMagicAtkA;
			Ctrl_HeroAttackByKey.Eve_PlayerControl += ResponseMagicAtkB;
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
				Ctrl_Enemy enemy = goItem.GetComponent<Ctrl_Enemy>();
				if (enemy && enemy.IsAlive) { 
					_Lis_Enemies.Add(goItem);
				}
			}
		}

		/// <summary>
		/// 把附近的所有敌人放入“敌人数组”
		/// 每隔2s处理一次
		/// </summary>
		IEnumerator RecordNearbyEnemiesTOArray() {
			Debug.Log("协程已开始：RecordNearbyEnemiesTOArray");
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
			Debug.Log("协程已开始：HeroRotationEnemy");
			while (true) {
				yield return new WaitForSeconds(0.5f); //每隔0.5s判断1次
																						//需要增加判断条件，必须是主角停下来时，才会自动注视
				if (_Tra_NearestEnemy != null && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle
					) {
					Debug.Log("旋转判定2起效！");
					float floDistance = Vector3.Distance(this.gameObject.transform.position, _Tra_NearestEnemy.position);
					//可以认为是近战攻击时的方向修正
					if (floDistance < FloMinFocusDistance) {
						Debug.Log("旋转判定2起效！");
						//应该仅仅按照Y轴进行旋转
						//this.transform.LookAt(_Tra_NearestEnemy);
						//另外的改进：

						//使用四元数，使仅仅头部发生旋转，并使用插值运算使之更加平滑
						this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
							Quaternion.LookRotation(new Vector3(_Tra_NearestEnemy.position.x, 0, _Tra_NearestEnemy.position.z) - new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)),
							FloHeroRotationSpeed);

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
			if (controlType == GlobalParameter.INPUT_MGR_NormalAtk) {
				//播放攻击动画
				Debug.Log("进行了1次普通攻击！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.NormalAtk);

				//固定时间触发1次。（单次伤害判定）
				//++这个不应该才是外循环吗？
				if (UnityHelper.GetInstance().GetSmallTime(0.2f)) {
					//给特定敌人以伤害处理
					AttackEnemyByNormal();
				}

			}
		}

		/// <summary>
		/// 响应魔法攻击A
		/// </summary>
		/// <param name="controlType"></param>
		public void ResponseMagicAtkA(string controlType) {
			if (controlType == GlobalParameter.INPUT_MGR_MagicAtkA) {
				//播放攻击动画
				Debug.Log("进行了1次魔法攻击A！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicAtkA);
				//给特定敌人以伤害处理
			}
		}

		/// <summary>
		/// 响应魔法攻击B
		/// </summary>
		/// <param name="controlType"></param>
		public void ResponseMagicAtkB(string controlType) {
			if (controlType == GlobalParameter.INPUT_MGR_MagicAtkB) {
				//播放攻击动画
				Debug.Log("进行了1次魔法攻击B！");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicAtkB);
				//给特定敌人以伤害处理
			}
		}
		#endregion

		#region 给特定敌人以伤害处理

		private void AttackEnemyByNormal() {
			//参数检查，如果敌人数量小于等于0，则直接跳过
			if(_Lis_Enemies==null || _Lis_Enemies.Count <=0) {
				_Tra_NearestEnemy = null;
				return;
			}

			//对多个敌人进行攻击判定
			foreach (GameObject enemyItem in _Lis_Enemies) {
				//首先判断敌人是否活着
				if (enemyItem.GetComponent<Ctrl_Enemy>().IsAlive) {
					//敌我距离
					float floDistance = Vector3.Distance(this.gameObject.transform.position, enemyItem.gameObject.transform.position);
					//定义敌我方向（使用向量减法）
					Vector3 dir = (enemyItem.transform.position - this.transform.position).normalized;  //归一化，不要长度
																										//定义敌我夹角（使用向量点乘）
					float floDiretion = Vector3.Dot(dir, this.gameObject.transform.forward);

					//如果主角和敌人在同一方向，且在有效攻击范围内，则对敌人进行伤害处理。
					if (floDiretion > 0 && floDistance <= FloRealAtkArea) {
						//不需要返回值，更好的办法是使用委托事件
						enemyItem.SendMessage("OnHurt", 4, SendMessageOptions.DontRequireReceiver);
					}
				}

			}

		}

		#endregion

	}//class_end
}