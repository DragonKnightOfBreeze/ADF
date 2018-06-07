//公共层，层消隐技术的实现
//优化：小物件在远距离消隐，在近距离显示

using System;
using System.Collections.Generic;
using UnityEngine;

using Kernel;

namespace Global {
	public class SmallLayerCtrl:MonoBehaviour {

		public int intDisappearDistance = 10;   //消隐距离
		private float[] distanceArray = new float[32];	

		private void Start() {
			//一种固定的写法
			distanceArray[8] = intDisappearDistance;
			Camera.main.layerCullDistances = distanceArray;
		}


	}
}
