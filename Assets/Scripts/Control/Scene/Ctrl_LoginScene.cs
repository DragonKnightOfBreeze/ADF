using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_LoginScene : BaseControl {

		public static Ctrl_LoginScene Instance; //本类的实例
		public AudioClip aucBackgroundMusic;	//登录场景背景音乐


		private void Awake() {
			Instance = this;	//得到这个脚本的实例
		}

		private void Start() {
			//确定音频的音量
			AudioManager.SetAudioBackgroundVolumns(0.5f);
			AudioManager.SetAudioEffectVolumns(0.5f);
			//播放背景音乐（单独调用背景音乐，不加入内存）
			AudioManager.PlayBackground(aucBackgroundMusic);
		}

		//播放英雄的转换音效
		public void PlayAudioEffect(PlayerType pt) {
			if (pt == PlayerType.Sworder) {
				AudioManager.PlayAudioEffectA("Hero_MagicA");
			}
			else if (pt == PlayerType.Mage) {
				AudioManager.PlayAudioEffectA("2_FireBallEffect_MagicHero");
			}
		}

		/// <summary>
		/// 进入下一个场景
		/// </summary>
		public void EnterNextScene() {
			//使用父类的方法，简化代码
			base.EnterNextScene(SceneEnum.LoginScene);
		}


	}
}