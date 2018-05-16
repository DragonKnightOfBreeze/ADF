//核心层，核心层的参数列表

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kernel {
	public class KernelParameter {

#if UNITY_STANDALONE_WIN

	//系统配置信息中的日志路径（只读）
	internal static readonly string SystemConfigInfo_LogPath = "file://" + Application.dataPath + "/StreamingAssets/SystemConfigInfo.xml";
	//系统配置信息中的日志根节点名称
	internal static readonly string SystemConfigInfo_RootNodeName = "SystemConfigInfo";

	//对话系统XML的路径（只读）
	internal static readonly string DialogConfig_Path = "file://" + Application.dataPath + "/StreamingAssets/SystemDialogInfo.xml";
	//对话系统XML的根节点名称
	internal static readonly string DialogConfig_RootNodeName = "Dialogs";

#elif UNITY_ANDROID

	//系统配置信息中的日志路径（只读）
	internal static readonly string SystemConfigInfo_LogPath = Application.dataPath + "!/Assets/SystemConfigInfo.xml";
	//系统配置信息中的日志根节点名称
	internal static readonly string SystemConfigInfo_RootNodeName= "SystemConfigInfo";

	//对话系统XML的路径（只读）
	internal static readonly string DialogConfig_Path =  Application.dataPath + "!/Assets/SystemDialogInfo.xml";
	//对话系统XML的根节点名称
	internal static readonly string DialogConfig_RootNodeName = "Dialogs";

#elif UNITY_IPHONE

	//系统配置信息中的日志路径（只读）
	internal static readonly string SystemConfigInfo_LogPath = Application.dataPath + "/Raw/SystemConfigInfo.xml";
	//系统配置信息中的日志根节点名称
	internal static readonly string SystemConfigInfo_RootNodeName= "SystemConfigInfo";

	//对话系统XML的路径（只读）
	internal static readonly string DialogConfig_Path = Application.dataPath + "/Raw/SystemDialogInfo.xml";
	//对话系统XML的根节点名称
	internal static readonly string DialogConfig_RootNodeName = "Dialogs";

#endif

	}
}