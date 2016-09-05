using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace VTT_to_SRT_Subtitle_Converter
{
	class Program
	{
		public static bool isDebugging;
		
		public static void Main(string[] args)
		{
			if (args.FirstOrDefault().Equals("--help", StringComparison.OrdinalIgnoreCase) || args.FirstOrDefault().Equals("-h", StringComparison.OrdinalIgnoreCase))
			{
				var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
				#region Disclaimer
					const string disclaimer = "This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.\n\nThis program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.\n\nAdditional disclaimer content is contained on the project page for this project: \"https://github.com/AnonymousUser200102010/VTT-SRTSubtitleConverter\"\n\nBy using this product, you agree to the full disclaimer as it is contained in the project page.";
				#endregion
				Console.WriteLine(string.Format("{0}:\nCopyright: {1}\nVersion: {2}\n\nUse:\n\"{3}\" \"FILE TO EDIT.vtt\" [optional]: \"SECOND FILE.vtt\" \"THIRD FILE.vtt\" (etc)\n\nDISCLAIMER:\n{4}", versionInfo.ProductName, versionInfo.LegalCopyright, versionInfo.FileVersion, versionInfo.OriginalFilename, disclaimer));
			}
			else
			{
				#if DEBUG
				isDebugging = true;
				#endif
				
				var srt = new SRT();
				for (int pos = 0, argsLength = args.Length; pos < argsLength; pos++) 
				{
					string file = args[pos];
					if (!File.Exists(file.Replace("vtt", "srt"))) srt.Convert(file);
				}
			}
		}
	}
	
	/// <summary>
	/// All functions regarding the vtt-srt conversion process.
	/// </summary>
	class SRT
	{
		/// <summary>
		/// Any content, in part or full, that is not compatible with the .srt format.
		/// </summary>
		private ReadOnlyCollection<string> badContents = new ReadOnlyCollection<string>(new Collection<string>{ "WEBVTT", "Kind:", "Language:" });
		
		/// <summary>
		/// Convert a .vtt subtitle file into a compatible .srt subtitle file.
		/// </summary>
		/// <param name="file">
		/// The source .vtt subtitle file.
		/// </param>
		public void Convert(string file)
		{
			StreamReader reader = new StreamReader(file);
			ReadOnlyCollection<string> contentsOfFile = reader.ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList().AsReadOnly();
			reader.Close();
			reader = null;
			
			int curLineNum = 1;
			string writeContents = string.Empty;
			foreach (string s in contentsOfFile.Where(content => !badContents.Any(badItem => content.Contains(badItem, StringComparison.OrdinalIgnoreCase))))
			{
				writeContents += string.Format("{0}\n", s.Contains("-->") && s.Contains(":") ? string.Format("\n{0}\n{1}", curLineNum++, s.Replace('.', ',')) : string.Join(" ", s.Split(null).Select(text => Reformat(text))));
			}
			
			if (Program.isDebugging) Console.WriteLine(writeContents);
			
			using (StreamWriter writer = new StreamWriter(string.Format("{0}{1}", file.Replace("vtt", "srt"), Program.isDebugging ? ".test" : string.Empty), false))
		    {
		        writer.Write(writeContents);                     
		        writer.Close();
		    }
		}
		
		/// <summary>
		/// Searches a string value for srt-incompatable formatting and converts it into srt formatting.
		/// </summary>
		/// <param name="text">
		/// The string to search. Must be only a single word or phrase.
		/// </param>
		/// <returns>
		/// If the string contained any srt-incompatable formatting, it is returned with said formatting converted to it's srt-compatible counterpart; else simnply returns the text.
		/// </returns>
		private string Reformat(string text)
		{
			bool isItalics = text.StartsWith("/", StringComparison.OrdinalIgnoreCase) && text.EndsWith("/", StringComparison.OrdinalIgnoreCase);
			
			return isItalics ? text.Insert(text.Length - 1, "<").Insert(text.Length + 1, "i>").Remove(0, 1).Insert(0, "<i>") : text;
		}
	}
	
	static class Extensions
	{
		/// <summary>
        /// Returns a value indicating if the provided source string object occurs within the provided string value.
        /// </summary>
        /// <param name="source">
        /// The string to check.
        /// </param>
        /// <param name="value">
        /// The keyword to seek.
        /// </param>
        /// <param name="comp">
        /// One of the enumeration values that specifies the rules for the search.
        /// </param>
        /// <returns>
        /// true if the value occurs within the source, or if the value is empty; else false.
        /// </returns>
        public static bool Contains (this string source, string value, StringComparison comp)
        {
            return source.IndexOf(value, comp) >= 0;
        }
	}
}