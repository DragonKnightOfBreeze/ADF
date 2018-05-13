//控制层，英雄的展示

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {


	public class Ctrl_DisplayHero : MonoBehaviour {

		public AnimationClip AniIdle;		//动画剪辑~休闲
		public AnimationClip AniRunning;    //动画剪辑~跑动
		public AnimationClip AniAttack;     //动画剪辑~攻击
		private Animation _AniCurrentAnimation; //当前动画

		private float _FlointernalTimes = 3f;    //间隔时间
		private int _IntRandomPlayNum;				//随机动作编号


		void Start() {
			_AniCurrentAnimation = this.GetComponent<Animation>(); 
		}
		
		/// <summary>
		/// 算法：每隔3秒钟，随机播放一个人物动作。
		/// </summary>
		void Update() {
			_FlointernalTimes -= Time.deltaTime;	//累减
			if(_FlointernalTimes <= 0) {
				_FlointernalTimes = 3f;
				//得到随机数
				_IntRandomPlayNum = Random.Range(1, 4);
				DislpayHeroPlaying(_IntRandomPlayNum);
			}
		}

		/// <summary>
		/// 展示英雄的动作
		/// </summary>
		/// <param name="intPlayingNum"动作的编号></param>
		internal void DislpayHeroPlaying(int intPlayingNum) {
			switch (intPlayingNum) {
				case 1:
					DisplayIdel();
					break;
				case 2:
					DispayRunning();
					break;
				case 3:
					DisplayAttack();
					break;
				default:
					break;
			}
		}



		/// <summary>
		/// 展示休闲动作
		/// </summary>
		internal void DisplayIdel() {
			if(_AniCurrentAnimation) {
				_AniCurrentAnimation.CrossFade(AniIdle.name);
			}
		}

		/// <summary>
		/// 展示跑动动作
		/// </summary>
		internal void DispayRunning() {
			if (_AniCurrentAnimation) {
				_AniCurrentAnimation.CrossFade(AniRunning.name);
			}
		}

		/// <summary>
		/// 展示攻击动作
		/// </summary>
		internal void DisplayAttack() {
			if (_AniCurrentAnimation) {
				_AniCurrentAnimation.CrossFade(AniAttack.name);
			}
		}


	}
}