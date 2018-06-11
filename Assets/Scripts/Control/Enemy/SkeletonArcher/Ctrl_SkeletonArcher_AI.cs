//［控制层］敌人：所有敌人的AI系统的父类

//特点：远程

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_SkeletonArcher_AI : Ctrl_BaseEnemy_AI {



		#region 【重载后的自动方法】

		protected override void Start() {
			base.Start();
		}

		protected override void OnEnable() {
			base.OnEnable();
		}

		protected override void OnDisable() {
			base.OnDisable();
		}

		#endregion

	}
}