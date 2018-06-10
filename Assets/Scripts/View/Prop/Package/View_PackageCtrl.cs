//视图层，背包系统控制（装备系统控制）
//作用：
//根据背包系统模型层后台的数据，显示背包系统的道具

//***改进：不是显示，而是直接创造游戏对象，并且确定在装备栏中要显示的位置***
//写一个这样的脚本真的好吗？

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Model;
using Global;
using Kernel;

namespace View {
	class View_PackageCtrl :MonoBehaviour{
		//道具对象
		public GameObject goItem_Weapon_1;
		public GameObject goItem_Shield_1;
		public GameObject goItem_Boot_1;
		public GameObject goItem_HPPotion_1;
		public GameObject goItem_MPPotion_1;

		//道具数量
		public Text Txt_HPPotion_1_Count;
		public Text Txt_MPPotion_1_Count;

		/// <summary>
		/// 如果不使用静态结构体字段，必须使用有延迟的方法
		/// </summary>
		/// <returns></returns>
		IEnumerator Start () {
			yield return new WaitForSeconds(1f);
			//事件注册（注册背包系统的后台）
			Mod_PlayerPackageData.eve_PlayerPackageData += DisplayWeapon_1;
			Mod_PlayerPackageData.eve_PlayerPackageData += DisplayShield_1;
			Mod_PlayerPackageData.eve_PlayerPackageData += DisplayBoot_1;
			Mod_PlayerPackageData.eve_PlayerPackageData += DisplayHPPotion_1;
			Mod_PlayerPackageData.eve_PlayerPackageData += DisplayMPPotion_1;
		}


		#region 【道具的具体显示方法】

		/// <summary>
		/// 显示战斧
		/// </summary>
		/// <param name="kv"></param>
		public void DisplayWeapon_1(KeyValuesUpdate kv, string itemName="") {
			//如果匹配道具名字，键值对的键匹配道具数量
			if (itemName == Mod_PlayerPackageDataProxy.GetInstance().Item_Weapon_1.Name && kv.Key.Equals("Count")) {
				//非空游戏对象判断
				if (goItem_Weapon_1) {
					//如果道具数量大于等于1，则显示道具
					if (Convert.ToInt32(kv.Values) >= 1) {
						goItem_Weapon_1.SetActive(true);
					}
				}
			}
		}

		/// <summary>
		/// 显示铁盾牌
		/// </summary>
		/// <param name="kv"></param>
		public void DisplayShield_1(KeyValuesUpdate kv, string itemName = "") {
			//如果匹配道具名字，键值对的键匹配道具数量
			if (itemName == Mod_PlayerPackageDataProxy.GetInstance().Item_Shield_1.Name && kv.Key.Equals("Count")) {
				//非空游戏对象判断
				if (goItem_Shield_1) {
					//如果道具数量大于等于1，则显示道具
					if (Convert.ToInt32(kv.Values) >= 1) {
						goItem_Shield_1.SetActive(true);
					}
				}
			}
		}

		/// <summary>
		/// 显示旅行靴
		/// </summary>
		/// <param name="kv"></param>
		public void DisplayBoot_1(KeyValuesUpdate kv, string itemName = "") {
			//如果匹配道具名字，键值对的键匹配道具数量
			if (itemName == Mod_PlayerPackageDataProxy.GetInstance().Item_Boot_1.Name && kv.Key.Equals("Count")) {
				//非空游戏对象判断
				if (goItem_Boot_1) {
					//如果道具数量大于等于1，则显示道具
					if (Convert.ToInt32(kv.Values) >= 1) {
						goItem_Boot_1.SetActive(true);
					}
				}
			}
		}

		/// <summary>
		/// 显示血瓶和血瓶数量
		/// </summary>
		/// <param name="kv"></param>
		public void DisplayHPPotion_1(KeyValuesUpdate kv, string itemName = "") {
			//if(kv.Key.Equals("HPPotion_1_Count")) { 
			//如果匹配道具名字，键值对的键匹配道具数量
			if(itemName == Mod_PlayerPackageDataProxy.GetInstance().Item_HPPotion_1.Name && kv.Key.Equals("Count") ) {
				//非空游戏对象判断
				if(goItem_HPPotion_1 && Txt_HPPotion_1_Count) {
					//如果道具数量大于等于1，则显示道具
					if(Convert.ToInt32(kv.Values) >= 1) {
						goItem_HPPotion_1.SetActive(true);	
						//显示道具数量
						Txt_HPPotion_1_Count.text = kv.Values.ToString();	
					}
				}
			}
		}

		/// <summary>
		/// 显示蓝瓶和蓝瓶数量
		/// </summary>
		/// <param name="kv"></param>
		public void DisplayMPPotion_1(KeyValuesUpdate kv, string itemName = "") {
			//如果匹配道具名字，键值对的键匹配道具数量
			if (itemName == Mod_PlayerPackageDataProxy.GetInstance().Item_MPPotion_1.Name && kv.Key.Equals("Count")) {
				//非空游戏对象判断
				if (goItem_MPPotion_1 && Txt_MPPotion_1_Count) {
					//如果道具数量大于等于1，则显示道具
					if (Convert.ToInt32(kv.Values) >= 1) {
						goItem_MPPotion_1.SetActive(true);  
						//显示道具数量
						Txt_MPPotion_1_Count.text = kv.Values.ToString();
					}
				}
			}
		}

		#endregion



		#region 【+++道具的通用显示方法++】

		//public GameObject goItem;
		//public Text Txt_Item_Count;

		///// <summary>
		///// 显示道具和道具数量
		///// 对应的道具的结构体，可能需要专门的脚本去定值
		///// </summary>
		///// <param name="itemStruct">对应的道具的结构体</param>
		///// <param name="kv"></param>
		//public void DisplayItem(ItemStruct itemStruct, KeyValuesUpdate kv) {
		//	//如果匹配键值对的键匹配道具数量
		//	if (kv.Key.Equals("Count")) {
		//		//非空游戏对象判断
		//		if (goItem && Txt_Item_Count) {
		//			//如果道具数量大于等于1，则显示道具
		//			if (Convert.ToInt32(kv.Values) >= 1) {
		//				goItem.SetActive(true);

		//				//如果为普通道具，则显示道具数量
		//				if (itemStruct.Type == ItemType.NormalItem) {
		//					Txt_Item_Count.gameObject.SetActive(true);
		//					Txt_Item_Count.text = kv.Values.ToString();
		//				}
		//			}
		//		}
		//	}
		//}

		#endregion

		
	}
}
