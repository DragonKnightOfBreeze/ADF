//测试类，测试对话系统

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

namespace Test {
	class TestDialogInfo: MonoBehaviour {

		public Text Txt_Side;
		public Text Txt_Person;
		public Text Txt_Content;


		/// <summary>
		/// 得到并展示下一条对话信息
		/// </summary>
		public void DisplayNextDialogInfo() {

			DialogSide diaSide = DialogSide.None;
			string strDialogPerson;
			string strDialogContent;

			bool boolResult = DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(1,out diaSide,out strDialogPerson,out strDialogContent);
			if (boolResult) {
				switch (diaSide) {
					case DialogSide.None:
						break;
					case DialogSide.HeroSide:
						Txt_Side.text = "英雄";
						break;
					case DialogSide.NPCSide:
						Txt_Side.text = "NPC";
						break;

					default:
						break;
				}
				Txt_Person.text = strDialogPerson;
				Txt_Content.text = strDialogContent;

			}
			else {
				Txt_Content.text = "没有输出数据了";
			}
			Log.SyncLogArrayToFile();

		}

	}
}
