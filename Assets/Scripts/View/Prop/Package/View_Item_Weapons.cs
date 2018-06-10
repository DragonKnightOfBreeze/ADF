//视图层，道具系统，武器道具

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Global;
using Kernel;
using Control;

namespace View {
	class View_Item_Weapons: View_BaseItem, IBeginDragHandler,IDragHandler,IEndDragHandler {

		//定义“目标格子”名称
		public string StrTargetGridName = "Grid_Weapon";

		//定义各种武器的攻击力
		public float Weapon_1_ATK = 14;
		//。。。。。。

		void Start() {
			//赋值目标格子名称
			base.strTargetGridName = StrTargetGridName;
			//运行父类的初始化
			base.RunInstanceByChildClass();
		}


		/// <summary>
		/// 实现拖拽前处理的方法
		/// </summary>
		/// <param name="eventData"></param>
		public void OnBeginDrag(PointerEventData eventData) {
			base.Base_OnBeginDrag(eventData);
		}

		/// <summary>
		/// 实现拖拽中处理的方法
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDrag(PointerEventData eventData) {
			Base_OnDrag(eventData);
		}

		/// <summary>
		/// 实现拖拽后处理的方法
		/// </summary>
		/// <param name="eventData"></param>
		public void OnEndDrag(PointerEventData eventData) {
			Base_OnEndDrag(eventData);
		}

		/// <summary>
		/// 重载：特定的装备方法
		/// </summary>
		protected override void InvokeMethodByEndDrag() {
			//更新主角攻击力
			Ctrl_HeroProperty.Instance.UpdateATK(Weapon_1_ATK);

		}

	}
}
