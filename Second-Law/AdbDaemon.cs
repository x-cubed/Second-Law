using System;
using System.Diagnostics;
using System.IO;
using SecondLaw.Android;
using SecondLaw.Properties;

namespace SecondLaw {
	static class AdbDaemon {
		public enum RebootMode {
			Normal,
			Recovery,
			Bootloader,
		}

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

		private static string GetTextFile(string fileName) {
			string errorMessages;
			string file = GetTextFiles(new[] { fileName }, out errorMessages);
			return string.IsNullOrEmpty(errorMessages) ? file : null;
		}

		private static string GetTextFiles(string[] fileNames, out string errorMessages) {
			if (fileNames.Length == 0) {
				throw new ArgumentOutOfRangeException("fileNames");
			}
			return RunADBCommand("shell cat " + string.Join(" ", fileNames), out errorMessages);
		}

		public static BuildProperties GetBuildProperties() {
			var text = GetTextFile(BuildProperties.PATH);
			return (text == null) ? null : new BuildProperties(text);
		}

		public static string GetSystemInformation() {
			string errorMessages;
			string output = GetTextFiles(new[] { "/proc/cpuinfo", "/proc/meminfo" }, out errorMessages);
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

		public static string GetSerialNumber() {
			string errorMessages;
			string serial = RunADBCommand("get-serialno", out errorMessages);
			return (string.IsNullOrEmpty(errorMessages)) ? serial : null;
		}
	}
}
