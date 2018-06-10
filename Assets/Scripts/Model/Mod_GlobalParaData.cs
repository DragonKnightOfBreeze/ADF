//模型层，全局参数数据
//作用：为了“对象持久化”服务

using System;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Model {
	public class Mod_GlobalParaData {

		//下一个场景的名称
		private SceneEnum _NextSceneName;
		//玩家的姓名，
		private string _PlayerName;
		//玩家的职业
		private PlayerType _PlayerType;

		/* 属性定义 */

		public SceneEnum NextSceneName {
			get {
				return _NextSceneName;
			}

			set {
				_NextSceneName = value;
			}
		}

		public string PlayerName {
			get {
				return _PlayerName;
			}

			set {
				_PlayerName = value;
			}
		}

		public PlayerType PlayerType {
			get {
				return _PlayerType;
			}

			set {
				_PlayerType = value;
			}
		}

		/// <summary>
		/// 无参构造函数
		/// </summary>
		private Mod_GlobalParaData() { }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="sceneName">场景名称</param>
		/// <param name="playerName">玩家姓名</param>
		/// <param name="playerType">玩家职业/类型</param>
		public Mod_GlobalParaData(SceneEnum sceneName,string playerName,PlayerType playerType) {
			_NextSceneName = sceneName;
			_PlayerName = playerName;
			_PlayerType = playerType;
		}


	}
}
