//控制层，主角攻击输入控制，通过键盘
//包括事件的调用

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroAttackByKey : BaseControl {

//使用预编译指令，只在标准环境或编译器环境下编译
# if UNITY_STANDALONE_WIN || UNITY_EDITOR


		public static event del_PlayerControlWithStr Eve_PlayerControl; //事件可以看成委托的事件

		void Update() {
			//必须是GetButtonDown()，不能是GetKeyDown()
			if (Input.GetButton(GlobalParameter.INPUT_MGR_NormalAtk)) {	
				//// // Debug.Log("NormalAtk，按下了J键。");
				if (Eve_PlayerControl != null) {
					Eve_PlayerControl(GlobalParameter.INPUT_MGR_NormalAtk);
				}
			}
			else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_MagicAtkA)) {
				if (Eve_PlayerControl != null) {
					Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkA);
				}
			}
			else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_MagicAtkB)) {
				if (Eve_PlayerControl != null) {
					Eve_PlayerControl(GlobalParameter.INPUT_MGR_MagicAtkB);
				}
			}
		}

# endif	

	}
}