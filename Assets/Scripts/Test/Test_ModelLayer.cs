//测试类，测试模型数据使用

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Model;

namespace Test {
	public class Test_ModelLayer : MonoBehaviour {

		//核心数值
		public Text Txt_HP;
		public Text Txt_MP;
		public Text Txt_MaxHP;
		public Text Txt_MaxMP;
		public Text Txt_ATK;
		public Text Txt_DEF;
		public Text Txt_DEX;

		//扩展数值
		public Text Txt_EXP;
		public Text Txt_Level;
		public Text Txt_KillNum;
		public Text Txt_Gold;
		public Text Txt_Diamond;


		private void Awake() {
			//多播委托，核心数值事件注册
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayHP;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayMP;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayMaxHP;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayMaxMP;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayATK;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayDEF;
			Mod_PlayerKernelData.eve_PlayerKernalData += DisplayDEX;

			//多播委托，扩展数值事件注册
			Mod_PlayerExtendedData.eve_PlayerExtendedData += DisplayEXP;
			Mod_PlayerExtendedData.eve_PlayerExtendedData += DisplayLevel;							  
			Mod_PlayerExtendedData.eve_PlayerExtendedData += DisplayKillNum;							
			Mod_PlayerExtendedData.eve_PlayerExtendedData += DisplayGold;
			Mod_PlayerExtendedData.eve_PlayerExtendedData += DisplayDiamond;
		}

		// Use this for initialization
		void Start() {

			Mod_PlayerKernelDataProxy playerKernelDataObj = new Mod_PlayerKernelDataProxy(100, 100, 10, 5, 50,100,100 ,0,0,0);
			Mod_PlayerExtendedDataProxy playerExtendedDataObj = new Mod_PlayerExtendedDataProxy(0, 0, 1, 0, 0);


			//显示初始值
			Mod_PlayerKernelDataProxy.GetInstance().DisplayAllOriginalValues(); //调用显示所有初始数据方法
			Mod_PlayerExtendedDataProxy.GetInstance().DisplayAllOriginalValues(); //调用显示所有初始数据方法
		}


		#region 事件用户点击（在按钮点击事件中注册）
		
		public void InHP() {
			//调用模型层的方法
			// // Debug.Log("调用了InHP方法");
			Mod_PlayerKernelDataProxy.GetInstance().AddCurHP(30);
		}

		public void DeHP() {
			//调用模型层的方法
			Mod_PlayerKernelDataProxy.GetInstance().SubCurHP(20);
		}

		public void InMP() {
			//调用模型层的方法
			Mod_PlayerKernelDataProxy.GetInstance().AddCurMP(60);

		}

		public void DeMP() {
			//调用模型层的方法
			Mod_PlayerKernelDataProxy.GetInstance().SubCurMP(60);
		}


		public void AddEXP() {
			Mod_PlayerExtendedDataProxy.GetInstance().AddEXP(30);
		}

		#endregion
		 
		#region 事件注册方法（在awake()方法中注册）

		void DisplayHP(KeyValuesUpdate kv) {
			//如果实例的值属性与之相等
			if (kv.Key.Equals("Health")) {
				Txt_HP.text = kv.Values.ToString();
			}
		}
		void DisplayMaxHP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("MaxHealth")) {
				Txt_MaxHP.text = kv.Values.ToString();
			}
		}
		void DisplayMP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Mana")) {
				Txt_MP.text = kv.Values.ToString();
			}
		}
		void DisplayMaxMP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("MaxMana")) {
				Txt_MaxMP.text = kv.Values.ToString();
			}
		}
		void DisplayATK(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Attack")) {
				Txt_ATK.text = kv.Values.ToString();
			}
		}
		void DisplayDEF(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Defence")) {
				Txt_DEF.text = kv.Values.ToString();
			}
		}
		void DisplayDEX(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Dexterity")) {
				Txt_DEX.text = kv.Values.ToString();
			}
		}


		void DisplayEXP(KeyValuesUpdate kv) {
			if (kv.Key.Equals("EXP")) {
				Txt_EXP.text = kv.Values.ToString();
			}
		}

		void DisplayLevel(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Level")) {
				Txt_Level.text = kv.Values.ToString();
			}
		}

		void DisplayKillNum(KeyValuesUpdate kv) {
			if (kv.Key.Equals("KillNum")) {
				Txt_KillNum.text = kv.Values.ToString();
			}
		}

		void DisplayGold(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Gold")) {
				Txt_Gold.text = kv.Values.ToString();
			}
		}

		void DisplayDiamond(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Diamond")) {
				Txt_Diamond.text = kv.Values.ToString();
			}
		}

		#endregion


	}
}