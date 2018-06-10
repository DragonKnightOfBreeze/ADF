//视图层，UI虚拟按键按压事件处理

using System.Collections;

using System.Collections.Generic;
using UnityEngine;

using Global;
using Control;
using Kernel;

namespace View {

	public class View_NormalAtkPressed : StateMachineBehaviour {

#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR

		//可能需要放到全局参数类中
		//private const float _FloTime = 0.2f;

		// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
		//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

		// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks

		//更新动画状态时，不断调用该方法
		override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			//// // Debug.Log("OnstateUpdete");

			//防止使用次数过多

			if (UnityHelper.GetInstance().GetSmallTime(Ctrl_HeroAnimationCtrl.Instance.ReCheckTime )) {
				Ctrl_HeroAttackByET.Instance.ResponseATKByNormal();
			}
		}

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

		// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
		//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

		// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
		//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//
		//}

# endif

	}
}