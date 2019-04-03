using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 2.1
	/// </summary>
	[XmlRoot("group")]
	public class Group : IXmlSerializable, IEquatable<Group>
	{
		/// <summary>
		///     Получить id.
		/// </summary>
		/// <value>
		///     Id.
		/// </value>
		[XmlElement("id")]
		public int Id { get; set; }

		/// <summary>
		///     Получить имя группы.
		/// </summary>
		/// <value>
		///     Имя.
		/// </value>
		[XmlElement("name")]
		public string Name { get; set; }

		/// <summary>
		///     Представляет пользователей группы.
		/// </summary>
		[XmlArray("users")]
		[XmlArrayItem("user")]
		public List<User> Users { get; set; }

		/// <summary>
		///     Полуить custom fields.
		/// </summary>
		/// <value>Сustom fields.</value>
		[XmlArray("custom_fields")]
		[XmlArrayItem("custom_field")]
		public IList<IssueCustomField> CustomFields { get; set; }

		/// <summary>
		///     Получить memberships.
		/// </summary>
		/// <value>Memberships.</value>
		[XmlArray("memberships")]
		[XmlArrayItem("membership")]
		public IList<Membership> Memberships { get; set; }

		#region Implementation of IEquatable<Group>

		/// <summary>
		///     
		/// Указывает, равен ли текущий объект другому объекту того же типа.
		/// </summary>
		/// <returns>
		///     true если текущий объект равен <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Group other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name && Users == other.Users &&
				   CustomFields == other.CustomFields && Memberships == other.Memberships;
		}

		#endregion

		public override string ToString()
		{
			return Id + ", " + Name;
		}

		#region Implementation of IXmlSerializable

		/// <summary>
		///     Этот метод зарезервирован и не должен использоваться. При реализации интерфейса IXmlSerializable interface,вы должны вернуть
		///     null из этого метода, и вместо этого, если требуется указать пользовательскую схему, примените
		///     <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> для класса.
		/// </summary>
		/// <returns>
		///     <see cref="T:System.Xml.Schema.XmlSchema" /> который описывает представление XML объекта
		///    Производится метод <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> 
		///    и используется <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />
		///     метод.
		/// </returns>
		public XmlSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		///    Создает объект из его представления XML.
		/// </summary>
		/// <param name="reader"><see cref="T:System.Xml.XmlReader" /> поток, из которого объект десериализован. </param>
		public void ReadXml(XmlReader reader)
		{
			reader.Read();
			while (!reader.EOF)
			{
				if (reader.IsEmptyElement && !reader.HasAttributes)
				{
					reader.Read();
					continue;
				}
				switch (reader.Name)
				{
					case "id":
						Id = reader.ReadElementContentAsInt();
						break;
					case "name":
						Name = reader.ReadElementContentAsString();
						break;
					case "users":
						Users = reader.ReadElementContentAsCollection<User>();
						break;
					case "custom_fields":
						CustomFields =
							reader.ReadElementContentAsCollection<IssueCustomField>();
						break;
					case "membershipds":
						Memberships = reader.ReadElementContentAsCollection<Membership>();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		/// <summary>
		///     Преобразует объект в его представление XML.
		/// </summary>
		/// <param name="writer"><see cref="T:System.Xml.XmlWriter" /> поток, из которого объект десериализован. </param>
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteElementString("name", Name);
			if (Users == null) return;
			writer.WriteStartElement("user_ids");
			writer.WriteAttributeString("type", "array");
			foreach (var userId in Users)
			{
				new XmlSerializer(typeof(int)).Serialize(writer, userId.Id);
			}
			writer.WriteEndElement();
		}

		#endregion
	}
}
