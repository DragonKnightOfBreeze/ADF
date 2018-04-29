//模型层，玩家的拓展数据
//模型层中都是定义的类，而非真正意义上的脚本

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model {
	public class Mod_PlayerExtendedData {
		private int _IntEXP;		//经验值
		private int _IntKillNum;    //杀敌数量
		private int _IntLevel;		//当前等级
		private int _IntGold;		//金钱（肝帝用）
		private int _IntDiamand;    //钻石（氪金用）

		//设置属性
		public int EXP {
			get {
				return _IntEXP;
			}

			set {
				_IntEXP = value;
			}
		}
		public int KillNum {
			get {
				return _IntKillNum;
			}

			set {
				_IntKillNum = value;
			}
		}
		public int Level {
			get {
				return _IntLevel;
			}

			set {
				_IntLevel = value;
			}
		}
		public int Gold {
			get {
				return _IntGold;
			}

			set {
				_IntGold = value;
			}
		}
		public int Diamand {
			get {
				return _IntDiamand;
			}

			set {
				_IntDiamand = value;
			}
		}
	}
}
