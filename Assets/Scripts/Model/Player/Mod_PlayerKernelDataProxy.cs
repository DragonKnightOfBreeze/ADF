//模型层，玩家的核心数据的代理类
//模型层中都是定义的类，而非真正意义上的脚本

//功能：为了简化数值的开发，我们把数值的直接存取与复杂的操作计算相分离
//（本质是“代理”设计模式的应用）
//本类设计为单例

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Model {
	public class Mod_PlayerKernelDataProxy : Mod_PlayerKernelData {
		private static Mod_PlayerKernelDataProxy _Instance = null;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="curHP"></param>
		/// <param name="curMP"></param>
		/// <param name="atk"></param>
		/// <param name="def"></param>
		/// <param name="dex"></param>
		/// <param name="maxHP"></param>
		/// <param name="maxMP"></param>
		/// <param name="atkByItem"></param>
		/// <param name="defByItem"></param>
		/// <param name="dexByItem"></param>
		public Mod_PlayerKernelDataProxy(float curHP, float curMP, float atk, float def, float dex, float maxHP, float maxMP, float atkByItem, float defByItem, float dexByItem) : base(curHP, curMP, atk, def, dex, maxHP, maxMP, atkByItem, defByItem, dexByItem) { 
			if(_Instance == null) {
				_Instance = this;
			}
			else {
				// // Debug.LogError(GetType() + "Mod_PlayerKernelDataProxy()，不允许构造函数实例化，请检查！");
			}
		}

		/// <summary>
		/// 得到本类的实例（单例模式）
		/// </summary>
		/// <returns></returns>
		public static Mod_PlayerKernelDataProxy GetInstance() {
			if(_Instance !=null) {
				return _Instance;
			} else {
				// // Debug.LogWarning("GetInstance()，请先调用构造函数");
			}
			return null;
		}


		#region  【生命值和最大生命值的操作】

		/// <summary>
		/// 减少生命值（例如：被敌人攻击）
		/// </summary>
		/// <param name="damage">伤害</param>
		public void SubCurHP(float damage) {
			//理论上来说这里的参数都是主动赋值的，不可能为负
			//因此这一系列的方法都不进行参数检查

			//真实伤害计算
			//公式：真实伤害 = 敌人攻击力 - (主角防御力 + 物品防御力)
			float realDamage =  damage - (base.DEF + base.DEFByItem);
			//最小伤害判断
			if(realDamage < GlobalParameter.MIN_DAMAGE) {
				realDamage = GlobalParameter.MIN_DAMAGE;
			}
			//一般情况
			if(base.CurHP >= realDamage) {
				base.CurHP -= realDamage;
			}
			//最小生命值判断
			else {
				base.CurHP = 0;
			}
}


		/// <summary>
		/// 增加生命值（例如：使用生命药水）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddCurHP(float addValue) {
			//一般情况
			if (base.CurHP + addValue <= base.MaxHP ) {
				base.CurHP += addValue;
			}
			//最大生命值判断
			else {
				base.CurHP = base.MaxHP;
			}
		}

		/// <summary>
		/// 得到当前的生命值
		/// </summary>
		/// <returns></returns>
		public float GetCurHP() {
			return base.CurHP;
		}

		/// <summary>
		/// 增加最大的生命值（例如：等级提升）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddMaxHP(float addValue) {
			base.MaxHP += addValue;
			//极限的最大生命值判断
			if (base.MaxHP > GlobalParameter.MAX_VALUE_999) {
				base.MaxHP = GlobalParameter.MAX_VALUE_999;
			}
		}

		/// <summary>
		/// 得到最大的生命值
		/// </summary>
		/// <returns></returns>
		public float GetMaxHP() {
			return base.MaxHP;
		}

		#endregion


		#region 【魔法值和最大魔法值的操作】

		/// <summary>
		/// 减少魔法值（例如：使用魔法）
		/// </summary>
		/// <param name="consumption">魔法值消耗</param>
		/// <returns>true：魔法值足够</returns>
		public bool SubCurMP(float consumption) {
			//一般情况
			if (base.CurMP >= consumption) {
				base.CurMP -= consumption;
				return true;
			}
			//魔法值不够
			return false;
		}

		/// <summary>
		/// 增加魔法值（例如：使用魔法药水）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddCurMP(float addValue) {
			//一般情况
			if (base.CurMP + addValue <= base.MaxHP) {
				base.CurMP += addValue;
			}
			//最大魔法值判断
			else {
				base.CurHP = base.MaxHP;
			}
		}

		/// <summary>
		/// 得到当前的魔法值
		/// </summary>
		/// <returns></returns>
		public float GetCurMP() {
			return base.CurMP;
		}

		/// <summary>
		/// 增加最大的魔法值（例如：等级提升）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddMaxMP(float addValue) {
			base.MaxMP += addValue;
			//极限的最大魔法值判断
			if (base.MaxMP > GlobalParameter.MAX_VALUE_999) {
				base.MaxMP = GlobalParameter.MAX_VALUE_999;
			}
		}

		/// <summary>
		/// 得到最大的魔法值
		/// </summary>
		/// <returns></returns>
		public float GetMaxMP() {
			return base.MaxMP;
		}


		#endregion


		#region  【攻击力数值的操作】

		/// <summary>
		/// 更新攻击力（例如：当装备新武器时）
		/// </summary>
		public void UpdateATK(float atkByItem) {
			//一般情况（使用最简单的计算方法）
			//公式：实际攻击力 = 基本攻击力 + 武器攻击力
			base.ATK -= ATKByItem;
			base.ATK += atkByItem;
			base.ATKByItem = atkByItem;
			//极限的攻击力判断
			if (base.ATK > GlobalParameter.MAX_VALUE_99) {
				base.ATK = GlobalParameter.MAX_VALUE_99;
			}	
		}

		/// <summary>
		/// 增加攻击力
		/// </summary>
		/// <param name="addValue"></param>
		public void AddATK(float addValue) {
			base.ATK += addValue;
			//极限的攻击力判断
			if (base.ATK > GlobalParameter.MAX_VALUE_99) {
				base.ATK = GlobalParameter.MAX_VALUE_99;
			}
		}

		/// <summary>
		/// 得到攻击力
		/// </summary>
		/// <returns></returns>
		public float GetATK() {
			return base.ATK;
		}

		#endregion


		#region  【防御力数值的操作】

		/// <summary>
		/// 更新防御力（例如：当装备新防具时）
		/// </summary>
		public void UpdateDEF(float defByItem = 0) {
			//一般情况（使用最简单的计算方法）
			//公式：实际防御力 = 基本防御力 + 防具防御力
			base.DEF -= DEFByItem;
			base.DEF += defByItem;
			base.DEFByItem = defByItem;
			//极限的防御力判断
			if (base.DEF > GlobalParameter.MAX_VALUE_99) {
				base.DEF = GlobalParameter.MAX_VALUE_99;
			}
		}

		/// <summary>
		/// 增加防御力
		/// </summary>
		/// <param name="addValue"></param>
		public void AddDEF(float addValue) {
			base.DEF += addValue;
			//极限的防御力判断
			if (base.DEF > GlobalParameter.MAX_VALUE_99) {
				base.DEF = GlobalParameter.MAX_VALUE_99;
			}
		}

		/// <summary>
		/// 得到防御力
		/// </summary>
		/// <returns></returns>
		public float GetDEF() {
			return base.DEF;
		}

		#endregion



		#region  【敏捷度数值的操作】

		/// <summary>
		/// 更新敏捷度（例如：当装备新靴子时）
		/// </summary>
		public void UpdateDEX(float dexByItem = 0) {
			//一般情况（使用最简单的计算方法）
			//公式：实际敏捷度 = 基本敏捷度 + 靴子敏捷度
			base.DEX -= DEXByItem;
			base.DEX += dexByItem;
			base.DEXByItem = dexByItem;
			//极限的防御力判断
			if (base.DEX > GlobalParameter.MAX_VALUE_99) {
				base.DEX = GlobalParameter.MAX_VALUE_99;
			}
		}

		/// <summary>
		/// 增加敏捷度
		/// </summary>
		/// <param name="addValue"></param>
		public void AddDEX(float addValue) {
			base.DEX += addValue;
			//极限的敏捷度判断
			if (base.DEX > GlobalParameter.MAX_VALUE_99) {
				base.DEX = GlobalParameter.MAX_VALUE_99;
			}
		}

		/// <summary>
		/// 得到防御力
		/// </summary>
		/// <returns></returns>
		public float GetDEX() {
			return base.DEX;
		}

		#endregion


		/// <summary>
		/// 显示所有初始数值
		/// 这他妈到底有什么用？
		/// </summary>
		public void DisplayAllOriginalValues() {
			base.CurHP = base.CurHP;
			base.CurMP = base.CurMP;
			base.ATK = base.ATK;
			base.DEF = base.DEF;
			base.DEX = base.DEX;

			base.MaxHP = base.MaxHP;
			base.MaxMP = base.MaxMP;

			base.ATKByItem = base.ATKByItem;
			base.DEFByItem = base.DEFByItem;
			base.DEXByItem = base.DEXByItem;

		}

	}
}
