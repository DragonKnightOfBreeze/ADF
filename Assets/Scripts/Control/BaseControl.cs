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
			//转到下一个场景（首先加载Loading场景）
			GlobalParaMgr.NextSceneName = sceneEnumName;
			Application.LoadLevel(ConvertEnumToStr.GetInstance().GetStrByEnumScene(SceneEnum.LoadingScene));
		}

		/// <summary>
		/// 公共方法：攻击敌人
		/// </summary>
		/// <param name="list">敌人列表（为了脱藕独立使用）</param>
		/// <param name="traNearestEnemy">最近的敌人（为了脱藕独立使用）</param>
		/// <param name="attackArea">攻击范围</param>
		/// <param name="attackMultiple">攻击倍率</param>
		/// <param name="isDirection">是否有方向性</param>
		protected void AttackEnemy(List<GameObject> list, Transform traNearestEnemy, float attackArea, float attackMultiple = 1f, bool isDirection = true) {

			//参数检查，如果敌人数量小于等于0，则直接跳过
			if (list == null || list.Count <= 0) {
				traNearestEnemy = null;
				return;
			}

			//对多个敌人进行攻击判定
			foreach (GameObject enemyItem in list) {
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
							enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetATK() * attackMultiple, SendMessageOptions.DontRequireReceiver);
						}
					}
					else {
						//如果在有效攻击范围内，则对敌人进行伤害处理。
						if (floDistance <= attackArea) {
							//不需要返回值，更好的办法是使用委托事件
							//参数：方法名，角色当前攻击力
							// // Debug.Log("OnHurt!");
							enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetATK() * attackMultiple, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			}
		}


		/// <summary>
		/// 粒子特效加载的公共方法
		/// 更好的方式：使用对象缓冲池技术
		/// </summary>
		/// <param name="PEName">粒子特效的名字</param>
		/// <param name="parentTra">父对象的方位</param>
		/// <param name="PERePosition">对于父对象的相对位置</param>
		/// <param name="destroyTime">等待破坏时间</param>
		/// <param name="isCatch"></param>
		/// <param name="strAudioEffect">播放的音频剪辑</param>
		/// <returns></returns>
		protected IEnumerator LoadParticalEffect(string PEName,Transform parentTra,Vector3 PERePosition, float destroyTime
			, bool isCatch = true, string strAudioEffect = null) {
			// 间隔时间
			yield return new WaitForSeconds(GlobalParameter.WAIT_FOR_PP);

			//提取的粒子预设
			GameObject goParticalPrefab = ResourceMgr.GetInstance().LoadAsset(PEName, isCatch);
			//设置父子对象
			goParticalPrefab.transform.parent = parentTra;
			//设置特效的生成位置
			goParticalPrefab.transform.position = parentTra.position + parentTra.TransformDirection(PERePosition);
			//设置特效的生成方向
			goParticalPrefab.transform.rotation = parentTra.rotation;
			//特效音频（这里调用的方法，应该和主角音效播放调用的方法不同）
			AudioManager.PlayAudioEffectB(strAudioEffect);

			//定义销毁时间
			if (destroyTime > 0) { 
			Destroy(goParticalPrefab, destroyTime);
			}
		}



		/// <summary>
		/// 粒子特效加载的公共方法（重载）
		/// 更好的方式：使用对象缓冲池技术
		/// </summary>
		/// <param name="PEName">粒子特效的名字</param>
		/// <param name="parentTra">父对象的位置</param>
		/// <param name="position">生成位置</param>
		/// <param name="quaternion">生成方向</param>
		/// <param name="destroyTime">等待销毁时间</param>
		/// <param name="isCatch">是否缓存</param>
		/// <param name="strAudioEffect">播放的音频剪辑</param>
		/// <returns></returns>
		protected IEnumerator LoadParticalEffect(string PEName,Transform parentTra, Vector3 position,Quaternion quaternion,float createTime = 0.02f, float destroyTime = 10f, bool isCatch = true, string strAudioEffect = null) {
			// 间隔时间
			yield return new WaitForSeconds(createTime);
			//提取的粒子预设
			GameObject goParticalPrefab = ResourceMgr.GetInstance().LoadAsset(PEName, isCatch);
			//设置父子对象
			if (parentTra != null) {
				goParticalPrefab.transform.parent = parentTra;
			}
			//设置特效的生成位置
			goParticalPrefab.transform.position = position;
			//设置特效的生成方向（粒子预设的旋转）
			goParticalPrefab.transform.rotation = quaternion;

			//可选：播放特效音频（这里调用的方法，应该和主角音效播放调用的方法不同）
			AudioManager.PlayAudioEffectB(strAudioEffect);
			//定义销毁时间
			if (destroyTime > 0) {
				Destroy(goParticalPrefab, destroyTime);
			}
		}









		/// <summary>
		/// 飘字特效方法，缓冲池加载
		/// </summary>
		/// <param name="internalTime">间隔时间</param>
		/// <param name="goPEPrefab">粒子预设特效</param>
		/// <param name="position">位置</param>
		/// <param name="quaternion">旋转</param>
		/// <param name="goTargetObj">目标对象</param>
		/// <param name="displayNum">显示的数值（HP减少值）</param>
		/// <param name="traParent">父节点</param>
		/// <param name="audioEffect">播放的音效</param>
		/// <returns></returns>
		protected IEnumerator LoadPEInPool_MoveUpLabel(float internalTime, GameObject goPEPrefab, Vector3 position, Quaternion quaternion, GameObject goTargetObj, int displayNum, Transform traParent, AudioClip audioEffect = null) {
			//克隆出的对象
			GameObject goPEClone;

			//间隔时间
			yield return new WaitForSeconds(internalTime);

			//在缓冲池中激活指定的对象
			goPEClone = PoolManager.PoolsArray["_ParticalSys"].GetGameObject(goPEPrefab, position, quaternion);
			//参数赋值
			if (goPEClone != null) {
				goPEClone.GetComponent<MoveUpLabel>().SetTargetEnemy(goTargetObj);
				goPEClone.GetComponent<MoveUpLabel>().SetSubHPValue(displayNum);
			}

			//确定父节点
			if (traParent != null) {
				goPEClone.transform.parent = traParent;
			}
			//特效音频
			if (audioEffect != null) {
				AudioManager.PlayAudioEffectB(audioEffect);
			}
		}




		/// <summary>
		/// 生成敌人（加入对象缓冲池技术）
		/// </summary>
		/// <param name="enemy">生成的敌人的预置体</param>
		/// <param name="spawnNum">生成敌人的数量</param>
		/// <param name="spawnTras">生成地点数组</param>
		/// <param name="isRaodom">是否随机生成（未写好）</param>
		/// <param name="hasHPBar">是否挂载血条</param>
		/// <returns></returns>
		public IEnumerator SpawnEnemy(GameObject enemy,int spawnNum,Transform[] spawnTras,bool isRaodom = true,bool hasHPBar = true) {
			yield return new WaitForSeconds(1f);

			for (int i = 1; i <= spawnNum; i++) {
				//克隆的对象
				GameObject goClone;

				//定义克隆体随机出现的位置
				Transform traEnemySpawnPosition = GetRandomEnemySpawnPosition(spawnTras);
				//在“对象缓冲池“”中激活指定的对象
				//***待优化：随机旋转，或者面向玩家出现***
				 goClone =  PoolManager.PoolsArray["_Enemys"].GetGameObject(enemy, traEnemySpawnPosition.position, Quaternion.identity);

				//如果需要挂载血条
				if (hasHPBar) {
					//调用预设
					GameObject goEnemyHP = ResourceMgr.GetInstance().LoadAsset("Prefabs/UI/UI_Enemy_HPBar", true);
					//确定父节点
					goEnemyHP.transform.parent = GameObject.FindGameObjectWithTag("Tag_UIPlayerInfo").transform;
					//参数赋值
					goEnemyHP.GetComponent<Enemy_HPBar>().SetTargetEnemy(goClone);
				}

				yield return new WaitForSeconds(2f);
			}
		}


		/// <summary>
		/// 得到敌人的随机出生点位置
		/// </summary>
		/// <param name="spawnTras">敌人位置数组</param>
		/// <returns></returns>
		public Transform GetRandomEnemySpawnPosition(Transform[] spawnTras) {
			int randomNum = UnityHelper.GetInstance().GetRandomNum(0, spawnTras.Length - 1);
			return spawnTras[randomNum];
		}
	}
}
