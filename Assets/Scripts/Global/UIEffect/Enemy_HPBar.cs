//公共层，敌人血条控制
//功能：查看敌人的当前生命值

//思路：
//设置预设
//开发脚本
//生成对应游戏对象，或者发生特定事件时，动态加载预设，并且赋值关联的游戏对象

//改进：
//显示敌人名称
//显示敌人等级，显示敌人体力值





using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

using Control;
using Model;
using View;

namespace Global {
	public class Enemy_HPBar:MonoBehaviour {
		private GameObject _GOTargetEnemy;  //目标游戏对象
		private Camera _MainCamera;        //主摄像机
		private Camera _UICamera;           //UI摄像机

		private Slider _UISlider;       //显示控件

		//敌人生命数值
		private float _FloCurHP;
		private float _FLoMaxHP;

		//敌人属性脚本
		private Ctrl_BaseEnemy_Prop _MyProperty;

		/*需要细调*/

		public readonly float FloLength = 2f;  //血条预设长度
		public readonly float FloHeight = 1f;  //血条预设宽度
		public readonly float FloUp = 2f;	//血条的抬高度（跟随偏移）


		/// <summary>
		/// 设置敌人目标
		/// （在生成敌人时调用，以获得敌人游戏对象）
		/// （如何获得？？？）
		/// </summary>
		/// <param name="goEnemy"></param>
		public void SetTargetEnemy(GameObject goEnemy) {
			_GOTargetEnemy = goEnemy;
		}



		private void Start() {

			_MyProperty = _GOTargetEnemy.GetComponent<Ctrl_BaseEnemy_Prop>();

			//得到UISlider控件
			_UISlider = this.gameObject.GetComponent<Slider>();
			//主摄像机
			_MainCamera = Camera.main;
			//UI摄像机
			_UICamera = GameObject.FindGameObjectWithTag(Tag.Tag_UICamera).GetComponent<Camera>(); 
			//参数检查
			if(_GOTargetEnemy == null) {
				Debug.LogError(GetType()+"\t目标敌人对象为空！");
				return;
			}
		}

		/// <summary>
		/// 计算敌人的血量
		/// </summary>
		private void Update() {
			if (Time.frameCount % 2 == 0) {
				try {
					//得到当前的生命数值与最大的生命值
					_FloCurHP = _MyProperty.CurHP;
					_FLoMaxHP = _MyProperty.MaxHP;
					//计算血量
					_UISlider.value = _FloCurHP / _FLoMaxHP;

					//控件尺寸
					this.transform.localScale = new Vector3(FloLength, FloHeight, 0);

					//何时销毁
					if (_FloCurHP <= 0) {
						Destroy(this.gameObject);
					}
				}
				catch (System.Exception) {
				}
			}
		}


		/// <summary>
		/// 三维坐标系转UI坐标系
		/// </summary>
		private void LateUpdate() {
			if (Time.frameCount % 2 == 0) {
				if (_GOTargetEnemy != null) {
					//获取目标物体的屏幕坐标
					Vector3 pos = _MainCamera.WorldToScreenPoint(_GOTargetEnemy.transform.position);
					//屏幕坐标转换为UI的世界坐标
					pos = _UICamera.ScreenToWorldPoint(pos);
					//确定UI的最终位置
					pos.z = 0;
					transform.position = new Vector3(pos.x, pos.y + FloUp, pos.z);
				}
			}
		}

	}
}
