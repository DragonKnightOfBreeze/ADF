//控制层，使用“对象缓冲池”技术，做按指定时间回收对象

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {

	public class Ctrl_RecoverObjByTime : BaseControl {

		public float FloRecoverTIme = 1f; //回收时间

		//根据脚本生命周期
		//也可以采用类似方式，控制动画播放同步、AI判断间隔同步、输入同步等同步问题
		private void OnEnable() {
			StartCoroutine(RecoverGameobjectByTime("_ParticleSys"));
		}

		private void OnDisable() {
			StopCoroutine(RecoverGameobjectByTime("_ParticleSys"));
		}


		/// <summary>
		/// 回收对象，根据指定的时间点（保证缓冲池中的对象数量）
		/// </summary>
		/// <param name="goName"></param>
		/// <returns></returns>
		IEnumerator RecoverGameobjectByTime(string goName) {
			yield return new WaitForSeconds(FloRecoverTIme);
			PoolManager.PoolsArray[goName].RecoverGameObject(this.gameObject);
		}
	}
}