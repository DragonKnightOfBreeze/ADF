//控制层，基本控制脚本，其他控制脚本的父类

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;
using Kernel;

namespace Control {
	public class BaseControl : MonoBehaviour {

		/// <summary>
		/// 进入下一个场景
		/// </summary>
		/// <param name="sceneEnumName">场景名称（枚举）</param>
		protected void EnterNextScene(SceneEnum sceneEnumName) {
			GlobalParaMgr.NextSceneName = sceneEnumName; //转到下一个场景
			Application.LoadLevel(ConvertEnumToStr.GetInstance().GetStrByEnumScene(GlobalParaMgr.NextSceneName));
		}

		/// <summary>
		/// 公共方法：攻击敌人
		/// </summary>
		/// <param name="listEnemies">敌人列表（为了脱藕独立使用）</param>
		/// <param name="traNearestEnemy">最近的敌人（为了脱藕独立使用）</param>
		/// <param name="attackArea">攻击范围</param>
		/// <param name="attackMultiple">攻击倍率</param>
		/// <param name="isDirection">是否有方向性</param>
		protected void AttackEnemy(List<GameObject> listEnemies, Transform traNearestEnemy, float attackArea, float attackMultiple = 1f, bool isDirection = true) {

			//参数检查，如果敌人数量小于等于0，则直接跳过
			if (listEnemies == null || listEnemies.Count <= 0) {
				traNearestEnemy = null;
				return;
			}

			//对多个敌人进行攻击判定
			foreach (GameObject enemyItem in listEnemies) {
				//首先判断敌人是否活着
				//前提是该游戏对象存在
				//if (enemyItem && enemyItem.GetComponent<Ctrl_Enemy>().IsAlive) {

				//这里仍然有待优化
				if (enemyItem && enemyItem.GetComponent<Ctrl_BaseEnemy_Prop>().CurrentState != EnemyActionState.Dead) {
					//敌我距离
					float floDistance = Vector3.Distance(this.gameObject.transform.position, enemyItem.gameObject.transform.position);
					//定义敌我方向（使用向量减法）
					//如果攻击具有方向性
					if (isDirection) {
						Vector3 dir = (enemyItem.transform.position - this.transform.position).normalized;  //归一化，不要长度
																											//定义敌我夹角（使用向量点乘）
						float floDiretion = Vector3.Dot(dir, this.gameObject.transform.forward);

						//如果主角和敌人在同一方向，且在有效攻击范围内，则对敌人进行伤害处理。
						if (floDiretion > 0 && floDistance <= attackArea) {
							//不需要返回值，更好的办法是使用委托事件
							//参数：方法名，角色当前攻击力
							// // Debug.Log("OnHurt!");
							enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurATK() * attackMultiple, SendMessageOptions.DontRequireReceiver);
						}
					}
					else {
						//如果在有效攻击范围内，则对敌人进行伤害处理。
						if (floDistance <= attackArea) {
							//不需要返回值，更好的办法是使用委托事件
							//参数：方法名，角色当前攻击力
							// // Debug.Log("OnHurt!");
							enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurATK() * attackMultiple, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			}
		}


		/// <summary>
		/// 粒子特效加载的公共方法
		/// 更好的方式：使用对象缓冲池技术
		/// </summary>
		/// <param name="PEName"></param>
		/// <param name="traPaticalPrefab"></param>
		/// <param name="PERePosition">相对位置</param>
		/// <param name="destroyTime"></param>
		/// <param name="isCatch"></param>
		/// <param name="strAudioEffect"></param>
		/// <returns></returns>
		protected IEnumerator LoadParticalEffect(string PEName,Transform traPaticalPrefab,Vector3 PERePosition, float destroyTime
			,bool isCatch = true, string strAudioEffect = null) {
			// 间隔时间
			yield return new WaitForSeconds(GlobalParameter.WAIT_FOR_PP);

			//提取的粒子预设
			GameObject goParticalPrefab = ResourceMgr.GetInstance().LoadAsset(PEName, isCatch);
			//设置父子对象
			goParticalPrefab.transform.parent = traPaticalPrefab;
			//设置特效的位置
			goParticalPrefab.transform.position = traPaticalPrefab.position + traPaticalPrefab.TransformDirection(PERePosition);
			//对齐方向
			goParticalPrefab.transform.rotation = traPaticalPrefab.rotation;
			
			
			//特效音频（这里调用的方法，应该和主角音效播放调用的方法不同）
			AudioManager.PlayAudioEffectB(strAudioEffect);

			//定义销毁时间
			if (destroyTime > 0) { 
			Destroy(goParticalPrefab, destroyTime);
			}
		}
	
		
	}
}
