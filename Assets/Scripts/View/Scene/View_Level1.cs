//视图层，第一个场景的按钮显示控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace View {
	public class View_Level1 : MonoBehaviour {

		public GameObject goUINormalAtk;    //普通攻击
		public GameObject goUIMagicAtk_A;   //技能A
		public GameObject goUIMagicAtk_B;   //技能B
		public GameObject goUIMagicAtk_C;   //技能C
		public GameObject goUIMagicAtk_D;   //技能D


		//有延迟的开始方法
		IEnumerator Start() {

			yield return new WaitForSeconds(0.2f);
			//大招的是否启用控制
			goUIMagicAtk_A.GetComponent<View_AtkButtonCDEffect>().EnableSelf();
			goUIMagicAtk_B.GetComponent<View_AtkButtonCDEffect>().EnableSelf();
			goUIMagicAtk_C.GetComponent<View_AtkButtonCDEffect>().DisableSelf();
			goUIMagicAtk_D.GetComponent<View_AtkButtonCDEffect>().DisableSelf();
		}

	}
}