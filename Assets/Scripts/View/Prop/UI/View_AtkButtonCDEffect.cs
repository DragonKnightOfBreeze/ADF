//视图层，UI 攻击虚拟按键，CD冷却特效

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Control;
using Global;

namespace View {
	public class View_AtkButtonCDEffect : MonoBehaviour {

		public Text Txt_CDTimeNum;			//技能冷却时间显示
		private float FloCDTime = 5f;    //技能冷却时间

		public Image Img_Circle;    //按钮的圆形边框
		public Image Img_WnB;       //黑白图片
		public GameObject goWnBImage;   //黑白图片对应游戏物体
		
		
		
		public KeyCode keyCode = KeyCode.Alpha0;		//对应键盘按键

		private float _FloTimeDelta = 0f;
		private bool _IsStartTimer = false;
		private Button Btn_Self;        //本脚本对应的按钮

		private bool _Enable = false;

		private void Start() {
			Btn_Self = gameObject.GetComponent<Button>();
			EnableSelf();
			Txt_CDTimeNum.enabled = false;
		}

		private void Update() {

			////是否启用本控件
			if (_Enable) {

				//支持键盘输入
				if (Input.GetKeyDown(keyCode)) {
					ResponseBtnClick();
				}

				//只有在点击按键后才触发，接着便会重新锁定
				if (_IsStartTimer) {

					goWnBImage.SetActive(true);
					_FloTimeDelta += Time.deltaTime;

					Txt_CDTimeNum.text = Mathf.RoundToInt(FloCDTime - _FloTimeDelta).ToString();	  //更新冷却时间，控件倒计时显示

					// // Debug.Log("更新");
				Img_Circle.fillAmount = _FloTimeDelta / FloCDTime;
					Btn_Self.interactable = false;

					//如果累加时间大于CD时间，就让累加时间置零，然后显现特效
					if (_FloTimeDelta >= FloCDTime) {
						Txt_CDTimeNum.enabled = false;	//超出冷却时间，就不显示了
						_IsStartTimer = false;
						Img_Circle.fillAmount = 1;
						_FloTimeDelta = 0f;

						goWnBImage.SetActive(false);    //不显示黑白图片
						Btn_Self.interactable = true;   //启用按钮
					}	
			}
			}
		}
			

		/// <summary>
		/// 响应用户点击，设置是否启用技能冷却效果
		/// </summary>
		public void ResponseBtnClick() {
			_IsStartTimer = true;
			Txt_CDTimeNum.enabled = true;	//显示冷却时间数字
		}

		/// <summary>
		/// 启用本控件
		/// </summary>
		public void EnableSelf() {
			_Enable = true;				
			goWnBImage.SetActive(false);	//不显示黑白图片
			Btn_Self.interactable = true;	//启用按钮
		}

		/// <summary>
		/// 禁用本控件
		/// </summary>
		public void DisableSelf() {
			_Enable = false;
			goWnBImage.SetActive(true);
			Btn_Self.interactable = false;
		}

	}
}
