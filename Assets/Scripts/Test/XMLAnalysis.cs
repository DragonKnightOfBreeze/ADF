//实体类 XML解析

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;   //XML命名空间
using System.IO;	//文件输入输出流

namespace Test {
	public class XMLAnalysis {

		List<Dialogs> _DialogsArray;		//存放集合

		public XMLAnalysis() {
			_DialogsArray = new List<Dialogs>();
		}

		/// <summary>
		/// 解析XML
		/// </summary>
		public void AnalysisXML() {
			XmlDocument doc = new XmlDocument();
			//加载XML文件
			doc.Load("XMLDemo.xml");
			//得到根节点
			XmlNode node = doc.FirstChild;
			//得到根节点后的节点集合
			XmlNodeList nodeArray = node.ChildNodes;    //nodeArray表示一个节点集合

			foreach (XmlNode nodeItem in nodeArray) {   //nodeItem表示一条回话信息
														//实例化实体类
				Dialogs diaObj = new Dialogs {
					//得到属性信息
					Language = nodeItem["Language"].InnerText,
					Number = Convert.ToInt32(nodeItem["num"].InnerText),
					Speaker = nodeItem["Speaker"].InnerText,
					SpeakContent = nodeItem["Content"].InnerText
				};

				//把单个实体对象加入集合
				_DialogsArray.Add(diaObj);
			}
		}


		/// <summary>
		/// 查看XML内容
		/// </summary>
		public void DisplayXMLContent() {
			Console.WriteLine("显示XML如下内容");
			if(_DialogsArray != null) {
				foreach (Dialogs diaItem in _DialogsArray) {
					//这里应该有一种遍历手段可以进行优化
					Console.WriteLine("语言："+diaItem.Language);
					Console.WriteLine("编号：" + diaItem.Number);
					Console.WriteLine("讲述者：" + diaItem.Speaker);
					Console.WriteLine("对话内容：" + diaItem.SpeakContent);
					Console.WriteLine();
					//......
				}
			}
		}

		
		static void Main(string[] args) {
			XMLAnalysis obj = new XMLAnalysis();
			//解析XML
			obj.AnalysisXML();
			//显示XML内容
			obj.DisplayXMLContent();
		}
	}
}
