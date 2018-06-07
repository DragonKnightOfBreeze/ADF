//视图层，显示玩家信息
//专门响应玩家的点击处理
//包括玩家点击攻击的虚拟按键，以及退出、设置等虚拟按键

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Control;
using Global;

namespace View {
	public class View_PlayerInfoResponse : MonoBehaviour {

		public static View_PlayerInfoResponse Instance;		//本脚本的实例
		public GameObject goPlayerDetailInfoPanel;  //玩家的详细信息面板
		public GameObject goET;     //ET的引用

		public GameObject goNormalAtk;      //虚拟按键（普通攻击）的引用
		public GameObject goMagicAtkA;
		public GameObject goMagicAtkB;
		public GameObject goMagicAtkC;
		public GameObject goMagicAtkD;
		public GameObject goHpPotion;
		public GameObject goMpPotion;
	
		public GameObject goTopLeftPanel;
		public GameObject goTopRightPanel;


		private void Awake() {
			Instance = this;
		}


		private void Start() {
			goPlayerDetailInfoPanel.SetActive(false);
		}

		#region UI的显示与隐藏

		/// <summary>
		/// 显示与隐藏玩家的详细信息面板
		/// </summary>
		public void IsShowingPlayerDetailInfoPanel() {
			//goPlayerDetailInfoPanel.SetActive(true);    //显示面板
			//goPlayerDetailInfoPanel.SetActive(false);    //隐藏面板
			goPlayerDetailInfoPanel.SetActive(!goPlayerDetailInfoPanel.activeSelf);	 //如果是开启的就隐藏，如果是隐藏的就显示
		}

		/// <summary>
		/// 显示ET
		/// </summary>
		public void DisplayET() {
			goET.SetActive(true);
		}

		/// <summary>
		/// 隐藏ET
		/// </summary>
		public void HideET() {
			goET.SetActive(false);
		}

		/// <summary>
		/// 显示所有的虚拟按键
		/// </summary>
		public void DisplayAllVK() {
			goNormalAtk.SetActive(true);
			goMagicAtkA.SetActive(true);
			goMagicAtkB.SetActive(true);
			goMagicAtkC.SetActive(true);
			goMagicAtkD.SetActive(true);
			goHpPotion.SetActive(true);
			goMpPotion.SetActive(true);
		}

		/// <summary>
		/// 隐藏所有虚拟按键
		/// </summary>
		public void HideAllVK() {
			goNormalAtk.SetActive(false);
			goMagicAtkA.SetActive(false);
			goMagicAtkB.SetActive(false);
			goMagicAtkC.SetActive(false);
			goMagicAtkD.SetActive(false);
			goHpPotion.SetActive(false);
			goMpPotion.SetActive(false);
		}

		/// <summary>
		/// 显示虚拟按键（普通攻击）
		/// </summary>
		public void DisplayVK() {
			goNormalAtk.SetActive(true);
		}

		/// <summary>
		/// 隐藏虚拟按键（普通攻击）
		/// </summary>
		public void HideVK() {
			goNormalAtk.SetActive(false);
		}

		/// <summary>
		/// 显示顶层左侧UI（角色简要属性）
		/// </summary>
		public void DisplayTopLeftPanel() {
			goTopLeftPanel.SetActive(true);
		}

		/// <summary>
		/// 隐藏顶层左侧UI（角色简要属性）
		/// </summary>
		public void HideTopLeftPanel() {
			goTopLeftPanel.SetActive(false);
		}

		/// <summary>
		/// 显示顶层左侧UI
		/// </summary>
		public void DisplayTopRightPanel() {
			goTopRightPanel.SetActive(true);
		}

		/// <summary>
		/// 隐藏顶层左侧UI
		/// </summary>
		public void HideTopRightPanel() {
			goTopRightPanel.SetActive(false);
		}

		#endregion



		/// <summary>
		/// 退出游戏系统
		/// </summary>
		public void ExitGame() {
			Application.Quit();
		}


#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR

		#region （视图层）按键响应

		/// <summary>
		/// （视图层）响应普通攻击，以下类推
		/// 使用动画控制脚本的协程方法来控制动画播放
		/// </summary>
		//由于有持续按压按键方法，可以不挂载到OnClick()方法中
		//public void ResponseNormalAtk() {
		//	//调用控制层的方法，控制层的方法再调用对应事件
		//	//// // Debug.Log("ResponseNormalAtkf方法！");
		//	Ctrl_HeroAttackByET.Instance.ResponseATKByNormal();
		//}

		public void ResponseMagicAtkA() {
			Ctrl_HeroAttackByET.Instance.ResponseATKBySkillA();

		}

		public void ResponseMagicAtkB() {
			Ctrl_HeroAttackByET.Instance.ResponseATKBySkillB();

		}

		public void ResponseMagicAtkC() {
			Ctrl_HeroAttackByET.Instance.ResponseATKBySkillC();

		}

		public void ResponseMagicAtkD() {
			Ctrl_HeroAttackByET.Instance.ResponseATKBySkillD();

		}

		#endregion

#endif

	}
}