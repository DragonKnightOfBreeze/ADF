//控制层，主城场景，商城面板控制
//相关后台逻辑的实现

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;
using Model;

namespace Control {
	public class Ctrl_MarketPanel :BaseControl{
		public static Ctrl_MarketPanel Instance;    //本脚本的单例实例

		private void Awake() {
			Instance = this;
		}

		//说明：
		//需要对接AppStore的收费SDK，进行实际玩家账户的扣费。
		//需要特别注意返回值。
		//这个功能只做示范

		/// <summary>
		/// 购买：10个钻石（充值）
		/// </summary>
		/// <returns>true：购买成功</returns>
		public bool Pay_Diamonds() {
			//逻辑：如果玩家充值成功，玩家的钻石+10
			//（省略真正的充值步骤）
			Ctrl_HeroProperty.Instance.AddDiamond(10);
			return true;
		}

		/// <summary>
		/// 购买：100金币
		/// </summary>
		/// <returns>true：购买成功</returns>
		public bool Buy_Golds() {
			//逻辑：玩家的钻石-1，如果成功，玩家的金币+100
			bool boolFlag = Ctrl_HeroProperty.Instance.SubDiamond(1);
			if (boolFlag) {
				Ctrl_HeroProperty.Instance.AddGold(100);
				return true;
			}
			return false;
		}
		
		//购买：武器道具1：战斧（180G）
		public bool Buy_Weapon_1() {
			//逻辑：玩家的金币-180，如果成功，玩家得到1个战斧
			bool boolFlag = Ctrl_HeroProperty.Instance.SubGold(180);
			if (boolFlag) {
				Ctrl_HeroProperty.Instance.AddItemCount(Mod_PlayerPackageDataProxy.GetInstance().Item_Weapon_1,1);
				return true;
			}
			return false;
		}

		//购买：盾牌道具1：铁盾牌（120G）
		public bool Buy_Shield_1() {
			//逻辑：玩家的金币-120，如果成功，玩家得到1个铁盾牌
			bool boolFlag = Ctrl_HeroProperty.Instance.SubGold(120);
			if (boolFlag) {
				Ctrl_HeroProperty.Instance.AddItemCount(Mod_PlayerPackageDataProxy.GetInstance().Item_Shield_1, 1);
				return true;
			}
			return false;
		}

		//购买：靴子道具1：旅行靴（120G）
		public bool Buy_Boot_1() {
			//逻辑：玩家的金币-120，如果成功，玩家得到1个旅行靴
			bool boolFlag = Ctrl_HeroProperty.Instance.SubGold(120);
			if (boolFlag) {
				Ctrl_HeroProperty.Instance.AddItemCount(Mod_PlayerPackageDataProxy.GetInstance().Item_Boot_1, 1);
				return true;
			}
			return false;
		}

		//购买：生命药水道具1：弱效生命药水（80G）
		public bool Buy_HPPotion_1() {
			//逻辑：玩家的金币-80，如果成功，玩家得到1个弱效生命药水
			bool boolFlag = Ctrl_HeroProperty.Instance.SubGold(80);
			if (boolFlag) {
				//需要编写专门的“背包系统”的模型层
				Mod_PlayerPackageDataProxy.GetInstance().AddItemCount(Mod_PlayerPackageDataProxy.GetInstance().Item_HPPotion_1, 1);
				////需要改进的原本写法
				//Mod_PlayerPackageDataProxy.GetInstance().AddHPPotion_1_Count(1);
				return true;
			}
			return false;
		}

		//购买：魔法药水道具1：弱效魔法药水（60G）
		public bool Buy_MPPotion_1() {
			//逻辑：玩家的金币-60，如果成功，玩家得到1个弱效魔法药水
			bool boolFlag = Ctrl_HeroProperty.Instance.SubGold(60);
			if (boolFlag) {
				Mod_PlayerPackageDataProxy.GetInstance().AddItemCount(Mod_PlayerPackageDataProxy.GetInstance().Item_MPPotion_1, 1);
				return true;
			}
			return false;
		}
	}
}
