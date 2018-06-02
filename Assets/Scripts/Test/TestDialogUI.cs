//测试：测试对话系统UI

using System;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;


namespace Test {
	class TestDialogUI:MonoBehaviour {

		private void Start() {
			DialogUIMgr.Instance.DisplayNextDialog(DialogType.Double, 1);
		}

		public void DisplayNextDialogInfo() {
			bool boolResult = DialogUIMgr.Instance.DisplayNextDialog(DialogType.Double, 1);
			if (!boolResult) {
				Log.Write(GetType() + "DisplayNextDialogInfo()/对话结束");

			}
			Log.SyncLogArrayToFile();
		}


	}
}
