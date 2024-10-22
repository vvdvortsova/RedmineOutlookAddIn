﻿using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api
{
	public static partial class RedmineSerialization
	{
		/// <summary>
		///     Serializes the specified System.Object and writes the XML document to a string.
		/// </summary>
		/// <typeparam name="T">The type of objects to serialize.</typeparam>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>The System.String that contains the XML document.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static string ToXml<T>(T obj) where T : class
		{
			var xws = new XmlWriterSettings
			{
				OmitXmlDeclaration = true
			};
			using (var stringWriter = new StringWriter())
			{
				using (var xmlWriter = XmlWriter.Create(stringWriter, xws))
				{
					var sr = new XmlSerializer(typeof(T));
					sr.Serialize(xmlWriter, obj);
					return stringWriter.ToString();
				}
			}
		}

		/// <summary>
		///     Deserializes the XML document contained by the specific System.String.
		/// </summary>
		/// <typeparam name="T">The type of objects to deserialize.</typeparam>
		/// <param name="xml">The System.String that contains the XML document to deserialize.</param>
		/// <returns>The T object being deserialized.</returns>
		/// <exception cref="System.InvalidOperationException">
		///     An error occurred during deserialization. The original exception is available
		///     using the System.Exception.InnerException property.
		/// </exception>
		public static T FromXml<T>(string xml) where T : class
		{
			using (var text = new StringReader(xml))
			{
				var sr = new XmlSerializer(typeof(T));
				return sr.Deserialize(text) as T;
			}
		}

		/// <summary>
		///     Deserializes the XML document contained by the specific System.String.
		/// </summary>
		/// <param name="xml">The System.String that contains the XML document to deserialize.</param>
		/// <param name="type">The type of objects to deserialize.</param>
		/// <returns>The System.Object being deserialized.</returns>
		/// <exception cref="System.InvalidOperationException">
		///     An error occurred during deserialization. The original exception is available
		///     using the System.Exception.InnerException property.
		/// </exception>
		public static object FromXml(string xml, Type type)
		{
			using (var text = new StringReader(xml))
			{
				var sr = new XmlSerializer(type);
				return sr.Deserialize(text);
			}
		}

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <remarks>http://florianreischl.blogspot.ro/search/label/c%23</remarks>
		public class XmlStreamingSerializer<T>
		{
			private static readonly XmlSerializerNamespaces Ns;
			private readonly XmlSerializer _serializer;
			private readonly XmlWriter _writer;
			private bool _finished;

			static XmlStreamingSerializer()
			{
				Ns = new XmlSerializerNamespaces();
				Ns.Add("", "");
			}

			private XmlStreamingSerializer()
			{
				_serializer = new XmlSerializer(typeof(T));
			}

			public XmlStreamingSerializer(TextWriter w)
				: this(XmlWriter.Create(w)) { }

			public XmlStreamingSerializer(XmlWriter writer)
				: this()
			{
				_writer = writer;
				writer.WriteStartDocument();
				writer.WriteStartElement("ArrayOf" + typeof(T).Name);
			}

			public void Finish()
			{
				_writer.WriteEndDocument();
				_writer.Flush();
				_finished = true;
			}

			public void Close()
			{
				if (!_finished)
					Finish();
				_writer.Close();
			}

			public void Serialize(T item)
			{
				_serializer.Serialize(_writer, item, Ns);
			}
		}
	}
}