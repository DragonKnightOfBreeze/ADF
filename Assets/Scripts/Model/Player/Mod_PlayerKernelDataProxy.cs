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
		public static Mod_PlayerKernelDataProxy _Instance = null;
		public const int ENEMY_MIN_ATK = 1;	//敌人的最低攻击力


		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="HP"></param>
		/// <param name="MP"></param>
		/// <param name="ATK"></param>
		/// <param name="DEF"></param>
		/// <param name="DEX"></param>
		/// <param name="maxHP"></param>
		/// <param name="maxMP"></param>
		/// <param name="maxATK"></param>
		/// <param name="maxDEF"></param>
		/// <param name="maxDEX"></param>
		/// <param name="ATKByI"></param>
		/// <param name="DEFByI"></param>
		/// <param name="DEXByI"></param>
		public Mod_PlayerKernelDataProxy(float HP, float MP, float ATK, float DEF, float DEX, float maxHP, float maxMP, float maxATK, float maxDEF, float maxDEX, float ATKByI, float DEFByI, float DEXByI) : base(HP, MP, ATK, DEF, DEX, maxHP, maxMP, maxATK, maxDEF, maxDEX, ATKByI, DEFByI, DEXByI) { 
			if(_Instance == null) {
				_Instance = this;
			}
			else {
				Debug.LogError(GetType() + "Mod_PlayerKernelDataProxy()，不允许构造函数实例化，请检查！");
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
				Debug.LogWarning("GetInstance()，请先调用构造函数");
			}
			return null;
		}


		#region  生命数值操作

		/// <summary>
		/// 减少生命数值（例如：被敌人攻击）
		/// 公式：伤害 = 敌人攻击力 - (主角防御力 + 物品防御力)
		/// </summary>
		/// <param name="enemyAttackValue"></param>
		public void DeHealth(float enemyAttackValue) {
			float damageValue =  enemyAttackValue - (base.Defence + base.DefenceByItem);
			//最小伤害判断
			if(damageValue >= ENEMY_MIN_ATK) {
				base.Health -= damageValue;
			} else {
				base.Health -= ENEMY_MIN_ATK;
			}
		}

		/// <summary>
		/// 增加生命数值（例如：使用生命药水）
		/// </summary>
		/// <param name="inHealthValue"></param>
		public void InHealth(float inHealthValue) {
			float floReaInhealthValue = base.Health + inHealthValue;
			//最大生命值判断
			if (floReaInhealthValue < base.MaxHealth ) {
				base.Health += inHealthValue;
			} else {
				base.Health = base.MaxHealth;
			}
		}

		/// <summary>
		/// 得到当前生命数值
		/// </summary>
		/// <param name="healthvalue"></param>
		/// <returns></returns>
		public float GetCurHealth() {
			return base.Health;
		}

		/// <summary>
		/// 增加最大的生命数值（例如：等级提升）
		/// </summary>
		/// <param name="InHealth"></param>
		public void InMaxHealth(float inMaxHealth) {
			base.MaxHealth += inMaxHealth;
		}

		/// <summary>
		/// 得到最大的生命数值
		/// </summary>
		/// <returns></returns>
		public float GetMaxHealth() {
			return base.MaxHealth;
		}

		#endregion


		 #region  魔法数值操作

		/// <summary>
		/// 减少魔法数值（例如：使用魔法）
		/// 公式：魔法值 = 魔法值-魔法值消耗
		/// </summary>
		/// <param name="deManaValue"></param>
		public void DeMana(float deManaValue) {
			float floReaMana = base.Mana - deManaValue;
			if(floReaMana > 0) {
				base.Mana -= Mathf.Abs(deManaValue);
			} else {
				base.Mana = 0;
			}
		}

		/// <summary>
		/// 增加魔法数值（例如：使用魔法药水）
		/// </summary>
		/// <param name="inManaValue"></param>
		public void InMana(float inManaValue) {
			float floReaInManaValue = base.Mana + inManaValue;
			//最大魔法值判断
			if (floReaInManaValue < base.MaxMana) {
				base.Mana += inManaValue;
			}
			else {
				base.Mana = base.MaxMana;
			}
		}

		/// <summary>
		/// 得到当前魔法数值
		/// </summary>
		/// <param name="healthvalue"></param>
		/// <returns></returns>
		public float GetCurMana() {
			return base.Mana;
		}

		/// <summary>
		/// 增加最大的魔法数值（例如：等级提升）
		/// </summary>
		/// <param name="InHealth"></param>
		public void InMaxMana(float inMaxMana) {
			base.MaxMana += inMaxMana;
		}

		/// <summary>
		/// 得到最大的生命数值
		/// </summary>
		/// <returns></returns>
		public float GetMaxMana() {
			return base.MaxMana;
		}


		#endregion


		#region  攻击力数值操作

		//最简单的实际攻击力计算公式
		//公式：实际攻击力 = 基本攻击力 + 武器攻击力
		/// <summary>
		/// 更新攻击力（例如：当装备新武器时）
		/// </summary>
		public void UpdateATK(float newItemATKValue = 0) {
			//如果获得了新的武器物品，就更新武器攻击力
			if(newItemATKValue > 0) {
				base.AttackByItem = newItemATKValue;
			}
			base.TotalAttack = base.Attack + base.AttackByItem;
			
			//最大攻击力判断
			if (base.TotalAttack > GlobalParameter.MAX_VALUE_B) {
				base.TotalAttack = GlobalParameter.MAX_VALUE_B;
			}	
		}

		//得到当前的实际攻击力
		public float GetCurTotalATK() {
			return base.TotalAttack;
		}

		#endregion


		#region  防御力数值操作

		//最简单的实际防御力计算公式
		//公式：实际防御力 = 基本防御力 + 武器防御力
		/// <summary>
		/// 更新防御力（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEF(float newItemDEFValue = 0) {
			//如果获得了新的武器物品，就更新武器防御力
			if (newItemDEFValue > 0) {
				base.DefenceByItem = newItemDEFValue;
			}
			base.TotalDefence = base.Defence + base.DefenceByItem;
			
			//最大防御力判断
			if (base.TotalDefence > GlobalParameter.MAX_VALUE_B) {
				base.TotalDefence = GlobalParameter.MAX_VALUE_B;
			}
		}

		//得到当前的实际防御力
		public float GetCurTotalDEF() {
			return base.TotalDefence;
		}

		#endregion


		#region  敏捷度数值操作操作

		//最简单的实际敏捷度计算公式
		//公式：实际敏捷度 = 基本敏捷度 + 武器敏捷度
		/// <summary>
		/// 更新敏捷度（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEX(float newItemDEXValue = 0) {
			//如果获得了新的武器物品，就更新武器敏捷度
			if (newItemDEXValue > 0) {
				base.DexterityByItem = newItemDEXValue;
			}
			base.TotalDexterity = base.Dexterity + base.DexterityByItem;

			//最大敏捷度判断
			if (base.TotalDexterity > GlobalParameter.MAX_VALUE_B) {
				base.TotalDexterity = GlobalParameter.MAX_VALUE_B;
			}
		}

		//得到当前的实际敏捷度
		public float GetCurTotalDEX() {
			return base.TotalDexterity;
		}

		#endregion


	}
}
