//控制层，开始场景

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_StartScene : BaseControl {

		public static Ctrl_StartScene Instance; //本类的实例
		public AudioClip AucBackground;	//音频剪辑

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			AudioManager.SetAudioBackgroundVolumns(0.5f);   //设置音频的音量
			AudioManager.SetAudioEffectVolumns(1f);
			AudioManager.PlayBackground("StartScenes");     //播放背景音乐
			//AudioManager.PlayBackground(AucBackground);	//第二种方式
		}


		/// <summary>
		/// 点击“新的旅程”
		/// </summary>
		internal void ClickNewGame() {
			// // Debug.Log(GetType() + "/ClickNewGame()");
			//进入“登录”场景
			StartCoroutine("EnterNextScene");

		}

		/// <summary>
		/// 点击“继续旅程”
		/// </summary>
		internal void ClickGameContinue() {
			print(GetType() + "/ClickGameCOntinue()");
		}

		/// <summary>
		/// 进入下一个场景（登录场景）
		/// </summary>
		IEnumerator EnterNextScene() {

			//场景淡出（场景变暗）
			FadeInAndOut.Instance.SetSceneToBlack();
			yield return new WaitForSeconds(1.5f);
			//调用父类的方法，简化代码
			base.EnterNextScene(SceneEnum.LoginScene);
		}
	}
}

