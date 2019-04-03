using System;
using System.Xml;
using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	[XmlRoot("role")]
	public class MembershipRole : IdentifiableName, IEquatable<MembershipRole>
	{
		/// <summary>
		///     Gets or sets a value indicating whether this <see cref="MembershipRole" /> is inherited.
		/// </summary>
		/// <value>
		///     <c>true</c> if inherited; otherwise, <c>false</c>.
		/// </value>
		[XmlAttribute("inherited")]
		public bool Inherited { get; set; }

		public bool Equals(MembershipRole other)
		{
			if (other == null) return false;
			return Id == other.Id && Name == other.Name && Inherited == other.Inherited;
		}

		/// <summary>
		///     Reads the XML.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public override void ReadXml(XmlReader reader)
		{
			Id = Convert.ToInt32(reader.GetAttribute("id"));
			Name = reader.GetAttribute("name");
			Inherited = reader.ReadAttributeAsBoolean("inherited");
			reader.Read();
		}

		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteValue(Id);
		}

		public override string ToString()
		{
			return Id + ", " + Name + ", " + Inherited;
		}
	}
}
