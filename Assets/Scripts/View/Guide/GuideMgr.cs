//视图层，新手引导模块，新手引导管理器
//作用：
//控制与协调所有的具体新手引导业务脚本的检查与执行

//使用GOF设计模式中的“责任链”模式
//使多个对象都有机会处理请求，从而避免请求的发送者和接受者的耦合关系，将这个对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止。

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace View {
	public class GuideMgr:MonoBehaviour {
		//所有新手引导业务逻辑脚本的集合。
		private List<IGuideTrigger> _LiGuideTrigger = new List<IGuideTrigger>();	  
		
		IEnumerator Start() {
			yield return new WaitForSeconds(GlobalParameter.WAIT_FOR_SECONDS_ON_START);

			//加入所有的“业务逻辑”脚本
			//面向接口编程，用接口来实现动态多态性
			//接口的使用都是统一的，可以使用List而不必要使用ArrayList
			IGuideTrigger iTri_1 = TriggerDialogs.Instance;
			IGuideTrigger iTri_2 = TriggerOperET.Instance;
			IGuideTrigger iTri_3 = TriggerOperVitualKey.Instance;
			_LiGuideTrigger.Add(iTri_1);
			_LiGuideTrigger.Add(iTri_2);
			_LiGuideTrigger.Add(iTri_3);

			//启动业务逻辑脚本的检查
			StartCoroutine(CheckGuideState());
		}

		/// <summary>
		/// 检查引导状态
		/// </summary>
		IEnumerator CheckGuideState() {
			Log.Write(GetType() + "/CheckGuideState");
			yield return new WaitForSeconds(0.2f);

			while (true) {
				yield return new WaitForSeconds(0.5f);
				for (int i = 0; i < _LiGuideTrigger.Count; i++) {
					IGuideTrigger iTrigger = _LiGuideTrigger[i];
					//检查每个业务脚本是否可以运行
					if(iTrigger.CheckCondition()) {
						//每个业务脚本，执行业务逻辑
						if (iTrigger.RunOperation()) {
							Log.Write(GetType() + "/CheckGuideState()/编号为："+i+"业务逻辑执行完毕，即将在集合中移除");
							_LiGuideTrigger.Remove(iTrigger);
						}
					}
				}
			}
		}










	}
}
