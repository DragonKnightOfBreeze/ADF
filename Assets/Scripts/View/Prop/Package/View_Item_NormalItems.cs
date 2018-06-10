//视图层，道具系统，一般道具（不可装备的）

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Global;
using Kernel;
using Control;

namespace View {
	class View_Item_NormalItems : View_BaseItem, IBeginDragHandler, IDragHandler, IEndDragHandler {

		//目标格子是不存在的，可以拖但不能装备
		private string _StrTargetGridName = null;

		void Start() {
			//赋值目标格子名称
			base.strTargetGridName = _StrTargetGridName;
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
			base.Base_OnDrag(eventData);
		}

		/// <summary>
		/// 实现拖拽后处理的方法
		/// </summary>
		/// <param name="eventData"></param>
		public void OnEndDrag(PointerEventData eventData) {
			base.Base_OnEndDrag(eventData);
		}

	}
}

