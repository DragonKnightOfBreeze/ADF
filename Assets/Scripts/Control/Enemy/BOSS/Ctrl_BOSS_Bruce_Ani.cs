//［控制层］敌人：BOSS的动画脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_BOSS_Bruce_Ani : Ctrl_BaseEnemy_Ani {

		private Ctrl_BOSS_Bruce_Prop _MyProperty_S; //特有属性

		/* 音效处理 */
		public AudioClip Auc_Show;			//出现音效
		public AudioClip Auc_NormalAttack;	//攻击音效
		public AudioClip Auc_Hurt;			//受伤音效
		public AudioClip Auc_Dead;			//死亡音效
		
		private readonly float CON_PETime_Attack = 1f;		//攻击粒子特效的显示时间
		private readonly float CON_PETime_Attack_Jump=4f;		//技能攻击粒子特效的显示时间
		private readonly float CON_PETime_Hurt = 2f;		//受伤粒子特效的显示时间
		private readonly float CON_PETime_Dead = 2f;		//死亡粒子特效的显示时间


		#region 【重载后的自动方法】

		protected override void Start() {
			_MyProperty_S = gameObject.GetComponent<Ctrl_BOSS_Bruce_Prop>();
			base.Start();
		}

		protected override void OnEnable() {
			PlayAudio_Show();
			//StartCoroutine(PlayAudio());
			base.OnEnable();
		}

		protected override void OnDisable() {
			//StopCoroutine(PlayAudio());
			base.OnDisable();
		}

		#endregion



		#region 【其他方法】

		//IEnumerator PlayAudio() {
		//	//等待画面渲染完毕
		//	yield return new WaitForEndOfFrame();

		//	while (true) {

		//		switch (_MyProperty.CurrentState) {
		//			//播放攻击音效
		//			case EnemyActionState.NormalAtk:
		//				AudioManager.PlayAudioEffect_A(Auc_NormalAttack);
		//				break;
		//			//播放受伤音效
		//			case EnemyActionState.Hurt:
		//				AudioManager.PlayAudioEffect_A(Auc_Hurt,true);
		//				break;
		//			//播放死亡音效
		//			case EnemyActionState.Dead:
		//				AudioManager.PlayAudioEffect_A(Auc_Dead, true);
		//				break;
		//			default:
		//				break;
		//		}
		//		yield return new WaitForSeconds(Ctrl_BaseEnemy_Prop.CON_RecheckTime);
		//	}
		//}

		/// <summary>
		/// 敌人出现音效
		/// </summary>
		public override void PlayAudio_Show() {
			AudioManager.PlayAudioEffect_A(Auc_Show);
		}

		#endregion



		#region 【公共动画事件】

		/// <summary>
		/// 动画事件：开始攻击
		/// </summary>
		public override void AniEvent_Attack_BG() {
			//播放攻击音效
			AudioManager.PlayAudioEffect_A(Auc_NormalAttack);
		}


		/// <summary>
		/// 动画事件：攻击判定
		/// </summary>
		public override IEnumerator AniEvent_Attack() {
			//播放攻击命中粒子特效
			StartCoroutine(base.LoadParticalEffect("ParticleProps/BOSS_Attack", this.transform, Vector3.zero, CON_PETime_Attack));

			//英雄减少敌人的攻击力的生命值
			base._HeroProperty.SubCurHP(base._MyProperty.ATK);
			yield break;
		}

		/// <summary>
		/// 动画事件：跳跃攻击判定
		/// </summary>
		public IEnumerator AniEvent_Attack_Jump() {
			//播放攻击粒子特效
			StartCoroutine(base.LoadParticalEffect("ParticleProps/BOSS_Attack_Jump", this.transform,Vector3.zero, CON_PETime_Attack_Jump));

			//英雄减少敌人的攻击力的一定倍率的生命值
			base._HeroProperty.SubCurHP(base._MyProperty.ATK * _MyProperty_S.ATKRatio_Jump);
			yield break;
		}


		/// <summary>
		/// 动画事件：受伤判定
		/// （待优化，使用对象缓冲池技术）
		/// </summary>
		public override IEnumerator AniEvent_Hurt() {
			//播放受伤粒子特效
			StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Hurt", this.transform, Vector3.zero, CON_PETime_Hurt));
			//播放受伤音效
			AudioManager.PlayAudioEffect_A(Auc_Hurt);
			yield break;
		}

		/// <summary>
		/// 动画事件：死亡判定
		/// </summary>
		/// <returns></returns>
		public override IEnumerator AniEvent_Dead() {
			//播放死亡粒子特效
			//StartCoroutine(base.LoadParticalEffect("ParticleProps/Enemy_Hurt", this.transform, Vector3.zero, CON_PETime_Dead));
			//播放死亡音效
			AudioManager.PlayAudioEffect_A(Auc_Dead);
			yield break;
		}

		#endregion
	}
}