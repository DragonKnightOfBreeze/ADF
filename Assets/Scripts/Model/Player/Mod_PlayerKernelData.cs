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
		public static event del_PlayerKernalModel Eve_PlayerKernalData;


		private float _FloHealth;       //生命值
		private float _FloMana;         //魔法值
		private float _FloAttack;       //攻击力
		private float _FloDefence;      //防御力
		private float _FloDexterity;    //敏捷度

		private float _FloMaxHealth;    //以下为最大值
		private float _FloMaxMana;

		private float _FloTotalAttack;  //以下为实际值（总的）
		private float _FloTotalDefence;
		private float _FloTotalDexterity;

		private float _FloAttackByItem = 0f; //物品（包括武器）的攻击力
		private float _FloDefenceByItem = 0f;//物品（包括武器）的防御力
		private float _FloDexterityByItem = 0f;//物品（包括武器）的敏捷度

		//Ctrl + 两次M，一切都清静了！
		//属性信息
		public float Health {
			get {
				return _FloHealth;
			}
			set {
				_FloHealth = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Health", Health);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float Mana {
			get {
				return _FloMana;
			}

			set {
				_FloMana = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Mana", Mana);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float Attack {
			get {
				return _FloAttack;
			}

			set {
				_FloAttack = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Attack", Attack);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float Defence {
			get {
				return _FloDefence;
			}

			set {
				_FloDefence = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Defence", Defence);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float Dexterity {
			get {
				return _FloDexterity;
			}

			set {
				_FloDexterity = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Dexterity", Dexterity);
					Eve_PlayerKernalData(kv);
				}
			}
		}

		public float MaxHealth {
			get {
				return _FloMaxHealth;
			}

			set {
				_FloMaxHealth = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("MaxHealth", MaxHealth);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float MaxMana {
			get {
				return _FloMaxMana;
			}

			set {
				_FloMaxMana = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("MaxMana", MaxMana);
					Eve_PlayerKernalData(kv);
				}
			}
		}

		public float TotalAttack {
			get {
				return _FloTotalAttack;
			}
			set {
				_FloTotalAttack = value;
			}
		}
		public float TotalDefence {
			get {
				return _FloTotalDefence;
			}

			set {
				_FloTotalDefence = value;
			}
		}
		public float TotalDexterity {
			get {
				return _FloTotalDexterity;
			}

			set {
				_FloTotalDexterity = value;

			}
		}

		public float AttackByItem {
			get {
				return _FloAttackByItem;
			}

			set {
				_FloAttackByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("AttackByItem", AttackByItem);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float DefenceByItem {
			get {
				return _FloDefenceByItem;
			}

			set {
				_FloDefenceByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DefenceByItem", DefenceByItem);
					Eve_PlayerKernalData(kv);
				}
			}
		}
		public float DexterityByItem {
			get {
				return _FloDexterityByItem;
			}

			set {
				_FloDexterityByItem = value;
				//事件调用
				//（先构造键值对更新类，然后调用带有此类实例的参数的事件）
				if (Eve_PlayerKernalData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("DexterityByItem", DexterityByItem);
					Eve_PlayerKernalData(kv);
				}
			}
		}



		//定义私有构造函数
		private Mod_PlayerKernelData() { }

		//定义公共构造函数
		public Mod_PlayerKernelData(float health, float mana, float attack,float defence,float dexterity,float maxHealth,float maxMana, float attackByItem,float defenceByItem,float dexterityByItem) {
			this._FloHealth = health;
			this._FloMana = mana;
			this._FloAttack = attack;
			this._FloDefence = defence;
			this._FloDexterity = dexterity;
			this._FloMaxHealth = maxHealth;
			this._FloMaxMana = maxMana;
			this._FloAttackByItem = attackByItem;
			this._FloDefenceByItem = defenceByItem;
			this._FloDexterityByItem = dexterityByItem;
		}

	}
}
