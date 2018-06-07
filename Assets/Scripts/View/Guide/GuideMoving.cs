//视图层，新手引导引导动画系统

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

namespace View {
	class GuideMoving:MonoBehaviour {
		public GameObject goMovingGoal;

		private void Start() {
			iTween.MoveTo(this.gameObject,
				iTween.Hash("position",goMovingGoal.transform.position,
				"time",1f,
				"looptype",iTween.LoopType.loop
				)
				);
		}
	}
}
