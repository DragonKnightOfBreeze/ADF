//［控制层］敌人：所有敌人的AI系统的父类

//引用：
//主角，自身的方位，自身的属性脚本，自身的动画控制脚本，自身的动画状态机

//功能：
//1.思考过程
//2.移动过程

//特点：
//使用随机数，实现个体差异性（简易版）

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_BaseEnemy_AI : BaseControl {

		private GameObject _GoHero;					//主角

		private Transform _MyTransform;             //该敌人的方位
		private CharacterController _MyCController;    //当前敌人的角色控制器
		private Ctrl_BaseEnemy_Prop _MyProperty;    //该敌人的属性脚本

		private float _CurReCheckTime;

		#region 【自动方法】

		protected virtual void Start() {
			//得到主角
			_GoHero = GameObject.FindGameObjectWithTag(Tag.Tag_Player);

			//得到该敌人的方位
			_MyTransform = this.gameObject.transform;
			//得到该敌人的角色控制器
			_MyCController = this.gameObject.GetComponent<CharacterController>();
			//得到该敌人的属性脚本实例
			_MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemy_Prop>();
		}


		/* 在敌人处于硬直状态（例：受伤）时，需要暂停思考协程和移动协程 */

		protected virtual void OnEnable() {
			//开始思考协程
			StartCoroutine(ThinkProcess());
			//开始移动协程
			StartCoroutine(MovingProcess());
		}

		protected virtual void OnDisable() {
			//停止思考协程
			StopCoroutine(ThinkProcess());
			//停止移动协程
			StopCoroutine(MovingProcess());
		}

		#endregion



		#region 【私有协程】

		/// <summary>
		/// 敌人的思考协程
		/// 较简单的方法：不断得到相对位置
		/// 注意正在播放的动画状态和设置的动画状态的同步问题！
		/// （当敌人进入受伤等硬直状态时，需要重置该协程）
		/// </summary>
		/// <returns></returns>
		IEnumerator ThinkProcess() {
			yield return new WaitForSeconds(0.5f);

			while (true) {
				//敌人存活判断
				_CurReCheckTime = Ctrl_BaseEnemy_Prop.CON_RecheckTime;
				if (_MyProperty && _MyProperty.CurrentState != EnemyActionState.Dead) {
					//得到主角的当前位置
					Vector3 VecHero = _GoHero.transform.position;
					//得到相隔距离
					float floDistance = Vector3.Distance(VecHero, _MyTransform.position);

					/* 相隔距离判断 */

					//小于攻击距离
					if (floDistance < _MyProperty.AttackDistance) {
						//进入攻击状态
						_MyProperty.CurrentState = EnemyActionState.NormalAtk;
						_CurReCheckTime = _MyProperty.AtkSpeed;
					}
					//如果小于警戒距离
					else if (floDistance < _MyProperty.AlertDistance) {
						//进入追击状态
						_MyProperty.CurrentState = EnemyActionState.Moving;
					}
					//如果大于警戒距离
					else {
						//进入等待状态
						_MyProperty.CurrentState = EnemyActionState.Idle;
					}
				}

				yield return new WaitForSeconds(_CurReCheckTime);
			}
		}

		/// <summary>
		/// 敌人的移动协程
		/// （当敌人进入受伤等硬直状态时，需要重置该协程）
		/// </summary>
		IEnumerator MovingProcess() {
			yield return new WaitForSeconds(0.5f);

			while (true) {
				if (_MyProperty && _MyProperty.CurrentState != EnemyActionState.Dead) {
					//注视玩家
					FaceToHero();
					//移动
					if(_MyProperty.CurrentState == EnemyActionState.Moving) {
						MoveToHero();
					}
				}
				yield return new WaitForSeconds(_CurReCheckTime);
			}
		}

		#endregion



		#region 【私有方法】

		/// <summary>
		/// 面向主角
		/// </summary>
		private void FaceToHero() {
			UnityHelper.GetInstance().FaceToGoal(this.gameObject.transform, _GoHero.transform, _MyProperty.RotationSpeed);
		}

		/// <summary>
		/// 朝主角移动
		/// （不使用导航寻路）
		/// </summary>
		private void MoveToHero() {
			//限定长度（英雄方位-当前敌人方位）
			Vector3 v = Vector3.ClampMagnitude((_GoHero.transform.position - _MyTransform.position), _MyProperty.MoveSpeed * Time.deltaTime);
			//移动
			_MyCController.Move(v);
		}

		#endregion

	}
}