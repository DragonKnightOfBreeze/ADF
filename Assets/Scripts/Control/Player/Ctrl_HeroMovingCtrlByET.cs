//控制层，主角移动控制，通过虚拟键盘

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroMovingCtrlByET : BaseControl {

# if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR

		public float FloHeroMovingSpeed = 5f;	//英雄的移动速度，可能要在外部定义

		public AnimationClip Anc_Idle;		//动画剪辑_休闲
		public AnimationClip Anc_Running;   //动画剪辑_移动

		private CharacterController CC;     //角色控制器

		//角色控制器重力系统（模拟）
		private float _FloGravity = 1f;		//角色控制器模拟重力

		#region 事件注册

		/// <summary>
		/// 游戏对象启用
		/// </summary>
		void OnEnable() {
			EasyJoystick.On_JoystickMove += OnJoystickMove;
			EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
		}

		/// <summary>
		/// 游戏对象的销毁
		/// </summary>
		public void OnDestroy() {
			EasyJoystick.On_JoystickMove -= OnJoystickMove;
			EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
		}

		/// <summary>
		/// 游戏对象的禁用
		/// </summary>
		public void OnDisable() {
			EasyJoystick.On_JoystickMove -= OnJoystickMove;
			EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
		}

		#endregion

		private void Start() {
			//得到角色控制器
			CC = this.GetComponent<CharacterController>();
		}

		/// <summary>
		/// 移动摇杆中
		/// </summary>
		/// <param name="move"></param>
		void OnJoystickMove(MovingJoystick move) {
			//判断不符合，直接跳过方法体
			if (move.joystickName != GlobalParameter.JOYSTICK_NAME) {
				return;
			}

			//获取摇杆中心偏移的坐标（实际上是反的）
			float joyPositionX = -move.joystickAxis.x;  //取负调整
			float joyPositionY = -move.joystickAxis.y;  //取负调整

			if (joyPositionY != 0 || joyPositionX != 0) {
				//设置角色的朝向（朝向当前坐标+摇杆偏移量）
				//采用俯视视角  

				//使用普通攻击或技能时，锁定方向

				if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.NormalAtk &&
					Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicAtkA &&
					Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicAtkB
				) {


					transform.LookAt(new Vector3(transform.position.x + joyPositionX, transform.position.y, transform.position.z + joyPositionY));
				}
				//移动玩家的位置（按朝向位置移动）  
				//transform.Translate(Vector3.forward * Time.deltaTime * 5);
				Vector3 movement = transform.forward * Time.deltaTime * FloHeroMovingSpeed;
				//角色控制器增加模拟重力
				//（因为有碰撞体的关系，在Y轴上不会发生穿墙）
				movement.y -= _FloGravity;

				//角色控制器
				//只有在空闲或移动状态下才能移动（攻击时不能）
				if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle || Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Running) {
					CC.Move(movement);
					//播放奔跑动画  
					//GetComponent<Animation>().CrossFade(Anc_Running.name);

					//脚本优化（这样的优化并不完美也不确定）
					//每隔一段时间设置一次英雄动画状态
					if (UnityHelper.GetInstance().GetSmallTime(GlobalParameter.CHECK_TIME)) {
						// // Debug.Log("虚拟按键控制：移动");
						Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Running);
					}
				}
			}
		}

		/// <summary>
		/// 移动摇杆结束
		/// </summary>
		/// <param name="move"></param>
		void OnJoystickMoveEnd(MovingJoystick move) {
			//停止时，角色恢复idle
			//同时判断摇杆以及当前动作状态
			
			if (move.joystickName == GlobalParameter.JOYSTICK_NAME && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Running ) {
				// // Debug.Log("虚拟按键控制：静止");
				Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
			}
		}

#endif

	}
} 