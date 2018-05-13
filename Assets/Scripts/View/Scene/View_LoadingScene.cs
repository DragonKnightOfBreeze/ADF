//视图层，场景异步加载控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;

namespace View {
	public class View_LoadingScene : MonoBehaviour {

		public Slider SliLoadingSlider;        //进度条控件
		private float _FloProgressNumber;   //进度数值
		private AsyncOperation _AsyOper;

		private void Start() {
			//调试进入指定的关卡
			GlobalParaMgr.NextSceneName = SceneEnum.Level1;	//进入第一关卡

			StartCoroutine("LoadingSceneProgress");

		}

		/// <summary>
		/// 显示进度条（有待改善）
		/// </summary>
		private void Update() {
			//实际测试时的合适最大长度
			if (_FloProgressNumber <= 0.434) {	
				_FloProgressNumber += 0.01F;
			}
			SliLoadingSlider.value = _FloProgressNumber;   //改变进度条长度
		}


		/// <summary>
		/// 异步加载协程
		/// </summary>
		/// <returns></returns>
		IEnumerator LoadingSceneProgress() {
			//已过时，使用SceneManager.LoadingSceneAsync
			_AsyOper = Application.LoadLevelAsync(ConvertEnumToStr.GetInstance().GetStrByEnumScene(GlobalParaMgr.NextSceneName));	  //固定代码，只要换场景就可以用
			_FloProgressNumber = _AsyOper.progress;
			yield return _AsyOper;
		}

	}
}
