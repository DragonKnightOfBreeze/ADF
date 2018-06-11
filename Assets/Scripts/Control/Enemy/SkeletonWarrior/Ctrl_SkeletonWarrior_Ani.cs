//［控制层］敌人：骷髅战士的动画脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_SkeletonWarrior_Ani : Ctrl_BaseEnemy_Ani {



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



		#region 【重载后的其他方法】

		public override IEnumerator AniEvent_Attack() {
			//英雄减少敌人的攻击力值的生命值
			this._HeroProperty.SubCurHP(base._MyProperty.ATK);
			yield break;
		}

		#endregion
	}
}