//模型层，玩家的核心数据
//模型层中都是定义的类，而非真正意义上的脚本

//这些数据后期要持久化到XML

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
	public class Mod_PlayerKernelData {

		private float _FloHealth;       //生命值
		private float _FloMana;         //魔法值
		private float _FloAttack;       //攻击力
		private float _FloDefence;      //防御力
		private float _FloDexterity;    //敏捷度

		private float _FloMaxHealth;	//以下为最大值
		private float _FloMaxMana;
		private float _FloTotalAttack;	//以下为实际值（总的）
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
			}
		}
		public float Mana {
			get {
				return _FloMana;
			}

			set {
				_FloMana = value;
			}
		}
		public float Attack {
			get {
				return _FloAttack;
			}

			set {
				_FloAttack = value;
			}
		}
		public float Defence {
			get {
				return _FloDefence;
			}

			set {
				_FloDefence = value;
			}
		}
		public float Dexterity {
			get {
				return _FloDexterity;
			}

			set {
				_FloDexterity = value;
			}
		}

		public float MaxHealth {
			get {
				return _FloMaxHealth;
			}

			set {
				_FloMaxHealth = value;
			}
		}
		public float MaxMana {
			get {
				return _FloMaxMana;
			}

			set {
				_FloMaxMana = value;
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
			}
		}
		public float DefenceByItem {
			get {
				return _FloDefenceByItem;
			}

			set {
				_FloDefenceByItem = value;
			}
		}
		public float DexterityByItem {
			get {
				return _FloDexterityByItem;
			}

			set {
				_FloDexterityByItem = value;
			}
		}



		//定义私有构造函数
		private Mod_PlayerKernelData() { }

		//定义公共构造函数
		public Mod_PlayerKernelData(float health, float mana, float attack,float defence,float dexterity,float maxHealth,float maxMana,float maxAttack,float maxDefence,float maxDexterity, float attackByItem,float defenceByItem,float dexterityByItem) {
			this._FloHealth = health;
			this._FloMana = mana;
			this._FloAttack = attack;
			this._FloDefence = defence;
			this._FloDexterity = dexterity;
			this._FloMaxHealth = maxHealth;
			this._FloMaxMana = maxMana;
			this._FloTotalAttack = maxAttack;
			this._FloTotalDefence = maxDefence;
			this._FloTotalDexterity = maxDexterity;
			this._FloAttackByItem = attackByItem;
			this._FloDefenceByItem = defenceByItem;
			this._FloDexterityByItem = dexterityByItem;
		}

	}
}
