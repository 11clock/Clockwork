using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Clockwork.Utils.Extensions;
using Microsoft.Xna.Framework.Content;

namespace Clockwork.Libraries
{
	public class ContentLib<T>
	{
		private readonly Dictionary<string, T> _library;
		private readonly Dictionary<T, string> _reverseLibrary;
		private readonly string _subfolder;

		public const string MainFolder = "Content";

		internal ContentLib(ContentManager content, bool stream = false)
		{
			_library = new Dictionary<string, T>();
			_reverseLibrary = new Dictionary<T, string>();
			_subfolder = typeof(T).Name;
			Load(content, stream);
		}

		private void Load(ContentManager content, bool stream)
		{
			string directory = $"{MainFolder}\\{_subfolder}";
			Debug.WriteLine($"Loading content from {directory}...");
			if (!Directory.Exists(directory))
			{
				Debug.WriteLine($"{_subfolder} library not loaded. Directory doesn't exist.");
				return;
			}

			string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

			for (int i = 0; i < files.Length; i++)
			{
				files[i] = files[i].Remove(0, $"{MainFolder}\\".Length);
				files[i] = Path.ChangeExtension(files[i], null);
			}

			foreach (string file in files)
			{
				string key = Path.GetFileNameWithoutExtension(file).ToLower().Replace("\\", "/");
				if (_library.ContainsKey(key))
				{
					throw new ContentLoadException(
						$"Multiple {_subfolder} with the name {key} exist. Asset names are case insensitive.");
				}

				T newContent = content.Load<T>(file);
				_library.Add(key, newContent);
				_reverseLibrary.Add(newContent, key);
			}

			Debug.WriteLine($"{_subfolder} library loaded: {_library.Keys.ToJoinedString()}");
		}

		public T Fetch(string textureName)
		{
			return _library[textureName.ToLower().Replace("\\", "/")];
		}

		public string FetchName(T texture)
		{
			return _reverseLibrary[texture];
		}
	}
}