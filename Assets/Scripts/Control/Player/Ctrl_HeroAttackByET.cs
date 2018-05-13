//控制层，主角动画控制，通过虚拟键盘

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Kernel;
using Global;
using Model;

namespace Control {
	public class Ctrl_HeroAttackByET : BaseControl {

//使用预编译指令，只在安卓或苹果，或编辑器环境下编译
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR

		//事件：主角控制
		public static event del_PlayerControlWithStr Eve_PlayerControl;

		public static Ctrl_HeroAttackByET Instance;

		private void Awake() {
			Instance = this;
		}

		/// <summary>
		/// 响应普通攻击
		/// </summary>
		public void ResponseATKByNormal() {
			//如果事件不为空，就调用对应参数的事件
			//参数是输入类型
			if( Eve_PlayerControl != null) {
				
				Eve_PlayerControl(GlobalParameter.INPUT_MGR_NormalAtk);
			}
		}

		/// <summary>
		/// 响应技能A
		/// </summary>
		public void ResponseATKBySkillA() {
			//如果事件不为空，就调用对应参数的事件
			if (Eve_PlayerControl != null) {

				Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkA);
			}
		}

		/// <summary>
		/// 响应技能B
		/// </summary>
		public void ResponseATKBySkillB() {
			//如果事件不为空，就调用对应参数的事件
			if (Eve_PlayerControl != null) {
				Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkB);
			}
		}

		/// <summary>
		/// 响应技能C
		/// </summary>
		public void ResponseATKBySkillC() {
			//如果事件为空，就调用对应参数的事件
			if (Eve_PlayerControl != null) {
				Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkC);
			}
		}

		/// <summary>
		/// 响应技能D
		/// </summary>
		public void ResponseATKBySkillD() {
			//如果事件不为空，就调用对应参数的事件
			if (Eve_PlayerControl != null) {
				Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkD);
			}
		}

# endif

	}
}