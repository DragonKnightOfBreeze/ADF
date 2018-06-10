//视图层，主城场景，商城面板，内购系统

//***待优化：使用XML文档存储需要持久化的信息***

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;
using Control;

namespace View {
	public class View_MarketPanel:MonoBehaviour {

		//不如只定义对应的空对象的公共引用，然后通过查找子游戏对象来进行各种操作

		/* 道具说明显示 */

		public Text Txt_Diamonds;		//10个钻石
		public Text Txt_Golds;			//100金币
		public Text Txt_Weapon_1;		//武器道具1：战斧
		public Text Txt_Shield_1;		//盾牌道具2：铁盾牌
		public Text Txt_Boot_1;			//靴子道具3：旅行靴
		public Text Txt_HPPotion_1;		//生命药水道具1：弱效生命药水
		public Text Txt_MPPotion_1;		//魔法药水道具1：弱效魔法药水

		//可改进：点击道具图像也会显示提示信息。

		/* 响应的按键 */

		public Button Btn_Diamonds;		//10个钻石
		public Button Btn_Golds;		//100金币
		public Button Btn_Weapon_1;		//武器道具1：战斧
		public Button Btn_Shield_1;		//盾牌道具2：铁盾牌
		public Button Btn_Boot_1;		//靴子道具3：旅行靴
		public Button Btn_HPPotion_1;	//生命药水道具1：弱效生命药水
		public Button Btn_MPPotion_1;	//魔法药水道具1：弱效魔法药水

		/* 具体道具的文字说明 */
		//public Text Txt_ItemName;
		public Text Txt_ItemDesc;


		private const string PAY_SUCCESS = "充值成功！祝你游戏愉快！";
		private const string PAY_FAILURE = "充值失败，请联系运营人员！";

		private void Awake() {
			//注册相关事件
			RigisterTxtAndBtn();
		}



		/// <summary>
		/// 注册相关文本和按钮
		/// </summary>
		private void RigisterTxtAndBtn() {
			/* 文字的注册 */
			if(Txt_Diamonds != null) {
				EventTriggerListener.Get(Txt_Diamonds.gameObject).onClick += Display_Diamonds;
			}
			if (Txt_Golds != null) {
				EventTriggerListener.Get(Txt_Golds.gameObject).onClick += Display_Golds;
			}
			if (Txt_Weapon_1 != null) {
				EventTriggerListener.Get(Txt_Weapon_1.gameObject).onClick += Display_Weapon_1;
			}
			if (Txt_Shield_1!= null) {
				EventTriggerListener.Get(Txt_Shield_1.gameObject).onClick += Display_Shield_1;
			}
		
			if (Txt_Boot_1 != null) {
				EventTriggerListener.Get(Txt_Boot_1.gameObject).onClick += Display_Boot_1;
			}
			if (Txt_HPPotion_1!= null) {
				EventTriggerListener.Get(Txt_HPPotion_1.gameObject).onClick += Display_HPPotion_1;
			}
			if (Txt_MPPotion_1 != null) {
				EventTriggerListener.Get(Txt_MPPotion_1.gameObject).onClick += Display_MPPotion_1;
			}

			/* 按钮的注册 */

			if (Btn_Diamonds != null) {
				EventTriggerListener.Get(Btn_Diamonds.gameObject).onClick += Pay_Diamonds;
			}
			if (Btn_Golds != null) {
				EventTriggerListener.Get(Btn_Golds.gameObject).onClick += Buy_Golds;
			}
			if (Btn_Weapon_1 != null) {
				EventTriggerListener.Get(Btn_Weapon_1.gameObject).onClick += Buy_Weapon_1;
			}
			if (Btn_Shield_1 != null) {
				EventTriggerListener.Get(Btn_Shield_1.gameObject).onClick += Buy_Shield_1;
			}
			if (Btn_Boot_1 != null) {
				EventTriggerListener.Get(Btn_Boot_1.gameObject).onClick += Buy_Boot_1;
			}
			if (Btn_HPPotion_1 != null) {
				EventTriggerListener.Get(Btn_HPPotion_1.gameObject).onClick += Buy_HPPotion_1;
			}
			if (Btn_MPPotion_1 != null) {
				EventTriggerListener.Get(Btn_MPPotion_1.gameObject).onClick += Buy_MPPotion_1;
			}

		}



		#region 【商品的显示信息（显示道具介绍）】

		/// <summary>
		/// 显示信息：10个钻石
		/// ***待优化：从XML文档中提取文本信息***
		/// ***待优化：将特定名称改为占位符，以便后续替换***
		/// </summary>
		/// <param name="go"></param>
		private void Display_Diamonds(GameObject go) {
			//不仅查找道具名称对应的文本，也可以再查找道具图标对应的图片
			if(go == Txt_Diamonds.gameObject || go == Txt_Diamonds.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买10个钻石，总计10元。";
			}
		}

		/// <summary>
		/// 显示信息：100金币
		/// </summary>
		/// <param name="go"></param>
		private void Display_Golds(GameObject go) {
			if (go == Txt_Golds.gameObject || go == Txt_Golds.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买100个金币，消耗1个钻石";
			}
		}

		/// <summary>
		/// 显示信息：武器道具1：战斧
		/// </summary>
		/// <param name="go"></param>
		private void Display_Weapon_1(GameObject go) {
			if (go == Txt_Weapon_1.gameObject || Txt_Weapon_1.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买1个战斧，提升攻击力，消耗180金币";
				//C#4中不能使用这种方式
				//Txt_ItemDesc.text = $"购买1个{Txt_Weapon_1.text}，提升攻击力，消耗{count}个钻石";
			}
		}

		/// <summary>
		/// 显示信息：盾牌道具1：铁盾牌
		/// </summary>
		/// <param name="go"></param>
		private void Display_Shield_1(GameObject go) {
			if (go == Txt_Shield_1.gameObject || Txt_Shield_1.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买1个铁盾牌，提升防御力，消耗120金币";
			}
		}

		/// <summary>
		/// 显示信息：靴子道具1：履行靴
		/// </summary>
		/// <param name="go"></param>
		private void Display_Boot_1(GameObject go) {
			if (go == Txt_Boot_1.gameObject || Txt_Boot_1.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买1个旅行靴，提升敏捷度，消耗120金币";
			}
		}

		/// <summary>
		/// 显示信息：生命药水道具1：弱效生命药水
		/// </summary>
		/// <param name="go"></param>
		private void Display_HPPotion_1(GameObject go) {
			if (go == Txt_HPPotion_1.gameObject || Txt_HPPotion_1.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买1瓶弱效生命药水，可以恢复少量生命值，消耗80金币";
			}
		}
		/// <summary>
		/// 显示信息：魔法药水道具1：弱效魔法药水
		/// </summary>
		/// <param name="go"></param>
		private void Display_MPPotion_1(GameObject go) {
			if (go == Txt_MPPotion_1.gameObject || Txt_MPPotion_1.transform.parent.Find("Img_ItemIcon").gameObject) {
				Txt_ItemDesc.text = "购买1瓶弱效魔法药水，可以恢复少量魔法值，消耗60金币";
			}
		}

		#endregion】



		#region 【商品的点击方法（购买该商品）】

		/// <summary>
		/// 购买钻石（充值）
		/// </summary>
		/// <param name="go"></param>
		private void Pay_Diamonds(GameObject go) {
			if(go == Btn_Diamonds.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Pay_Diamonds();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买金币
		/// </summary>
		/// <param name="go"></param>
		private void Buy_Golds(GameObject go) {
			if (go == Btn_Golds.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_Golds();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买：武器道具1：战斧（180G）
		/// </summary>
		/// <param name="go"></param>
		private void Buy_Weapon_1(GameObject go) {
			if (go == Btn_Weapon_1.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_Weapon_1();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买：盾牌道具1：铁盾牌（120G）
		/// </summary>
		/// <param name="go"></param>
		private void Buy_Shield_1(GameObject go) {
			if (go == Btn_Shield_1.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_Shield_1();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买：靴子道具1：履行靴（120G）
		/// </summary>
		/// <param name="go"></param>
		private void Buy_Boot_1(GameObject go) {
			if (go == Btn_Boot_1.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_Boot_1();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买：生命药水道具1：弱效生命药水（80G）
		/// </summary>
		/// <param name="go"></param>
		private void Buy_HPPotion_1(GameObject go) {
			if (go == Btn_HPPotion_1.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_HPPotion_1();
				PayHander(boolResult);
			}
		}

		/// <summary>
		/// 购买：魔法药水道具1：弱效魔法药水（60G）
		/// </summary>
		/// <param name="go"></param>
		private void Buy_MPPotion_1(GameObject go) {
			if (go == Btn_MPPotion_1.gameObject) {
				//方法的返回结果
				bool boolResult = false;
				//调用商城的控制层的脚本
				boolResult = Ctrl_MarketPanel.Instance.Buy_MPPotion_1();
				PayHander(boolResult);
			}
		}



		/// <summary>
		/// 充值状态处理（成功或失败）
		/// </summary>
		/// <param name="result"></param>
		private void PayHander(bool result) {
			//如果操作成功
			if (result) {
				Txt_ItemDesc.text = PAY_SUCCESS;
			}
			//如果操作不成功
			else {
				Txt_ItemDesc.text = PAY_FAILURE;
			}
		}

		#endregion

	}
}
