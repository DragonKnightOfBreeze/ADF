using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroAudioCtrl : MonoBehaviour {

		public static Ctrl_HeroAudioCtrl Instance;

		public AudioClip Auc_Running;       //跑动音效
		public AudioClip Auc_NormalAtk_1;	//普通攻击音效
		public AudioClip Auc_NormalAtk_2;
		public AudioClip Auc_NormalAtk_3;
		public AudioClip Auc_MagicAtkA;		//技能音效
		public AudioClip Auc_MagicAtkB;

		private AudioClip _Auc_CurNormalAtk;        //当前普通攻击音效

		private void Awake() {
			Instance = this;
		}


		void Start() {
			StartCoroutine("CtrlHeroAudioState");
		}


		/// <summary>
		/// 主角的音频播放控制（根据当前动作状态）
		/// </summary>
		/// <returns></returns>
		IEnumerator CtrlHeroAudioState() {
			while (true) {

				///* 音效同步停止 */

				//if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.Running && AudioManager.IsPlayingAudioEffect_A(Auc_Running)) {
				//	AudioManager.StopAudioEffect_A();         
				//}

				/* 音效同步播放 */

				switch (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState) {
					case HeroActionState.None:
						break;

					case HeroActionState.Idle:
						break;

					case HeroActionState.Running:
						//设置循环播放，且只在跑动状态下播放
						AudioManager.PlayAudioEffect_A(Auc_Running);  
						break;

					case HeroActionState.NormalAtk:
						switch (Ctrl_HeroAnimationCtrl.Instance.CurAtkCombo) {
							case NormalAtkComboState.NormalAtk1:
								_Auc_CurNormalAtk = Auc_NormalAtk_1;
								break;
							case NormalAtkComboState.NormalAtk2:
								_Auc_CurNormalAtk = Auc_NormalAtk_2;
								break;
							case NormalAtkComboState.NormalAtk3:
								_Auc_CurNormalAtk = Auc_NormalAtk_3;
								break;
							default:
								break;
						}
						AudioManager.PlayAudioEffect_A(_Auc_CurNormalAtk,true);
						break;

					case HeroActionState.MagicAtkA:
						AudioManager.PlayAudioEffect_A(Auc_MagicAtkA,true);
						break;

					case HeroActionState.MagicAtkB:
						AudioManager.PlayAudioEffect_A(Auc_MagicAtkB,true);
						break;

					default:
						break;
				}
				//等待当前单次动画播放完，或者重复判定持续动画
				yield return new WaitForSeconds(Ctrl_HeroAnimationCtrl.Instance.ReCheckTime);
			}
		}




		/*
		/// <summary>
		/// 循环判断
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		private bool GetLoopTime(float length) {
			if (_LoopTime == 0) {
				return true;
			}
			_LoopTime += Time.deltaTime;
			if(_LoopTime == length) {
				_LoopTime = 0;
				return true;
			} else {
				return false;
			}
		}
		*/

	}
}