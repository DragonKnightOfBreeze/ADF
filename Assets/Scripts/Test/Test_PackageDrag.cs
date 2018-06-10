//测试：学习背包系统的拖拽的基本原理
//测试脚本，以学习原理为主

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;	//Unity事件系统的命名空间

namespace Test {
	public class Test_PackageDrag:MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

		private CanvasGroup _CanvasGroup;		//用于贴图的穿透处理（否则不能检测下面的贴图）
		private Vector3 _OriginalPosition;		//原始方位
		private RectTransform _MyReTransform;	//二维方位

		private void Start() {
			//贴图穿透组件
			_CanvasGroup = this.GetComponent<CanvasGroup>();
			//二维方位
			_MyReTransform = this.transform as RectTransform;
			//获得原始方位
			_OriginalPosition = _MyReTransform.position;
			
		}

		/// <summary>
		/// 实现拖拽前处理的方法
		/// 功能：可以拖拽
		/// </summary>
		/// <param name="eventData"></param>
		public void OnBeginDrag(PointerEventData eventData) {
			//忽略自身（可以穿透）
			_CanvasGroup.blocksRaycasts = false;
			//保证当前贴图可见，不被覆盖，不再层级面板中乱改变方法
			this.gameObject.transform.SetAsLastSibling();
		}

		/// <summary>
		/// 实现拖拽中处理的方法
		/// 功能：可以拖拽
		/// </summary>
		/// <param name="eventData"></param>
		public void OnDrag(PointerEventData eventData) {
			//当前鼠标位置
			Vector3 globalMousePosition;
			//屏幕坐标，需要转二维矩阵坐标
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_MyReTransform,eventData.position,eventData.pressEventCamera,out globalMousePosition)) {
				//输出当前鼠标位置
				_MyReTransform.position = globalMousePosition;
			}
		}

		/// <summary>
		/// 实现拖拽后处理的方法
		/// 功能：拖拽后的位置正确，则固定到目标位置，否则回到原来位置
		/// </summary>
		/// <param name="eventData"></param>
		public void OnEndDrag(PointerEventData eventData) {
			//当前鼠标经过的“格子名称”
			GameObject cur = eventData.pointerEnter;    //后者相当于当前贴图
			if (cur != null) {
				//如果遇到了目标点，则固定到格子所在位置，并且更新原始位置
				if (cur.name.Equals("Img_GoPosition")) {
					_MyReTransform.position = cur.transform.position;
					_OriginalPosition = _MyReTransform.position;
				}
				//如果没有遇到目标点，则回到原来位置
				else {
					_MyReTransform.position = _OriginalPosition;
					//阻止穿透，可以再次进行移动
					_CanvasGroup.blocksRaycasts = true;
				}
			}
			//如果拖拽到一个空的区域
			else{
				_MyReTransform.position = _OriginalPosition;
			}
		}
	}
}
