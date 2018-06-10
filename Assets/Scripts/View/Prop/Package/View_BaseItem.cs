//视图层，背包系统（装备系统）
//定义父类
//作用：
//定义背包系统的一般性操作，例如拖拽

//特别注意：必须给每一个相同类型的道具添加同样的标签

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Global;
using Kernel;

namespace View {
	class View_BaseItem :MonoBehaviour {

		protected string strTargetGridName;	//目标格子的名称

		private CanvasGroup _CanvasGroup;	//用于贴图的穿透处理
		private Vector3 _OriginalPosition;	//原始位置
		private Transform _MyTransform;		//本对象的方位
		private RectTransform _MyRectTransform;	//本对象的二维方位

		/// <summary>
		/// 运行本类实例，通过子类调用
		/// </summary>
		protected void RunInstanceByChildClass() {
			Base_Start();
		}

		/// <summary>
		/// 父类实例化的方法
		/// </summary>
		void Base_Start() {
			//获得贴图穿透组件
			_CanvasGroup = this.GetComponent<CanvasGroup>();
			//获得本对象的方位
			_MyTransform = this.transform;
			//获得本对象的二维方位
			_MyRectTransform = this.transform as RectTransform;
		}


		/// <summary>
		/// 实现拖拽前处理的方法
		/// 功能：可以拖拽
		/// </summary>
		/// <param name="eventData"></param>
		public void Base_OnBeginDrag(PointerEventData eventData) {
			//忽略自身（可以穿透）
			_CanvasGroup.blocksRaycasts = false;
			//保证当前贴图可见，不被覆盖，不再层级面板中乱改变方法
			this.gameObject.transform.SetAsLastSibling();
			//获得原始位置
			_OriginalPosition = _MyTransform.position;
		}

		#region 【背包中的物品拖拽到装备栏的方法】

		/// <summary>
		/// 实现拖拽中处理的方法
		/// 功能：可以拖拽
		/// </summary>
		/// <param name="eventData"></param>
		public void Base_OnDrag(PointerEventData eventData) {
			//当前鼠标位置
			Vector3 globalMousePosition;
			//屏幕坐标，需要转二维矩阵坐标
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_MyRectTransform, eventData.position, eventData.pressEventCamera, out globalMousePosition)) {
				//输出当前鼠标位置
				_MyRectTransform.position = globalMousePosition;
			}
		}

		/// <summary>
		/// 实现拖拽后处理的方法
		/// 功能：拖拽后的位置正确，则固定到目标位置，否则回到原来位置
		/// </summary>
		/// <param name="eventData"></param>	
		public void Base_OnEndDrag(PointerEventData eventData) {
			//cur当前鼠标经过的“格子名称”
			GameObject cur = eventData.pointerEnter;    //后者相当于当前贴图
			if (cur != null) {
				//如果遇到了目标点，则固定到格子所在位置，并且更新原始位置
				//移动到符合条件的物品格子上
				if (cur.name.Equals(strTargetGridName)) {
					_MyTransform.position = cur.transform.position;
					_OriginalPosition = _MyTransform.position;
					//执行相应的装备方法
					InvokeMethodByEndDrag();

				}
				//如果没有遇到目标点
				else {
					//移动到背包系统的其他有效位置上
					//如果是同种类型的可装备道具，则替换位置（替换装备）
					//***使用Tag设置（是那些预设的道具游戏物体的Tag）***
					if(cur.tag ==eventData.pointerDrag.tag && cur.name != eventData.pointerDrag.name) {
						//“被覆盖贴图”的位置与“当前贴图”位置互换（替换装备）
						Vector3 targetPosition = cur.transform.position;
						//（下面的变为原始的）
						cur.transform.position = _OriginalPosition;
						//（当前的变为下面的）
						_MyTransform.position = targetPosition;            
						//新的位置，确定为新的“原始位置”（原始的变为当前的）
						_OriginalPosition = _MyTransform.position;
					}
					//如果拖拽到背包界面的其他对象上
					//else {
					//	_MyTransform.position = _OriginalPosition;
					//}
					//_MyTransform.position = _OriginalPosition;
					////阻止穿透，可以再次进行移动
					//_CanvasGroup.blocksRaycasts = true;
					}
			}
			////如果拖拽到一个空的区域
			//else {
			//	_MyRectTransform.position = _OriginalPosition;
			//}

			_MyTransform.position = _OriginalPosition;
			//阻止穿透，可以再次进行移动
			_CanvasGroup.blocksRaycasts = true;
		}

		/// <summary>
		/// 执行特定的装备方法（虚方法，在子类中重载）
		/// </summary>
		protected virtual void InvokeMethodByEndDrag() { }

		#endregion

	}
}
