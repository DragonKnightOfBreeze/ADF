//公共层， 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global {
	public class FadeInAndOut : MonoBehaviour {

		public static FadeInAndOut Instance;    //本类实例
		public float FloColorChangeSpeed = 1f;  //颜色的变化速度
		public GameObject goRawImage;           //RawImage对象
		private RawImage _RawImage;             //RawImage组件

		private bool _BoolSceneToClear = true;  //屏幕逐渐清晰
		private bool _BoolSceneToBlack = false;	//屏幕逐渐暗淡

		void Awake() {
			//得到本类的实例
			Instance = this;
			//得到RawImage组件
			if (goRawImage) {
				_RawImage = goRawImage.GetComponent<RawImage>();
			}
		}

		/// <summary>
		/// 设置场景的淡入
		/// </summary>
		public void SetSceneToClear() {
			_BoolSceneToClear = true;
			_BoolSceneToBlack = false;
		}

		/// <summary>
		/// 设置场景的淡出
		/// </summary>
		public void SetSceneToBlack() {
			_BoolSceneToClear = false;
			_BoolSceneToBlack = true;
		}

		/// <summary>
		/// 淡入效果（屏幕逐渐清晰）
		/// </summary>
		private void FadeToClear() {
			//运用Color类的插值计算，逐渐变化
			_RawImage.color = Color.Lerp(_RawImage.color, Color.clear, FloColorChangeSpeed * Time.deltaTime);
		}

		/// <summary>
		/// 淡出效果（屏幕逐渐暗淡）
		/// </summary>
		private void FadeToBlack() {
			_RawImage.color = Color.Lerp(_RawImage.color, Color.black, FloColorChangeSpeed * Time.deltaTime);
		}

		/// <summary>
		/// 屏幕淡入
		/// </summary>
		private void SceneToClear() {
			FadeToClear();
			if(_RawImage.color.a <= 0.05) {
				_RawImage.color = Color.clear;
				_RawImage.enabled = false;
				_BoolSceneToClear = false;
			}
		}

		//屏幕淡出
		private void SceneToBlack() {
			_RawImage.enabled = true;
			FadeToBlack();
			if(_RawImage.color.a >= 0.95) {
				_RawImage.color = Color.black;
				_BoolSceneToBlack = false;
			}
		}


		void Update() {
			if(_BoolSceneToClear) {
				//屏幕淡入
				SceneToClear();
			}	
			else if(_BoolSceneToBlack) {
				//屏幕淡出
				SceneToBlack(); 
			}
		}//Update_end
	}//Class_end
}
