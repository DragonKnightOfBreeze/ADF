//模型层，玩家的拓展数据
//模型层中都是定义的类，而非真正意义上的脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;
using Test;

namespace Model {
	public class Mod_PlayerExtendedData {
		//定义事件：玩家的扩展数值
		public static event del_PlayerKernalModel Eve_PlayerExtendedData;

		private int _IntEXP;		//经验值
		private int _IntKillNum;    //杀敌数量
		private int _IntLevel;		//当前等级
		private int _IntGold;		//金钱（肝帝用）
		private int _IntDiamond;    //钻石（氪金用）

		//设置属性（在属性的set方法里调用事件）
		public int EXP {
			get {
				return _IntEXP;
			}
			set {
				_IntEXP = value;
				//事件调用
				if(Eve_PlayerExtendedData!=null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("EXP",EXP);
					Eve_PlayerExtendedData(kv);
				}
			}
		}
		public int KillNum {
			get {
				return _IntKillNum;
			}

			set {
				_IntKillNum = value;
				//事件调用
				if (Eve_PlayerExtendedData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("KillNum", KillNum);
					Eve_PlayerExtendedData(kv);
				}
			}
		}
		public int Level {
			get {
				return _IntLevel;
			}

			set {
				_IntLevel = value;
				//事件调用
				if (Eve_PlayerExtendedData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Level", Level);
					Eve_PlayerExtendedData(kv);
				}
			}
		}
		public int Gold {
			get {
				return _IntGold;
			}

			set {
				_IntGold = value;
				//事件调用
				if (Eve_PlayerExtendedData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Gold", Gold);
					Eve_PlayerExtendedData(kv);
				}
			}
		}
		public int Diamond {
			get {
				return _IntDiamond;
			}

			set {
				_IntDiamond = value;
				//事件调用
				if (Eve_PlayerExtendedData != null) {
					KeyValuesUpdate kv = new KeyValuesUpdate("Diamond", Diamond);
					Eve_PlayerExtendedData(kv);
				}
			}
		}

		private Mod_PlayerExtendedData() { }

		/// <summary>
		/// 私有带参构造函数
		/// </summary>
		/// <param name="exp"></param>
		/// <param name="killNum"></param>
		/// <param name="level"></param>
		/// <param name="gold"></param>
		/// <param name="diamond"></param>
		public Mod_PlayerExtendedData(int exp, int killNum, int level, int gold, int diamond) {
			_IntEXP = exp;
			_IntKillNum = killNum;
			_IntLevel = level;
			_IntGold = gold;
			_IntDiamond = diamond;
		}
	}
}
