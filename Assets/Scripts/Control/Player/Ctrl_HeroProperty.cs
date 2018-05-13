//控制层，英雄属性脚本
//作用：
//1.实例化对应模型层类，且初始化数据
//2.整合模型层关于“玩家”模块的核心方法，供本控制层使用

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Model;

namespace Control {
	public class Ctrl_HeroProperty : BaseControl {
		//实例化本类
		public static Ctrl_HeroProperty Instance;


		//玩家的核心数值；
		//定义公共变量，并且赋予初值，然后用来初始化模型层的单例类

		public float PlayerCurHp = 160f;   //玩家的当前生命值
		public float PlayerMaxHP = 160f;   //玩家的最大生命值
		public float PlayerCurMP = 100f;   //玩家的当前魔法值
		public float PlayerMaxMP = 100f;   //玩家的最大魔法值
		public float PlayerATK = 15f;   //玩家的攻击力
		public float PlayerDEF = 5f;   //玩家的防御力
		public float PlayerDEX = 50f;   //玩家的当前生命值

		public float PlayerATKByI = 0f;		//物品攻击力
		public float PlayerDEFByI = 0f;		//物品防御力
		public float PlayerDEXByI = 0f;		//物品敏捷度

		//玩家拓展数值
		public int PlayerEXP = 0;			//等级
		public int PlayerKillNum = 0;		//
		public int PlayerLevel = 1;
		public int PlayerGold = 0;
		public int PlayerDiamond = 0;

		private float _MPRecoverSpeed = 1f; //MP恢复速度（每s）


		private void Awake() {
			Instance = this;	//得到本类引用
		}


		private void Start() {
			//初始化模型层数据
			Mod_PlayerKernelDataProxy playerKernelDataObj = new Mod_PlayerKernelDataProxy(PlayerCurHp, PlayerCurMP, PlayerATK, PlayerDEF, PlayerDEX, PlayerMaxHP, PlayerMaxMP, PlayerATKByI, PlayerDEFByI, PlayerDEXByI);

			Mod_PlayerExtendedDataProxy playerExtendedDataObj = new Mod_PlayerExtendedDataProxy(PlayerEXP, PlayerKillNum, PlayerLevel, PlayerGold, PlayerDiamond);

			StartCoroutine("RecoverMana");
		}




		#region  生命数值操作

		/// <summary>
		/// 减少生命数值（例如：被敌人攻击）
		/// 公式：伤害 = 敌人攻击力 - (主角防御力 + 物品防御力)
		/// </summary>
		/// <param name="enemyAttackValue"></param>
		public void DeHealth(float enemyAttackValue) {
			if (enemyAttackValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().DeHealth(enemyAttackValue);
			}
		}

		/// <summary>
		/// 增加生命数值（例如：使用生命药水）
		/// </summary>
		/// <param name="inHealthValue"></param>
		public void InHealth(float inHealthValue) {
			if (inHealthValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InHealth(inHealthValue);
			}
		}

		/// <summary>
		/// 得到当前生命数值
		/// </summary>
		/// <param name="healthvalue"></param>
		/// <returns></returns>
		public float GetCurHealth() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurHealth();
		}

		/// <summary>
		/// 增加最大的生命数值（例如：等级提升）
		/// </summary>
		/// <param name="InHealth"></param>
		public void InMaxHealth(float inMaxHealth) {
			if (inMaxHealth > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InMaxHealth(inMaxHealth);
			}
		}

		/// <summary>
		/// 得到最大的生命数值
		/// </summary>
		/// <returns></returns>
		public float GetMaxHealth() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetMaxHealth();
		}

		#endregion


		#region  魔法数值操作

		/// <summary>
		/// 减少魔法数值（例如：使用魔法）
		/// 公式：魔法值 = 魔法值-魔法值消耗
		/// </summary>
		/// <param name="deManaValue"></param>
		public void DeMana(float deManaValue) {
			if (deManaValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().DeMana(deManaValue);
			}
		}

		/// <summary>
		/// 增加魔法数值（例如：使用魔法药水）
		/// </summary>
		/// <param name="inManaValue"></param>
		public void InMana(float inManaValue) {
			if (inManaValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InMana(inManaValue);
			}
		}

		/// <summary>
		/// 得到当前魔法数值
		/// </summary>
		/// <param name="healthvalue"></param>
		/// <returns></returns>
		public float GetCurMana() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurMana();
		}

		/// <summary>
		/// 增加最大的魔法数值（例如：等级提升）
		/// </summary>
		/// <param name="InHealth"></param>
		public void InMaxMana(float inMaxMana) {
			if (inMaxMana > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InMaxMana(inMaxMana);
			}
		}

		/// <summary>
		/// 得到最大的魔法数值
		/// </summary>
		/// <returns></returns>
		public float GetMaxMana() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetMaxMana();
		}


		/// <summary>
		/// 恢复魔法数值
		/// </summary>
		public IEnumerator RecoverMana() {
			while (true) {
				if (GetCurMana() < GetMaxMana()) {
					InMana(_MPRecoverSpeed);
				}
				//为什么每次会同时重复两次协程？
				// // Debug.Log("一次");
				yield return new WaitForSeconds(0.5f);//每秒判定一次
				
			}
			
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
			if (newItemATKValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateATK(newItemATKValue);
			}
		}

		/// <summary>
		/// 增加攻击力
		/// </summary>
		/// <param name="inATK"></param>
		public void InATK(float inATK) {
			Mod_PlayerKernelDataProxy.GetInstance().InATK(inATK);
		}

		/// <summary>
		/// 得到当前的攻击力
		/// </summary>
		/// <returns></returns>
		public float GetCurATK() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurATK();
		}

		#endregion


		#region  防御力数值操作

		//最简单的实际防御力计算公式
		//公式：实际防御力 = 基本防御力 + 武器防御力
		/// <summary>
		/// 更新防御力（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEF(float newItemDEFValue = 0) {
			if (newItemDEFValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateDEF(newItemDEFValue);
			}
		}

		/// <summary>
		/// 增加防御力
		/// </summary>
		/// <param name="inDEF"></param>
		public void InDEF(float inDEF) {
			if (inDEF > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InDEF(inDEF);
			}
		}

		/// <summary>
		/// 得到当前的防御力
		/// </summary>
		/// <returns></returns>
		public float GetCurDEF() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurDEF();
		}

		#endregion


		#region  敏捷度数值操作操作

		//最简单的实际敏捷度计算公式
		//公式：实际敏捷度 = 基本敏捷度 + 武器敏捷度
		/// <summary>
		/// 更新敏捷度（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEX(float newItemDEXValue = 0) {
			if (newItemDEXValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateDEX(newItemDEXValue);
			}
		}

		/// <summary>
		/// 增加敏捷度
		/// /// </summary>
		/// <param name="inDEX"></param>
		public void InDEX(float inDEX) {
			if (inDEX > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().InDEX(inDEX);
			}
		}

		/// <summary>
		/// 得到当前的敏捷度
		/// </summary>
		/// <returns></returns>
		public float GetCurDEX() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurDEX();
		}

		#endregion


		


		#region 经验值处理

		/// <summary>
		/// 增加经验值
		/// </summary>
		public void AddEXP(int expValue) {
			if (expValue > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddEXP(expValue);
			}
		}

		/// <summary>
		/// 得到经验值
		/// </summary>
		/// <returns></returns>
		public int GetEXP() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetEXP();
		}

		#endregion


		#region 杀敌数量处理

		/// <summary>
		/// 增加杀敌数量
		/// </summary>
		public void AddKillNum() {
			Mod_PlayerExtendedDataProxy.GetInstance().AddKillNum();
		}

		/// <summary>
		/// 得到杀敌数量
		/// </summary>
		/// <returns></returns>
		public int GetKillNum() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetKillNum();
		}

		#endregion


		#region 等级处理

		/// <summary>
		/// 提升当前等级
		/// </summary>
		public void AddLevel() {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();	
		}

		/// <summary>
		/// 得到当前等级
		/// </summary>
		public int GetLevel() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetLevel();
		}

		#endregion


		#region 金币处理

		/// <summary>
		/// 增加一定数量的金币数量
		/// </summary>
		/// <param name="goldNumber"></param>
		public void AddGold(int goldNumber) {
			if (goldNumber > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddGold(goldNumber);
			}
		}

		/// <summary>
		/// 得到当前金币数量
		/// </summary>
		/// <returns></returns>
		public int GetGold() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetGold();
		}

		#endregion


		#region 钻石处理

		/// <summary>
		/// 增加一定数量的钻石数量
		/// </summary>
		/// <param name="diamondNumber"></param>
		public void AddDiamond(int diamondNumber) {
			if (diamondNumber > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddDiamond(diamondNumber);
			}
		}

		/// <summary>
		/// 得到当前钻石数量
		/// </summary>
		/// <returns></returns>
		public int GetDiamond() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetDiamond();
		}

		#endregion


	}

}