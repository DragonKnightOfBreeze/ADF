//公共层，枚举转换成字符串


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {
	public class ConvertEnumToStr {
		//使用单例模式
		private static ConvertEnumToStr _Instance;	//本类实例
		//枚举场景类型集合
		private Dictionary<SceneEnum, string> _DicSceneEnumLib;


		/// <summary>
		/// 构造函数
		/// </summary>
		private ConvertEnumToStr() {
			_DicSceneEnumLib = new Dictionary<SceneEnum, string> {
				{ SceneEnum.StartScene, "1_StartScene" },
				{ SceneEnum.LoginScene, "2_LoginScene" },
				{ SceneEnum.LoadingScene, "LoadingScene" },
				{SceneEnum.Level1,"3_Level1" },
				{SceneEnum.Level2,"4_Level2" },
				{SceneEnum.Level3,"5_Level3" },
				{SceneEnum.MajorCity,"MajorCity" },
				{SceneEnum.TestScene,"102_TestDialogScene" }
			};
		}

		/// <summary>
		/// 得到实例（单例模式）（通过这个方法来调用）
		/// </summary>
		/// <returns></returns>
		public static ConvertEnumToStr GetInstance() {
			if (_Instance == null) {
				_Instance = new ConvertEnumToStr();
			}
			return _Instance;
		}
		
		/// <summary>
		/// 得到字符串形式的场景名称
		/// </summary>
		/// <param name="sceneEnum">枚举类型的场景名称</param>
		/// <returns></returns>
		public string GetStrByEnumScene(SceneEnum sceneEnum) {
			if (_DicSceneEnumLib != null && _DicSceneEnumLib.Count >=1) {
				return _DicSceneEnumLib[sceneEnum];
			}
			else {
				// // Debug.LogWarning(GetType() + "/GetStrByEnumScene()/_DicSceneEnumLib.Count <= 0!");
				return null;
			}
		}

	}
}