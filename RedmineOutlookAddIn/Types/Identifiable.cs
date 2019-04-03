using System.Xml.Serialization;

namespace Redmine.Net.Api.Types
{
	/// <summary>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Identifiable<T>
		where T : Identifiable<T>
	{
		/// <summary>
		///     Содержит id.
		/// </summary>
		/// <value>Id.</value>
		[XmlAttribute("id")]
		public int Id { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as T;
			if (other == null) return false;
			var thisIsNew = Equals(Id, default(int));
			var otherIsNew = Equals(other.Id, default(int));
			if (thisIsNew && otherIsNew)
				return ReferenceEquals(this, other);
			return Id.Equals(other.Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Identifiable<T> left, Identifiable<T> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Identifiable<T> left, Identifiable<T> right)
		{
			return !Equals(left, right);
		}
	}
}
