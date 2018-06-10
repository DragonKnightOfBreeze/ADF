//视图层，道具系统，盾牌道具

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Global;
using Kernel;
using Control;

namespace View {
	class View_Item_Shields : View_BaseItem, IBeginDragHandler, IDragHandler, IEndDragHandler {

		//定义“目标格子”名称
		public string StrTargetGridName = "Grid_Shield";

		//定义各种盾牌的防御力
		public float Shield_1_DEF = 6;
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
			//更新主角防御力
			Ctrl_HeroProperty.Instance.UpdateDEF(Shield_1_DEF);

		}

	}
}

