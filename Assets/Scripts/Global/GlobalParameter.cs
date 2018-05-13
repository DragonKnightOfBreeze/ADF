//公共层，全局参数。
//定义整个项目的枚举类型
//定义整个项目的委托
//定义整个项目的系统常量
//定义系统中的所有标签Tag

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {
	#region 项目的系统常量
	public class GlobalParameter {

		/// <summary>
		/// 延迟开始方法的等待时间
		/// </summary>
		public const float WAIT_FOR_SECONDS_ON_START = 0.2f;

		public const float WAIT_FOR_PP = 0.01f;
		/// <summary>
		/// 每次判断动画状态的等待时间
		/// </summary>
		public const float CHECK_TIME = 0.02f;	

		public const float MAX_VALUE_A = 999f;
		public const float MAX_VALUE_B = 99f;

		/// <summary>
		/// EasyTouch插件定义的摇杆名称
		/// </summary>
		public const string JOYSTICK_NAME = "HeroJoystick";

		/// <summary>
		/// 输入管理器定义_普通攻击
		/// </summary>
		public const string INPUT_MGR_NormalAtk = "NormalAtk";
		/// <summary>
		/// 输入管理器定义_魔法攻击A
		/// </summary>
		public const string INPUT_MGR_MagicAtkA = "MagicAtkA";
		/// <summary>
		/// 输入管理器定义_魔法攻击B
		/// </summary>
		public const string INPUT_MGR_MagicAtkB = "MagicAtkB";
		/// <summary>
		/// 输入管理器定义_魔法攻击C
		/// </summary>
		public const string INPUT_MGR_MagicAtkC = "MagicAtkC";
		/// <summary>
		/// 输入管理器定义_魔法攻击D
		/// </summary>
		public const string INPUT_MGR_MagicAtkD = "MagicAtkD";

		/// <summary>
		/// 间隔时间
		/// </summary>
		//public const float TIME0d1 = 0.1f;
		//public const float INTERVAL_TIME_0d2 = 0.2f;
		//public const float INTERVAL_TIME_0d3 = 0.3f;
		//public const float INTERVAL_TIME_0d4 = 0.4f;
		//public const float INTERVAL_TIME_0d5 = 0.5f;
		//public const float INTERVAL_TIME_0d6 = 0.6f;
		//public const float INTERVAL_TIME_0d7 = 0.7f;
		//public const float INTERVAL_TIME_0d8 = 0.8f;
		//public const float INTERVAL_TIME_0d9 = 0.9f;
		//public const float INTERVAL_TIME_1 = 1f;

	}

	#endregion

	#region 项目的标签定义

	public class Tag {
		public static string Tag_Enemy = "Tag_Enemy";
		public static string Tag_Player = "Player";		//不要轻易修改成"Tag_Player"
	}

	#endregion

	#region 项目的枚举类型

	/// <summary>
	/// 场景名称
	/// </summary>
	public enum SceneEnum {
		StartScene,
		LoadingScene,
		LoginScene,
		Level1,
		Level2,
		Level3,
		BaseScene
	}

	/// <summary>
	/// 玩家类型
	/// </summary>
	public enum PlayerType {
		Sworder,	//剑士
		Mage,		//魔法师
		Others
	}

	/// <summary>
	/// 主角的动作状态
	/// </summary>
	public enum HeroActionState {
		None,		//无
		Idle,		//休闲
		Running,	//移动
		NormalAtk,	//普通攻击
		MagicAtkA,	//魔法攻击A
		MagicAtkB	//魔法攻击B
	}

	/// <summary>
	/// 普通攻击连招
	/// </summary>
	public enum NormalAtkComboState {
		NormalAtk1,
		NormalAtk2,
		NormalAtk3
	}

	/// <summary>
	/// 等级名称
	/// </summary>
	public enum LevelName {
		Level_1 = 1,
		Level_2,
		Level_3,
		Level_4,
		Level_5,
		Level_6,
		Level_7,
		Level_8,
		Level_9,
		Level_10
		//...
	}

	/// <summary>
	/// 敌人的AI状态（简单的）
	/// 要于敌人动画状态机里面的名称保持一致
	/// </summary>
	public enum EnemyActionState {
		/// <summary>
		/// 等待状态（持续）
		/// </summary>
		Idle ,		
		/// <summary>
		/// 移动状态（持续）
		/// </summary>
		Moving,	
		/// <summary>
		/// 攻击状态（瞬时）
		/// </summary>
		NormalAtk,		
		/// <summary>
		/// 受伤状态（瞬时）
		/// </summary>
		Hurt,		
		/// <summary>
		/// 死亡状态（持续）
		/// </summary>
		Dead		
	}

	///// <summary>
	///// 单次动画播放状态
	///// </summary>
	//public enum SinglePlayState {
	//	Init,           //默认值，未播放
	//	Start,          //开始
	//	Playing,        //正在播放
	//	End,            //结束
	//	Waiting         //等待（后摇，思考时间）
	//}

	#endregion

	#region 项目的委托类型

	/// <summary>
	/// 委托：主角控制
	/// </summary>
	/// <param name="controlType">控制的类型</param>
	//使用参数来区别不同的控制类型，区分不同的输入，实现多播委托 
	public delegate void del_PlayerControlWithStr(string controlType);

	/// <summary>
	/// 键值更新类（对于人物属性来说）
	/// </summary>
	/// <param name="kv"></param>
	//两种参数：类别和数值
	//参数实际上是一个类，构造函数带有键、值两个参数
	public delegate void del_PlayerKernalModel(KeyValuesUpdate kv);

	//键值对（键来区分不同的类别，值可以通过委托得到数据）
	public class KeyValuesUpdate {
		private string _Key;        //键
		private object _Values;     //值

		//属性（只读）
		public string Key {
			get {
				return _Key;
			}
		}
		public object Values {
			get {
				return _Values;
				
			}
		}

		public KeyValuesUpdate(string key,object values) {
			_Key = key;
			_Values = values;
		}
	}

	#endregion

}