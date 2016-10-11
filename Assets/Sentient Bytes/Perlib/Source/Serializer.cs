using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace SentientBytes.Perlib
{
	/// <summary>
	/// Convenience class for serializing into and from xml
	/// </summary>
	public static class Serializer
	{
		/// <summary>
		///  Serializes an object to an xml string
		/// </summary>
		/// <param name="toSerialize">Object to serialize</param>
		/// <param name="sharedSecret">Optional encryption secret</param>
		public static string SerializeObject<T>(T toSerialize, string sharedSecret = null)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
			using (StringWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				return string.IsNullOrEmpty(sharedSecret) ? textWriter.ToString() : Cryptographer.EncryptStringAES(textWriter.ToString(), sharedSecret);
			}
		}

		/// <summary>
		/// Deserializes an object from a given xml string
		/// </summary>
		/// <param name="toDeserialize">String to deserialize</param>
		/// <param name="sharedSecret">Optional decryption secret</param>
		public static T DeserializeObject<T>(string toDeserialize, string sharedSecret = null)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			string toRead = string.IsNullOrEmpty(sharedSecret) ? toDeserialize : Cryptographer.DecryptStringAES(toDeserialize, sharedSecret);
			using (StringReader textReader = new StringReader(toRead))
				return (T)xmlSerializer.Deserialize(textReader);
		}

		/// <summary>
		/// Serializes an object to a given file path
		/// </summary>
		/// <param name="toSerialize">Object to serialize</param>
		/// <param name="filePath">File path</param>
		/// <param name="sharedSecret">Optional encryption secret</param>
		public static void SerializeObjectToFile<T>(T toSerialize, string filePath, string sharedSecret = null)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
			using (StringWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				using (StreamWriter fileWriter = new StreamWriter(filePath, false, Encoding.Unicode))
					fileWriter.Write(string.IsNullOrEmpty(sharedSecret) ? textWriter.ToString() : Cryptographer.EncryptStringAES(textWriter.ToString(), sharedSecret));
			}
		}

		/// <summary>
		/// Deserializes an object from a given file path
		/// </summary>
		/// <param name="filePath">File path</param>
		/// <param name="sharedSecret">Optional decryption secret</param>
		/// <returns></returns>
		public static T DeserializeObjectFromFile<T>(string filePath, string sharedSecret = null)
		{
			using (StreamReader fileReader = new StreamReader(filePath, Encoding.Unicode))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				string toRead = string.IsNullOrEmpty(sharedSecret) ? fileReader.ReadToEnd() : Cryptographer.DecryptStringAES(fileReader.ReadToEnd(), sharedSecret);
				using (StringReader textReader = new StringReader(toRead))
					return (T)xmlSerializer.Deserialize(textReader);
			}
		}
	}
}
