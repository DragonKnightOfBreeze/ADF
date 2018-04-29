//视图层，开始场景

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;	//视图层调用控制层

namespace View {
	public class View_StartScene : MonoBehaviour {

		/// <summary>
		/// 新的游戏
		/// </summary>
		public void ClickNewGame() {
			//得到这个类所在的全路径
			print(GetType() + "/ClickNewGame()");
			//调用控制层的开始场景方法，进入“新的旅程”
			Ctrl_StartScene.Instance.ClickNewGame();	
		}

		/// <summary>
		/// 继续游戏
		/// </summary>
		public void ClickGameContinue() {
			print(GetType() + "/ClickGameContinue()");
			//调用控制层的开始场景方法，进入“继续旅程”
			Ctrl_StartScene.Instance.ClickGameContinue();
		}
	}
}
