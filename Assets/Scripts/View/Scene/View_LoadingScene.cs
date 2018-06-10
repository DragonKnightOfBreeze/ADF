//视图层，场景异步加载控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;

namespace View {
	public class View_LoadingScene : MonoBehaviour {

		public Slider SliLoadingSlider;        //进度条控件
		private float _FloProgressNumber = 0;   //进度数值
		private AsyncOperation _AsyOper;

		IEnumerator Start() {
			
			#region 测试内容（Log系统）

			/* 启用Log日志系统 */

			//面向接口编程
			IConfigManager configMgr = new ConfigManager(KernelParameter.GetLogPath(),KernelParameter.GetLogRootNodeName());

			//string strLogPath = configMgr.AppSetting["LogPath"];
			//string strLogState = configMgr.AppSetting["LogState"];
			//string strLogMaxCapacity = configMgr.AppSetting["LogMaxCapacity"];
			//string strLogCacheNumber = configMgr.AppSetting["LogCacheNumber"];
			//print("LogPath：" + strLogPath);
			//print("LogState：" + strLogState);
			//print("LogMaxCapacity：" + strLogMaxCapacity);
			//print("LogCacheNumber：" + strLogCacheNumber);

			//测试Log.cs类（让构造函数运行起来）
			//Log.Write("我的企业日志系统开始运行了，第一次测试");

			#endregion

			#region 测试内容（XML解析）

			///* 测试XML解析程序 */
			////或许可以封装为一个方法，提供参数：对话XML文件地址，根节点名
			////输出参数：对话数据格式类列表

			//参数赋值
			DialogDataAnalysisMgr.GetInstance().SetXMLPathAndRooNodeName(KernelParameter.GetDialogConfigPath(), KernelParameter.GetDialogConfigRootNodeName());
			//等待参数设置完毕（要比DialogDataAnalysisMgr的延迟方法慢）
			yield return new WaitForSeconds(0.5f);      //很重要
			//得到XML中所有的数据
			List<DialogDataFormat> DialogsDataArray = DialogDataAnalysisMgr.GetInstance().GetAllXmlDataArray();

			//foreach (DialogDataFormat data in DialogsDataArray) {
			//	Log.Write("");      //空一行
			//	Log.Write("SectionNum: " + data.DiaSectionNum);
			//	Log.Write("SectionName: " + data.DiaSectionName);
			//	Log.Write("Index: " + data.DiaIndex);
			//	Log.Write("Side: " + data.DiaSide);
			//	Log.Write("Person: " + data.DiaPerson);
			//	Log.Write("Content:" + data.DiaContent);
			//}
			//Log.SyncLogArrayToFile();

			// 测试给“对话数据管理器”加载数据
			bool boolResult = DialogDataMgr.GetInstance().LoadAllDialogData(DialogsDataArray);
			if (!boolResult) {
				Log.Write(GetType() + "/Start()/对话数据管理器加载数据失败");
			}
			//###调试进入指定关卡（在这里改变调试关卡）###
			GlobalParaMgr.NextSceneName = SceneEnum.MajorCity;

			#endregion

			yield return new WaitForSeconds(1f);
			StartCoroutine("LoadingSceneProgress");
		}


		/// <summary>
		/// 显示进度条（正在加载场景的时候有停顿效果）
		/// </summary>
		private void Update() {
			if (_FloProgressNumber <= 1) {
				_FloProgressNumber += 0.01F;
			}
			SliLoadingSlider.value = _FloProgressNumber;   //改变进度条长度
		}


		/// <summary>
		/// 异步加载协程（在进入Loading场景后就自动被调用的）
		/// </summary>
		/// <returns></returns>
		IEnumerator LoadingSceneProgress() {
			//已过时，使用SceneManager.LoadingSceneAsync
			_AsyOper = Application.LoadLevelAsync(ConvertEnumToStr.GetInstance().GetStrByEnumScene(GlobalParaMgr.NextSceneName));	  //固定代码，只要换场景就可以用
			//_FloProgressNumber = _AsyOper.progress;
			yield return _AsyOper;
		}

	}
}
