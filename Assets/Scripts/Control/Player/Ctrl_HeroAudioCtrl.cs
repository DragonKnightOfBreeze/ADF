using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroAudioCtrl : MonoBehaviour {

		public static Ctrl_HeroAudioCtrl Instance;

		public AudioClip Auc_HeroRunning;       //跑动音效

		private string _Auc_CurNormalAtk;        //当前普通攻击音效

		//private float _LoopTime = 0f;

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
				yield return new WaitForSeconds(0.01f);

				#region 音效同步停止

				if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.Running && AudioManager.IsPlayAudioEffectA(Auc_HeroRunning)) {
					AudioManager.StopAudioEffectA(Auc_HeroRunning);         
				}

				#endregion

				#region 音效同步播放

				switch (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState) {
					case HeroActionState.None:
						yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);
						break;

					case HeroActionState.Idle:
						yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);
						break;

					case HeroActionState.Running:
						

						//设置循环播放，且只在跑动状态下播放
						if (!AudioManager.IsPlayAudioEffectA(Auc_HeroRunning)) {
							AudioManager.PlayAudioEffectA(Auc_HeroRunning);         //播放跑动音效
						}
						
						yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);
						break;

					case HeroActionState.NormalAtk:
						switch (Ctrl_HeroAnimationCtrl.Instance.CurAtkCombo) {
							case NormalAtkComboState.NormalAtk1:
								_Auc_CurNormalAtk = "BeiJi_DaoJian_3";
								break;
							case NormalAtkComboState.NormalAtk2:
								_Auc_CurNormalAtk = "BeiJi_DaoJian_2";
								break;
							case NormalAtkComboState.NormalAtk3:
								_Auc_CurNormalAtk = "BeiJi_DaoJian_1";
								break;
							default:
								break;
						}
						AudioManager.PlayAudioEffectA(_Auc_CurNormalAtk);
						yield return new WaitForSeconds(Ctrl_HeroAnimationCtrl.Instance.WaitTime);
						break;

					case HeroActionState.MagicAtkA:
						AudioManager.PlayAudioEffectA("Hero_MagicA");
						yield return new WaitForSeconds(Ctrl_HeroAnimationCtrl.Instance.WaitTime);
						break;

					case HeroActionState.MagicAtkB:
						// // Debug.Log("播放魔法攻击B音频");
						AudioManager.PlayAudioEffectA("Hero_MagicB");
						yield return new WaitForSeconds(Ctrl_HeroAnimationCtrl.Instance.WaitTime);
						break;

					default:
						break;
				}

				#endregion

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