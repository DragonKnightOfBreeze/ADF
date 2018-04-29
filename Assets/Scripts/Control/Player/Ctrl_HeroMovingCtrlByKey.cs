//控制层，主角移动控制，通过键盘
//如果不用委托事件的话，虽然看起来能行，但是会导致许多细节上的问题，例如：在移动过程中攻击会发生什么？

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroMovingCtrlByKey : BaseControl {

		public float FloHeroMovingSpeed = 5f;   //英雄的移动速度，可能要在外部定义

		public AnimationClip Anc_Idle;      //动画剪辑_休闲
		public AnimationClip Anc_Running;   //动画剪辑_移动

		private CharacterController CC;     //角色控制器
		private static bool _IsMoved = false;		

		//角色控制器重力系统（模拟）
		private float _FloGravity = 1f;     //角色控制器模拟重力


		private void Start() {
			//得到角色控制器
			CC = this.GetComponent<CharacterController>();
		}

		private void Update() {
			//降低游戏帧数，提升性能
			//if (Time.frameCount % 3 == 0) {
				ControlMoving();
			//}
		}

		/// <summary>
		/// 控制主角移动（通过键盘）
		/// </summary>
		/// <param name="move"></param>
		void ControlMoving() {

			//点击键盘按键，获取水平与垂直偏移值（从-1到1）
			float floMovingXPos = -Input.GetAxis("Horizontal");
			float floMovingYPos = -Input.GetAxis("Vertical");

			//除了偏移值的改变以外，为了确保正确性，可能还需确定是否按下了攻击按键
			if ((floMovingXPos != 0 || floMovingYPos != 0 ) &&
				(Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle || Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Running)
				) {

				_IsMoved = true;

				//设置角色的朝向
				//采用俯视视角  
				transform.LookAt(new Vector3(transform.position.x + floMovingXPos, transform.position.y, transform.position.z + floMovingYPos));

				//移动玩家的位置（按朝向位置移动）  
				//transform.Translate(Vector3.forward * Time.deltaTime * 5);
				Vector3 movement = transform.forward * Time.deltaTime * FloHeroMovingSpeed;
				//角色控制器增加模拟重力
				//（因为有碰撞体的关系，在Y轴上不会发生穿墙）
				movement.y -= _FloGravity;
				//角色控制器 
				CC.Move(movement);
				//播放奔跑动画  
				//GetComponent<Animation>().CrossFade(Anc_Running.name);
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Running);

				//脚本优化
				if(UnityHelper.GetInstance().GetSmallTime(0.2f)) {
					Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Running);
				}

			}
			//下面的语句应该只会执行一次。
			//使用bool值判定，岂不美哉？
			else if (_IsMoved == true){
				Debug.Log("重置到Idle状态");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
				_IsMoved = false;
			}
		}

	}
}