using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace Test3
{
	class Program
	{
		static void Main(string[] args)
		{
			XmlReader reader = null;
			// Load the reader with the data file and ignore all white space nodes.         
			reader = XmlReader.Create("http://79.137.216.214/redmine/projects.xml");
			//reader.WhitespaceHandling = WhitespaceHandling.None;
			string Path = @"../../MyTestXML.xml";

			using (StreamWriter sw = new StreamWriter(@"../../rw2.txt"))
			{
				sw.WriteLine("awdawdwad");
				sw.Write("wadad");

				// Parse the file and display each of the nodes.

				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.EndElement:
							Console.WriteLine("</{0}>", reader.Name);
							sw.WriteLine("</{0}>", reader.Name);
							break;
						case XmlNodeType.Element:
							
							Console.WriteLine("<{0}>", reader.Name);
							sw.WriteLine("<{0}>", reader.Name);
							break;
						case XmlNodeType.Text:
							Console.WriteLine(reader.Value);
							sw.WriteLine(reader.Value);
							break;
						case XmlNodeType.CDATA:
							Console.WriteLine("<![CDATA[{0}]]>", reader.Value);
							sw.WriteLine("<![CDATA[{0}]]>", reader.Value);
							break;
						case XmlNodeType.ProcessingInstruction:
							Console.WriteLine("<?{0} {1}?>", reader.Name, reader.Value);
							sw.WriteLine("<?{0} {1}?>", reader.Name, reader.Value);
							break;
						case XmlNodeType.Comment:
							Console.WriteLine("<!--{0}-->", reader.Value);
							sw.WriteLine("<!--{0}-->", reader.Value);
							break;
						case XmlNodeType.XmlDeclaration:
							Console.WriteLine("<?xml version='1.0'?>");
							sw.WriteLine("<?xml version='1.0'?>");
							break;
						case XmlNodeType.Document:
							break;
						case XmlNodeType.DocumentType:
							Console.WriteLine("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
							sw.WriteLine("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
							break;
						case XmlNodeType.EntityReference:
							Console.WriteLine(reader.Name);
							sw.WriteLine(reader.Name);
							break;
						
					}
				}




				if (reader != null)
					reader.Close();
			}
			Console.ReadLine();
		}
	}
}
