using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Redmine.Net.Api.Types;
using Redmine.Net.Api;
using Group = Redmine.Net.Api.Types.Group;
using Version = Redmine.Net.Api.Types.Version;
using System.Xml;

namespace MyRedm
{
	class Program
	{

		
		static void Main(string[] args)
		{
			string host = "http://79.137.216.214/redmine/";
			string apiKey = "4444025d7e83c49e92466b5399ba7ee06c464637";

			string Path = @"../../Test1.xml";
			String URLString = "http://79.137.216.214/redmine/projects.xml";
			XmlTextReader reader = new XmlTextReader(URLString);
			 

     try {
       
        // Load the reader with the data file and ignore all white space nodes.         
      
        reader.WhitespaceHandling = WhitespaceHandling.None;

        // Parse the file and display each of the nodes.
        while (reader.Read()) {
           switch (reader.NodeType) {
             case XmlNodeType.Element:
               Console.Write("<{0}>", reader.Name);
               break;
             case XmlNodeType.Text:
               Console.Write(reader.Value);
               break;
             case XmlNodeType.CDATA:
               Console.Write("<![CDATA[{0}]]>", reader.Value);
               break;
             case XmlNodeType.ProcessingInstruction:
               Console.Write("<?{0} {1}?>", reader.Name, reader.Value);
               break;
             case XmlNodeType.Comment:
               Console.Write("<!--{0}-->", reader.Value);
               break;
             case XmlNodeType.XmlDeclaration:
               Console.Write("<?xml version='1.0'?>");
               break;
             case XmlNodeType.Document:
               break;
             case XmlNodeType.DocumentType:
               Console.Write("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
               break;
             case XmlNodeType.EntityReference:
               Console.Write(reader.Name);
               break;
             case XmlNodeType.EndElement:
               Console.Write("</{0}>", reader.Name);
               break;
           }       
        }           
     }

     finally {
        if (reader!=null)
          reader.Close();
     }
			using (StreamWriter sw = new StreamWriter(Path, true, System.Text.Encoding.Default))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element: // Узел является элементом.
							Console.Write("<" + reader.Name);
							sw.Write("<" + reader.Name);
							while (reader.MoveToNextAttribute()) // Выполняется чтение атрибутов.
							{ Console.Write(" " + $"'/{reader.Name}'/" + "=" + $"'/{reader.Value}'/" + "");
								sw.Write(" " + $"'/{reader.Name}'/" + "=" + $"'/{reader.Value}'/" + "");
							}
							
							Console.Write("");
							Console.WriteLine(">");

							sw.Write("");
							sw.WriteLine(">");
							break;
						case XmlNodeType.Text: //Отображается текст в каждом элементе.
							
							Console.WriteLine(reader.Value);
							sw.WriteLine(reader.Value);
							break;
						case XmlNodeType.EndElement: //Отображается конец элемента.
							
							Console.Write("</" + reader.Name);
							Console.WriteLine(">");
							sw.Write("</" + reader.Name);
							sw.WriteLine(">");
							break;

					}
				}

			}
			XmlReader reader1 = XmlReader.Create("http://79.137.216.214/redmine/projects.xml");
			XmlWriter writer = XmlWriter.Create("output.xml");

			while (reader.Read())
			{
				writer.WriteNode(reader1, true);
			}

			reader1.Close();
			writer.Close();
			Console.ReadLine();

			Console.ReadLine();
		}
	}
}
