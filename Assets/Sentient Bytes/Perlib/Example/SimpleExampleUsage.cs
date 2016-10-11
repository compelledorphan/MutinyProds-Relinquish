using UnityEngine;
using System.Collections;
using SentientBytes.Perlib;

#pragma warning disable 219, 649

namespace PerlibExamples
{


	// This will show you basic usage of a Perlib.
	// Please follow the comments for explanations.


	public class SimpleExampleUsage : MonoBehaviour
	{

		// You can make a Perlib static and simply use the reference to it throughout the game.
		// This Perlib could be a generic savegame file

		static Perlib savefile;

		void Awake()
		{
			// Opening a new Perlib:

			// The following code creates a new file for the Perlib if none already exists.
			// If a file is found, it will instead be loaded into memory and a Perlib of it will be returned.

			// The two passwords provided in the constructor will be used for all encryption and decryption.

			// The "path" parameter was left null, so the Perlib will use the default path (Application.persistentDataPath),
			// while creating or looking for the file

			// If you are building for Web platforms, Perlib will fallback to using PlayerPrefs,
			// due to Unity's file system restrictions on these platforms.

			if (savefile == null)
				savefile = new Perlib("MyAwesomeGame.sav", null, "MyPassword", "MyOtherPassword");

			MyClass myObject = new MyClass();


			// You can save any type that can be serialized
			savefile.SetValue("Highscore", 5318008);
			savefile.SetValue("Dexterity", 8.7f);
			savefile.SetValue("Fullscreen", true);
			savefile.SetValue("Name", "Moogledoop the Merciless");
			savefile.SetValue("My Object", myObject);



			// And get it back
			int Highscore		= savefile.GetValue<int>("Highscore");
			float Dexterity		= savefile.GetValue<float>("Dexterity");
			bool Fullscreen		= savefile.GetValue<bool>("Fullscreen");
			string Name			= savefile.GetValue<string>("Name");
			MyClass LoadedObj	= savefile.GetValue<MyClass>("My Object");



			// If a key is not found, a default value will instead be returned

			int x = savefile.GetValue<int>("Population"); // This will return 0, if not found
			int y = savefile.GetValue<int>("Population", 55378008); // This will return 55378008, if not found


			// Most of Perlibs was designed to mimic the PlayerPrefs interface
			if (!savefile.HasKey("Highscore"))
				savefile.SetValue("Highscore", 0);
		}
	}

	class MyClass
	{
		public int X, Y, Z;
		public Vector3 Point;
	}
}

#pragma warning restore 219, 649