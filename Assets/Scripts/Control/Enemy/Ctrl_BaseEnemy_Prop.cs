//控制层，所有敌人的属性的父类
//包含所有敌人的公共属性
//运用重构的思想，来构造更加灵活与低耦合度的敌人

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_BaseEnemy_Prop : BaseControl {

		public int IntMaxHP = 0;   //敌人的最大生命数值
		public int IntATK = 0;     //敌人的攻击力
		public int IntDEF = 0;         //敌人的防御力

		public int IntEnemyEXP = 0; //英雄的经验数值

		public float FloMoveSpeed = 0;   //敌人移动速度
		public float FloRotationSpeed = 0; //敌人旋转速度


		private float _FloCurHp;    //敌人的当前生命数值


		//这个状态仅作判断，不是实时（真实）状态；
		//持续播放的动画持续判断，单次播放的动画只做1次判断
		//对于单次播放的动画，确定要播放后，就回到Idle状态
		//如果要得到实时（真实）状态，考虑使用（虽然也不准确）：
		//_MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(CurrentState.ToString());
		private EnemyActionState _CurrentState = EnemyActionState.Idle; //敌人的当前动画状态

		//private SinglePlayState _CurSinglePlayState = SinglePlayState.Init;

		public EnemyActionState CurrentState {
			get {
				return _CurrentState;
			}
			set {
				_CurrentState = value;
			}
		}

		//public SinglePlayState CurSinglePlayState {
		//	get {
		//		return _CurSinglePlayState;
		//	}
		//	set {
		//		_CurSinglePlayState = value;
		//	}
		//}

		/* 使用对象缓冲池技术后，协程方法应该根据脚本生命周期开始和结束 */

		private void OnEnable() {
			//判断是否存活
			StartCoroutine("CheckLifeContinue");
			//重置生命值为最大生命值
			_FloCurHp = IntMaxHP;
		}

		private void OnDisable() {
			//停止判断是否存活
			StopCoroutine("CheckLifeContinue");
		}


		///// <summary>
		///// 在子类中运行的方法
		///// </summary>
		//public void RunMethodInChildren() {
		//	_FloCurHp = IntMaxHP;

		//	////判断是否存活
		//	//StartCoroutine("CheckLifeContinue");
		//}



		/// <summary>
		/// 伤害处理
		/// </summary>
		/// <param name="heroAtk"></param>
		public void OnHurt(int heroAtk) {

			_CurrentState = EnemyActionState.Hurt;

			// // Debug.Log("进行伤害处理！");
			int hurtValue;
			if (heroAtk > IntDEF) {
				hurtValue = heroAtk - IntDEF;
			} else {
				hurtValue = 1;
			}

			if (_FloCurHp - hurtValue > 0) {
				_FloCurHp -= hurtValue;
				// // Debug.Log("当前HP：" + _FloCurHp);
			} else {
				_FloCurHp = 0;
			}
		}

		/// <summary>
		/// 检查是否存活
		/// </summary>
		/// <returns></returns>
		IEnumerator CheckLifeContinue() {
			//协程需要重复执行
			while (true) {
				//这里需要加以改动
				if (_FloCurHp <= 0) {
					if (_CurrentState != EnemyActionState.Dead) {
						_CurrentState = EnemyActionState.Dead;

						//Destroy(this.gameObject, 5f);   //销毁对象（敌人死亡），5s的延迟
						StartCoroutine("RecoverEnemies");	//回收对象（作为代替）

						Ctrl_HeroProperty.Instance.AddEXP(IntEnemyEXP); //玩家获得经验值
						Ctrl_HeroProperty.Instance.AddKillNum();    //增加玩家的杀敌数量
	
					}
				}
				yield return new WaitForFixedUpdate();        //每1帧判断1次				  
			}
		}

		/// <summary>
		/// 回收对象（敌人）
		/// </summary>
		/// <returns></returns>
		IEnumerator RecoverEnemies() {
			yield return new WaitForSeconds(5f);
			//敌人回收前的状态重置
			_FloCurHp = IntMaxHP;
			_CurrentState = EnemyActionState.Idle;
			//回收对象
			PoolManager.PoolsArray["_Enemies"].RecoverGameObject(this.gameObject);
			 

		}
	}
}
