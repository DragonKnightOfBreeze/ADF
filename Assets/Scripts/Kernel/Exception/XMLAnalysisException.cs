//核心层，自定义异常：XML解析异常
//作用：专门定位XML解析异常，如果出现此异常，表示XML文档有错

using System;
using System.Collections;
using System.Collections.Generic;

namespace Kernel {
	public class XMLAnalysisException : Exception {
		
		public XMLAnalysisException():base() { }

		public XMLAnalysisException(string exceptionMessage):base(exceptionMessage) { }

	}
}