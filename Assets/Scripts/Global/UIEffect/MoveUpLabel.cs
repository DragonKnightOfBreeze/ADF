//公共层：“漂字”特效

//功能：表示特定对象（主角/敌人），失血数值显示




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kernel;

using Control;
using Model;
using View;

namespace Global {
	public class MoveUpLabel : MonoBehaviour {
		private GameObject _GOTargetEnemy;  //目标游戏对象
		private Camera _MainCamera;        //主摄像机
		private Camera _UICamera;           //UI摄像机

		private Text _UIText;       //显示控件

		private float _IntSubHP = 0;		//减少的生命数值

		//敌人属性脚本
		//private Ctrl_BaseEnemy_Prop _MyProperty;

		/*需要细调*/

		public readonly float FloLength = 2f;  //飘字预设长度
		public readonly float FloHeight = 1f;  //飘条预设宽度
		public  float FloUp = 2f;		//飘字的抬高度（位置的偏移量）


		/// <summary>
		/// 外部调用，公共方法：设置敌人目标
		/// （在生成敌人时调用，以获得敌人游戏对象）
		/// （如何获得？？？）
		/// </summary>
		/// <param name="goEnemy"></param>
		public void SetTargetEnemy(GameObject goEnemy) {
			_GOTargetEnemy = goEnemy;
		}

		/// <summary>
		/// 外部调用，公共方法：设置减少的生命数值
		/// </summary>
		/// <param name="SubValue"></param>
		public void SetSubHPValue(int SubValue) {
			_IntSubHP = SubValue;
		}


		private void Start() {

			//_MyProperty = _GOTargetEnemy.GetComponent<Ctrl_BaseEnemy_Prop>();

			//得到Text控件
			_UIText = this.gameObject.GetComponent<Text>();
			//主摄像机
			_MainCamera = Camera.main;
			//UI摄像机
			_UICamera = GameObject.FindGameObjectWithTag(Tag.Tag_UICamera).GetComponent<Camera>();
			//参数检查
			if (_GOTargetEnemy == null) {
				Debug.LogError(GetType() + "\t 目标敌人对象为空！");
				return;
			}
			if(_IntSubHP == 0) {
				Debug.LogError(GetType() + "\t 非法数值！");
				return;
			}
		}

		/// <summary>
		/// 计算敌人的血量减少量
		/// </summary>
		private void Update() {
			if (Time.frameCount % 2 == 0) { 
			//控件显示血量
			_UIText.text = _IntSubHP.ToString();

			//控件尺寸
			this.transform.localScale = new Vector3(FloLength, FloHeight, 0);
			//位置的偏移量（向上移动）
			//
			FloUp += 0.01f;
				//销毁：使用对象缓冲池
			}

		}


		/// <summary>
		/// 飘字特效，三维坐标系转UI坐标系
		/// </summary>
		private void LateUpdate() {
			if (_GOTargetEnemy != null) {
				if (Time.frameCount % 2 == 0) {
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


		IEnumerator RecoverGOByTime() {
			yield return new WaitForSeconds(2f);
			PoolManager.PoolsArray["_ParticalSys"].RecoverGameObject(this.gameObject);

		}


	}
}
