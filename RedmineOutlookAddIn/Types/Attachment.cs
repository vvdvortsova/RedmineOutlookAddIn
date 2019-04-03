using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	///     Availability 1.3
	/// </summary>
	[XmlRoot("attachment")]
	public class Attachment : Identifiable<Attachment>,
		IXmlSerializable,
		IEquatable<Attachment>
	{
		/// <summary>
		///    Получить или задать имя файла.
		/// </summary>
		/// <value>Имя файла.</value>
		[XmlElement("filename")]
		public string FileName { get; set; }

		/// <summary>
		///     Получить или задать размер файла.
		/// </summary>
		/// <value>Размер файла.</value>
		[XmlElement("filesize")]
		public int FileSize { get; set; }

		/// <summary>
		///     Получить или задать тип контента.
		/// </summary>
		/// <value>Тип контента.</value>
		[XmlElement("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		///     Получить ил задать описание.
		/// </summary>
		/// <value>Описание.</value>
		[XmlElement("description")]
		public string Description { get; set; }

		/// <summary>
		///     Получить url контента.
		/// </summary>
		/// <value>Url контента</value>
		[XmlElement("content_url")]
		public string ContentUrl { get; set; }

		/// <summary>
		///     Получить автора.
		/// </summary>
		/// <value>Автор.</value>
		[XmlElement("author")]
		public IdentifiableName Author { get; set; }

		/// <summary>
		///     Получить дату создания.
		/// </summary>
		/// <value>Дата создания.</value>
		[XmlElement("created_on")]
		public DateTime? CreatedOn { get; set; }

		public bool Equals(Attachment other)
		{
			if (other == null) return false;
			return (Id == other.Id && FileName == other.FileName &&
					FileSize == other.FileSize && ContentType == other.ContentType &&
					Description == other.Description && ContentUrl == other.ContentUrl &&
					Author == other.Author && CreatedOn == other.CreatedOn);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

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
					case "filename":
						FileName = reader.ReadElementContentAsString();
						break;
					case "filesize":
						FileSize = reader.ReadElementContentAsInt();
						break;
					case "content_type":
						ContentType = reader.ReadElementContentAsString();
						break;
					case "author":
						Author = new IdentifiableName(reader);
						break;
					case "created_on":
						CreatedOn = reader.ReadElementContentAsNullableDateTime();
						break;
					case "description":
						Description = reader.ReadElementContentAsString();
						break;
					case "content_url":
						ContentUrl = reader.ReadElementContentAsString();
						break;
					default:
						reader.Read();
						break;
				}
			}
		}

		public void WriteXml(XmlWriter writer) { }
	}
}
