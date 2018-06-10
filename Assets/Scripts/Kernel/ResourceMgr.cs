//核心层，资源动态加载管理器
//开发目的：开发出具有“对象缓冲”功能的资源加载脚本

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Global;
using System;

namespace Kernel {

	public class ResourceMgr : MonoBehaviour {
		private static ResourceMgr _Instance = null;   //本脚本私有单例实例

		/// <summary>
		/// //容器键值对的集合（不是泛型集合）
		/// 键：路径path		值：资源TResource
		/// </summary>
		private Hashtable ht = null;		


		private ResourceMgr() {
			ht = new Hashtable();
		}

		/// <summary>
		/// 得到本类的实例（单例）
		/// </summary>
		/// <returns></returns>
		public static ResourceMgr GetInstance() {
			if (_Instance == null) {
				_Instance = new GameObject("_ResourceMgr").AddComponent<ResourceMgr>();	
			}
			return _Instance;
		}

		/// <summary>
		/// 调用资源（带对象缓冲技术）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path">路径</param>
		/// <param name="isCatch"></param>
		/// <returns></returns>
		public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object {

			//如果路径对应的值在哈希表中，就从哈希表汇中返回，并确定类型
			if (ht.Contains(path)) {	
				return ht[path] as T;	
			}

			//否则从总资源中返回
			T TResource = Resources.Load<T>(path);
			if(TResource ==null) {
				// // Debug.LogWarning(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查");
			}
			else if(isCatch) {
				ht.Add(path, TResource);
			}
			return TResource;
		}

		/// <summary>
		/// 克隆资源（带对象缓冲技术）
		/// </summary>
		/// <param name="path"></param>
		/// <param name="isCatch"></param>
		/// <returns>克隆的游戏物体</returns>
		public GameObject LoadAsset(string path,bool isCatch) {

			GameObject goObj = LoadResource<GameObject>(path,isCatch);	//动态加载游戏资源
			GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);  //克隆

			if(goObjClone == null) {
				// // Debug.LogWarning(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path = " + path);
			}
			return goObjClone;
		}
	}
}