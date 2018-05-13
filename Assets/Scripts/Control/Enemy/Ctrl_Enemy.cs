/*

//控制层，敌人属性脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_Enemy : BaseControl {
		private bool _IsAlive = true; //敌人是否生存

		public int IntMaxHP = 20;   //敌人的最大生命数值
		private float _FloCurHp; //敌人的当前生命数值

		public int IntEnemyEXP;	//英雄的经验数值

		//属性：是否存活
		public bool IsAlive {
			get { return _IsAlive; }
		}
		
		void Start() {
			_FloCurHp = IntMaxHP;
			//判断是否存活
			StartCoroutine("CheckLifeContinue");
		}

		/// <summary>
		/// 检查是否存活
		/// </summary>
		/// <returns></returns>
		IEnumerator CheckLifeCOntinue() {
			//协程需要重复执行
			while(true) {
				yield return new WaitForSeconds(1f);        //每1s判断1次
															//根据游戏的不同，这里需要加以改动
				if (_FloCurHp <= IntMaxHP * 0.01) {
					_IsAlive = false;
					
					Destroy(this.gameObject);   //销毁对象（敌人死亡）
					Ctrl_HeroProperty.Instance.AddEXP(IntEnemyEXP); //玩家获得经验值
					Ctrl_HeroProperty.Instance.AddKillNum();	//增加玩家的杀敌数量


				}
			}
		}

		/// <summary>
		/// 伤害处理
		/// </summary>
		/// <param name="hurtValue"></param>
		public void OnHurt(int hurtValue) {
			// // Debug.Log("进行伤害处理！"); 
			int hurtValues = 0;
			hurtValues = Mathf.Abs(hurtValue);
			if(hurtValues >0) {
				_FloCurHp -= hurtValues;
			}
		}
	}
}

*/