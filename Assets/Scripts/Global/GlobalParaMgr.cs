//公共层，全局参数管理器
//作用：跨场景全局数值传递

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {

	public static class GlobalParaMgr {
		//下一个场景的名称
		public static SceneEnum NextSceneName = SceneEnum.LoginScene;
		//玩家的姓名
		public static string PlayerName = "";
		public static PlayerType playerType = PlayerType.Sworder;
	}
}