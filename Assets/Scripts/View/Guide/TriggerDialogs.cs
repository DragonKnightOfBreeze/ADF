//视图层，新手引导模块，触发对话引导 

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;
using Control;


namespace View {
	public class TriggerDialogs:MonoBehaviour,IGuideTrigger {

		/// <summary>
		/// 对话状态
		/// </summary>
		public enum DialogStateStep {		
			None,
			Step1_DoublePersonDialog,		//双人对话
			Step2_AliceSpeakET,				//介绍ET
			Step3_AliceSpeakVirtualKey,		//介绍虚拟按键
			Step4_AliceEnd					//结束语
		}


		public static TriggerDialogs Instance;      //本类实例
		public GameObject GoBackground;     //背景游戏对象

		private bool _IsExistNextDIalogsRecorder = false;       //是否存在下一条对话记录
		private Image _Img_BGDIalogs;       //背景对话贴图
		private DialogStateStep _DialogState = DialogStateStep.None;	//当前对话状态

		void Awake() {
			Instance = this;		//得到本类实例
		}

		private void Start() {
			Log.Write(GetType() + "/Start()/");
			//当前状态
			_DialogState = DialogStateStep.Step1_DoublePersonDialog;
			//得到背景贴图
			_Img_BGDIalogs = transform.parent.Find("BG").GetComponent<Image>();
			//注册“背景贴图”
			RegisterDialogs();

			//显示第一句话
			DialogUIMgr.Instance.DisplayNextDialog(DialogType.Double, 1);
		}

		/// <summary>
		/// 注册“背景贴图”
		/// 点击背景图片，就会显示下一条对话记录（直接的话只是改变布尔值）
		/// </summary>
		public void RegisterDialogs() {
			if (_Img_BGDIalogs != null) {
				EventTriggerListener.Get(_Img_BGDIalogs.gameObject).onClick += DisplayNextDialogRecord;
			}
		}

		/// <summary>
		/// 取消注册“背景贴图”
		/// </summary>
		public void UnRegisterDialogs() {
			if (_Img_BGDIalogs != null) {
				EventTriggerListener.Get(_Img_BGDIalogs.gameObject).onClick -= DisplayNextDialogRecord;
			}
		}


		/// <summary>
		/// 显示下一条对话记录
		/// </summary>
		/// <param name="go">注册的游戏对象</param>
		private void DisplayNextDialogRecord(GameObject go) {
			if(go == _Img_BGDIalogs.gameObject) {
				_IsExistNextDIalogsRecorder = true;
			}
		}


		/// <summary>
		/// 检查触发条件
		/// </summary>
		/// <returns>
		/// true: 表示条件成立，触发后续业务逻辑
		/// </returns>
		public bool CheckCondition() {
			Log.Write(GetType() + "/CheckCondition()");
			//如果存在下一条对话记录
			if(_IsExistNextDIalogsRecorder) {
				return true;
			}
			else {
				return false;
			}
		}


		/// <summary>
		/// 运行业务逻辑
		/// </summary>
		/// <returns>
		/// true: 表示业务逻辑执行完毕
		/// </returns>
		public bool RunOperation() {
			Log.Write(GetType() + "/RunOperation()");
			bool boolIsEnd = false;     //本方法是否结束标识位
			bool boolCurrentDialogResult = false;	//当前方法是否结束标识位
			_IsExistNextDIalogsRecorder = false;

			//业务逻辑判断
			switch (_DialogState) {
				case DialogStateStep.None:
					break;

				case DialogStateStep.Step1_DoublePersonDialog:
					//除非结束，否则一直返回false
					boolCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.Double, 1);
					break;

				case DialogStateStep.Step2_AliceSpeakET:
					Log.Write(GetType() + "/RunOperation()/###开始介绍EasyTouch###");
					boolCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.Single, 2);
					break;

				case DialogStateStep.Step3_AliceSpeakVirtualKey:
					Log.Write(GetType() + "/RunOperation()/###开始介绍虚拟按键###");
					boolCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.Single, 3);
					break;

				case DialogStateStep.Step4_AliceEnd:
					boolCurrentDialogResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.Single, 4);
					break;

				default:
					break;
			}

			//当前对话结束处理
			if (boolCurrentDialogResult) {
				switch (_DialogState) {
					case DialogStateStep.None:
						break;

					case DialogStateStep.Step1_DoublePersonDialog:
						break;

					case DialogStateStep.Step2_AliceSpeakET:
						//###有问题：不显示最后一段对话，或者只显示瞬间###
						
						//介绍ET完毕，进行后台处理
						//显示引导ET贴图，控制权暂时转移到TriggerOperET脚本
						TriggerOperET.Instance.DisplayGuideET();
						//暂停会话（反注册）
						UnRegisterDialogs();
						break;

					case DialogStateStep.Step3_AliceSpeakVirtualKey:
						//介绍虚拟按键完毕，进行后台处理
						//显示引导虚拟按键贴图，控制权暂时转移到TriggerOperVK脚本
						TriggerOperVitualKey.Instance.DisplayGuideVK();
						//暂停会话（反注册）
						UnRegisterDialogs();
						break;

					case DialogStateStep.Step4_AliceEnd:
						//全部介绍完毕
						//显示ET
						View_PlayerInfoResponse.Instance.DisplayET();
						//显示所有的UI
						View_PlayerInfoResponse.Instance.DisplayAllVK();
						View_PlayerInfoResponse.Instance.DisplayTopLeftPanel();
						View_PlayerInfoResponse.Instance.DisplayTopRightPanel();
						//隐藏本对话界面
						GoBackground.SetActive(false);

						//允许生成敌人
						GameObject.Find("_GameMgr/_ScenesCtrl").GetComponent<View_Level1>().enabled = true;
						GameObject.Find("_GameMgr/_ScenesCtrl").GetComponent<Ctrl_Level1_Scene>().enabled = true;
						boolIsEnd = true;
						break;

					default:
						break;
				}

				//进入下一个状态
				EnterNextState();
			}

			return boolIsEnd;

		}

		/// <summary>
		/// 进入下一个状态
		/// </summary>
		private void EnterNextState() {
			switch (_DialogState) {
				case DialogStateStep.None:
					break;
				case DialogStateStep.Step1_DoublePersonDialog:
					_DialogState = DialogStateStep.Step2_AliceSpeakET;
					//+++自动跳转，不必连按两次才会真正进入下一个+++
					DisplayNextDialogRecord(_Img_BGDIalogs.gameObject);
					break;
				case DialogStateStep.Step2_AliceSpeakET:
					_DialogState = DialogStateStep.Step3_AliceSpeakVirtualKey;
					break;
				case DialogStateStep.Step3_AliceSpeakVirtualKey:
					_DialogState = DialogStateStep.Step4_AliceEnd;
					break;
				case DialogStateStep.Step4_AliceEnd:
					_DialogState = DialogStateStep.None;
					break;
				default:
					break;
			}
		}
	}
}
