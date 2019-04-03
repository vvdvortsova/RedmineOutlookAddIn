using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	public class IdentifiableName : Identifiable<IdentifiableName>,
		IXmlSerializable,
		IEquatable<IdentifiableName>
	{
		/// <summary>
		///     Инициализирует новый экземпляр <see cref="IdentifiableName" /> класса.
		/// </summary>
		public IdentifiableName() { }

		/// <summary>
		///     Инициализирует новый экземпляр <see cref="IdentifiableName" /> класса.
		/// </summary>
		/// <param name="reader">Reader.</param>
		public IdentifiableName(XmlReader reader)
		{
			// TODO: избегать вызова виртуального метода в конструкторе
			ReadXml(reader);
		}

		/// <summary>
		///     Содержит имя.
		/// </summary>
		/// <value>Name.</value>
		[XmlAttribute("name")]
		public string Name { get; set; }

		public bool Equals(IdentifiableName other)
		{
			if (other == null) return false;
			return (Id == other.Id && Name == other.Name);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			Id = Convert.ToInt32(reader.GetAttribute("id"));
			Name = reader.GetAttribute("name");
			reader.Read();
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteAttributeString("id", Id.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("name", Name);
		}

		public override string ToString()
		{
			return Id + ", " + Name;
		}
	}
}