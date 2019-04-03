using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Test2
{
	class Program
	{
		static void Main(string[] args)
		{
			XmlTextReader reader = null;



			// Load the reader with the data file and ignore all white space nodes.         
			reader = new XmlTextReader("http://79.137.216.214/redmine/projects.xml");
			reader.WhitespaceHandling = WhitespaceHandling.None;
			string Path = @"../../MyTestXML.txt";
			using (StreamWriter sw1 = new StreamWriter(@"../../SW2.txt"))
			{
				sw1.Write("sefesf");
			}
			using (StreamWriter sw = new StreamWriter(Path))
			{
				sw.Write("wadad");

				// Parse the file and display each of the nodes.

				//while (reader.read())
				//{
				//	switch (reader.nodetype)
				//	{
				//		case xmlnodetype.element:
				//			console.writeline("<{0}>", reader.name);
				//			sw.writeline("<{0}>", reader.name);
				//			break;
				//		case xmlnodetype.text:
				//			console.writeline(reader.value);
				//			sw.writeline(reader.value);
				//			break;
				//		case xmlnodetype.cdata:
				//			console.writeline("<![cdata[{0}]]>", reader.value);
				//			sw.writeline("<![cdata[{0}]]>", reader.value);
				//			break;
				//		case xmlnodetype.processinginstruction:
				//			console.writeline("<?{0} {1}?>", reader.name, reader.value);
				//			sw.writeline("<?{0} {1}?>", reader.name, reader.value);
				//			break;
				//		case xmlnodetype.comment:
				//			console.writeline("<!--{0}-->", reader.value);
				//			sw.writeline("<!--{0}-->", reader.value);
				//			break;
				//		case xmlnodetype.xmldeclaration:
				//			console.writeline("<?xml version='1.0'?>");
				//			sw.writeline("<?xml version='1.0'?>");
				//			break;
				//		case xmlnodetype.document:
				//			break;
				//		case xmlnodetype.documenttype:
				//			console.writeline("<!doctype {0} [{1}]", reader.name, reader.value);
				//			sw.writeline("<!doctype {0} [{1}]", reader.name, reader.value);
				//			break;
				//		case xmlnodetype.entityreference:
				//			console.writeline(reader.name);
				//			sw.writeline("<!doctype {0} [{1}]", reader.name, reader.value);
				//			break;
				//		case xmlnodetype.endelement:
				//			console.writeline("</{0}>", reader.name);
				//			sw.writeline("</{0}>", reader.name);
				//			break;
				//	}

				//}




				if (reader != null)
					reader.Close();

				Console.ReadLine();
			}


		}



	}
}
