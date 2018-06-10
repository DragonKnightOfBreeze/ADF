//控制层，主角动画控制

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Kernel;
using Model;


namespace Control {
	public class Ctrl_HeroAnimationCtrl : BaseControl {

		public static Ctrl_HeroAnimationCtrl Instance;  //本类的实例

		//主角的动画状态（最初的状态不要设置为Idle）
		//主角的动画状态应该是唯一的
		//例如，不能在移动的同时攻击，必须先暂停移动。
		private HeroActionState _CurrentActionState = HeroActionState.None;

		/* 动画片段*/

		public AnimationClip Anc_Idle;			//休闲
		public AnimationClip Anc_Running;		//跑动
		public AnimationClip Anc_NormalAtk1;	//普通攻击（三个循环播放或随机播放）
		public AnimationClip Anc_NormalAtk2;	//普通攻击
		public AnimationClip Anc_NormalAtk3;    //普通攻击
		public AnimationClip Anc_CurNormalAtk;	//当前的普通攻击

		public AnimationClip Anc_MagicAtkA;		//魔法攻击1
		public AnimationClip Anc_MagicAtkB;     //魔法攻击2

		/* 粒子特效*/

		public GameObject ppHeroNormalAtk1;
		public GameObject ppHeroNormalAtk2;
		public GameObject ppHeroMagicAtkA;
		public GameObject ppHeroMagicAtkB;
		

		private CharacterController _cc;

		//动画句柄
		private Animation _AnimationHandle;
		//定义动画单次开关（按一次键，只攻击一次）
		//private bool _IsSinglePlay = true;

		private float _FwRate = 1f;			//攻击的前进距离
		private static float _RecheckTime= 0;   //再次判断时间
		private static float _AccTime = 2f;    //加快速度

		//定义动画连招
		private NormalAtkComboState _CurAtkCombo = NormalAtkComboState.NormalAtk1;


		//设置只读属性，获取主角当前的动画状态
		public HeroActionState CurrentActionState {
			get { return _CurrentActionState;}
		}

		public float ReCheckTime {
			get {
				return _RecheckTime;
			}
			set {
				_RecheckTime = value;
			}
		}

		public NormalAtkComboState CurAtkCombo {
			get {
				return _CurAtkCombo;
			}
		}


		//单次开关
		private bool _IsSingleTime = true;



		private void Awake() {
			//事件注册（等级提升）
			Mod_PlayerKernelDataProxy.eve_PlayerKernalData += LevelUp;
			Instance = this;
		}

		private void Start() {
			//默认动作状态
			_CurrentActionState = HeroActionState.Idle;
			//得到动画句柄实例
			_AnimationHandle = this.GetComponent<Animation>();

			_cc = this.GetComponent<CharacterController>();


			//加快普通连招的播放速度
			_AnimationHandle[Anc_NormalAtk1.name].speed = _AccTime;
			_AnimationHandle[Anc_NormalAtk2.name].speed = _AccTime;
			_AnimationHandle[Anc_NormalAtk3.name].speed = _AccTime;

			//启动协程，控制主角动画
			StartCoroutine("CtrlHeroAnimationState");
		}

		/// <summary>
		/// 设置当前动画状态
		/// </summary>
		/// <param name="currentActionState"></param>
		public void SetCurrentActionState(HeroActionState currentActionState) {
			_CurrentActionState = currentActionState;
		}

		//使用动态等待时间不是一个好方法！
		/// <summary>
		/// 主角的动画控制
		/// </summary>
		IEnumerator  CtrlHeroAnimationState() {
			while(true) {

				ReCheckTime = GlobalParameter.CHECK_TIME;
				switch (CurrentActionState) {
					case HeroActionState.None:
						yield return new WaitForSeconds(ReCheckTime);
						break;

					case HeroActionState.Idle:
						//播放动画
						_AnimationHandle.CrossFade(Anc_Idle.name);
						yield return new WaitForSeconds(ReCheckTime);
						break;

					case HeroActionState.Running:
						_AnimationHandle.CrossFade(Anc_Running.name);
						yield return new WaitForSeconds(ReCheckTime);
						break;

					case HeroActionState.NormalAtk:

						//攻击连招处理（自动状态转换）
						//有缺陷，无论间隔时间有多长，必定会连上
						switch (CurAtkCombo) {
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

						ForwardWhileHurt(_FwRate);
						//StartCoroutine(SinglePlay(Anc_CurNormalAtk));
						
						_AnimationHandle.CrossFade(Anc_CurNormalAtk.name);
						//协程的等待值（总是和执行的动画长度保持一致）
						//这个等待时间必须写且写在无限循环里面，并且应该恰好足够长，否则调试时Unity会卡死（死循环）
						ReCheckTime = Anc_CurNormalAtk.length / _AccTime;
						yield return new WaitForSeconds(ReCheckTime);
						SetCurrentActionState(HeroActionState.Idle);

						break;

					case HeroActionState.MagicAtkA:
						//StartCoroutine(SinglePlay(Anc_MagicAtkA));

						// // Debug.Log("单次动画控制");
						_AnimationHandle.CrossFade(Anc_MagicAtkA.name);
						ReCheckTime = Anc_MagicAtkA.length / _AccTime;
						yield return new WaitForSeconds(ReCheckTime);
						SetCurrentActionState(HeroActionState.Idle);
						
						break;

					case HeroActionState.MagicAtkB:
						//StartCoroutine(SinglePlay(Anc_MagicAtkB));

						// // Debug.Log("单次动画控制");
						ReCheckTime = Anc_MagicAtkB.length / _AccTime;
						_AnimationHandle.CrossFade(Anc_MagicAtkB.name);
						yield return new WaitForSeconds(ReCheckTime);
						SetCurrentActionState(HeroActionState.Idle);
						break;

					default:
						yield return new WaitForSeconds(ReCheckTime);
						break;
				}//switch_end
			}
		}



		#region 动画事件

		/// <summary>
		/// 动画事件_主角技能攻击A
		/// 添加到技能攻击A动画的合适的帧上
		/// </summary>
		/// <returns></returns>
		public IEnumerator AniEvent_Hero_MagicAtkA() {
			/* 重构之后的代码片段，传统方式 */

			//StartCoroutine(base.LoadParticalEffect("ParticleProps/Hero_MagicAtkA(groundBrokeRed)",this.transform,new Vector3(0f,0f,5f), 2f));
			//yield break;

			/* 使用对象缓冲池技术 */

			//在缓冲池中，得到一个指定的预设“激活体”
			ppHeroMagicAtkA.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 5f));
			PoolManager.PoolsArray["_ParticleSys"].GetGameObject(ppHeroMagicAtkA, transform.position, transform.rotation);
			yield break;

			/* 重构之前的代码片段 */

			//yield return new WaitForSeconds(GlobalParameter.WAIT_FOR_PP);
			//GameObject goMagicAtkA = ResourceMgr.GetInstance().LoadAsset("ParticleProps/Hero_MagicAtkA(groundBrokeRed)", true);        //得到特效动画，启用缓存

			//goMagicAtkA.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 5f));    //设置特效的位置

			////定义特效的父子对象
			//goMagicAtkA.transform.parent = transform;
			////注意要对齐方向
			//goMagicAtkA.transform.rotation = gameObject.transform.rotation;
			//定义特效音频

		}

		/// <summary>
		/// 动画事件_主角技能攻击B
		/// 添加到技能攻击A动画的合适的帧上
		/// </summary>
		/// <returns></returns>
		public IEnumerator AniEvent_Hero_MagicAtkB() {

			/* 重构之后的代码片段，传统方式 */

			//StartCoroutine(base.LoadParticalEffect("ParticleProps/Hero_MagicAtkB(bruceSkill)", this.transform, new Vector3(0f, 0f, 2f), 2f));
			//yield break;

			/* 使用对象缓冲池技术 */

			//在缓冲池中，得到一个指定的预设“激活体”
			ppHeroMagicAtkB.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 1f));
			PoolManager.PoolsArray["_ParticleSys"].GetGameObject(ppHeroMagicAtkB, transform.position, transform.rotation);
			yield break;
		}


		/// <summary>
		/// 定义普通攻击的粒子特效（横向斩）
		/// </summary>
		public IEnumerator AniEvent_Hero_NormalAtk1() {

			/* 重构之后的代码片段，传统方式 */

			//StartCoroutine(base.LoadParticalEffect("ParticleProps/Hero_NormalAtk1", this.transform, new Vector3(0f, 0f, 1f), 1f));
			//yield break;

			/* 使用对象缓冲池技术 */

			//在缓冲池中，得到一个指定的预设“激活体”
			ppHeroNormalAtk1.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 1f));
			PoolManager.PoolsArray["_ParticleSys"].GetGameObject(ppHeroNormalAtk1,transform.position,transform.rotation);
			yield break;
		}


		/// <summary>
		/// 定义普通攻击的粒子特效（纵向斩）
		/// </summary>
		public IEnumerator AniEvent_Hero_NormalAtk2() {

			/* 重构之后的代码片段，传统方式 */

			//StartCoroutine(base.LoadParticalEffect("ParticleProps/Hero_NormalAtk2", this.transform, new Vector3(0f, 0f, 1f), 1f));
			//yield break;

			/* 使用对象缓冲池技术 */

			//在缓冲池中，得到一个指定的预设“激活体”
			ppHeroNormalAtk2.transform.position = transform.position + transform.TransformDirection(new Vector3(0f, 0f, 1f));
			PoolManager.PoolsArray["_ParticleSys"].GetGameObject(ppHeroNormalAtk2, transform.position, transform.rotation);
			yield break;

		}

		#endregion



		/// <summary>
		/// 主角升级
		/// </summary>
		private void LevelUp(KeyValuesUpdate kv) {
			if (kv.Key.Equals("Level")) {
				if (_IsSingleTime) {
					_IsSingleTime = false;
				}
				else {
					HeroLevelUp();
				}
			}
		}

		/// <summary>
		/// 主角升级时，播放粒子特效
		/// </summary>
		private void HeroLevelUp() {

			/* 重构之后的代码片段，传统方式 */

			GameObject goHeroLevelUp = ResourceMgr.GetInstance().LoadAsset("ParticleProps/Hero_LevelUp",true);
			//增加音效
			AudioManager.PlayAudioEffect("LevelUp");

		

		}


		/*
		/// <summary>
		/// 单次动画控制（按下1次按键，动画只播放1次）
		/// 状态值是瞬时的
		/// </summary>
		/// <param name="ac"></param>
		/// <returns></returns>
		IEnumerator SinglePlay(AnimationClip ac) {
			// // Debug.Log("单次动画控制");
			WaitTime = ac.length / accTime;
			_AnimationHandle.CrossFade(ac.name);

			yield return new WaitForSeconds(WaitTime);
			SetCurrentActionState(HeroActionState.Idle);
		}

		*/

		/// <summary>
		/// 攻击时的位移
		/// </summary>
		/// <param name="">位移程度</param>
		private void ForwardWhileHurt(float bwRate) {
			Vector3 v = transform.forward * bwRate * Time.deltaTime;
			_cc.Move(v);
		}




		/*
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
		*/

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