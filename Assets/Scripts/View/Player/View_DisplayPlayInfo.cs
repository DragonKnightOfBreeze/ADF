//视图层，显示玩家信息
//作用：显示玩家的各种信息

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Model;

namespace View {
	public class View_DisplayPlayInfo : MonoBehaviour {

		//屏幕上的信息显示
		public Text Txt_PlayerName; //玩家的姓名
		//Modify：
		public Text Txt_PlayerNameByDetail;	//详细面板的玩家姓名

		public Slider Sli_HP;		//玩机的生命值条
		public Slider Sli_MP;		//玩家的魔法值条

		public Text Txt_LevelByScreen;   //等级
		public Text Txt_HPByScreen;			//生命值
		public Text Txt_MPByScreen;			//魔法值
		public Text Txt_EXPByScreen;        //经验值
		public Text Txt_GoldByScreen;       //金币
		public Text Txt_DiamondByScreen;    //钻石

		//玩家详细信息
		public Text Txt_Level;	//等级
		public Text Txt_CurHP;		//当前生命值
		public Text Txt_MaxHP;		//最大生命值
		public Text Txt_CurMP;		//当前魔法值
		public Text Txt_MaxMP;		//最大魔法值
		public Text Txt_EXP;		//经验值

		public Text Txt_KillNum;	//杀敌数
		public Text Txt_Gold;		//金币
		public Text Txt_Diamond;	//钻石

		public Text Txt_ATK;	//攻击力（原始攻击力+物品攻击力）
		public Text Txt_DEF;	//防御力（同上）
		public Text Txt_DEX;    //敏捷度（同上）

		private void Awake() {

			//多播委托，核心数值事件注册
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayCurHP;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayCurMP;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayMaxHP;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayMaxMP;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayATK;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayDEF;
			Mod_PlayerKernelData.Eve_PlayerKernalData += DisplayDEX;

			//多播委托，扩展数值事件注册
			Mod_PlayerExtendedData.Eve_PlayerExtendedData += DisplayEXP;
			Mod_PlayerExtendedData.Eve_PlayerExtendedData += DisplayLevel;
			Mod_PlayerExtendedData.Eve_PlayerExtendedData += DisplayKillNum;
			Mod_PlayerExtendedData.Eve_PlayerExtendedData += DisplayGold;
			Mod_PlayerExtendedData.Eve_PlayerExtendedData += DisplayDiamond;
		}


		/// <summary>
		/// 有延迟的开始方法，必须要靠后执行
		/// </summary>
		/// <returns></returns>
		public IEnumerator Start() {
			yield return new  WaitForSeconds(GlobalParameter.WAIT_FOR_SECONDS_ON_START);

			//显示初始值
			Mod_PlayerKernelDataProxy.GetInstance().DisplayAllOriginalValues(); //调用显示所有初始数据方法
			Mod_PlayerExtendedDataProxy.GetInstance().DisplayAllOriginalValues(); //调用显示所有初始数据方法
		
			//玩家的姓名处理
			if (string.IsNullOrEmpty(GlobalParaMgr.PlayerName)) {
				Txt_PlayerName.text = GlobalParaMgr.PlayerName;
				Txt_PlayerNameByDetail.text = GlobalParaMgr.PlayerName; 
			}
				
		}



		#region 事件注册方法（在awake()方法中注册）

		/// <summary>
		/// 显示最大生命值
		/// </summary>
		/// <param name="kv"></param>
		void DisplayMaxHP(KeyValuesUpdate kv) {

			bool SingleControl = true;

			if (kv.Key.Equals("MaxHealth")) {
				if (Txt_HPByScreen && Txt_MaxHP) {
					Txt_HPByScreen.text = Txt_HPByScreen.text.Split('/')[0] + '/' + kv.Values.ToString();
					Txt_MaxHP.text = kv.Values.ToString();
					//滑动条处理
					Sli_HP.minValue = 0;
					Sli_HP.maxValue = (float)kv.Values;
					if (SingleControl) {
						Sli_HP.value = Sli_HP.maxValue;
						SingleControl = false;
					}
				}
			}
		}

		/// <summary>
		/// 显示当前生命值
		/// </summary>
		/// <param name="kv"></param>
		void DisplayCurHP(KeyValuesUpdate kv) {
			//如果实例的值属性与之相等
			if (kv.Key.Equals("Health")) {
				if(Txt_HPByScreen && Txt_CurHP) {
					Txt_HPByScreen.text = kv.Values.ToString() + '/' + Txt_HPByScreen.text.Split('/')[1]; 
					Txt_CurHP.text = kv.Values.ToString();
					//滑动条处理
					//必须先设最大值，再设当前值
					//这个算法有待改善
					Sli_HP.value = (float)kv.Values;
				}
			}
		}

		/// <summary>
		/// 显示最大魔法值
		/// </summary>
		/// <param name="kv"></param>
		void DisplayMaxMP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("MaxMana")) {
				if (Txt_MPByScreen && Txt_MaxMP) {
					Txt_MPByScreen.text = Txt_MPByScreen.text.Split('/')[0] + '/' + kv.Values.ToString();
					Txt_MaxMP.text = kv.Values.ToString();
					//滑动条处理
					Sli_MP.minValue = 0;
					Sli_MP.maxValue = (float)kv.Values;
				}
			}
		}

		/// <summary>
		/// 显示当前魔法值
		/// </summary>
		/// <param name="kv"></param>
		void DisplayCurMP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Mana")) {
				if (Txt_MPByScreen && Txt_CurMP) {
					Txt_MPByScreen.text = kv.Values.ToString() + '/' + Txt_MPByScreen.text.Split('/')[1];
					Txt_CurMP.text = kv.Values.ToString();
					//滑动条处理
					Sli_MP.value = (float)kv.Values;
					
				}
			}
		}

		

		/// <summary>
		/// 显示攻击力
		/// </summary>
		/// <param name="kv"></param>
		void DisplayATK(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Attack")) {
				Txt_ATK.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示防御力
		/// </summary>
		/// <param name="kv"></param>
		void DisplayDEF(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Defence")) {
				Txt_DEF.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示敏捷度
		/// </summary>
		/// <param name="kv"></param>
		void DisplayDEX(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Dexterity")) {
				Txt_DEX.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示当前经验值
		/// </summary>
		/// <param name="kv"></param>
		void DisplayEXP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("EXP")) {
				Txt_EXP.text = kv.Values.ToString();
				Txt_EXPByScreen.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示等级
		/// </summary>
		/// <param name="kv"></param>
		void DisplayLevel(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Level")) {
				Txt_Level.text = kv.Values.ToString();
				Txt_LevelByScreen.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示杀敌数
		/// </summary>
		/// <param name="kv"></param>
		void DisplayKillNum(KeyValuesUpdate kv) {
			if (kv.Key.Equals("KillNum")) {
				Txt_KillNum.text = kv.Values.ToString();
			}
		}  

		/// <summary>
		/// 显示当前金币数量
		/// </summary>
		/// <param name="kv"></param>
		void DisplayGold(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Gold")) {
				Txt_Gold.text = kv.Values.ToString();
				Txt_GoldByScreen.text = kv.Values.ToString();
			}
		}

		/// <summary>
		/// 显示当前钻石数量
		/// </summary>
		/// <param name="kv"></param>
		void DisplayDiamond(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Diamond")) {
				Txt_Diamond.text = kv.Values.ToString();
				Txt_DiamondByScreen.text = kv.Values.ToString();

			}
		}

		#endregion
	}
}