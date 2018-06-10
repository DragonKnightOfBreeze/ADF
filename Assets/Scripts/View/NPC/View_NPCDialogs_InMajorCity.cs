//视图层：主城NPC对话显示脚本
//激活&点击继续
//注意：开始对话后，角色应该立刻进入等待状态。

//注意：利用触发器触发对话可以只能发生一次（在一般情况下）
//注意：但是在这之后应该可以用其他方式再次触发对话
//注意：在NPC附近按下特定键

//***在NPC附近按下特定键***

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;

namespace View {

	public class View_NPCDialogs_InMajorCity : MonoBehaviour {

		public GameObject goDialogsPanel;   //对话面板
		private Image _Img_BackGround;      //背景对话贴图

		/// <summary>NPC1的图片路径</summary>
		private const string IMG_PATH_1 = "Texture/NPCs/NPCTrue_1.png";
		/// <summary>NPC1的黑白图片路径</summary>
		private const string IMG_PATH_1_BW = "Texture/NPCs/NPCBW_1.png";
		/// <summary>NPC2的黑白图片路径</summary>
		private const string IMG_PATH_2 = "Texture/NPCs/NPCTrue_2.png";
		/// <summary>NPC2的图片路径</summary>
		private const string IMG_PATH_2_BW = "Texture/NPCs/NPCBW_2.png";

		/// <summary>NPC1的对话序号</summary>
		private const int NPC1_SECTION_NUM = 5;
		/// <summary>NPC2的对话序号</summary>
		private const int NPC2_SECTION_NUM = 6;


		//当前“触发对话目标”（区分到底是哪个NPC）
		private CommonTriggerType _CommonTriggerType = CommonTriggerType.None;



		private void Start() {
			//查找背景对话贴图
			_Img_BackGround = this.transform.parent.Find("Background").GetComponent<Image>();

			//注册“触发器，对话系统”（目的是准备对话）
			RigisterTriggerDialogs();
			//注册“背景贴图”（目的是发起对话）
			RigisterBGImg();
			//“背景贴图”一开始是不显示的
			_Img_BackGround.gameObject.SetActive(false);
		}

		#region 【对话准备阶段】

		/// <summary>
		/// 注册“触发器，对话系统”（一个封装方法）
		/// </summary>
		private void RigisterTriggerDialogs() {
			TriggerCommonEvent.eve_CommonTrigger += StartdialogsPrepare;
		}

		/// <summary>
		/// 开始对话准备
		/// </summary>
		/// <param name="ctt"></param>
		private void StartdialogsPrepare(CommonTriggerType ctt) {
			switch (ctt) {
				case CommonTriggerType.None:
					break;
				case CommonTriggerType.NPC1_Dialog:
					ActiveNPC1_Dialog();
					break;
				case CommonTriggerType.NPC2_Dialog:
					ActiveNPC2_Dialog();
					break;
				case CommonTriggerType.NPC3_Dialog:
					break;
				default:
					break;
			}
		}


		/// <summary>
		/// 激活NPC1对话
		/// ***改进：重构（4个参数）***
		/// </summary>
		private void ActiveNPC1_Dialog() {
			//给NPC1，动态加载贴图
			LoadNPC1_Texture();
			//赋值当前状态
			_CommonTriggerType = CommonTriggerType.NPC1_Dialog;
			//禁用ET
			View_PlayerInfoResponse.Instance.HideET();
			//显示对话UI面板
			goDialogsPanel.gameObject.SetActive(true);
			//显示首句对话
			DisplayNextDialog(NPC1_SECTION_NUM);
		}

		/// <summary>
		/// 激活NPC2对话
		/// </summary>
		private void ActiveNPC2_Dialog() {
			//给NPC2，动态加载贴图
			LoadNPC2_Texture();
			//赋值当前状态
			_CommonTriggerType = CommonTriggerType.NPC2_Dialog;
			//禁用ET
			View_PlayerInfoResponse.Instance.HideET();
			//显示对话UI面板
			goDialogsPanel.gameObject.SetActive(true);
			//显示首句对话
			DisplayNextDialog(NPC2_SECTION_NUM);
		}


		/// <summary>
		/// 给NPC1，动态加载贴图
		/// ***改进：重构：两个参数，随引用的方法的值***
		/// </summary>
		private void LoadNPC1_Texture() {
			DialogUIMgr.Instance.Spr_NPC_Right[0] = ResourceMgr.GetInstance().LoadResource<Sprite>(IMG_PATH_1,true);
			DialogUIMgr.Instance.Spr_NPC_Right[1] = ResourceMgr.GetInstance().LoadResource<Sprite>(IMG_PATH_1_BW, true);
		}

		/// <summary>
		/// 给NPC2，动态加载贴图
		/// </summary>
		private void LoadNPC2_Texture() {
			DialogUIMgr.Instance.Spr_NPC_Right[0] = ResourceMgr.GetInstance().LoadResource<Sprite>(IMG_PATH_2, true);
			DialogUIMgr.Instance.Spr_NPC_Right[1] = ResourceMgr.GetInstance().LoadResource<Sprite>(IMG_PATH_2_BW, true);
		}


		#endregion



		#region 【正式对话阶段】

		/// <summary>
		/// 注册“背景贴图”（点击事件）
		/// </summary>
		private void RigisterBGImg() {
			if (_Img_BackGround != null) {
				EventTriggerListener.Get(_Img_BackGround.gameObject).onClick += DisplayDialogByNPC;
			}
		}

		/// <summary>
		/// 显示NPC对话（注册给点击事件）
		/// </summary>
		/// <param name="go"></param>
		private void DisplayDialogByNPC(GameObject go) {
			switch (_CommonTriggerType) {
				case CommonTriggerType.None:
					break;
				case CommonTriggerType.NPC1_Dialog:
					DisplayNextDialog(NPC1_SECTION_NUM);
					break;
				case CommonTriggerType.NPC2_Dialog:
					DisplayNextDialog(NPC2_SECTION_NUM);
					break;
				case CommonTriggerType.NPC3_Dialog:
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 显示下一条对话信息
		/// </summary>
		/// <param name="sectionNum">段落编号</param>
		private void DisplayNextDialog(int sectionNum) {
			bool boolResult =  DialogUIMgr.Instance.DisplayNextDialog(DialogType.Double, sectionNum);
			if (boolResult) {
				//对话结束，关闭对话面板
				goDialogsPanel.gameObject.SetActive(false);
				//启用ET
				View_PlayerInfoResponse.Instance.DisplayET();
			}
		}

		#endregion
	}

}