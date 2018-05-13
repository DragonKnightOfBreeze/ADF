//控制层，物体的自动销毁
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control {

	public class Ctrl_DestroyObjByTime : BaseControl {
		public float floDestroyTime = 2f;	//可更改的

		private void Start() {
			Destroy(this.gameObject, floDestroyTime);
		}
	}
}