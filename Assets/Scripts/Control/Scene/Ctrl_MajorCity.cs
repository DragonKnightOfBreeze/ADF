//控制层，主城场景控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {
	class Ctrl_MajorCity:BaseControl {

		public AudioClip Auc_Background;    //主城背景音乐

		IEnumerator Start() {
			//播放背景音乐
			if(Auc_Background != null) {
				AudioManager.PlayBackground(Auc_Background);
			}
			//+++新加的功能+++
			//读取单机玩家数据进度
			if(GlobalParaMgr.CurGameStatus == GameStatus.Continue) {
				//读取进度
				yield return new WaitForSeconds(2f);
				SaveAndLoading.GetInstance().LoadingGame_PlayerData();
			}
		}

	}
}
