//控制层，主城场景控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Global;

namespace Control {
	class Ctrl_MajorCity:BaseControl {

		public AudioClip Auc_Background;    //主城背景音乐

		private void Start() {
			if(Auc_Background != null) {
				AudioManager.PlayBackground(Auc_Background);
			}
		}

	}
}
