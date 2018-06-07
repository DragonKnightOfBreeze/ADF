//核心层，手工遮挡剔除脚本
//“模块加载”
//优化游戏性能

using System;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Kernel {
	class TriggerDisplayAndHide:MonoBehaviour {
		//标签：英雄
		public string TagNameByHero = "Player";
		//标签：需要显示的游戏对象
		public string TagNameByDisplayObject = "TagNameDisplayName";
		//标签：需要隐藏的游戏对象
		public string TagNameByHideObject = "TagNameHideName";  

		private GameObject[] GoDisplayObjectArray;	//需要显示的游戏对象
		private GameObject[] GoHideObjectArray;     //需要隐藏的游戏对象

		private void Start() {
			//得到需要显示的游戏对象
			GoDisplayObjectArray = GameObject.FindGameObjectsWithTag(TagNameByDisplayObject);
			//得到需要隐藏的游戏对象
			GoHideObjectArray = GameObject.FindGameObjectsWithTag(TagNameByHideObject);
		}

		/// <summary>
		/// 进入触发检测
		/// </summary>
		/// <param name="coll"></param>
		private void OnTriggerEnter(Collider coll) {
			//发现英雄（如果发现，就显示需要显示的游戏对象）
			if(coll.gameObject.tag == TagNameByHero) {
				foreach (GameObject goItem in GoDisplayObjectArray) {
					goItem.SetActive(true);
				}
			}
		}

		/// <summary>
		/// 离开触发检测
		/// </summary>
		/// <param name="coll"></param>
		private void OnTriggerExit(Collider coll) {
			//发现英雄
			if (coll.gameObject.tag == TagNameByHero) {
				foreach (GameObject goItem in GoHideObjectArray) {
					goItem.SetActive(false);
				}
			}
		}

	}
}
