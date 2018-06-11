//［控制层］所有敌人的属性的父类
//包含所有敌人的公共属性
//运用重构的思想，来构造更加灵活与低耦合度的敌人

//结藕：对象缓冲池管理器


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_BaseEnemy_Prop : BaseControl {

		#region 【私有字段】

		/* 需要在子类中赋值的公共属性，对应的字段 */

		private int _IntMaxHP = 0;				//敌人的最大生命值
		private int _IntATK = 0;				//敌人的攻击力
		private int _IntDEF = 0;                //敌人的防御力

		private float _FLoAtkSpeed = 0;			//敌人的攻击速度
		//private float _FloAtkDistance = 0;		//敌人的有效攻击距离

		private float _FloMoveSpeed = 0;		//敌人的移动速度
		private float _FloRotationSpeed = 0;	//敌人的转身速度
		private float _FloAlertDistance = 0;	//敌人的警戒距离
		private float _FloAttackDistance = 0;	//敌人发起攻击的最小距离
		
		
		private int _IntEnemyEXP = 0;			//敌人死亡时，主角获得的的经验值
		private int _IntEnemyGold = 0;          //敌人死亡时，主角获得的金钱

		/* 不需要在子类中赋值的公共属性，对应的字段 */

		private float _FloCurHP = 0;            //敌人的当前生命值
		//private float _FloRealATK = 0;		//敌人的真实攻击力
		//private float _FloRealDEF = 0;		//敌人的真实防御力

		private EnemyActionState _CurrentState = EnemyActionState.Idle; //敌人的当前动画状态（瞬时的）

		/* 常量 */

		public const int CON_MinDamage = 1;			//敌人能够造成的最小伤害
		public const float CON_RecheckTime = 0.02f;	//循环协程的再次检查时间
		public const float CON_RecoverTime = 5f;     //敌人死亡后，等待回收的时间

		#endregion



		#region 【公共属性】

		/// <summary>
		/// 属性：敌人的最大生命值
		/// </summary>
		public int MaxHP {
			get { return _IntMaxHP; }
			set { _IntMaxHP = value; }
		}
		/// <summary>
		/// 属性：敌人的攻击力
		/// </summary>
		public int ATK {
			get { return _IntATK; }
			set { _IntATK = value; }
		}
		/// <summary>
		/// 属性：敌人的防御力
		/// </summary>
		public int DEF {
			get { return _IntDEF; }
			set { _IntDEF = value; }
		}

		/// <summary>
		/// 属性：敌人的攻击速度
		/// </summary>
		public float AtkSpeed {
			get { return _FLoAtkSpeed; }
			set { _FLoAtkSpeed = value;	}
		}

		/// <summary>
		/// 属性：敌人的移动速度
		/// </summary>
		public float MoveSpeed {
			get { return _FloMoveSpeed; }
			set { _FloMoveSpeed = value; }
		}
		/// <summary>
		/// 属性：敌人的转身速度
		/// </summary>
		public float RotationSpeed {
			get { return _FloRotationSpeed; }
			set { _FloRotationSpeed = value; }
		}
		/// <summary>
		/// 属性：敌人的警戒距离
		/// </summary>
		public float AlertDistance {
			get { return _FloAlertDistance; }
			set { _FloAlertDistance = value; }
		}
		/// <summary>
		/// 属性：敌人发起攻击的最小距离
		/// </summary>
		public float AttackDistance {
			get { return _FloAttackDistance; }
			set { _FloAttackDistance = value; }
		}

		/// <summary>
		/// 属性：敌人死亡时，主角获得的的经验值
		/// </summary>
		public int EnemyEXP {
			get { return _IntEnemyEXP; }
			set { _IntEnemyEXP = value; }
		}
		/// <summary>
		/// 属性：敌人死亡时，主角获得的的金钱
		/// </summary>
		public int EnemyGold {
			get { return _IntEnemyGold; }
			set { _IntEnemyGold = value; }
		}

		/// <summary>
		/// 属性：敌人的当前生命值
		/// </summary>
		public float CurHP {
			get { return _FloCurHP; }
			set { _FloCurHP = value; }
		}
		/// <summary>
		/// 属性：敌人的当前动画状态
		/// </summary>
		public EnemyActionState CurrentState {
			get { return _CurrentState; }
			set { _CurrentState = value; }
		}



		#endregion



		#region 【可以重载的受保护方法】

		/// <summary>
		/// 有待重载的启用时方法
		/// 使用对象缓冲池技术后，协程方法应该根据脚本生命周期开始和结束
		/// </summary>
		protected virtual void OnEnable() {
			//初始化敌人的状态
			InitStatus();
			//子类重载：设置好各种属性数值
			//......
			//协程：判断是否存活
			StartCoroutine(CheckLifeContinue());
		}

		/// <summary>
		/// 有待重载的禁用时方法
		/// 使用对象缓冲池技术后，协程方法应该根据脚本生命周期开始和结束
		/// </summary>
		protected virtual void OnDisable() {
			//协程：停止判断是否存活
			StopCoroutine(CheckLifeContinue());
		}

		#endregion


		#region 【公共方法】

		/// <summary>
		/// 公共方法：伤害处理
		/// </summary>
		/// <param name="damage">伤害</param>
		public void OnHurt(float damage) {
			//设置当前动画状态
			_CurrentState = EnemyActionState.Hurt;
			//处理敌人的生命值
			SubCurHP(damage);
		}

		/// <summary>
		/// 公共方法：减少生命值
		/// </summary>
		/// <param name="damage">伤害</param>
		public void SubCurHP(float damage) {
			//真实伤害计算
			float realDamage = damage - DEF;
			//最小伤害判断
			if (realDamage < CON_MinDamage) {
				realDamage = CON_MinDamage;
			}
			//一般情况
			if (CurHP - realDamage > 0) {
				CurHP -= realDamage;
			}
			//最小生命值判断
			else {
				CurHP = 0;
			}
		}

		/*
		 * 
		public void AddCurHP(float addValue) {}
		public void GetCurHP() { }
		public void AddMaxHP(float addValue) { }
		public void GetMaxHP() { }

		public void UpdateATK(float atkByItem) { }
		public void AddATK(float addValue) { }
		public void SubATK(float aubValue) {}
		public void GetATK() { }
		public void UpdateDEF(float defByItem) { }
		public void AddDEF(float addValue) { }
		public void SubDEF(float subValue) { }
		public void GetDEF() { }

		public void AddEnemyEXP(int addValue) { }
		public void SubEnemyEXP(int subValue) { }
		public void AddEnemyGold(int addValue) { }
		public void SubEnemyGold(int subValue) { }

		*/



		#endregion



		#region 【私有协程】

		/// <summary>
		/// 协程：检查敌人是否存活
		/// </summary>
		/// <returns></returns>
		IEnumerator CheckLifeContinue() {
			while (true) {
				//如果生命值已经为0
				if (CurHP == 0) {
					if (_CurrentState != EnemyActionState.Dead) {
						//设置为死亡状态
						_CurrentState = EnemyActionState.Dead;
						//玩家得到各种奖励（获得经验值、金钱，增加杀敌量等）
						DeathReward();
						//开始协程：回收对象
						StartCoroutine(RecoverGO());
					}
				}
				yield return new WaitForSeconds(CON_RecheckTime);     
			}
		}
	


		/// <summary>
		/// 协程：回收对象
		/// </summary>
		IEnumerator RecoverGO() {
			//等待一定时间
			yield return new WaitForSeconds(CON_RecoverTime);
			//重置到回收前的状态
			InitStatus();
			//使用对象缓冲池技术，回收对象
			PoolManager.PoolsArray["_Enemys"].RecoverGameObject(gameObject);
		}

		#endregion



		#region 私有方法

		/// <summary>
		/// 私有方法：敌人死亡后，玩家获得奖励
		/// </summary>
		private void DeathReward() {
			Ctrl_HeroProperty.Instance.AddEXP(EnemyEXP);
			Ctrl_HeroProperty.Instance.AddGold(EnemyGold);
			Ctrl_HeroProperty.Instance.AddKillNum();
		}

		/// <summary>
		/// 私有方法：初始化或重置敌人的状态
		/// </summary>
		private void InitStatus() {
			_CurrentState = EnemyActionState.Idle;
			CurHP = MaxHP;
		}

		#endregion




	}
}

