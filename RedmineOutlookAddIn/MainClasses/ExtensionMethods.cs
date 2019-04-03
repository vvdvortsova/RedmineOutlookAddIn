﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Redmine.Net.Api.JSonConverters;
using Redmine.Net.Api.Types;

namespace Redmine.Net.Api
{
	public static class ExtensionMethods
	{
		/// <summary>
		///     Reads the attribute as int.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static int ReadAttributeAsInt(this XmlReader reader, string attributeName)
		{
			try
			{
				var attribute = reader.GetAttribute(attributeName);
				int result;
				if (string.IsNullOrWhiteSpace(attribute) ||
					!int.TryParse(
						attribute,
						NumberStyles.Any,
						NumberFormatInfo.InvariantInfo,
						out result)) return default(int);
				return result;
			}
			catch
			{
				return -1;
			}
		}

		public static int? ReadAttributeAsNullableInt(
			this XmlReader reader,
			string attributeName)
		{
			try
			{
				var attribute = reader.GetAttribute(attributeName);
				int result;
				if (string.IsNullOrWhiteSpace(attribute) ||
					!int.TryParse(
						attribute,
						NumberStyles.Any,
						NumberFormatInfo.InvariantInfo,
						out result)) return null;
				return result;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		///     Reads the attribute as boolean.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="attributeName">Name of the attribute.</param>
		/// <returns></returns>
		public static bool ReadAttributeAsBoolean(
			this XmlReader reader,
			string attributeName)
		{
			try
			{
				var attribute = reader.GetAttribute(attributeName);
				bool result;
				if (string.IsNullOrWhiteSpace(attribute) ||
					!bool.TryParse(attribute, out result)) return false;
				return result;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		///     Reads the element content as nullable date time.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static DateTime? ReadElementContentAsNullableDateTime(
			this XmlReader reader)
		{
			var str = reader.ReadElementContentAsString();
			DateTime result;
			if (string.IsNullOrWhiteSpace(str) || !DateTime.TryParse(str, out result))
				return null;
			return result;
		}

		/// <summary>
		///     Reads the element content as nullable float.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static float? ReadElementContentAsNullableFloat(this XmlReader reader)
		{
			var str = reader.ReadElementContentAsString();
			float result;
			if (string.IsNullOrWhiteSpace(str) ||
				!float.TryParse(
					str,
					NumberStyles.Any,
					NumberFormatInfo.InvariantInfo,
					out result)) return null;
			return result;
		}

		/// <summary>
		///     Reads the element content as nullable int.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static int? ReadElementContentAsNullableInt(this XmlReader reader)
		{
			var str = reader.ReadElementContentAsString();
			int result;
			if (string.IsNullOrWhiteSpace(str) ||
				!int.TryParse(
					str,
					NumberStyles.Any,
					NumberFormatInfo.InvariantInfo,
					out result)) return null;
			return result;
		}

		/// <summary>
		///     Reads the element content as nullable decimal.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static decimal? ReadElementContentAsNullableDecimal(this XmlReader reader)
		{
			var str = reader.ReadElementContentAsString();
			decimal result;
			if (string.IsNullOrWhiteSpace(str) ||
				!decimal.TryParse(
					str,
					NumberStyles.Any,
					NumberFormatInfo.InvariantInfo,
					out result)) return null;
			return result;
		}

		/// <summary>
		///     Reads the element content as collection.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="reader">The reader.</param>
		/// <returns></returns>
		public static List<T> ReadElementContentAsCollection<T>(this XmlReader reader)
			where T : class
		{
			var result = new List<T>();
			var serializer = new XmlSerializer(typeof(T));
			var xml = reader.ReadOuterXml();
			using (var sr = new StringReader(xml))
			{
				var r = new XmlTextReader(sr);
				r.ReadStartElement();
				while (!r.EOF)
				{
					if (r.NodeType == XmlNodeType.EndElement)
					{
						r.ReadEndElement();
						continue;
					}
					T temp;
					if (r.IsEmptyElement && r.HasAttributes)
					{
						temp = serializer.Deserialize(r) as T;
					}
					else
					{
						var subTree = r.ReadSubtree();
						temp = serializer.Deserialize(subTree) as T;
					}
					if (temp != null) result.Add(temp);
					if (!r.IsEmptyElement)
						r.Read();
				}
			}
			return result;
		}

		public static ArrayList ReadElementContentAsCollection(
			this XmlReader reader,
			Type type)
		{
			var result = new ArrayList();
			var serializer = new XmlSerializer(type);
			var xml = reader.ReadOuterXml();
			using (var sr = new StringReader(xml))
			{
				var r = new XmlTextReader(sr);
				r.ReadStartElement();
				while (!r.EOF)
				{
					if (r.NodeType == XmlNodeType.EndElement)
					{
						r.ReadEndElement();
						continue;
					}
					var subTree = r.ReadSubtree();
					var temp = serializer.Deserialize(subTree);
					if (temp != null) result.Add(temp);
					r.Read();
				}
			}
			return result;
		}

		/// <summary>
		///     Writes the id if not null.
		/// </summary>
		/// <param name="writer">The writer.</param>
		/// <param name="ident">The ident.</param>
		/// <param name="tag">The tag.</param>
		public static void WriteIdIfNotNull(
			this XmlWriter writer,
			IdentifiableName ident,
			string tag)
		{
			if (ident != null)
				writer.WriteElementString(
					tag,
					ident.Id.ToString(CultureInfo.InvariantCulture));
		}

		public static void WriteIdIfNotNull(
			this Dictionary<string, object> dictionary,
			IdentifiableName ident,
			string key)
		{
			if (ident != null) dictionary.Add(key, ident.Id);
		}

		/// <summary>
		///     Writes if not default or null.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="writer">The writer.</param>
		/// <param name="val">The value.</param>
		/// <param name="tag">The tag.</param>
		public static void WriteIfNotDefaultOrNull<T>(
			this XmlWriter writer,
			T? val,
			string tag) where T : struct
		{
			if (!val.HasValue) return;
			if (!EqualityComparer<T>.Default.Equals(val.Value, default(T)))
				writer.WriteElementString(
					tag,
					string.Format(NumberFormatInfo.InvariantInfo, "{0}", val.Value));
		}

		public static void WriteIfNotDefaultOrNull<T>(
			this Dictionary<string, object> dictionary,
			T? val,
			string tag) where T : struct
		{
			if (!val.HasValue) return;
			if (!EqualityComparer<T>.Default.Equals(val.Value, default(T)))
				dictionary.Add(tag, val.Value);
		}

		public static T GetValue<T>(
			this IDictionary<string, object> dictionary,
			string key)
		{
			object val;
			var dict = dictionary;
			var type = typeof(T);
			if (!dict.TryGetValue(key, out val)) return default(T);
			if (type.IsGenericType &&
				type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if (val == null) return default(T);
				type = Nullable.GetUnderlyingType(type);
			}
			if (val.GetType() == typeof(ArrayList)) return (T)val;
			if (type.IsEnum) val = Enum.Parse(type, val.ToString(), true);
			return (T)Convert.ChangeType(val, type);
		}

		public static IdentifiableName GetValueAsIdentifiableName(
			this IDictionary<string, object> dictionary,
			string key)
		{
			object val;
			if (!dictionary.TryGetValue(key, out val)) return null;
			var ser = new JavaScriptSerializer();
			ser.RegisterConverters(new[] { new IdentifiableNameConverter() });
			var result = ser.ConvertToType<IdentifiableName>(val);
			return result;
		}

		/// <summary>
		///     For Json
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static List<T> GetValueAsCollection<T>(
			this IDictionary<string, object> dictionary,
			string key) where T : new()
		{
			object val;
			if (!dictionary.TryGetValue(key, out val)) return null;
			var ser = new JavaScriptSerializer();
			ser.RegisterConverters(new[] { RedmineSerialization.Converters[typeof(T)] });
			return
				(from object item in (ArrayList)val select ser.ConvertToType<T>(item))
					.ToList();
		}
	}
}
