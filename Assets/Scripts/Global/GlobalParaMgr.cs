//公共层，全局参数管理器
//作用：
//跨场景全局数值传递
//设置默认值

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {

	public static class GlobalParaMgr {
		//下一个场景的名称
		public static SceneEnum NextSceneName = SceneEnum.LoginScene;
		//玩家的姓名，默认为“亚瑟”
		public static string PlayerName = "亚瑟";
		//玩家的职业，默认为剑士
		public static PlayerType CurPlayerType = PlayerType.Sworder;
		//游戏进行情况
		public static GameStatus CurGameStatus = GameStatus.NewGame;
	}
}