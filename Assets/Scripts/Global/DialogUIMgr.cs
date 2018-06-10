//公共层，通用对话UI管理器
//对话模块中最上层的脚本
//只要在加载场景对应的脚本中，将其他三个脚本的对应加载和初始化语句写好，
//就可以在其他脚本中，使用DialogUIMgr.Instance.DisplayNextDialog方法
//以在对话页面预置体中，显示正确的下一条语句

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

namespace Global {

	/// <summary>
	/// 对话类型
	/// </summary>
	enum DialogType {
		None,
		Single,
		Double
	}



	class DialogUIMgr:MonoBehaviour {
		public static DialogUIMgr Instance;       //本类实例（单例）


		//UI对象
		public GameObject goHero;			//英雄
		public GameObject goNPC_Left;		//左边的NPC
		public GameObject goNPC_Right;		//右边的NPC
		public GameObject goSingleDialogArea;   //单人对话区域
		public GameObject goDoubleDialogArea;   //双人对话区域

		//对话显示控件
		public Text Txt_PersonName;     //人物名称
		public Text Txt_SingleDialogContent;		//单人对话系统
		public Text Txt_DoubleDialogContent;        //双人对话内容

		//精灵资源数组（静态的）
		//（规定，0下标表示彩色精灵，1下标表示黑白精灵）
		//+++++++ 拓展：动态的精灵资源（一组图片/一组动画）
		public Sprite[] Spr_Hero;		//英雄的精灵资源
		public Sprite[] Spr_NPC_Left;   //左边NPC的精灵资源
		public Sprite[] Spr_NPC_Right;	//

		void Awake() {
			Instance = this;
		}



		/// <summary>
		/// 公共方法：显示下一条对话，返回真表示对话结束
		/// </summary>
		/// <param name="diaType"></param>
		/// <param name="SectionNum"></param>
		/// <returns>
		/// true: 对话结束
		/// </returns>
		public bool DisplayNextDialog(DialogType diaType,int SectionNum) {
			bool isDialogEnd = false;		//会话是否结束
			DialogSide diaSide = DialogSide.None;       //正在说话的一方
			string strPerson;       //讲述者
			string strContent;		//对话内容


			//切换（选择）对话类型
			ChangeDialogType(diaType);

			//得到会话信息（数据）
			bool flag = DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(SectionNum, out diaSide, out strPerson, out strContent);
			if(flag) {
				//显示对话信息
				DisplayDialogInfo(diaType, diaSide, strPerson, strContent);
			}
			else {
				//对话结束（没有对话信息了）
				isDialogEnd = true;
			}

			return isDialogEnd;
		}


		/// <summary>
		/// 切换（选择）对话类型
		/// </summary>
		/// <param name="diaType">对话类型（单人/双人）</param>
		private void ChangeDialogType(DialogType diaType) {
			switch (diaType) {
				case DialogType.None:
					goHero.SetActive(false);
					goNPC_Left.SetActive(false);
					goNPC_Right.SetActive(false);
					goSingleDialogArea.SetActive(false);
					goDoubleDialogArea.SetActive(false);
					break;

				//单人对话
				case DialogType.Single:
					goHero.SetActive(false);
					goNPC_Left.SetActive(true);
					goNPC_Right.SetActive(false);
					goSingleDialogArea.SetActive(true);
					goDoubleDialogArea.SetActive(false);
					break;

				//双人对话
				case DialogType.Double:
					goHero.SetActive(true);
					goNPC_Left.SetActive(false);
					goNPC_Right.SetActive(true);
					goSingleDialogArea.SetActive(false);
					goDoubleDialogArea.SetActive(true);
					break;

				default:
					goHero.SetActive(false);
					goNPC_Left.SetActive(false);
					goNPC_Right.SetActive(false);
					goSingleDialogArea.SetActive(false);
					goDoubleDialogArea.SetActive(false);
					break;
			}
		}



		/// <summary>
		/// 显示对话信息
		/// </summary>
		/// <param name="diaType">对话类型（单人/双人）</param>
		/// <param name="diaSide">对话双方</param>
		/// <param name="strPerson">对话人名</param>
		/// <param name="strContent">对话内容</param>
		private void DisplayDialogInfo(DialogType diaType,DialogSide diaSide,string strPerson,string strContent) {
			
			switch (diaType) {
				case DialogType.None:
					break;

				case DialogType.Single:
					//显示人名
					//if (!string.IsNullOrEmpty(strPerson) && !string.IsNullOrEmpty(strContent)) {
						//Txt_PersonName.text = strPerson;
						Txt_SingleDialogContent.text = strContent;
					//}
					break;

				case DialogType.Double:
					//显示人名，对话信息
					if (!string.IsNullOrEmpty(strPerson) && !string.IsNullOrEmpty(strContent)) {
						//Modify：使英雄的名字正确显示
						if (diaSide == DialogSide.HeroSide) {
							Txt_PersonName.text = GlobalParaMgr.PlayerName;
						}
						Txt_PersonName.text = strPerson;
						Txt_DoubleDialogContent.text = strContent;
					}

					//确定显示精灵（彩色为正在说话者，灰色为旁听者）
					switch (diaSide) {
						case DialogSide.None:
							break;
						case DialogSide.HeroSide:
							goHero.GetComponent<Image>().overrideSprite = Spr_Hero[0];	  //0表示彩色
							goNPC_Right.GetComponent<Image>().overrideSprite = Spr_NPC_Right[1];		//1表示黑白
							break;
						case DialogSide.NPCSide:
							goHero.GetComponent<Image>().overrideSprite = Spr_Hero[1];    //1表示彩色
							goNPC_Right.GetComponent<Image>().overrideSprite = Spr_NPC_Right[0];        //0表示黑白
							break;

						default:
							break;
					}
					break;

				default:
					break;
			}
		}
	}
}
