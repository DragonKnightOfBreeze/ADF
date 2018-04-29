//控制层，主角动画控制

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Kernel;

namespace Control {
	public class Ctrl_HeroAnimationCtrl : BaseControl {

		public static Ctrl_HeroAnimationCtrl Instance;  //本类的实例
		//主角的动画状态（最初的状态不要设置为Idle）
		private HeroActionState _CurrentActionState = HeroActionState.None;

		public AnimationClip Anc_Idle;			//休闲
		public AnimationClip Anc_Running;		//跑动
		public AnimationClip Anc_NormalAtk1;	//普通攻击（三个循环播放或随机播放）
		public AnimationClip Anc_NormalAtk2;	//普通攻击
		public AnimationClip Anc_NormalAtk3;    //普通攻击
		public AnimationClip Anc_CurNormalAtk;	//当前的普通攻击

		public AnimationClip Anc_MagicAtkA;		//魔法攻击1
		public AnimationClip Anc_MagicAtkB;		//魔法攻击2

		//动画句柄
		private Animation _AnimationHandle;
		//定义动画单次开关（按一次键，只攻击一次）
		private bool _IsSinglePlay = true;

		private  static float accTime = 2.5f;	//加快速度
		private static float waitTime;

		//定义动画连招
		private NormalAtkComboState _CurAtkCombo = NormalAtkComboState.NormalAtk1;


		//设置只读属性，获取主角当前的动画状态
		public HeroActionState CurrentActionState {
			get { return _CurrentActionState;}
		}


		private void Awake() {
			Instance = this;
		}

		private void Start() {
			//默认动作状态
			_CurrentActionState = HeroActionState.Idle;
			//得到动画句柄实例
			_AnimationHandle = this.GetComponent<Animation>();
			//启动协程，控制主角动画
			StartCoroutine("CtrlHeroAnimationState");

			//加快普通连招的播放速度
			_AnimationHandle[Anc_NormalAtk1.name].speed = accTime;
			_AnimationHandle[Anc_NormalAtk2.name].speed = accTime;
			_AnimationHandle[Anc_NormalAtk3.name].speed = accTime;
		}

		/// <summary>
		/// 设置当前动画状态
		/// </summary>
		/// <param name="currentActionState"></param>
		public void SetCurrentActionState(HeroActionState currentActionState) {
			_CurrentActionState = currentActionState;
		}

		/// <summary>
		/// 主角的动画控制
		/// </summary>
		IEnumerator  CtrlHeroAnimationState() {
			while(true) {
				waitTime = 0.1f;
				switch (CurrentActionState) {
					case HeroActionState.None:
						break;
					case HeroActionState.Idle:
						//播放动画
						_AnimationHandle.CrossFade(Anc_Idle.name);
						break;
					case HeroActionState.Running:
						_AnimationHandle.CrossFade(Anc_Running.name);
						break;
					case HeroActionState.NormalAtk:

						//攻击连招处理（自动状态转换）
						//有缺陷，无论间隔时间有多长，必定会连上
						switch (_CurAtkCombo) {
							case NormalAtkComboState.NormalAtk1:
								_CurAtkCombo = NormalAtkComboState.NormalAtk2;
								Anc_CurNormalAtk = Anc_NormalAtk1;
								break;
							case NormalAtkComboState.NormalAtk2:
								_CurAtkCombo = NormalAtkComboState.NormalAtk3;
								Anc_CurNormalAtk = Anc_NormalAtk2;
								break;
							case NormalAtkComboState.NormalAtk3:
								_CurAtkCombo = NormalAtkComboState.NormalAtk1;
								Anc_CurNormalAtk = Anc_NormalAtk3;
								break;
							default:
								break;
						}

						StartCoroutine(SinglePlay(Anc_CurNormalAtk));
						break;
					case HeroActionState.MagicAtkA:
						StartCoroutine(SinglePlay(Anc_MagicAtkA));
						break;
					case HeroActionState.MagicAtkB:
						StartCoroutine(SinglePlay(Anc_MagicAtkB));
						break;
					default:
						break;
				}//switch_end

				//协程的等待值（总是和执行的动画长度保持一致）
				yield return new WaitForSeconds(waitTime / accTime);
			}
		}


		/// <summary>
		/// 单次动画控制（按下1次按键，动画只播放1次）
		/// </summary>
		/// <param name="ac"></param>
		/// <returns></returns>
		private IEnumerator SinglePlay(AnimationClip ac) {
			if (_IsSinglePlay) {
				waitTime = ac.length;
				//等待一段时间，可认为是前摇时间
				yield return new WaitForSeconds(0.05f);
				_IsSinglePlay = false;
				_AnimationHandle.CrossFade(ac.name);
			} else {
				_CurrentActionState = HeroActionState.Idle;
				waitTime = 0.1f;
				//等待一段时间，可以认为是后摇时间
				yield return new WaitForSeconds(0.05f);
				_IsSinglePlay = true;
			}
			
		}

		///// <summary>
		///// 返回原始状态（Idle）
		///// </summary>
		//private IEnumerator ReturnOriginAction() {
		//	waitTime = 0;
		//	_CurrentActionState = HeroActionState.Idle;
		//	//等待一段时间，可以认为是后摇时间
		//	yield return new WaitForSeconds(0.05f);
		//	_IsSinglePlay = true;
		//}
	}
}