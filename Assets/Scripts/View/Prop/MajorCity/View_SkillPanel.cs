//视图层，主城场景，技能面板显示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;
using Global;

namespace View {
	class View_SkillPanel:MonoBehaviour {
		//查看的项目
		public Image Img_NormalAtk;		//普通攻击
		public Image Img_CloseAtk;		//技能：近距离攻击
		public Image Img_JumpAtk;		//技能：跳跃攻击
		public Image Img_FireAtk;		//技能：火球
		public Image Img_ThunderAtk;    //技能雷电

		//显示文件控件
		public Text Txt_SkillName;
		public Text Txt_SkillDesc;

		
		private void Awake() {
			//攻击贴图注册
			RigisterAtk();
		}

		private void Start() {
			//默认显示普通攻击的介绍
			NormalATK(Img_NormalAtk.gameObject);
		}

		public void RigisterAtk() {
			//使用之前写过的事件触发监听器脚本
			if(Img_NormalAtk!=null) {
				EventTriggerListener.Get(Img_NormalAtk.gameObject).onClick += NormalATK;
			}
			if (Img_CloseAtk != null) {
				EventTriggerListener.Get(Img_CloseAtk.gameObject).onClick += CloseATK;
			}
			if (Img_JumpAtk != null) {
				EventTriggerListener.Get(Img_JumpAtk.gameObject).onClick += JumpATK;
			}
			if (Img_FireAtk != null) {
				EventTriggerListener.Get(Img_FireAtk.gameObject).onClick += FireATK;
			}
			if (Img_ThunderAtk != null) {
				EventTriggerListener.Get(Img_ThunderAtk.gameObject).onClick += ThunderATK;
			}
		}

		//***待优化：从XML文件中提取信息***
	

		/// <summary>
		/// 普通攻击技能介绍
		/// </summary>
		/// <param name="go"></param>
		private void NormalATK(GameObject go) {
			if (go == Img_NormalAtk.gameObject) {
				Txt_SkillName.text = "普通攻击";
				Txt_SkillDesc.text = "角色的普通攻击，伤害来自于主角的攻击力。";
			}
		}

		/// <summary>
		/// 近距离攻击技能介绍
		/// </summary>
		/// <param name="go"></param>
		private void CloseATK(GameObject go) {
			if (go == Img_CloseAtk.gameObject) {
				Txt_SkillName.text = "螺旋冲锋";
				Txt_SkillDesc.text = "角色的武器技能，可以对正前方的敌人造成巨大的伤害。";
			}
		}

		/// <summary>
		/// 跳跃攻击技能介绍
		/// </summary>
		/// <param name="go"></param>
		private void JumpATK(GameObject go) {
			if (go == Img_JumpAtk.gameObject) {
				Txt_SkillName.text = "裂地斩";
				Txt_SkillDesc.text = "角色的武器技能，可以对前方一定范围内的所有敌人造成伤害。";
			}
		}

		/// <summary>
		/// 火球攻击技能介绍
		/// </summary>
		/// <param name="go"></param>
		private void FireATK(GameObject go) {
			if (go == Img_FireAtk.gameObject) {
				Txt_SkillName.text = "火球";
				Txt_SkillDesc.text = "角色的魔法技能，发射出一颗火球，对所有接触到火球的敌人造成伤害。";
			}
		}

		/// <summary>
		/// 雷电攻击技能介绍
		/// </summary>
		/// <param name="go"></param>
		private void ThunderATK(GameObject go) {
			if (go == Img_ThunderAtk.gameObject) {
				Txt_SkillName.text = "雷电";
				Txt_SkillDesc.text = "角色的魔法技能，向地面刺入雷电柱，对所有接触到雷电柱的敌人造成大量伤害。";
			}
		}


	}
}
