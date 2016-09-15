using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace VTT_to_SRT_Subtitle_Converter
{
	class Program
	{
<<<<<<< HEAD
		public static bool isDebugging;
		
		public static void Main(string[] args)
		{
=======
		/// <summary>
		/// The program was compiled under  the "Debug" configuration.
		/// </summary>
		public static bool isDebugging;
		
		/// <summary>
		/// The program's entry point.
		/// </summary>
		/// <param name="args">
		/// The supplied arguments to the program at runtime.
		/// </param>
		public static void Main(string[] args)
		{
			#if DEBUG
			isDebugging = true;
			#endif
			
>>>>>>> refs/remotes/origin/development
			if (args.FirstOrDefault().Equals("--help", StringComparison.OrdinalIgnoreCase) || args.FirstOrDefault().Equals("-h", StringComparison.OrdinalIgnoreCase))
			{
				var versionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
				#region Disclaimer
<<<<<<< HEAD
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
=======
					const string disclaimer = "This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.\n\nThis program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.\n\nAdditional disclaimer content is contained on the project page for this project: \"https://github.com/AnonymousUser200102010/VTT-SRTSubtitleConverter\" or by looking within the readme (that should be) contained in the contents of the project you downloaded\n\nBy using this project and any of it's contents, you agree to the full disclaimer as it is contained in the project page and readme file.";
				#endregion
				Console.WriteLine(string.Format("{0}:\nCopyright: {1}\nVersion: {2}{3}\n\nUse:\n\"{4}\" [COMMAND]\n\nCommands:\n\"FILE TO CONVERT.vtt\" [optional: \"SECOND FILE.vtt\" \"THIRD FILE.vtt\" (etc)]\n-h (or --help)\n\nDISCLAIMER:\n{5}", versionInfo.ProductName, versionInfo.LegalCopyright, versionInfo.FileVersion, Program.isDebugging ? string.Format("\n(Internal Project Version: {0})", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) : string.Empty, versionInfo.OriginalFilename, disclaimer));
			}
			else
			{
				SRT srt = new SRT();
				foreach (string file in args) 
				{
					if (file.EndsWith(".vtt", StringComparison.OrdinalIgnoreCase)) srt.Convert(file);
					else Console.WriteLine(string.Format("Could not utilize \"{0}\"; not a .vtt subtitle file.", file));
				}
			}
			Environment.Exit(0);
>>>>>>> refs/remotes/origin/development
		}
	}
	
	/// <summary>
<<<<<<< HEAD
	/// All functions regarding the vtt-srt conversion process.
=======
	/// All functions regarding the vtt-srt conversion process as well as any srt-specific variables and values.
>>>>>>> refs/remotes/origin/development
	/// </summary>
	class SRT
	{
		/// <summary>
		/// Any content, in part or full, that is not compatible with the .srt format.
		/// </summary>
		private ReadOnlyCollection<string> badContents = new ReadOnlyCollection<string>(new Collection<string>{ "WEBVTT", "Kind:", "Language:" });
		
		/// <summary>
<<<<<<< HEAD
=======
		/// Types of .srt-compliant formatting.
		/// </summary>
		public enum FormatType
		{
			None,
			Italics,
			Bold,
			Underlined,
			Colored
		}
		
		/// <summary>
>>>>>>> refs/remotes/origin/development
		/// Convert a .vtt subtitle file into a compatible .srt subtitle file.
		/// </summary>
		/// <param name="file">
		/// The source .vtt subtitle file.
		/// </param>
		public void Convert(string file)
		{
<<<<<<< HEAD
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
=======
			string convertedFile = file.Replace("vtt", "srt");
			if (!File.Exists(convertedFile)) 
			{
				using (StreamReader reader = new StreamReader(file)) 
				{
					String line;
					int curLineNum = 1;
					while (!reader.EndOfStream) 
					{
						if (!string.IsNullOrEmpty(line = reader.ReadLine()) && !badContents.Any(badItem => line.Contains(badItem, StringComparison.OrdinalIgnoreCase))) 
						{
							using (StreamWriter writer = new StreamWriter(string.Format("{0}{1}", convertedFile, Program.isDebugging ? ".test" : string.Empty), true)) 
							{
								writer.Write(string.Format("{0}\n", line.Contains("-->") && line.Contains(":") ? string.Format("\n{0}\n{1}", curLineNum++, line.Replace('.', ',')) : string.Join(" ", line.Split(null).Select(text => CheckFormatting(text)))));                     
								writer.Close();
							}
						}
						if (Program.isDebugging) Console.WriteLine(string.Format("{0}", line)); //Console.Read();
					}
					reader.Close();
				}
			} else Console.WriteLine(string.Format("{0} already exists.", convertedFile));
		}
		
		/// <summary>
		/// Searches a string value for srt-incompatable formatting.
>>>>>>> refs/remotes/origin/development
		/// </summary>
		/// <param name="text">
		/// The string to search. Must be only a single word or phrase.
		/// </param>
		/// <returns>
		/// If the string contained any srt-incompatable formatting, it is returned with said formatting converted to it's srt-compatible counterpart; else simnply returns the text.
		/// </returns>
<<<<<<< HEAD
		private string Reformat(string text)
		{
			bool isItalics = text.StartsWith("/", StringComparison.OrdinalIgnoreCase) && text.EndsWith("/", StringComparison.OrdinalIgnoreCase);
			
			return isItalics ? text.Insert(text.Length - 1, "<").Insert(text.Length + 1, "i>").Remove(0, 1).Insert(0, "<i>") : text;
=======
		private string CheckFormatting(string text)
		{
			//Due to problems compiling the program with a remove-insert paradigm, all the formatting changes are done at once and sent back immediately as a new string.
			//As such, the program may insert and/or remove text multiple times in the same operation.
			//
			//My thinking here was to not use regex, as it confuses me. If you should know of a regex paradigm that can do the same as I've done here,
			//please insert it into the code or tell me.
			var textFormatting = text.Formatting();
			return textFormatting.Equals(FormatType.None) ? text : Reformat(text, textFormatting);
		}
		
		/// <summary>
		/// Reformats a string with formatting to be srt compliant.
		/// </summary>
		/// <param name="text">
		/// The string to convert.
		/// </param>
		/// <param name="format">
		/// The srt-compliant format of the text provided.
		/// </param>
		/// <returns>
		/// Returns a previously incompatibly formatted string to it's compatible counterpart.
		/// </returns>
		private string Reformat(string text, FormatType format)
		{
			switch (format)
			{
				case FormatType.Italics:
					return text.Insert(text.Length - 1, "<").Insert(text.Length + 1, "i>").Remove(0, 1).Insert(0, "<i>");
				default:
					throw new NotImplementedException("When attempting to reformat a string for srt compatibility, a FormatType was passed which had no implimentation.");
			}
>>>>>>> refs/remotes/origin/development
		}
	}
	
	static class Extensions
	{
		/// <summary>
<<<<<<< HEAD
=======
		/// Find the .srt-compliant formatting of the supplied string.
		/// </summary>
		/// <param name="text">
		/// The text to check for formatting issues.
		/// </param>
		/// <returns>
		/// If the string contained any known incompatible formatting types, it is modified to the closest srt-compliant formatting type; else returns no formatting.
		/// </returns>
		public static SRT.FormatType Formatting(this string text)
		{
			return text.StartsWith("/", StringComparison.OrdinalIgnoreCase) && text.EndsWith("/", StringComparison.OrdinalIgnoreCase) ? SRT.FormatType.Italics : SRT.FormatType.None;
		}
		
		/// <summary>
>>>>>>> refs/remotes/origin/development
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