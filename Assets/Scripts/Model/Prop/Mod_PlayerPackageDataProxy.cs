//模型层，玩家背包数据代理类（道具的数据）
//作用：
//封装核心背包数据，向外提供各种方法（增加，查找，查询等）

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Model {
	public class Mod_PlayerPackageDataProxy:Mod_PlayerPackageData {
		private static Mod_PlayerPackageDataProxy _Instance = null;	//本类的单例私有实例

		/// <summary>
		/// 本类的构造方法
		/// </summary>
		public Mod_PlayerPackageDataProxy(int weapon_1_Count, int shield_1_Count, int boot_1_Count, int hpPotion_1_Count, int mpPotion_1_Count) : base(weapon_1_Count, shield_1_Count, boot_1_Count, hpPotion_1_Count, mpPotion_1_Count) {
			if(_Instance == null) {
				_Instance = this; 
			}
			else {
				Debug.LogError(GetType() + "Mod_PlayerPackageDataProxy()/不允许构造函数的重复实例化！");
			}
		}

		/// <summary>
		/// 得到本类实例
		/// </summary>
		/// <returns></returns>
		public static Mod_PlayerPackageDataProxy GetInstance() {
			if (_Instance != null) {
				return _Instance;
			}
			else {
				Debug.LogWarning("Mod_PlayerPackageDataProxy.cs/GetInstance()/请先调用构造函数");
				return null;
			}
		}



		#region 【通用方法：增加一个道具的数量（存在最大数量）】

		/// <summary>
		/// 增加当某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <param name="number"></param>
		public void AddItemCount(ItemStruct item,int number) {
			item.Count += number;
		}

		/// <summary>
		/// 减少某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <param name="number"></param>
		/// <returns>是否可减</returns>
		public bool SubItemCount(ItemStruct item, int number) {
			//如果结果不为负数
			if (item.Count >= Mathf.Abs(number)) {
				item.Count -= Mathf.Abs(number);
				return true;
			}
			return false;

		}

		/// <summary>
		/// 得到某个道具的数量
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int GetItemCount(ItemStruct item) {
			return item.Count;
		}

		#endregion


		/*

		#region 【弱效生命药水的数量处理】

		/// <summary>
		/// 增加当前弱效生命药水的数量
		/// </summary>
		/// <param name="number"></param>
		public void AddHPPotion_1_Count(int number) {
			base.HPPotion_1_Count += number;
		}

		/// <summary>
		/// 减少当前弱效生命药水的数量
		/// </summary>
		/// <param name="number"></param>
		/// <returns>是否可减</returns>
		public bool SubHPPotion_1_Count(int number) {
			//如果结果不为负数
			if (base.HPPotion_1_Count >= Mathf.Abs(number)) {
				base.HPPotion_1_Count -= Mathf.Abs(number);
				return true;
			}
			return false;
		}

		/// <summary>
		/// 得到当前弱效生命药水的数量
		/// </summary>
		/// <returns></returns>
		public int GetHPPotion_1_Count() {
			return base.HPPotion_1_Count;
		}

		#endregion

		*/




	}
}
