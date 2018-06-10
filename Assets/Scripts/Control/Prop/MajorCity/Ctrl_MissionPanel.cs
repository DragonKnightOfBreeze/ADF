//控制层：主城UI界面，任务系统的功能实现
//对于具体的任务

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;

namespace Control {
	class Ctrl_MissionPanel:BaseControl {
		public static Ctrl_MissionPanel Instance;	//本脚本的单例实例

		private void Awake() {
			Instance = this;
		}

		/// <summary>
		/// 进入第二关卡
		/// </summary>
		public void EnterLevel2() {
			//***待优化：在这之前首先进入Loading场景***
			EnterNextScene(SceneEnum.Level2);
		}

	}
}
