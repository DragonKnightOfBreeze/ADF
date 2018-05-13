//控制层，物体的自动旋转

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control {
	public class Ctrl_MakeObjRotation : MonoBehaviour {

		public float floRotateSpeed = 1f;	//旋转速度


		void Start() {

		}

		void Update() {
			this.gameObject.transform.Rotate(Vector3.up, floRotateSpeed);
		}
	}
}
