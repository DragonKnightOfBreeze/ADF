//视图层，玩家主城信息响应
//作用
//主城场景中，关于玩家各种面板的显示与隐藏处理

//模态窗口：最多只能同时打开一个按钮，打开一个窗口时，只能操作当前窗口

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;

namespace View {
	public class View_PlayerInfoResponse_InMajorCity:MonoBehaviour {

		public GameObject goSkillPanel;	//技能面板
		public GameObject goMissionPanel;	//任务面板
		public GameObject goMarketPanel;	//商城面板
		public GameObject goPackagePanel;	//背包面板

		/// <summary>
		/// 显示英雄的角色
		/// </summary>
		public void DisplayRole() {
			//调用其他脚本
		}

		/// <summary>
		/// 隐藏英雄的角色
		/// </summary>
		public void HideRole() {
			//调用其他脚本
		}

		/// <summary>
		/// 显示技能面板
		/// </summary>
		public void DisplaySkillPanel() {
			goSkillPanel.SetActive(true);
		}

		/// <summary>
		/// 隐藏技能面板
		/// </summary>
		public void HideSkillPanel() {
			goSkillPanel.SetActive(false);

		}

		/// <summary>
		/// 显示任务面板
		/// </summary>
		public void DisplayMissionPanel() {
			goMissionPanel.SetActive(true);

		}

		/// <summary>
		/// 隐藏任务面板
		/// </summary>
		public void HideMissionPanel() {
			goMissionPanel.SetActive(false);
		}

		/// <summary>
		/// 显示商城面板
		/// </summary>
		public void DisplayMarketPanel() {
			goMarketPanel.SetActive(false);
		}

		/// <summary>
		/// 隐藏商城面板
		/// </summary>
		public void HideMarketPanel() {
			goMarketPanel.SetActive(false);
		}

		/// <summary>
		/// 显示背包面板
		/// </summary>
		public void DisplayPackagePanel() {
			goPackagePanel.SetActive(true);
		}

		/// <summary>
		/// 隐藏背包面板
		/// </summary>
		public void HidePackagePanel() {
			goPackagePanel.SetActive(false);
		}
	}
}
