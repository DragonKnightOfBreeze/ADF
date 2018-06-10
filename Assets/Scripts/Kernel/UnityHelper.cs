//核心层，帮助类
//作用：集成大量通用算法
//指定为单例类

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Kernel {
	public class UnityHelper {
		private static UnityHelper _Instance;	//本类实例
		private float floDeltaTime = 0;     //累加时间
		//private float floCheckTime = 0;		//判断时间
		//private bool _SingleCtrl = true;

		private UnityHelper() {}


		/// <summary>
		/// 得到本类实例（单例）
		/// </summary>
		/// <returns></returns>
		public static UnityHelper GetInstance() {
			if(_Instance ==null) {
				_Instance = new UnityHelper();
			}
			return _Instance;
		}


		/// <summary>
		/// 间隔指定时间段，返回bool值
		/// 我觉得不需要
		/// </summary>
		/// <param name="smallIntervalTime">指定的时间段间隔（0.1-3f秒之间）</param>
		/// <returns>true：表明指定的时间段到了</returns>
		public bool GetSmallTime(float smallIntervalTime) {
			floDeltaTime += Time.deltaTime;
			//到了指定时间就归零，并且返回true 

			if (floDeltaTime >= smallIntervalTime) {
				floDeltaTime = 0;
				return true;
			}
			else {
				return false;
			}
		}

		//public bool GetSmallTime(float smallIntervalTime, bool includeStart) {
		//	floDeltaTime += Time.deltaTime;
		//	//到了指定时间就归零，并且返回true 
		//	if (includeStart == true && floDeltaTime == 0) {
		//		return true;
		//	}
		//	else if (floDeltaTime >= smallIntervalTime) {
		//		floDeltaTime = 0;
		//		return true;
		//	}
		//	else {
		//		return false;
		//	}
		//}

		///// <summary>
		///// 开始判断单次动画播放状态
		///// </summary>
		///// <param name="aniLength"></param>
		//public void CheckState(SinglePlayState curState, float aniLength) {
		//	if (curState == SinglePlayState.Start || curState == SinglePlayState.Playing) {
		//		floCheckTime += Time.deltaTime;
		//		if (floCheckTime >= aniLength) {
		//			curState = SinglePlayState.End;
		//			floCheckTime = 0;
		//		}
		//	}
		//	else {
		//		return;
		//	}	
		//}



		/// <summary>
		/// （角色）面向指定目标旋转
		/// 适用于所有人形角色
		/// </summary>
		/// <param name="self">本身</param>
		/// <param name="goal">目标</param>
		/// <param name="rotationSpeed">旋转速度</param>
		public void FaceToGoal(Transform self,Transform goal,float rotationSpeed) {
				self.rotation = Quaternion.Slerp(self.rotation, Quaternion.LookRotation(new Vector3(goal.position.x, 0, goal.position.z) - new Vector3(self.position.x, 0, self.position.z)), rotationSpeed);
		}



		/// <summary>
		/// 得到指定范围的随机整数
		/// </summary>
		/// <param name="minNum"></param>
		/// <param name="MaxNum"></param>
		/// <returns></returns>
		public int GetRandomNum(int minNum,int maxNum) {
			int num;
			if(minNum == maxNum) {
				num = minNum;
			}
			num = Random.Range(minNum,maxNum+1);
			return num;
		}

		/// <summary>
		/// 交换数值
		/// </summary>
		public void SwapValue<T>(T value_A,T value_B) {
			T temp = value_A;
			value_A = value_B;
			value_B = temp;
		}
	}
}