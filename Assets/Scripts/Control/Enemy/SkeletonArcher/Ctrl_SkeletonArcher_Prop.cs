//［控制层］骷髅弓箭手的属性

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_SkeletonArcher_Prop : Ctrl_BaseEnemy_Prop {

		/* 在这里修改敌人的真实属性 */

		public int IntMaxHP = 30;                   //敌人的最大生命值
		public int IntATK = 30;                      //敌人的攻击力（为0，真的攻击力在箭上）
		public int IntDEF = 2;                      //敌人的防御力

		public float IntAtkSpeed = 4;				//敌人的攻击速度

		public float FloMoveSpeed = 4f;             //敌人移动速度
		public float FloRotationSpeed = 1f;         //敌人转身速度
		public float FloAlertDistance = 20f;        //敌人的警戒距离
		public float FloAttackDistance = 10f;		//敌人发起攻击的最小距离

		public int IntEnemyEXP = 5;                 //敌人死亡时，主角获得的的经验值
		public int IntEnemyGold = 10;               //敌人死亡时，主角获得的金钱

		/* 特有的属性 */

		public float ArrowSpeed = 5f;   //道具的速度

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

			base.AtkSpeed = IntAtkSpeed;

			base.MoveSpeed = FloMoveSpeed;
			base.RotationSpeed = FloRotationSpeed;
			base.AlertDistance = FloAlertDistance;
			base.AttackDistance = FloAttackDistance;

			base.EnemyEXP = IntEnemyEXP;
			base.EnemyGold = IntEnemyGold;
		}
	}
}