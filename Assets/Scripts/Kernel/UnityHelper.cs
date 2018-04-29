//核心层，帮助类
//作用：集成大量通用算法
//指定为单例类

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kernel {
	public class UnityHelper {
		private static UnityHelper _Instance;	//本类实例
		private float floDeltaTime = 0;     //累加时间

		private UnityHelper() {}

		/// <summary>
		/// 得到本类实例（单例）
		/// </summary>
		/// <returns></returns>
		public static UnityHelper GetInstance() {
			if(_Instance ==null) {
				_Instance = new UnityHelper();
			}
			return _Instance;
		}

		/// <summary>
		/// 间隔指定时间段，返回bool值
		/// 我觉得不需要
		/// </summary>
		/// <param name="smallIntervalTime">指定的时间段间隔（0.1-3f秒之间）</param>
		/// <returns>true：表明指定的时间段到了</returns>
		public bool GetSmallTime(float smallIntervalTime) {
			floDeltaTime += Time.deltaTime;
			//到了指定时间就归零，并且返回true 
			if (floDeltaTime >= smallIntervalTime) {
				floDeltaTime = 0;
				return true;
			} else {
				return false;
			}
		}
		


	}
}