//视图层，主城场景，任务面板显示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;
using Control;

namespace View{
	class View_MissionPanel:MonoBehaviour {

		public Text Txt_MissionName_1;	//任务名称
		public Text Txt_MissionDesc_1;	//任务描述

		public Text Txt_MissionName_2;
		public Text Txt_MissionDesc_2;

		//public Text Txt_MissionName_3;
		//public Text Txt_MissionDesc_3;

		//public Text Txt_MissionName_4;
		//public Text Txt_MissionDesc_4;

		//public Text Txt_MissionName_5;
		//public Text Txt_MissionDesc_5;

		private void Start() {
			SetMission_1();
			SetMission_2();
		}



		#region 文本赋值

		/// <summary>
		/// 为第一个任务的文本赋值
		/// 待优化：替换为用XML文件赋值
		/// </summary>
		private void SetMission_1() {
			Txt_MissionName_1.text = "街舞较量";
			Txt_MissionDesc_1.text = "消灭10个骷髅，证明自己比它们更加擅长街舞，是A大的忠实粉丝。";
		}

		private void SetMission_2() {
			Txt_MissionName_2.text = "街舞王之争";
			Txt_MissionDesc_2.text = "猎杀骷髅王，证明自己比它更加擅长街舞，是街舞队的一名正式成员，也是街舞大剑的熟练使用之人；同时，也证明自己比它更理解墓王尼特，是一名合格的死宅。";
		}

		#endregion



		#region 任务控制

		/// <summary>
		/// 任务1：进入第2关卡
		/// </summary>
		public void EnterLevel2() {
			//调用控制层
			//。。。
			Ctrl_MissionPanel.Instance.EnterLevel2();
		}


		#endregion



		/// <summary>
		/// 解锁任务
		/// </summary>
		/// <param name="item"></param>
		public void UnlockMission(GameObject item) {
			item.transform.Find("Btn_Unlock").gameObject.SetActive(true);
			item.transform.Find("Btn_Lock").gameObject.SetActive(false);
		}

	}
}
