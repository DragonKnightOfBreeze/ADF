//［控制层］敌人：骷髅国王的动画脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_SkeletonKing_Ani : Ctrl_BaseEnemy_Ani {

		//飘字预设
		public GameObject goMoveUpLabel;
		//玩家UI
		private GameObject goUIPlayerInfo;


		#region 【重载后的自动方法】

		protected override void Start() {
			base.Start();
			//得到玩家UI
			goUIPlayerInfo = GameObject.FindGameObjectWithTag("UI_PlayerInfo");
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
			base._HeroProperty.SubCurHP(base._MyProperty.ATK);

			//获得伤害
			int damage = System.Convert.ToInt32(_MyProperty.ATK - _HeroProperty.GetDEF());
			//显示飘字特效
			StartCoroutine(LoadPEInPool_MoveUpLabel(0.1f, goMoveUpLabel, goHero.transform.position + transform.TransformDirection(Vector3.zero),Quaternion.identity,goHero,damage,goUIPlayerInfo.transform));

			yield break;
		}

		#endregion
	}
}