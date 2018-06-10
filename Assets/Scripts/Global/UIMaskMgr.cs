//公共层，UI遮罩管理器
//作用：
//实现弹出“模态窗体”（然后其他地方都无法操作）

//模态窗口是玩家点击某个窗体，或者对话框时，其背景暂时禁用（失效）的一种技术实现。
//通过开发UIMaskMgr.cs（UI遮罩管理器）脚本，解决“模态窗口”的技术难点
//原理：想办法将要操作的面板，在层级面板中放到遮罩图片下面

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

namespace Global {
	public class UIMaskMgr:MonoBehaviour {

		public GameObject goTopPanel;				//顶层面板（可能需要动态赋值）
		public GameObject goMaskPanel;				//遮罩面板
		private Camera _UICamera;					//UI摄像机
		private float _OriginalUICameraDepth;       //原始UI摄像机的层深

		private const int ADD_DEPTH = 20;	//增加的层深


		private void Start() {
			//得到UI摄像机的原始层深
			_UICamera = gameObject.transform.parent.Find("UICamera").GetComponent<Camera>();
			if(_UICamera != null) {
				_OriginalUICameraDepth = _UICamera.depth;
			}
			else {
				Debug.LogError(GetType() + "/Start()/UICamera is Null,please check!");
			}
		}

		/// <summary>
		/// 设置遮罩状态
		/// </summary>
		/// <param name="goDisplayPanel">需要显示的窗体</param>
		public void SetMaskWindow(GameObject goDisplayPanel) {
			//顶层窗体下移（作为最后一个）
			goTopPanel.transform.SetAsLastSibling();
			//启用遮罩窗体
			goMaskPanel.SetActive(true);
			//遮罩窗体下移
			goMaskPanel.transform.SetAsLastSibling();
			//显示窗体下移
			goDisplayPanel.transform.SetAsLastSibling();
			//增加当前UI摄像机的“层深”
			if (_UICamera != null) {
				_UICamera.depth += ADD_DEPTH;
			}
		}

		/// <summary>
		/// 取消遮罩窗体
		/// </summary>
		public void CancelMaskWindow() {
			//顶层窗体上移
			goTopPanel.transform.SetAsFirstSibling();
			//禁用遮罩窗体
			goMaskPanel.SetActive(false);
			//恢复UI摄像机的原来的“层深”
			if (_UICamera != null) {
				_UICamera.depth -= ADD_DEPTH;
			}
		}

	}
}
