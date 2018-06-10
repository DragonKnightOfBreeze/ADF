//视图层，登录场景


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Global;
using Kernel;
using Control;

namespace View {
 	public class View_LoginScene : MonoBehaviour {

		public GameObject goSworder;			//剑士对象
		public GameObject goMage;				//魔法师对象
		public GameObject goUISworderInfo;		//剑士的UI面板
		public GameObject goUIMageInfo;         //魔法师的UI面板
		public InputField inpUserName;          //（输入的）用户名称


		private void Start() {
			////获取玩家的类型（系统默认）
			//GlobalParaMgr.playerType = PlayerType.Sworder;
			//用户名称默认数值（默认是“亚瑟”）
			inpUserName.text = GlobalParaMgr.PlayerName;
		}

		/// <summary>
		/// 选择剑士
		/// </summary>
		public void ChangeToSworder() {
			//显示对象
			goSworder.SetActive(true);
			goMage.SetActive(false);
			//显示对应的UI
			goUISworderInfo.SetActive(true);
			goUIMageInfo.SetActive(false);
			//播放音效
			Ctrl_LoginScene.Instance.PlayAudioEffect(PlayerType.Sworder);
		}

		/// <summary>
		/// 选择魔法师
		/// </summary>
		public void ChangeToMage() {
			//显示对象
			goSworder.SetActive(false);
			goMage.SetActive(true);
			//显示对应的UI
			goUISworderInfo.SetActive(false);
			goUIMageInfo.SetActive(true);
			//获取玩家类型
			GlobalParaMgr.CurPlayerType = PlayerType.Mage;
			//播放音效
			Ctrl_LoginScene.Instance.PlayAudioEffect(PlayerType.Mage);
		}


		/// <summary>
		/// 提交信息
		/// </summary>
		public void SubmitInfo() {
			//获取用户名称（跨场景赋值）
			GlobalParaMgr.PlayerName = inpUserName.text;
			//获取玩家的类型（默认为剑士）
			//跳转下一个场景（由控制层处理）
			//玩家操作完毕后自动跳转
			Ctrl_LoginScene.Instance.EnterNextScene();

		}
	}
}
