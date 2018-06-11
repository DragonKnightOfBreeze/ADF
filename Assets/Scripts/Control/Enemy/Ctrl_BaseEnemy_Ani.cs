//［控制层］所有敌人的动画控制脚本的父类

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_BaseEnemy_Ani : BaseControl {

		protected GameObject goHero;                          //英雄的游戏对象
		protected Ctrl_HeroProperty _HeroProperty;            //英雄的属性

		private Animator _MyAnimator;                       //敌人自身的动画状态机
		private CharacterController _MyCController;                 //当前敌人的角色控制器
		protected Ctrl_BaseEnemy_Prop _MyProperty;            //该敌人的属性
		private Ctrl_BaseEnemy_AI _MyAI;                    //该敌人的AI

		private float bwRate = 0.5f;                        //敌人受攻击时的被击退距离

		private bool _SingleCtrl = true;                    //单次开关


		private readonly float CON_PETime_Show = 3f;           //出现时的粒子特效的显示时间
		//private readonly float CON_PETime_Attack = 1f;      //攻击粒子特效的显示时间
		private readonly float CON_PETime_Hurt = 2f;		  //受伤粒子特效的显示时间
		//private readonly float CON_PETime_Dead = 2f;        //死亡粒子特效的显示时间


		#region 【自动方法】

		protected virtual void Start() {
			//查找英雄的引用
			goHero = GameObject.FindGameObjectWithTag(Tag.Tag_Player);
			if (goHero) {
				_HeroProperty = goHero.GetComponent<Ctrl_HeroProperty>();
			}
			//查找自身的引用
			_MyAnimator = gameObject.GetComponent<Animator>();
			_MyCController = this.gameObject.GetComponent<CharacterController>();
			_MyProperty = gameObject.GetComponent<Ctrl_BaseEnemy_Prop>();
			_MyAI = gameObject.GetComponent<Ctrl_BaseEnemy_AI>();
		}


		protected virtual void OnEnable() {
			//播放出现特效
			EnemyShowParticleEffect();
			//播放动画
			StartCoroutine(PlayAnimation());
		}

		protected virtual void OnDisable() {
			//停止播放动画
			StopCoroutine(PlayAnimation());
			//恢复动画状态机（到等待状态）
			if (_MyAnimator != null) {
				_MyAnimator.SetTrigger("Recover");
			}
		}

		#endregion



		#region 【私有协程】

		/// <summary>
		/// 播放骷髅弓箭手的动画
		/// 注意：在这里控制动画状态机
		/// </summary>
		/// <returns></returns>
		protected virtual IEnumerator PlayAnimation() {
			//等待画面渲染完毕
			yield return new WaitForEndOfFrame();

			while (true) {

				switch (_MyProperty.CurrentState) {
					case EnemyActionState.Idle:
						// Debug.Log("播放等待动画");
						_MyAnimator.SetFloat("MoveSpeed", 0);
						_MyAnimator.SetBool("IsAttacking", false);
						break;

					case EnemyActionState.Moving:
						Debug.Log("播放移动动画");
						_MyAnimator.SetFloat("MoveSpeed", _MyProperty.MoveSpeed);
						break;

					case EnemyActionState.NormalAtk:
						// Debug.Log("播放攻击动画");
						_MyAnimator.SetFloat("MoveSpeed", 0);
						_MyAnimator.SetBool("IsAttacking", true);
						_MyProperty.CurrentState = EnemyActionState.Idle;
						break;

					case EnemyActionState.Hurt:
						// Debug.Log("播放受伤动画");
						_MyAnimator.SetTrigger("IsAttacked");
						//被击退
						BackwardWhileHurt(bwRate);
						_MyProperty.CurrentState = EnemyActionState.Idle;
						break;

					case EnemyActionState.Dead:
						//设置单次动画控制
						if (_SingleCtrl) {
							// Debug.Log("播放死亡动画");
							_MyAnimator.SetTrigger("IsDead");
							_SingleCtrl = false;
						}
						break;

					default:
						break;
				}

				yield return new WaitForSeconds(Ctrl_BaseEnemy_Prop.CON_RecheckTime);
			}
		}

		#endregion



		#region 【公共方法】

		/// <summary>
		/// 公共方法：敌人出现
		/// </summary>
		public virtual void PlayAudio_Show() {
			//AudioManager.PlayAudioEffect_A(Auc_Show);
		}


		#endregion



		#region 【受保护方法】

		/// <summary>
		/// 敌人出现特效
		/// （待优化，使用对象缓冲池技术）
		/// </summary>
		protected virtual void EnemyShowParticleEffect() {
			StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Show2", transform, Vector3.zero, CON_PETime_Show));
		}

		/// <summary>
		/// 受到攻击时的被击退
		/// （敌人位置-玩家位置）
		/// </summary>
		/// <param name="">击退程度</param>
		private void BackwardWhileHurt(float bwRate) {
			Vector3 v = -transform.forward * bwRate * Time.deltaTime;
			_MyCController.Move(v);
		}

		#endregion



		#region 【动画事件】

		/// <summary>
		/// 动画事件：开始攻击
		/// </summary>
		public virtual void AniEvent_Attack_BG() {
			//播放攻击音效
			//
		}

		




		/// <summary>
		/// 动画事件：攻击判定
		///（需要完善）
		/// </summary>
		public virtual IEnumerator AniEvent_Attack() {
			//播放攻击命中特效
			//

			//数据处理
			//_HeroProperty.SubCurHP(_MyProperty.ATK);        //英雄减少敌人的攻击力值的生命值
			yield break;
		}

		/// <summary>
		/// 动画事件：受伤判定
		/// （待优化，使用对象缓冲池技术）
		/// </summary>
		public virtual IEnumerator AniEvent_Hurt() {
			//播放受伤粒子特效
			StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Hurt", this.transform, Vector3.zero, CON_PETime_Hurt));
			//播放受伤音效
			//
			yield break;
		}

		/// <summary>
		/// 动画事件：死亡判定
		/// </summary>
		/// <returns></returns>
		public virtual IEnumerator AniEvent_Dead() {
			//播放死亡粒子特效
			//
			//播放死亡音效
			//
			yield break;
		}

		#endregion
	}
}