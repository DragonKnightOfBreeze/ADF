//控制层，敌人：骷髅战士的属性

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_SkeletonWarrior_Prop : Ctrl_BaseEnemy_Prop {
		public int MaxHP = 40;   //敌人的最大生命数值
		public int ATK = 8;     //敌人的攻击力
		public int DEF = 2;         //敌人的防御力

		public int EnemyEXP = 5; //英雄的经验数值

		public float MoveSpeed = 5f;   //敌人移动速度
		public float RotationSpeed = 1f; //敌人旋转速度

		private void Start() {
			base.IntMaxHP = MaxHP;
			base.IntATK = ATK;
			base.IntDEF = DEF;
		
			base.IntEnemyEXP = EnemyEXP;

			base.FloMoveSpeed = MoveSpeed;
			base.FloRotationSpeed = RotationSpeed;

			base.RunMethodInChildren();
		}
	}
}