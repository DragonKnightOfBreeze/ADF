//［控制层］敌人：骷髅弓箭手的“箭”的控制

using System;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {
	public class Ctrl_Arrow:BaseControl {

		public float ArrowSpeed = 10f;   //道具的飞行速度

		private GameObject _HeroGO;     //英雄的游戏对象
		private GameObject _MyGO;		//该敌人的游戏对象
		private Ctrl_HeroProperty _HeroProperty;    //英雄的属性
		private Ctrl_SkeletonArcher_Prop _MyProperty;    //该敌人的属性


		private void Start() {
			//得到英雄、英雄的属性脚本
			_HeroGO = GameObject.FindGameObjectWithTag(Tag.Tag_Player);
			if (_HeroGO) {
				_HeroProperty = _HeroGO.GetComponent<Ctrl_HeroProperty>();
			}
			//得到该敌人、该敌人的属性脚本
			_MyGO = UnityHelper.GetInstance().SelectGOWhithTag(transform, Tag.Tag_Enemy);
			if (_MyGO) {
				_MyProperty = _MyGO.GetComponent<Ctrl_SkeletonArcher_Prop>();
			}
		}

		private void Update() {
			//弓箭发射出去，解除父子对象关系
			transform.parent = _MyGO.transform.parent;

			//道具飞行
			if (Time.frameCount % 2 == 0) {
				this.gameObject.transform.Translate(Vector3.forward *ArrowSpeed*Time.deltaTime);
			}
		}

		/// <summary>
		/// 碰撞检测
		/// </summary>
		/// <param name="other"></param>
		private void OnTriggerEnter(Collider other) {
			if(other.gameObject.tag == Tag.Tag_Player) {
				//造成伤害
				_HeroProperty.SubCurHP(_MyProperty.ATK);
			}
		}



		///// <summary>
		///// 设置各项值
		///// </summary>
		///// <param name="arrowATK">攻击力</param>
		///// <param name="arrowSpeed">飞行速度</param>
		//public void SetValues(int arrowATK,float arrowSpeed) {
		//	ArrowATK = arrowATK;
		//	ArrowSpeed = arrowSpeed;

		//}


	}
}
