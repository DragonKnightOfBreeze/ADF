//控制层，英雄属性脚本
//作用：
//1.实例化对应模型层类，且初始化数据
//2.整合模型层关于“玩家”模块的核心方法，供本控制层使用

//待优化：使用XML赋值数据


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Model;

namespace Control {
	public class Ctrl_HeroProperty : BaseControl {
		//实例化本类
		public static Ctrl_HeroProperty Instance;



		#region 【玩家的属性赋值（改进：XML持久化）】

		//玩家的核心数值；
		//定义公共变量，并且赋予初值，然后用来初始化模型层的单例类

		public float PlayerCurHp = 160f;	//玩家的当前生命值
		public float PlayerMaxHP = 160f;	//玩家的最大生命值
		public float PlayerCurMP = 100f;	//玩家的当前魔法值
		public float PlayerMaxMP = 100f;	//玩家的最大魔法值
		public float PlayerATK = 15f;		//玩家的攻击力
		public float PlayerDEF = 5f;		//玩家的防御力
		public float PlayerDEX = 50f;		//玩家的当前生命值

		public float PlayerATKByItem = 0f;		//物品攻击力
		public float PlayerDEFByItem = 0f;		//物品防御力
		public float PlayerDEXByItem = 0f;		//物品敏捷度

		//玩家的拓展数值
		public int PlayerEXP = 0;			//等级
		public int PlayerKillNum = 0;		//
		public int PlayerLevel = 1;
		public int PlayerGold = 0;
		public int PlayerDiamond = 0;

		private float _MPRecoverSpeed = 1f;  //玩家的MP恢复速度（每秒）


		//玩家背包数量（这里应该需要动态添加变量，并且初始值是0）
		//不如使用实例化结构体或类的方式
		//***待优化***
		public int IntWeapon_1_Count = 0;
		public int IntShield_1_Count = 0;
		public int IntBoot_1_Count = 0;
		public int IntHPPotion_1_Count = 0;
		public int IntMPPotion_1_Count = 0;



		#endregion



		private void Awake() {
			Instance = this;	//得到本类引用
		}


		private void Start() {

			//另一种方式：使用XML解析的时候在加载场景时（或者游戏开始时）
			//进行一次性初始化

			//初始化模型层数据
			Mod_PlayerKernelDataProxy playerKernelDataObj = new Mod_PlayerKernelDataProxy(PlayerCurHp, PlayerCurMP, PlayerATK, PlayerDEF, PlayerDEX, PlayerMaxHP, PlayerMaxMP, PlayerATKByItem, PlayerDEFByItem, PlayerDEXByItem);

			Mod_PlayerExtendedDataProxy playerExtendedDataObj = new Mod_PlayerExtendedDataProxy(PlayerEXP, PlayerKillNum, PlayerLevel, PlayerGold, PlayerDiamond);

			//***这个写法有待改进***
			Mod_PlayerPackageDataProxy playerPackageData = new Mod_PlayerPackageDataProxy(IntWeapon_1_Count,IntShield_1_Count,IntBoot_1_Count, IntHPPotion_1_Count,IntMPPotion_1_Count);

			//开始MP恢复协程
			StartCoroutine("RecoverMP");
		}




		#region  【生命值和最大生命值的操作】

		/// <summary>
		/// 减少当前的生命值（例如：被敌人攻击）
		/// </summary>
		/// <param name="damage"></param>
		public void SubCurHP(float damage) {
			if (damage > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().SubCurHP(damage);
			}
		}

		/// <summary>
		/// 增加当前的生命值（例如：使用生命药水）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddCurHP(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddCurHP(addValue);
			}
		}

		/// <summary>
		/// 得到当前的生命值
		/// </summary>
		/// <returns></returns>
		public float GetCurHP() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurHP();
		}

		/// <summary>
		/// 增加最大的生命值（例如：等级提升）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddMaxHP(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddMaxHP(addValue);
			}
		}

		/// <summary>
		/// 得到最大的生命值
		/// </summary>
		/// <returns></returns>
		public float GetMaxHP() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetMaxHP();
		}

		#endregion


		#region  【魔法值和最大魔法值的操作】

		/// <summary>
		/// 减少当前的魔法值（例如：使用魔法）
		/// </summary>
		/// <param name="consumption"></param>
		/// <returns>魔法值是否足够</returns>
		public bool SubCurMP(float consumption) {
			if (consumption > 0) {
				return Mod_PlayerKernelDataProxy.GetInstance().SubCurMP(consumption);
			}
			return false;
		}

		/// <summary>
		/// 增加当前的魔法值（例如：使用魔法药水）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddCurMP(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddCurMP(addValue);
			}
		}

		/// <summary>
		/// 得到当前的魔法值
		/// </summary>
		/// <returns></returns>
		public float GetCurMP() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetCurMP();
		}

		/// <summary>
		/// 增加最大的魔法值（例如：等级提升）
		/// </summary>
		/// <param name="addValue"></param>
		public void AddMaxMP(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddMaxMP(addValue);
			}
		}

		/// <summary>
		/// 得到最大的魔法数值
		/// </summary>
		/// <returns></returns>
		public float GetMaxMana() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetMaxMP();
		}


		/// <summary>
		/// 恢复魔法值的方法
		/// </summary>
		public IEnumerator RecoverMP() {
			while (true) {
				if (GetCurMP() < GetMaxMana()) {
					AddCurMP(_MPRecoverSpeed);
				}
				//为什么每次会同时重复两次协程？
				// // Debug.Log("一次");
				yield return new WaitForSeconds(0.5f);//每秒判定一次
				
			}
			
		}

		#endregion


		#region  【攻击力的操作】
		
		/// <summary>
		/// 更新攻击力（例如：当装备新武器时）
		/// </summary>
		public void UpdateATK(float atkByItem = 0) {
			//如果获得了新的武器物品，就更新武器攻击力
			if (atkByItem > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateATK(atkByItem);
			}
		}

		/// <summary>
		/// 增加攻击力
		/// </summary>
		/// <param name="addValue"></param>
		public void AddATK(float addValue) {
			Mod_PlayerKernelDataProxy.GetInstance().AddATK(addValue);
		}

		/// <summary>
		/// 得到攻击力
		/// </summary>
		/// <returns></returns>
		public float GetATK() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetATK();
		}

		#endregion


		#region  【防御力的操作】

		/// <summary>
		/// 更新防御力（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEF(float defByItem = 0) {
			if (defByItem > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateDEF(defByItem);
			}
		}

		/// <summary>
		/// 增加防御力
		/// </summary>
		/// <param name="addValue"></param>
		public void AddDEF(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddDEF(addValue);
			}
		}

		/// <summary>
		/// 得到防御力
		/// </summary>
		/// <returns></returns>
		public float GetDEF() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetDEF();
		}

		#endregion


		#region  【敏捷度的操作】
		
		/// <summary>
		/// 更新敏捷度（例如：当装备新武器时）
		/// </summary>
		public void UpdateDEX(float defByItem = 0) {
			if (defByItem > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().UpdateDEX(defByItem);
			}
		}

		/// <summary>
		/// 增加敏捷度
		/// /// </summary>
		/// <param name="addValue"></param>
		public void AddDEX(float addValue) {
			if (addValue > 0) {
				Mod_PlayerKernelDataProxy.GetInstance().AddDEX(addValue);
			}
		}

		/// <summary>
		/// 得到敏捷度
		/// </summary>
		/// <returns></returns>
		public float GetDEX() {
			return Mod_PlayerKernelDataProxy.GetInstance().GetDEX();
		}

		#endregion


		


		#region 【经验值处理】

		/// <summary>
		/// 增加经验值
		/// </summary>
		public void AddEXP(int addValue) {
			if (addValue > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddEXP(addValue);
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


		#region 【杀敌数量处理】

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


		#region 【等级处理】

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


		#region 【金币处理】

		/// <summary>
		/// 增加一定数量的金币
		/// </summary>
		/// <param name="number"></param>
		public void AddGold(int number) {
			if (number > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddGold(number);
			}
		}

		/// <summary>
		/// 减少一定数量的金币
		/// </summary>
		/// <param name="number"></param>
		/// <returns>金币是否足够</returns>
		public bool SubGold(int number) {
			if (number > 0) {
				return Mod_PlayerExtendedDataProxy.GetInstance().SubGold(number);
			}
			return false;
		}

		/// <summary>
		/// 得到当前的金币数量
		/// </summary>
		/// <returns></returns>
		public int GetGold() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetGold();
		}

		#endregion


		#region 钻石处理

		/// <summary>
		/// 增加一定数量的钻石
		/// </summary>
		/// <param name="number"></param>
		public void AddDiamond(int number) {
			if (number > 0) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddDiamond(number);
			}
		}


		/// <summary>
		/// 减少一定数量的钻石
		/// </summary>
		/// <param name="number"></param>
		/// <returns>钻石是否足够</returns>
		public bool SubDiamond(int number) {
			if (number > 0) {
				return Mod_PlayerExtendedDataProxy.GetInstance().SubDiamond(number);
			}
			return false;
		}

		/// <summary>
		/// 得到当前的钻石数量
		/// </summary>
		/// <returns></returns>
		public int GetDiamond() {
			return Mod_PlayerExtendedDataProxy.GetInstance().GetDiamond();
		}

		#endregion



		#region 【通用方法：增加一个道具的数量（应该存在最大数量）】

		/// <summary>
		/// 控制层：增加当某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <param name="number"></param>
		public void AddItemCount(ItemStruct item, int number) {
			if (number > 0) {
				Mod_PlayerPackageDataProxy.GetInstance().AddItemCount(item, number);
			}
		}

		/// <summary>
		/// 控制层：减少某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <param name="number"></param>
		/// <returns>是否可减</returns>
		public bool SubItemCount(ItemStruct item, int number) {
			if (number > 0) {
				return Mod_PlayerPackageDataProxy.GetInstance().SubItemCount(item, number);
			}
			return false;
		}

		/// <summary>
		/// 控制层：得到某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetItemCount(ItemStruct item) {
			return Mod_PlayerPackageDataProxy.GetInstance().GetItemCount(item);
		}

		#endregion


	}

}