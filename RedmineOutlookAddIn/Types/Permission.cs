using System;

using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	[XmlRoot("permission")]
	public class Permission
	{
		[XmlText]
		public string Info { get; set; }

		public override string ToString()
		{
			return Info;
		}
	}
}
