using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace SentientBytes.Perlib
{
	/// <summary>
	/// File-based, generic, optionally encrypted, and unliited-size alternative to PlayerPrefs
	/// </summary>
	public class Perlib
	{
		#region Public Vars
		public const string Version = "1.0.0";

		/// <summary>
		/// Default file path used by Perlib
		/// </summary>
		public static string DefaultPath { get { return Application.persistentDataPath; } }

		/// <summary>
		/// Name of this Perlib
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// FileInfo of this Perlib. Use this if you need to know the filename and path
		/// </summary>
		public FileInfo Info { get; private set; }

		/// <summary>
		/// Whether the Perlib should be written to disk each time a value is set
		/// </summary>
		public bool SaveOnSetValue { get; set; }

		/// <summary>
		/// Password used for the entire library
		/// </summary>
		public string LibraryPassword { get; private set; }

		/// <summary>
		/// Password used for values only
		/// </summary>
		public string ValuesPassword { get; private set; }
		#endregion

		#region Private Vars
		/// <summary>
		/// This is where our keys and values are stored in memory
		/// </summary>
		SerializableDictionary<string, string> library { get; set; }
		#endregion

		#region Public Functions
		/// <summary>
		/// Delete a Perlib file from disk, or PlayerPrefs if on a Web platform
		/// </summary>
		/// <param name="name">Name of the Perlib to delete. Also the filename</param>
		/// <param name="path">Path of where the Perlib is located. Default path will be used if null. Is ignored on Web platforms</param>
		public static void Delete(string name, string path = null)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

#if (UNITY_WEBPLAYER || UNITY_WEBGL)
			PlayerPrefs.DeleteKey(name);
#else
			FileInfo info = new FileInfo(!string.IsNullOrEmpty(path) ? path : DefaultPath + @"\" + name);
			info.Delete();
#endif
		}

		/// <summary>
		/// Whether a Perlib of a given name exists on the disk, or PlayerPrefs if on a Web platform
		/// </summary>
		/// <param name="name">Name of the Perlib to check. Also the filename</param>
		/// <param name="filePath">Path of where the Perlib is located. Default path will be used if null. Is ignored on Web platforms</param>
		/// <returns></returns>
		public static bool Exists(string name, string filePath = null)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

#if (UNITY_WEBPLAYER || UNITY_WEBGL)
			return PlayerPrefs.HasKey(name);
#else
			string path = string.IsNullOrEmpty(filePath) ? DefaultPath : filePath;
			FileInfo info = new FileInfo(path + @"\" + name);
			return info.Exists;
#endif
		}

		/// <summary>
		/// Returns a new Perlib reference. This will open a Perlib file if it already exists, or create a new one if not.
		/// If there are any errors reading an already existing Perlib file, it will be replaced by a new one.
		/// </summary>
		/// <param name="name">Name of the Perlib to open. Also the filename</param>
		/// <param name="path">Path of where the Perlib should be located. Default path will be used if null. Is ignored on Web platforms</param>
		/// <param name="libraryPassword">Password used for the entire library. Library will not be encrypted if null</param>
		/// <param name="valuesPassword">Password used for values only. Values will not be encrypted if null</param>
		public Perlib(string name, string path = null, string libraryPassword = null, string valuesPassword = null)
		{
			LibraryPassword = libraryPassword;
			ValuesPassword = valuesPassword;

			Name = name;

			try
			{
#if (UNITY_WEBPLAYER || UNITY_WEBGL)
				if (PlayerPrefs.HasKey(Name))
					library = Serializer.DeserializeObject<SerializableDictionary<string, string>>(PlayerPrefs.GetString(Name), LibraryPassword);
#else
				Info = new FileInfo(!string.IsNullOrEmpty(path) ? path : DefaultPath + @"\" + name);

				if (Info.Exists)
					library = Serializer.DeserializeObjectFromFile<SerializableDictionary<string, string>>(Info.FullName, LibraryPassword);
#endif
				else
					CreateNew();
			}
			catch (Exception e)
			{
				Debug.LogWarning("Something went wrong trying to deserialize PersistentLibrary " + name + ".\nCreating new Library.\n" + e.Message);
				CreateNew();
			}
		}

		/// <summary>
		/// Returns true if key exists in the Perlib
		/// </summary>
		public bool HasKey(string key)
		{
			return library.ContainsKey(key);
		}

		/// <summary>
		/// Removes all keys and values from the Perlib. Use with caution
		/// </summary>
		public void DeleteAll()
		{
			library.Clear();
		}

		/// <summary>
		/// Removes key and its corresponding value from the Perlib.
		/// </summary>
		public void DeleteKey(string key)
		{
			if (HasKey(key))
				library.Remove(key);
		}

		/// <summary>
		/// Writes Perlib to disk, or PlayerPrefs on Web platforms
		/// </summary>
		public void Save()
		{
#if (UNITY_WEBPLAYER || UNITY_WEBGL)
			PlayerPrefs.SetString(Name, Serializer.SerializeObject(library, LibraryPassword));
#else
			Serializer.SerializeObjectToFile(library, Info.FullName, LibraryPassword);
#endif
		}

		/// <summary>
		/// Sets the value corresponding to key
		/// </summary>
		public void SetValue<T>(string key, T value)
		{
			string valueString = null;

			try
			{
				valueString = Serializer.SerializeObject(value);
				if (!string.IsNullOrEmpty(ValuesPassword))
					valueString = Cryptographer.EncryptStringAES(valueString, ValuesPassword);
			}
			catch (Exception e)
			{
				Debug.LogWarning("Something went wrong trying to set Value for Key " + key + ".\nNothing was set.\n" + e.Message);
				return;
			}

			library[key] = valueString;

			if (SaveOnSetValue)
				Save();
		}

		/// <summary>
		/// Gets the value corresponding to key. If key is not found, a default value for the requested type will instead be returned
		/// </summary>
		/// <typeparam name="T">Type of the value you want returned</typeparam>
		public T GetValue<T>(string key, T defaultValue = default(T))
		{
			if (HasKey(key))
			{
				try
				{
					string valueString = library[key];
					if (!string.IsNullOrEmpty(ValuesPassword))
						valueString = Cryptographer.DecryptStringAES(valueString, ValuesPassword);
					return Serializer.DeserializeObject<T>(valueString);
				}
				catch (Exception e)
				{
					Debug.LogWarning("Something went wrong trying to deserialize Value for Key " + key + ".\nReturning default.\n" + e.Message);
					return defaultValue;
				}
			}
			else
				return defaultValue;
		}
		#endregion

		#region Private Functions
		/// <summary>
		/// Creates a new library that is then written to disk
		/// </summary>
		void CreateNew()
		{
			library = new SerializableDictionary<string, string>();
			Save();
		}
		#endregion
	}
}