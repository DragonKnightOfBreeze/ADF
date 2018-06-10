//控制层，关卡二控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {
	public class Ctrl_Level2_Scene:MonoBehaviour {
		//背景音乐与音效处理
		public AudioClip Auc_Background;

		//标签：需要隐藏的对象
		public string[] TagNameByHideObject;

		void Start() {
			//播放背景音乐
			AudioManager.PlayBackground(Auc_Background);
			//默认因此场景中非活动的区域
			StartCoroutine("HideUnactiveArea");
		}


		/// <summary>
		/// 场景优化管理：默认隐藏非活动的区域
		/// </summary>
		/// <returns></returns>
		IEnumerator HideUnactiveArea() {
			yield return new WaitForEndOfFrame();

			foreach (string  tagName in TagNameByHideObject) {
				//得到需要隐藏的游戏对象
				GameObject[] goHideObjArray = GameObject.FindGameObjectsWithTag(tagName);
				foreach (GameObject item in goHideObjArray) {
					item.SetActive(false);
				}

			}
		}

		




	}
}
