//模型层，玩家背包数据（道具的数据）

//***优化：使用结构体或者某种集合、实例类来限定“一个物品”***
//***包括名称、价格、数量、效果描述等属性。***

//有必要使用构造方法吗？

//主体内容定义一个“物品”类需要的基础属性，在子类中添加特有的属性
//通过某个实例化物品类的方法，来实例化不同的物品

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Model {

	/// <summary>
	/// 结构体：每个物品
	/// </summary>
	public struct ItemStruct {
		private string _StrName;    //名字
		private ItemType _Type;	//类型
		private int _StrCount;      //数量

		/// <summary>
		/// 属性：姓名
		/// </summary>
		public string Name {
			get { return _StrName; }
			set { _StrName = value; }
		}
		/// <summary>
		/// 属性：类型
		/// </summary>
		public ItemType Type {
			get { return _Type; }
			set { _Type = value; }
		}
		/// <summary>
		/// 属性：名字
		/// </summary>
		public int Count {
			get { return _StrCount; }
			set {
				_StrCount = value;
				//事件调用
				if (Mod_PlayerPackageData.eve_PlayerPackageData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Count", Count);
					Mod_PlayerPackageData.eve_PlayerPackageData(kv,_StrName);
				}
			}
		}
	}



	public class Mod_PlayerPackageData {

		//定义事件（玩家的背包数据事件）
		public static del_PlayerItemModel eve_PlayerPackageData;

		/// <summary>
		/// 结构体：战斧
		/// </summary>
		public ItemStruct Item_Weapon_1 = new ItemStruct() {
			Name = "战斧",
			Type = ItemType.Weapon,
			Count = 0
		};
		/// <summary>
		/// 结构体：铁盾牌
		/// </summary>
		public ItemStruct Item_Shield_1 = new ItemStruct() {
			Name = "贴盾牌",
			Type = ItemType.Shield,
			Count = 0
		};
		/// <summary>
		/// 结构体：旅行靴
		/// </summary>
		public ItemStruct Item_Boot_1 = new ItemStruct() {
			Name = "旅行靴",
			Type = ItemType.Boot,
			Count = 0
		};
		/// <summary>
		/// 结构体：弱效生命药水
		/// </summary>
		public ItemStruct Item_HPPotion_1 = new ItemStruct() {
			Name = "弱效生命药水",
			Type = ItemType.NormalItem,
			Count = 0
		};
		/// <summary>
		/// 结构体：弱效魔法药水
		/// </summary>
		public ItemStruct Item_MPPotion_1 = new ItemStruct() {
			Name = "弱效魔法药水",
			Type = ItemType.NormalItem,
			Count = 0
		};


		//private string _StrHPPotion_1_Name;
		//private string _StrMPPotion_1_Name;

		//private int _IntHPPotion_1_Count;       //弱效生命药水的数量
		//private int _IntMPPotion_1_Count;       //弱效魔法药水的数量

		///// <summary>
		///// 属性：弱效生命药水的数量
		///// </summary>
		//public int HPPotion_1_Count {
		//	get { return _IntHPPotion_1_Count; }
		//	set { _IntHPPotion_1_Count = value;
		//		//事件调用
		//		if (eve_PlayerPackageData != null) {
		//			KeyValuesUpdate kv = new KeyValuesUpdate("HPPotion_1_Count", HPPotion_1_Count);
		//			eve_PlayerPackageData(kv);
		//		}
		//	}
		//}
		///// <summary>
		///// 属性：弱效魔法药水的数量
		///// </summary>
		//public int MPPotion_1_Count {
		//	get { return _IntMPPotion_1_Count; }
		//	set {
		//		_IntMPPotion_1_Count = value;
		//		//事件调用
		//		if (eve_PlayerPackageData != null) {
		//			KeyValuesUpdate kv = new KeyValuesUpdate("MPPotion_1_Count", MPPotion_1_Count);
		//			eve_PlayerPackageData(kv);
		//		}
		//	}
		//}



		/// <summary>
		/// 定义私有构造函数
		/// </summary>
		private Mod_PlayerPackageData() {}

		/* 这里应该是实例化作为一个道具的结构体，然后可能需要初始化Count属性 */

		/// <summary>
		/// 公共构造函数
		/// 如果属性过多，大概需要另想办法优化？
		/// </summary>
		/// <param name=""></param>
		public Mod_PlayerPackageData(int weapon_1_Count,int shield_1_Count,int boot_1_Count,int hpPotion_1_Count,int mpPotion_1_Count) {

			//this._IntHPPotion_1_Count = hpPotion_1_Count;
			//this._IntMPPotion_1_Count = mpPotion_1_Count;
			
			Item_Weapon_1.Count = weapon_1_Count;
			Item_Shield_1.Count = shield_1_Count;
			Item_Boot_1.Count = boot_1_Count;
			Item_HPPotion_1.Count = hpPotion_1_Count;
			Item_MPPotion_1.Count = mpPotion_1_Count;
		}





	}
}