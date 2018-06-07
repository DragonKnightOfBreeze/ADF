//控制层，场景异步加载
//学学人家新战神，在神不知鬼不觉的时候偷偷加载场景资源

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;

namespace Control {
	class Ctrl_LoadingScene:BaseControl {

		IEnumerator Start() {
			yield return new WaitForSeconds(0.1f);
			//关卡预处理逻辑
			StartCoroutine("ScenePreProgressing");
			//垃圾收集
			StartCoroutine("HandleGC");

		}

		/// <summary>
		/// 关卡预处理
		/// </summary>
		/// <returns></returns>
		IEnumerator ScenePreProgressing() {
			yield return new WaitForSeconds(0.1f);	

			switch (GlobalParaMgr.NextSceneName) {
				case SceneEnum.StartScene:
					break;
				case SceneEnum.LoginScene:
					break;
				case SceneEnum.Level1:
					StartCoroutine("ScenePreProgressing_Level1");
					break;
				case SceneEnum.Level2:
					//...
					break;
				case SceneEnum.Level3:
					//...
					break;
				case SceneEnum.BaseScene:
				default:
					break;
			}
		}

		/// <summary>
		/// 预处理第一关卡
		/// </summary>
		/// <returns></returns>
		IEnumerator ScenePreProgressing_Level1() {
			yield return new WaitForSeconds(0.1f);

			//参数赋值
			DialogDataAnalysisMgr.GetInstance().SetXMLPathAndRooNodeName(KernelParameter.GetDialogConfigPath(), KernelParameter.GetDialogConfigRootNodeName());
			//等待参数设置完毕（要比DialogDataAnalysisMgr的延迟方法慢）
			yield return new WaitForSeconds(0.7f);      //很重要

			//得到XML中所有的数据
			List<DialogDataFormat> DialogsDataArray = DialogDataAnalysisMgr.GetInstance().GetAllXmlDataArray();
			// 测试给“对话数据管理器”加载数据
			bool boolResult = DialogDataMgr.GetInstance().LoadAllDialogData(DialogsDataArray);
			if (!boolResult) {
				Log.Write(GetType() + "/Start()/对话数据管理器加载数据失败",Log.Level.High);
			}

		}

		//资源垃圾收集
		IEnumerator HandleGC() {
			yield return new WaitForSeconds(0.1f);
			//卸载无用的资源
			Resources.UnloadUnusedAssets();
			//强制垃圾收集
			System.GC.Collect();
		}

	}
}
