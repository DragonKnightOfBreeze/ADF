//视图层，新手引导模块，引导触发接口

using System;
using System.Collections.Generic;
using UnityEngine;

namespace View {
	public interface IGuideTrigger {

		/// <summary>
		/// 检查触发条件
		/// </summary>
		/// <returns>
		/// true: 表示条件成立，触发后续业务逻辑
		/// </returns>
		bool CheckCondition();

		/// <summary>
		/// 运行业务逻辑
		/// </summary>
		/// <returns>
		/// true: 表示业务逻辑执行完毕
		/// </returns>
		bool RunOperation();
	}
}
