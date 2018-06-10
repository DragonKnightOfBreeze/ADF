//视图层，玩家主城信息响应
//作用
//主城场景中，关于玩家各种面板的显示与隐藏处理
//模态窗口：最多只能同时打开一个按钮，打开一个窗口时，只能操作当前窗口

//***优化：点击一个按钮，可以打开对应面板；再点击又可以关闭***

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;

namespace View {
	public class View_PlayerInfoResponse_InMajorCity:MonoBehaviour {

		public GameObject goSkillPanel;		//技能面板
		public GameObject goMissionPanel;	//任务面板
		public GameObject goMarketPanel;	//商城面板
		public GameObject goPackagePanel;   //背包面板

		public GameObject goET;             //ET

		#region 具体的显示与隐藏面板的方法

		/// <summary>
		/// 显示和隐藏英雄的角色面板
		/// </summary>
		public void DisplayOrHideRolePanel() {
			//调用其他脚本（注意相应的UI摄像机的深度）
			View_PlayerInfoResponse.Instance.DisplayOrHidePlayerDetailInfoPanel();
		}

		/// <summary>
		/// 显示技能面板
		/// </summary>
		public void DisplaySkillPanel() {
			DisplayPanel(goSkillPanel);
		}

		/// <summary>
		/// 隐藏技能面板
		/// </summary>
		public void HideSkillPanel() {
			HidePanel(goSkillPanel);
		}

		/// <summary>
		/// 显示任务面板
		/// </summary>
		public void DisplayMissionPanel() {
			DisplayPanel(goMissionPanel);
		}

		/// <summary>
		/// 隐藏任务面板
		/// </summary>
		public void HideMissionPanel() {
			HidePanel(goMissionPanel);
		}

		/// <summary>
		/// 显示商城面板
		/// </summary>
		public void DisplayMarketPanel() {
			DisplayPanel(goMarketPanel);
		}

		/// <summary>
		/// 隐藏商城面板
		/// </summary>
		public void HideMarketPanel() {
			HidePanel(goMarketPanel);
		}

		/// <summary>
		/// 显示背包面板
		/// </summary>
		public void DisplayPackagePanel() {
			DisplayPanel(goPackagePanel);
		}

		/// <summary>
		/// 隐藏背包面板
		/// </summary>
		public void HidePackagePanel() {
			HidePanel(goPackagePanel);
		}

		#endregion



		#region 通用的显示与隐藏面板的方法

		/// <summary>
		/// 显示面板的通用方法
		/// </summary>
		public void DisplayPanel(GameObject goPanel) {
			//预处理
			if (goPanel != null) {
				BeforeOpenWindow(goPanel);
			}
			//显示相应的面板
			goPanel.SetActive(true);

			////预处理
			//if (goPanel != null) {
			//	if (!goPanel.activeSelf) {
			//		BeforeOpenWindow(goPanel);
			//	}
			//	else {
			//		BeforeCloseWindow();
			//	}
			//}
			////显示相应的面板（如果已经打开，则要关闭）
			//goPanel.SetActive(!goPanel.activeSelf);
		}

		/// <summary>
		/// 隐藏面板的通用方法
		/// </summary>
		public void HidePanel(GameObject goPanel) {
			//预处理
			if (goPanel != null) {
				BeforeCloseWindow();
			}
			//显示相应的面板
			goPanel.SetActive(false);
		}

		#endregion



		#region 预处理方法

		/// <summary>
		/// 打开窗体之前的预处理
		/// </summary>
		/// <param name="goDisplayPanel"></param>
		private void BeforeOpenWindow(GameObject goDisplayPanel) {
			//禁用ET
			goET.SetActive(false);
			//窗体的模态化处理
			gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goDisplayPanel);
		}

		/// <summary>
		/// 关闭窗体之前的预处理
		/// </summary>
		private void BeforeCloseWindow() {
			//开启ET
			goET.SetActive(true);
			//取消窗体的模态化
			gameObject.GetComponent<UIMaskMgr>().CancelMaskWindow();
		}

		#endregion

	}
}
