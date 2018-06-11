  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_SkeletonArcher_Ani : Ctrl_BaseEnemy_Ani {

		public GameObject goArrow;      //弓箭预设（确定位置）

		public Ctrl_SkeletonArcher_Prop _MyProperty_S;	//特有属性

		#region 【重载后的自动方法】

		protected override void Start() {
			_MyProperty_S = gameObject.GetComponent<Ctrl_SkeletonArcher_Prop>();
			base.Start();
		}

		protected override void OnEnable() {
			base.OnEnable();
		}

		protected override void OnDisable() {
			base.OnDisable();
		}

		#endregion



		#region 【重载后的其他方法】

		/// <summary>
		/// 使用弓箭进行攻击
		/// </summary>
		/// <returns></returns>
		public IEnumerator AniEvent_FarAttack() {
			

			//注意：直接挂载到弓箭位置上，不然会出现跟踪的情况
			//箭和敌人是同级对象，只是一个妥协的方式
			StartCoroutine(LoadParticalEffect("Prefabs/Prop/Arrow", goArrow.transform, goArrow.transform.position,goArrow.transform.rotation, 20));
			yield break;
		}

		#endregion

	}
}