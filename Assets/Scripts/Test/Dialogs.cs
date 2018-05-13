// 实体类，对话信息

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test {

	public class Dialogs {
		//语言
		public string Language { set; get; }	//简化属性方法
		//编号
		public int Number { set; get; }
		//讲述者
		public string Speaker { set; get; }
		//对话内容
		public string SpeakContent { set; get; }

	}
}

