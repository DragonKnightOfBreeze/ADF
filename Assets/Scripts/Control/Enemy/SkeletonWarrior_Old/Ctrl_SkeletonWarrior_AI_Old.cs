//控制层，敌人：骷髅战士的AI系统
//功能：
//1.思考过程
//2.移动过程

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Global;
using Kernel;

namespace Control {

	public class Ctrl_SkeletonWarrior_AI_Old : BaseControl {
		//使用随机数，实现个体差异性（简易版）
		public float FloMinAlertDist = 2.5f; //最小警戒距离（再小就会攻击）
		public float FloMinIdleDist = 5f;   //最小空闲距离

		public float FloThinkTime = 1f;     //思考间隔时间
		//private float _WaitTime= 0f;
		//private float _Length;

		public bool SingleCtrl = true;
		//private bool _Check = true;

		private GameObject _GoHero;     //主角
		private Transform _MyTransform; //当前敌人的方位
		private Ctrl_BaseEnemy_Prop_Old _MyProperty;  //得到当前敌人的属性脚本
		private Animator _MyAnimator;

		private CharacterController _cc;    //当前敌人的角色控制器

		private void OnEnable() {
			//这个两个协程应该只在自然状态（例如，非受伤）下进行
			//如果不是则结束，如果又是则重新开始

			//开始思考协程
			StartCoroutine("ThinkProcess");
			//开始移动协程
			StartCoroutine("MovingProcess");
		}

		private void OnDisable() {
			//停止思考协程
			StopCoroutine("ThinkProcess");
			//停止移动协程
			StopCoroutine("MovingProcess");
		}



		private void Start() {
			//得到主角
			_GoHero = GameObject.FindGameObjectWithTag(Tag.Tag_Player);
			//当前范围
			_MyTransform = this.gameObject.transform;
			//得到“属性”实例
			_MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemy_Prop_Old>();
			_MyAnimator = this.gameObject.GetComponent<Animator>();
			//得到角色控制器
			_cc = this.gameObject.GetComponent<CharacterController>();

			//确定个体差异性参数
			FloMinIdleDist = UnityHelper.GetInstance().GetRandomNum(5, 10);
			FloThinkTime = UnityHelper.GetInstance().GetRandomNum(1, 4);

			////这个两个协程应该只在自然状态（例如，非受伤）下进行
			////如果不是则结束，如果又是则重新开始

			////开始思考协程
			//StartCoroutine("ThinkProcess");
			////开始移动协程
			//StartCoroutine("MovingProcess");
		}

		/// <summary>
		/// 敌人的思考协程
		/// 较简单的方法：不断得到相对位置
		/// 注意正在播放的动画状态和设置的动画状态的同步问题！
		/// 当敌人进入受伤等硬直状态时需要重置该协程
		/// </summary>
		/// <returns></returns>
		IEnumerator ThinkProcess() {
			yield return new WaitForSeconds(1f);
			while (true) {

				//应该等到当前动画播放结束后才能进行判断
				//（除了等待和警戒状态）

				//如果敌人存在，且未死亡
				if (_MyProperty && _MyProperty.CurrentState != EnemyActionState.Dead) {

					//得到主角的当前方位值
					Vector3 VecHero = _GoHero.transform.position;
					//得到主角与当前（敌人）的距离
					float floDistance = Vector3.Distance(VecHero, _MyTransform.position);

					//距离判断

					//小于攻击距离
					if (floDistance < FloMinAlertDist) {
						// // Debug.Log("进行攻击");
						_MyProperty.CurrentState = EnemyActionState.NormalAtk;
						yield return new WaitForSeconds(FloThinkTime - GlobalParameter.CHECK_TIME);

						// 						if (UnityHelper.GetInstance().GetSmallTime(FloThinkTime, true)) {
						//进入攻击状态
						//if(_MyProperty.CurSinglePlayState ==SinglePlayState.Init) {
						//if (waitTime == 0 || waitTime >=FloThinkTime) {
						//waitTime = 0;
						//if (_MyProperty.CurrentState != EnemyActionState.NormalAtk) {

						
						//if (SingleCtrl) {
						//	// // Debug.Log("进行攻击");
							//_Length = _MyAnimator.GetCurrentAnimatorStateInfo(0).length/3;
						//	_WaitTime = FloThinkTime;
						//	_MyProperty.CurrentState = EnemyActionState.NormalAtk;
						
						//	SingleCtrl = false;
						//}

						
						//if (UnityHelper.GetInstance().GetSmallTime(FloThinkTime)) {
						//		if (!SingleCtrl) {
						//			SingleCtrl = true;
						//	}
						//}


						//if(_MyProperty.CurrentState != EnemyActionState.Idle &&_MyProperty.CurrentState != EnemyActionState.NormalAtk){
						//	waitTime = 0;
						//}
						

						//if (_MyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
						//	if (_SingleCtrl) {
							
								//// // Debug.Log("进行攻击");
								//_MyProperty.CurrentState = EnemyActionState.NormalAtk;
								//_SingleCtrl = false;
						//	}
						//}
						//else {
						//	_SingleCtrl = true;
						//}

						//}
						//}
						//else {
						//waitTime += Time.deltaTime;
						//}

						//_MyProperty.CurSinglePlayState = SinglePlayState.Start;
						//}
						//						}
					}

					//小于警戒距离
					else if (floDistance < FloMinIdleDist) {
							//进入警戒（追击）状态
							_MyProperty.CurrentState = EnemyActionState.Moving;	
					}
					//大于警戒距离
					else {
						//进入休闲状态
						//if (!_MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(EnemyActionState.Idle.ToString())) {
						_MyProperty.CurrentState = EnemyActionState.Idle;
						//}
					}
				}

				yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);
				//yield return new WaitForSeconds(GlobalParameter.CHECK_TIME+waitTime);
			}
		}

		/// <summary>
		/// 敌人的移动协程
		/// 当敌人进入受伤等硬直状态时需要重置该协程
		/// </summary>
		/// <returns></returns>
		IEnumerator MovingProcess() {
			//建议将这里的参数调的非常小
			yield return new WaitForSeconds(1f);

			while (true) {

				if (_MyProperty && _MyProperty.CurrentState != EnemyActionState.Dead) {
					//注视
					FaceToHero();

					//_WaitTime = 0f;

					//敌人移动
					switch (_MyProperty.CurrentState) {
						case EnemyActionState.Idle:
							break;

						case EnemyActionState.Moving:
							//if (_MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Moving")) { 
							//限定长度（英雄方位-当前敌人方位）
							//这里不实用导航寻路
							Vector3 v = Vector3.ClampMagnitude((_GoHero.transform.position - _MyTransform.position), _MyProperty.FloMoveSpeed * Time.deltaTime);
							_cc.Move(v);

							////敌人受伤，并且被击退
							//else if (_MyProperty.CurrentState == EnemyState.Hurt) {
							//	Vector3 v1 = transform.forward * _MyProperty.FloMoveSpeed / 2 * Time.deltaTime;
							//	_cc.Move(v1);
							//}
							break;

						case EnemyActionState.NormalAtk:
							break;

						case EnemyActionState.Hurt:
							break;

						default:
							break;
					}
				}
				yield return new WaitForSeconds(GlobalParameter.CHECK_TIME);

			}
		}


		/// <summary>
		/// 敌人面向主角
		/// </summary>
		public void FaceToHero() {

			//this.transform.rotation = Quaternion.Slerp(this.transform.rotation,Quaternion.LookRotation(new Vector3(_GOHero.transform.position.x, 0, _GOHero.transform.position.z) - new Vector3(_MyTransform.position.x, 0, _MyTransform.position.z)),FloRotationSpeed);

			//重构代码

			UnityHelper.GetInstance().FaceToGoal(this.gameObject.transform, _GoHero.transform, _MyProperty.FloRotationSpeed);
		}


	}
}