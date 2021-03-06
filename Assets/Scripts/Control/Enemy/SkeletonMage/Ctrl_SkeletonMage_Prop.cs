﻿//［控制层］骷髅法师的属性

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_SkeletonMage_Prop : Ctrl_BaseEnemy_Prop {

		/* 在这里修改敌人的真实属性 */

		public int IntMaxHP = 20;                       //敌人的最大生命值
		public int IntATK = 8;                         //敌人的攻击力
		public int IntDEF = 1;                          //敌人的防御力

		public float FloAtkSpeed = 2f;                  //敌人的攻击速度

		public float FloMoveSpeed = 6f;                 //敌人移动速度
		public float FloRotationSpeed = 1f;             //敌人转身速度
		public float FloAlertDistance = 15f;            //敌人的警戒距离
		public float FloAttackDistance = 1.5f;            //敌人发起攻击的最小距离

		public int IntEnemyEXP = 4;                     //敌人死亡时，主角获得的的经验值
		public int IntEnemyGold = 10;                   //敌人死亡时，主角获得的金钱



		/// <summary>
		/// 重载后的启用时方法
		/// </summary>
		protected override void OnEnable() {
			InitValues();
			base.OnEnable();
		}

		/// <summary>
		/// 重载后的禁用时方法
		/// </summary>
		protected override void OnDisable() {
			base.OnDisable();
		}



		/// <summary>
		/// 初始化敌人属性
		/// </summary>
		private void InitValues() {
			base.MaxHP = IntMaxHP;
			base.ATK = IntATK;
			base.DEF = IntDEF;

			base.AtkSpeed = FloAtkSpeed;

			base.MoveSpeed = FloMoveSpeed;
			base.RotationSpeed = FloRotationSpeed;
			base.AlertDistance = FloAlertDistance;
			base.AttackDistance = FloAttackDistance;

			base.EnemyEXP = IntEnemyEXP;
			base.EnemyGold = IntEnemyGold;
		}
	}
}