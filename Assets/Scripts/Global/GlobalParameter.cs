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

	#endregion

	#region 项目的委托类型

	/// <summary>
	/// 委托：主角控制
	/// </summary>
	/// <param name="controlType">控制的类型</param>
	public delegate void del_PlayerControlWithStr(string controlType);

	#endregion

}