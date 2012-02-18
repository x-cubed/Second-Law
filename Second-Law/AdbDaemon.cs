using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SecondLaw.Properties;

namespace SecondLaw {
	static class AdbDaemon {
		public const string BUILD_VERSION_SDK = "ro.build.version.sdk";

		public enum RebootMode {
			Normal,
			Recovery,
			Bootloader,
		}

		private static readonly Dictionary<byte, string> SdkVersions = new Dictionary<byte, string> {
			{ 1, "1.0"}, // Base
			{ 2, "1.1"}, // Base
			{ 3, "1.5"}, // Cupcake
			{ 4, "1.6"}, // Donut
			{ 5, "2.0"},  // Eclair
			{ 6, "2.0.1"}, // Eclair
			{ 7, "2.1"}, // Eclair MR1
			{ 8, "2.2"}, // Froyo
			{ 9, "2.3"}, // Gingerbread
			{ 10, "2.3"}, // Gingerbread MR1
			{ 11, "3.0"}, // Honeycomb
			{ 12, "3.1"}, // Honeycomb
			{ 13, "3.2"}, // Honeycomb
			{ 14, "4.0"}, // Ice Cream Sandwich
			{ 15, "4.0.3"}, // Ice Cream Sandwich MR1
		};

		public static string PathToADB {
			get { return Path.Combine(Settings.Default.PathToADK, "platform-tools", "adb.exe"); }
		}

		private static string RunADBCommand(string adbArguments, out string errorMessages) {
			var process = new Process();
			process.StartInfo.FileName = PathToADB;
			process.StartInfo.Arguments = adbArguments;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();

			string result;
			using (var output = process.StandardOutput) {
				using (var error = process.StandardError) {
					result = output.ReadToEnd();
					errorMessages = error.ReadToEnd();
				}
			}
			result = result.Replace("\r\r", "\r");
			result = result.Trim();
			return (result == "") ? null : result;
		}

		public static Dictionary<string, string> GetBuildProperties() {
			string errorMessages;
			var text = RunADBCommand("shell cat /system/build.prop", out errorMessages);
			if (!string.IsNullOrEmpty(errorMessages)) {
				throw new Exception(errorMessages);
			}

			var lines = text.Split('\r', '\n');

			var buildProps = new Dictionary<string, string>();
			foreach (string line in lines) {
				string cleanLine = line.Trim();
				if ((cleanLine != "") && (cleanLine[0] != '#')) {
					string[] kvp = cleanLine.Split(new[] { '=' }, 2);
					if (kvp.Length == 1) {
						Debug.WriteLine("Ignoring invalid build property: \"{0}\"", kvp[0]);
					} else {
						buildProps[kvp[0]] = kvp[1];
					}
				}
			}
			return buildProps;
		}

		public static string GetBuildProperty(string name) {
			string value;
			return GetBuildProperties().TryGetValue(name, out value) ? value : null;
		}

		public static string GetSystemInformation() {
			string errorMessages;
			string output = RunADBCommand("shell cat /proc/cpuinfo;cat /proc/meminfo", out errorMessages);
			output += Environment.NewLine + errorMessages;
			return output;
		}

		public static string Reboot(RebootMode mode = RebootMode.Normal) {
			string command;
			switch (mode) {
				case RebootMode.Recovery:
					command = "reboot recovery";
					break;
				case RebootMode.Bootloader:
					command = "reboot bootloader";
					break;
				default:
					command = "reboot";
					break;
			}
			string errorMessages;
			RunADBCommand(command, out errorMessages);
			return errorMessages;
		}

		public static void LaunchLogCat() {
			Process.Start(PathToADB, "logcat");
		}

		public static string GetSystemVersion() {
			string buildVersion;
			try {
				buildVersion = GetBuildProperty(BUILD_VERSION_SDK);
			} catch (Exception) {
				return null;
			}

			byte versionNumber;
			if (!byte.TryParse(buildVersion, out versionNumber)) {
				return null;
			}
			string version;
			if (!SdkVersions.TryGetValue(versionNumber, out version)) {
				return null;
			}
			return version;
		}

		public static string GetSerialNumber() {
			string errorMessages;
			string serial = RunADBCommand("get-serialno", out errorMessages);
			return (string.IsNullOrEmpty(errorMessages)) ? serial : null;
		}
	}
}
