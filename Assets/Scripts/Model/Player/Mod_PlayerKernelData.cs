//模型层，玩家的核心数据
//模型层中都是定义的类，而非真正意义上的脚本
//当属性改变时，要自动在显示层里显示出来
//这些数据后期要持久化到XML

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Model {
	public class Mod_PlayerKernelData {
		//定一一个事件：玩家的核心数值事件
		public static event del_PlayerKernalModel eve_PlayerKernalData;


		private float _FloCurHP;       //生命值
		private float _FloCurMP;         //魔法值
		private float _FloATK;       //攻击力
		private float _FloDEF;      //防御力
		private float _FloDEX;    //敏捷度

		private float _FloMaxHP;    //以下为最大值
		private float _FloMaxMP;

		private float _FloTotalATK;  //以下为实际值（总的）
		private float _FloTotalDEF;
		private float _FloTotalDEX;

		private float _FloATKByItem = 0f; //物品（包括武器）的攻击力
		private float _FloDEFByItem = 0f;//物品（包括武器）的防御力
		private float _FloDEXByItem = 0f;//物品（包括武器）的敏捷度

		//Ctrl + 两次M，一切都清静了！
		//属性信息
		public float CurHP {
			get {
				return _FloCurHP;
			}
			set {
				_FloCurHP = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("CurHP", CurHP);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float CurMP {
			get {
				return _FloCurMP;
			}
			set {
				_FloCurMP = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("CurMP", CurMP);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float ATK {
			get {
				return _FloATK;
			}

			set {
				_FloATK = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("ATK", ATK);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float DEF {
			get {
				return _FloDEF;
			}
			set {
				_FloDEF = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DEF", DEF);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float DEX {
			get {
				return _FloDEX;
			}
			set {
				_FloDEX = value;
				//事件调用（在初始化数据，或者更新数据的时候）
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DEX", DEX);
					eve_PlayerKernalData(kv);
				}
			}
		}

		public float MaxHP {
			get {
				return _FloMaxHP;
			}
			set {
				_FloMaxHP = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("MaxHP", MaxHP);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float MaxMP {
			get {
				return _FloMaxMP;
			}
			set {
				_FloMaxMP = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("MaxMP", MaxMP);
					eve_PlayerKernalData(kv);
				}
			}
		}

		public float TotalATK {
			get {
				return _FloTotalATK;
			}
			set {
				_FloTotalATK = value;
			}
		}
		public float TotalDEF {
			get {
				return _FloTotalDEF;
			}
			set {
				_FloTotalDEF = value;
			}
		}
		public float TotalDEX {
			get {
				return _FloTotalDEX;
			}

			set {
				_FloTotalDEX = value;

			}
		}

		public float ATKByItem {
			get {
				return _FloATKByItem;
			}

			set {
				_FloATKByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("ATKByItem", ATKByItem);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float DEFByItem {
			get {
				return _FloDEFByItem;
			}
			set {
				_FloDEFByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DEFByItem", DEFByItem);
					eve_PlayerKernalData(kv);
				}
			}
		}
		public float DEXByItem {
			get {
				return _FloDEXByItem;
			}
			set {
				_FloDEXByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DEXByItem", DEXByItem);
					eve_PlayerKernalData(kv);
				}
			}
		}



		//定义私有构造函数
		private Mod_PlayerKernelData() { }

		//定义公共构造函数
		public Mod_PlayerKernelData(float curHP, float curMP, float atk,float def,float dex,float maxHP,float maxMP, float atkByItem,float defByItem,float dexByItem) {
			this._FloCurHP = curHP;
			this._FloCurMP = curMP;
			this._FloATK = atk;
			this._FloDEF = def;
			this._FloDEX = dex;
			this._FloMaxHP = maxHP;
			this._FloMaxMP = maxMP;
			this._FloATKByItem = atkByItem;
			this._FloDEFByItem = defByItem;
			this._FloDEXByItem = dexByItem;
		}

	}
}
