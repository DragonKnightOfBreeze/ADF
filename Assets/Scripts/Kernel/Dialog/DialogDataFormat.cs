//核心层，实体类，对话数据
using System;
using System.Collections.Generic;
using UnityEngine;

using Global;

namespace Kernel {
	class DialogDataFormat {
		/// <summary>
		/// 对话段落编号
		/// </summary>
		public int DiaSectionNum { set; get; }
		/// <summary>
		/// 对话段落名称
		/// </summary>
		public string DiaSectionName { set; get; }
		/// <summary>
		/// 段落序号
		/// </summary>
		public int DiaIndex { set; get; }
		/// <summary>
		/// 对话方（讲述者）
		/// </summary>
		public string DiaSide { set; get; }
		/// <summary>
		/// 对话人物（倾听者）
		/// </summary>
		public string DiaPerson{ set; get; }
		/// <summary>
		/// 对话内容
		/// </summary>
		public string DiaContent { set; get; }
	}
}
