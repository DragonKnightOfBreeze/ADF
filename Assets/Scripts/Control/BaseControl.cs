//控制层，基本控制脚本，其他控制脚本的父类

using UnityEngine;
using System.Collections;
using Global;
using Kernel;

namespace Control {
	public class BaseControl : MonoBehaviour {

		/// <summary>
		/// 进入下一个场景
		/// </summary>
		/// <param name="sceneEnumName">场景名称（枚举）</param>
		protected void EnterNextScene(SceneEnum sceneEnumName) {
			GlobalParaMgr.NextSceneName = sceneEnumName; //转到下一个场景
			Application.LoadLevel(ConvertEnumToStr.GetInstance().GetStrByEnumScene(SceneEnum.Level1));
		}
	}
}
