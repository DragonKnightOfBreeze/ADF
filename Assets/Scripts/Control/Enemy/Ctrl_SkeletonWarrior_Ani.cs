using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control{
	public class Ctrl_SkeletonWarrior_Ani : BaseControl {
		private Ctrl_BaseEnemy_Prop _MyProperty;      //本身的属性
		private Ctrl_HeroProperty _HeroProperty;        //英雄的属性
		private Ctrl_SkeletonWarrior_AI _MyAI;

		private Animator _MyAnimator;             //骷髅战士的动画状态机
		private CharacterController _cc;    //当前敌人的角色控制器

		private GameObject goHero;

		private float bwRate = 1f;

		//private  float waitTime = 0.1f;           //协程等待时间
		//private float aniLength = 0.1f;

		//这个字段准确来说，应该在 HeroProperty.cs 中定义

		private bool _SingleCtrl = true;     //单次开关

		//public float WaitTime {
		//	get {
		//		return waitTime;
		//	}
		//}


		private void OnEnable() {
			//播放动画
			StartCoroutine("PlayAnimation");
			//开启单次模式
			_SingleCtrl = true;
		}

		private void OnDisable() {
			//停止播放动画
			StopCoroutine("PlayAnimation");
			//恢复动画状态机（到等待状态）
			if(_MyAnimator!= null) {
				_MyAnimator.SetTrigger("Recover");
			}
		}


		void Start() {
			_MyProperty = gameObject.GetComponent<Ctrl_BaseEnemy_Prop>();   //得到本身属性
			_MyAI = gameObject.GetComponent<Ctrl_SkeletonWarrior_AI>();
			_MyAnimator = gameObject.GetComponent<Animator>();    //得到动画状态机
			goHero = GameObject.FindGameObjectWithTag(Tag.Tag_Player);
			_cc = this.gameObject.GetComponent<CharacterController>();

			if (goHero) {
				_HeroProperty = goHero.GetComponent<Ctrl_HeroProperty>();   //得到英雄的属性脚本
			}
		}

		/// <summary>
		/// 播放骷髅战士的动画
		/// 在这里控制动画状态机
		/// </summary>
		/// <returns></returns>
		IEnumerator PlayAnimation() {
			//每个状态都应该有最小/默认持续时间
			//每一帧都要判断，当前的算法还需改进

			//使用waitTime判断并不灵活
			//一个妥协的办法：分为两个方法，采用不同的间隔时间

			//一个更好的方法：使用状态枚举判断
			//（AniNotStart，默认值；AniStart，播放动画；AniOngoing，无操作；AniEnd，恢复到默认动画状态以及默认状态枚举）

			//等待画面渲染完毕
			yield return new WaitForEndOfFrame();

			while (true) {

				switch (_MyProperty.CurrentState) {
					case EnemyActionState.Idle:
						// // Debug.Log("播放等待动画");
						_MyAnimator.SetFloat("MoveSpeed", 0);
						_MyAnimator.SetBool("IsAttacking", false);
						break;

					case EnemyActionState.Moving:
						// // Debug.Log("播放移动动画");
						_MyAI.SingleCtrl = true;
						_MyAnimator.SetFloat("MoveSpeed", _MyProperty.FloMoveSpeed);
						break;

					case EnemyActionState.NormalAtk:
						// // Debug.Log("播放攻击动画");
						_MyAnimator.SetFloat("MoveSpeed", 0);
						_MyAnimator.SetBool("IsAttacking", true);
						_MyProperty.CurrentState = EnemyActionState.Idle;
						break;

					case EnemyActionState.Hurt:
						// // Debug.Log("播放受伤动画");
						_MyAI.SingleCtrl = true;
						_MyAnimator.SetTrigger("IsAttacked");
						BackwardWhileHurt(bwRate);		//被击退
						_MyProperty.CurrentState = EnemyActionState.Idle;
						break;

					case EnemyActionState.Dead:
						if (_SingleCtrl) {
							// // Debug.Log("播放死亡动画");
							_MyAnimator.SetTrigger("IsDead");
							_SingleCtrl = false;
						}
						break;

					default:
						break;
				}

				/*

				if (_MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
					_SingleCtrl = true;
				}
				
				//空闲
				if (_MyProperty.CurrentState == EnemyActionState.Idle) {
					_SingleCtrl = true;
					_MyAnimator.SetFloat("MoveSpeed", 0);
					_MyAnimator.SetBool("IsAttacking", false);
				}

				//移动
				else if (_MyProperty.CurrentState == EnemyActionState.Moving) {
					_MyAnimator.SetFloat("MoveSpeed", _MyProperty.FloMoveSpeed);
					_MyAnimator.SetBool("IsAttacking", false);
				}

				//普通攻击
				else if (_MyProperty.CurrentState == EnemyActionState.NormalAtk ) {
					//if (_MyProperty.CurSinglePlayState == SinglePlayState.Start) {
						// // Debug.Log("播放攻击动画");
						_MyAnimator.SetFloat("MoveSpeed", 0);
						_MyAnimator.SetBool("IsAttacking", true);
						_MyProperty.CurrentState = EnemyActionState.Idle;

				}
				//else if (!_MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("NormalAtk")) {
				//	_MyProperty.CurSinglePlayState = SinglePlayState.End;
				//}
				//else if (_MyProperty.CurSinglePlayState == SinglePlayState.End) {
				//	_MyProperty.CurSinglePlayState = SinglePlayState.Init;
				//}

				//}

				//受伤
				else if (_MyProperty.CurrentState == EnemyActionState.Hurt ) {
					// // Debug.Log("播放受伤动画");
					_MyAnimator.SetTrigger("IsAttacked");
					_MyProperty.CurrentState = EnemyActionState.Idle;
				}

				//死亡
				else if (_MyProperty.CurrentState == EnemyActionState.Dead ) {
					if (_SingleCtrl) {
						// // Debug.Log("播放死亡动画");
						_MyAnimator.SetTrigger("IsDead");
						_SingleCtrl = false;
					}
				}

				*/

				yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);
			}	
		}

		

		/// <summary>
		/// 受到攻击时的击退
		/// （敌人位置-玩家位置）
		/// </summary>
		/// <param name="">击退程度</param>
		private void BackwardWhileHurt(float bwRate) {
			Vector3 v = - transform.forward * bwRate * Time.deltaTime;
			_cc.Move(v);
		}



		/// <summary>
		/// 动画事件：攻击英雄的英雄受伤判定
		/// 应该还有一个碰撞判定作为前提
		/// 在攻击动画的某一帧（动态/静态）执行
		/// </summary>
		public void AtkHeroByAnimationEvent() {
			
			_HeroProperty.DeHealth(_MyProperty.IntATK);		//英雄减少敌人的攻击力值的生命值
		}



		/// <summary>
		/// 定义骷髅战士受伤的粒子特效
		/// （待优化，使用对象缓冲池技术）
		/// </summary>
		public IEnumerator AniEvent_SkeletonWawrrior_Hurt() {
			StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Hurt", this.transform, Vector3.zero, 2f));
			yield break;
		}
	}
}