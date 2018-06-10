//控制层，玩家UI界面响应
//包括的一些功能：

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {
	public class Ctrl_PlayerUIResponse:BaseControl {
		public static Ctrl_PlayerUIResponse Instance;	//本类的单例公共实例

		private void Awake() {
			Instance = this;
		}

		/// <summary>
		/// 退出游戏
		/// </summary>
		public void ExitGame() {
			StartCoroutine(HandleSavingGame());
		}

		/// <summary>
		/// 处理退出游戏前的必要操作
		/// </summary>
		/// <returns></returns>
		IEnumerator HandleSavingGame() {
			bool boolResult = SaveAndLoading.GetInstance().SaveGameProcess();
			yield return new WaitForSeconds(1f);
			//yield return boolResult;
			Application.Quit();
		}

	}
}
