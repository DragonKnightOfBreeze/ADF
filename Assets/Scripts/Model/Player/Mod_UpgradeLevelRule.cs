//模型层，玩家的升级规则类
//描述项目中的“升级”的规则
//按照开发封闭原则和单一职责原则来定义本类的所有功能

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Model {
	public class Mod_UpgradeLevelRule {

		private static Mod_UpgradeLevelRule _Instance;	//本类实例

		private Mod_UpgradeLevelRule() { }

		/// <summary>
		/// 得到本类实例
		/// </summary>
		public static Mod_UpgradeLevelRule GetInstance() {
			if(_Instance ==null) {
				_Instance = new Mod_UpgradeLevelRule();
			}
			return _Instance;
		}

		/// <summary>
		/// 升级判定（达到多少经验时升级）
		/// </summary>
		/// <param name="experience">升级需要的经验值</param>
		public void LevelUpCheck(int experience) {
			int currentLevel = 1;		//当前等级

			//这个算法本身应该可以简化
			//这届算法不行。
			//后期可以使用XML配置文件设置
			if(experience >= 100 && experience <200 && currentLevel ==1) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
			else if (experience >=200 && experience <500 && currentLevel ==2) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
			else if(experience >= 200 && experience < 500 && currentLevel ==3) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
			else if (experience >= 200 && experience < 500 && currentLevel ==4) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
			else if (experience >= 200 && experience < 500) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
			else if (experience >= 200 && experience < 500 && currentLevel == 5) {
				Mod_PlayerExtendedDataProxy.GetInstance().AddLevel();
			}
		}


		/// <summary>
		/// 升级规则的总管理器（玩家属性的提升）
		/// 1：所有的核心最大数值的增加。
		/// 2：对应的核心数值当前值，加到最大数值。（升级时会回复HP和MP）
		/// </summary>
		public void LevelUpOperationMgr(LevelName levelName) {
			switch (levelName) {
				case LevelName.Level_1:
					//定义一个方法来处理这些
					//具体的数据后期应该存储到XML设置文档中。
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_2:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_3:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_4:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_5:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_6:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_7:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_8:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_9:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
				case LevelName.Level_10:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;

				default:
					LevelUpOperation(10, 10, 2, 1, 10);
					break;
			}

		}

		/// <summary>
		/// 具体的升级规则
		/// </summary>
		/// <param name="maxhp">最大生命值增量</param>
		/// <param name="maxmp">最大魔法值增量</param>
		/// <param name="atk">攻击力增量</param>
		/// <param name="def">防御力增量</param>
		/// <param name="dex">敏捷度增量</param>
		public void LevelUpOperation(float maxhp,float maxmp,float atk,float def,float dex) {

			//所有的核心最大数值增加
			Mod_PlayerKernelDataProxy.GetInstance().AddMaxHP(maxhp);
			Mod_PlayerKernelDataProxy.GetInstance().AddMaxMP(maxmp);

			//对应的当前数值增加到最大数值
			//有必要用Get系列方法吗？
			Mod_PlayerKernelDataProxy.GetInstance().AddCurHP(Mod_PlayerKernelDataProxy.GetInstance().GetMaxHP());
			Mod_PlayerKernelDataProxy.GetInstance().AddCurMP(Mod_PlayerKernelDataProxy.GetInstance().GetMaxMP());

			//其他核心数值增加
			Mod_PlayerKernelDataProxy.GetInstance().AddATK(atk);
			Mod_PlayerKernelDataProxy.GetInstance().AddDEF(def);
			Mod_PlayerKernelDataProxy.GetInstance().AddDEX(dex);
		}
	}
}