//模型层，玩家的拓展数据的代理类
//模型层中都是定义的类，而非真正意义上的脚本
//单例模式

//设计模式中的两条基本原则：“开发封闭原则”、“单一职责原则”
//增加功能是以增加类/脚本为代价，而尽量不去动原来的类/脚本
//一个类/脚本尽量只承担一个功能

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Model {
	public class Mod_PlayerExtendedDataProxy:Mod_PlayerExtendedData {
		public static Mod_PlayerExtendedDataProxy _Instance = null; //该类的实例

		public Mod_PlayerExtendedDataProxy(int exp,int killNumber,int level,int gold,int diamond) : base(exp, killNumber,level,gold,diamond) {
			if(_Instance == null) {
				_Instance = this;
			} else {
				// // Debug.LogError(GetType() + "/Mod_PlayerExtendedDataProxy/不允许构造函数重复实例化");
			}
		}

		/// <summary>
		/// 得到本类实例
		/// </summary>
		/// <returns></returns>
		public static Mod_PlayerExtendedDataProxy GetInstance() {
			if(_Instance != null) {
				return _Instance;
			} else {
				// // Debug.LogWarning("GetInstance()/请先调用构造函数");
				return null;
			}
		}


		#region 经验值处理

		/// <summary>
		/// 增加经验值
		/// </summary>
		public void AddEXP(int addValue) {
			base.EXP += addValue;
			//经验值达到一定阶段，会自动提升当前等级
			//***可能还可以改进？***
			Mod_UpgradeLevelRule.GetInstance().LevelUpCheck(base.EXP);
		}

		/// <summary>
		/// 得到经验值
		/// </summary>
		/// <returns></returns>
		public int GetEXP() {
			return base.EXP;
		}

		#endregion


		#region 杀敌数量处理

		/// <summary>
		/// 增加杀敌数量
		/// </summary>
		public void AddKillNum() {
			++base.KillNum;
		}
		
		/// <summary>
		/// 得到杀敌数量
		/// </summary>
		/// <returns></returns>
		public int GetKillNum() {
			return base.KillNum;
		}

		#endregion


		#region 等级处理

		/// <summary>
		/// 提升等级
		/// </summary>
		public void AddLevel() {
			++base.Level;
			//等级提升，玩家的各种属性都会有所提升。
			//***肯定需要加以改进***
			Mod_UpgradeLevelRule.GetInstance().LevelUpOperationMgr((LevelName)base.Level);	//数值型转换成枚举类型
		}

		/// <summary>
		/// 得到等级
		/// </summary>
		public int GetLevel() {
			return base.Level;
		}

		#endregion


		#region 金币处理

		/// <summary>
		/// 增加一定数量的金币
		/// </summary>
		/// <param name="number"></param>
		public void AddGold(int number) {
			base.Gold += Mathf.Abs(number);
		}

		/// <summary>
		/// 减少一定数量的金币
		/// </summary>
		/// <param name="number"></param>
		/// <returns>金币是否足够</returns>
		public bool SubGold(int number) {
			//一般情况
			if (base.Gold >= Mathf.Abs(number)) {
				base.Gold -= Mathf.Abs(number);
				return true;
			}
			//如果金币数量不足
			return false;
		}

		/// <summary>
		/// 得到当前金币数量
		/// </summary>
		/// <returns></returns>
		public int  GetGold() {
			return base.Gold;
		}

		#endregion


		#region 钻石处理
		
		/// <summary>
		/// 增加一定数量的钻石
		/// </summary>
		/// <param name="number"></param>
		public void AddDiamond(int number) {
			base.Diamond += Mathf.Abs(number);
		}

		/// <summary>
		/// 减少一定数量的钻石
		/// </summary>
		/// <param name="number"></param>
		/// <returns>是否可减</returns>
		public bool SubDiamond(int number) {
			//如果结果不为负数
			if (base.Diamond >= Mathf.Abs(number)) {
				base.Diamond -= Mathf.Abs(number);
				return true;
			}
			return false;

		}

		/// <summary>
		/// 得到当前的钻石数量
		/// </summary>
		/// <returns></returns>
		public int GetDiamond() {
			return base.Diamond;
		}

		#endregion

		//显示所有初始数据
		public void DisplayAllOriginalValues() {
			base.EXP = base.EXP;
			base.KillNum = base.KillNum;
			base.Level = base.Level;
			base.Gold = base.Gold;
			base.Diamond = base.Diamond;
		}
	}
}
