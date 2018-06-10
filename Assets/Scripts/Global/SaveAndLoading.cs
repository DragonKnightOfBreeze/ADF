//公共层，游戏的存档和读档
//***非常重要！***
//原理：
//对于“模型层”核心类，做“对象持久化”处理
//不可以把XML文件，放到StreamingAssets文件夹中去，因为可读不可写

//问题：如何遍历一个类中的所有公共属性，自动选择合适的名字？

using System;
using System.Collections.Generic;
using UnityEngine;

using Kernel;
using Model;

namespace Global {
	public class SaveAndLoading:MonoBehaviour {

		private static SaveAndLoading _Instance;   //本类单例实例

		#region【定义数据持久化路径】

		/* 数据持久化路径（persistentDataPath）*/
		/* 沙盒目录：支持文件的读和写，在运行时动态创建 */
		//全局参数对象路径
		private static string _FileNameByGlobalParameterData = Application.persistentDataPath + "/"+"GlobalParameterData.xml";
		//玩家核心数据对象路径
		private static string _FileNameByPlayerKernalData = Application.persistentDataPath + "/" + "PlayerKernalData.xml";
		//玩家扩展数据对象路径
		private static string _FileNameByPlayerExtendedData = Application.persistentDataPath + "/" + "PlayerExtendedData.xml";
		//玩家背包数据对象路径
		private static string _FileNameByPlayerPackageData = Application.persistentDataPath + "/" + "PlayerPackageData.xml";

		/* 模型层代理类 */

		//核心数据代理类
		private Mod_PlayerKernelDataProxy _PlayerKernelDataProxy;
		//拓展数据代理类
		private Mod_PlayerExtendedDataProxy _PlayerExtendedDataProxy;
		//背包数据代理类（最重要的物品数量，在核心数据类中）
		//所以这里到底该放什么才好呢。。。
		private Mod_PlayerPackageDataProxy _PlayerPackageDataProxy;

		#endregion




		/// <summary>
		/// 得到本类的单例实例
		/// （没有的话，要动态创建空游戏对象并挂载上）
		/// </summary>
		/// <returns></returns>
		public static SaveAndLoading GetInstance() {
			if(_Instance = null) {
				_Instance = new GameObject("_SaveAndLoading").AddComponent<SaveAndLoading>();
			}
			return _Instance;
		}



		#region 【存储游戏进度】


		/// <summary>
		/// 公共方法：存储游戏进度
		/// </summary>
		/// <returns>是否保存成功</returns>
		public bool SaveGameProcess() {
			//得到各种数据
			//###这里为何要用代理类？
			_PlayerKernelDataProxy = Mod_PlayerKernelDataProxy.GetInstance();
			_PlayerExtendedDataProxy = Mod_PlayerExtendedDataProxy.GetInstance();
			//_PlayerPackageDataProxy = Mod_PlayerPackageDataProxy.GetInstance();

			//存储游戏的全局参数
			SaveToXML_GlobalParameterData();
			//存储玩家的核心数据
			SaveToXML_GlobalParameterData();
			//存储玩家的扩展数据
			SaveToXML_PlayerExtendedData();
			//存储玩家的背包数据
			SaveToXML_PlayerPackageData();

			return false;
		}




		/// <summary>
		/// 存储游戏的全局参数
		/// </summary>
		private void SaveToXML_GlobalParameterData() {
			//提取属性数值
			//这里的局部变量类型可以全部统一使用var
			SceneEnum sceneName = GlobalParaMgr.NextSceneName;
			string playerName = GlobalParaMgr.PlayerName;
			PlayerType playerType = GlobalParaMgr.CurPlayerType;
			//实例化类
			Mod_GlobalParaData gpd = new Mod_GlobalParaData(sceneName, playerName,playerType);

			//对象序列化
			string fileData = XmlOperation.GetInstance().SerializeObject(gpd, typeof(Mod_GlobalParaData));
			//创建XML文件，且写入
			if (!String.IsNullOrEmpty(_FileNameByGlobalParameterData)) {
				XmlOperation.GetInstance().CreateXML(_FileNameByGlobalParameterData, fileData);
			}
		}


		/// <summary>
		/// 存储玩家的核心数据
		/// </summary>
		private void SaveTOXML_PlayerKernalData() {
			//数据准备（提取属性数值）
			float curHP = _PlayerKernelDataProxy.CurHP;
			float maxHP = _PlayerKernelDataProxy.MaxHP;
			float curMP = _PlayerKernelDataProxy.CurMP;
			float maxMP = _PlayerKernelDataProxy.MaxMP;
			float atk = _PlayerKernelDataProxy.ATK;
			float atkByItem = _PlayerKernelDataProxy.ATKByItem;
			float def = _PlayerKernelDataProxy.DEF;
			float defByItem = _PlayerKernelDataProxy.DEFByItem;
			float dex = _PlayerKernelDataProxy.DEX;
			float dexByItem = _PlayerKernelDataProxy.DEXByItem;
			//实例化类
			Mod_PlayerKernelData pkd = new Mod_PlayerKernelData( curHP,  curMP,  atk,  def,  dex,  maxHP,  maxMP,  atkByItem,  defByItem,  dexByItem);

			//对象序列化
			string fileData = XmlOperation.GetInstance().SerializeObject(pkd, typeof(Mod_PlayerKernelData));
			//创建XML文件，且写入
			if (!String.IsNullOrEmpty(_FileNameByPlayerKernalData)) {
				XmlOperation.GetInstance().CreateXML(_FileNameByPlayerKernalData, fileData);
			}
		}


		/// <summary>
		/// 存储玩家的扩展数据
		/// </summary>
		private void SaveToXML_PlayerExtendedData() {
			//数据准备（提取属性数值）
			int exp = _PlayerExtendedDataProxy.EXP;
			int killNum = _PlayerExtendedDataProxy.KillNum;
			int level = _PlayerExtendedDataProxy.Level;
			int gold = _PlayerExtendedDataProxy.Gold;
			int diamond = _PlayerExtendedDataProxy.Diamond;
			//实例化类
			Mod_PlayerExtendedData ped = new Mod_PlayerExtendedData(exp, killNum, level, gold, diamond);

			//对象序列化
			string fileData = XmlOperation.GetInstance().SerializeObject(ped, typeof(Mod_PlayerExtendedData));
			//创建XML文件，且写入
			if (!String.IsNullOrEmpty(_FileNameByPlayerExtendedData)) {
				XmlOperation.GetInstance().CreateXML(_FileNameByPlayerExtendedData, fileData);
			}
		}


		/// <summary>
		/// 存储玩家的背包数据
		/// </summary>
		private void SaveToXML_PlayerPackageData() {
			//数据准备（提取属性数值）
			int weapon_1_Count = _PlayerPackageDataProxy.Item_Weapon_1.Count;
			int shield_1_Count = _PlayerPackageDataProxy.Item_Shield_1.Count;
			int boot_1_Count = _PlayerPackageDataProxy.Item_Boot_1.Count;
			int hpPotion_1_Count = _PlayerPackageDataProxy.Item_HPPotion_1.Count;
			int mpPotion_1_Count = _PlayerPackageDataProxy.Item_MPPotion_1.Count;
			//实例化类
			Mod_PlayerPackageData ppd = new Mod_PlayerPackageData(weapon_1_Count, shield_1_Count, boot_1_Count, hpPotion_1_Count, mpPotion_1_Count);

			//对象序列化
			string fileData = XmlOperation.GetInstance().SerializeObject(ppd, typeof(Mod_PlayerPackageData));
			//创建XML文件，且写入
			if (!String.IsNullOrEmpty(_FileNameByPlayerPackageData)) {
				XmlOperation.GetInstance().CreateXML(_FileNameByPlayerPackageData, fileData);
			}
		}

		#endregion




		#region 【读取游戏进度】

		/// <summary>
		/// 公共方法：提取游戏全局参数数据
		/// 这个方法必须首先调用，才能确定之后如何读取
		/// </summary>
		/// <returns></returns>
		public bool LoadingGame_GlobalParameter() {
			//读取游戏的全局参数
			LoadFromXML_GlobalParameterData();
			return true;
		}

		/// <summary>
		/// 公共方法：提取游戏玩家数据
		/// </summary>
		/// <returns></returns>
		public bool LoadingGame_PlayerData() {
			//读取玩家的核心数据
			LoadFromXML_PlayerKernalData();
			//读取玩家的扩展数据
			LoadFromXML_PlayerExtendedData();
			//读取玩家的背包数据
			LoadFromXML_PlayerPackageData();
			return true;
		}




		/// <summary>
		/// 读取游戏的全局参数
		/// </summary>
		private void LoadFromXML_GlobalParameterData() {
			//参数检查
			if (string.IsNullOrEmpty(_FileNameByGlobalParameterData)) {
				Debug.LogError("读取游戏全局参数失败！");
				return;
			}

			try {
				//读取XML数据
				string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByGlobalParameterData);
				//反序列化
				Mod_GlobalParaData dpd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(Mod_GlobalParaData)) as Mod_GlobalParaData;
				//赋值
				GlobalParaMgr.PlayerName = dpd.PlayerName;
				GlobalParaMgr.NextSceneName = dpd.NextSceneName;
				GlobalParaMgr.CurPlayerType = dpd.PlayerType;
				if (GlobalParaMgr.CurGameStatus != GameStatus.Continue) {
					GlobalParaMgr.CurGameStatus = GameStatus.Continue;
				}
			}
			catch {
				Debug.LogError("读取游戏全局参数失败！");
			}			
		}


		/// <summary>
		/// 读取玩家的核心数据
		/// </summary>
		private void LoadFromXML_PlayerKernalData() {
			//参数检查
			if (string.IsNullOrEmpty(_FileNameByPlayerKernalData)) {
				Debug.LogError("读取玩家核心参数失败！");
				return;
			}

			try {
				//读取XML数据
				string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByPlayerKernalData);
				//反序列化
				Mod_PlayerKernelData pkd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(Mod_PlayerKernelData)) as Mod_PlayerKernelData;
				//赋值
				Mod_PlayerKernelDataProxy.GetInstance().CurHP = pkd.CurHP;
				Mod_PlayerKernelDataProxy.GetInstance().MaxHP = pkd.MaxHP;
				Mod_PlayerKernelDataProxy.GetInstance().CurMP = pkd.CurMP;
				Mod_PlayerKernelDataProxy.GetInstance().MaxMP = pkd.MaxMP;
				Mod_PlayerKernelDataProxy.GetInstance().ATK = pkd.ATK;
				Mod_PlayerKernelDataProxy.GetInstance().DEF = pkd.DEF;
				Mod_PlayerKernelDataProxy.GetInstance().DEX = pkd.DEX;
				Mod_PlayerKernelDataProxy.GetInstance().ATKByItem = pkd.ATKByItem;
				Mod_PlayerKernelDataProxy.GetInstance().DEFByItem = pkd.DEFByItem;
				Mod_PlayerKernelDataProxy.GetInstance().DEXByItem = pkd.DEXByItem;
			}
			catch {
				Debug.LogError("读取玩家核心参数失败！");
			}
		}


		/// <summary>
		/// 读取玩家的扩展数据
		/// </summary>
		private void LoadFromXML_PlayerExtendedData() {
			//参数检查
			if (string.IsNullOrEmpty(_FileNameByPlayerExtendedData)) {
				Debug.LogError("读取玩家扩展参数失败！");
				return;
			}

			try {
				//读取XML数据
				string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByPlayerExtendedData);
				//反序列化
				Mod_PlayerExtendedData ped = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(Mod_PlayerExtendedData)) as Mod_PlayerExtendedData;
				//赋值
				Mod_PlayerExtendedDataProxy.GetInstance().EXP = ped.EXP;
				Mod_PlayerExtendedDataProxy.GetInstance().KillNum = ped.KillNum;
				Mod_PlayerExtendedDataProxy.GetInstance().Level = ped.Level;
				Mod_PlayerExtendedDataProxy.GetInstance().Gold = ped.Gold;
				Mod_PlayerExtendedDataProxy.GetInstance().Diamond = ped.Diamond;

			}
			catch {
				Debug.LogError("读取玩家扩展参数失败！");
			}
		}


		/// <summary>
		/// 读取玩家的背包数据
		/// </summary>
		private void LoadFromXML_PlayerPackageData() {
			//参数检查
			if (string.IsNullOrEmpty(_FileNameByPlayerPackageData)) {
				Debug.LogError("读取玩家背包页面参数失败！");
				return;
			}

			try {
				//读取XML数据
				string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByPlayerPackageData);
				//反序列化
				Mod_PlayerPackageData ppd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(Mod_PlayerPackageData)) as Mod_PlayerPackageData;
				//赋值
				Mod_PlayerPackageDataProxy.GetInstance().Item_Weapon_1.Count = ppd.Item_Weapon_1.Count;
				Mod_PlayerPackageDataProxy.GetInstance().Item_Shield_1.Count = ppd.Item_Shield_1.Count;
				Mod_PlayerPackageDataProxy.GetInstance().Item_Boot_1.Count = ppd.Item_Boot_1.Count;
				Mod_PlayerPackageDataProxy.GetInstance().Item_HPPotion_1.Count = ppd.Item_HPPotion_1.Count;
				Mod_PlayerPackageDataProxy.GetInstance().Item_MPPotion_1.Count = ppd.Item_MPPotion_1.Count;

			}
			catch {
				Debug.LogError("读取玩家背包页面参数失败！");
			}
		}

		#endregion


	}
}
