using System.Diagnostics;
using System.IO;
using SecondLaw.Properties;

namespace SecondLaw {
	public static class AdbDaemon {
		public enum RebootMode {
			Normal,
			Recovery,
			Bootloader,
		}

		public static string PathToADB {
			get { return Path.Combine(Settings.Default.PathToADK, "platform-tools", "adb.exe"); }
		}

		public static string RunADBCommand(string adbArguments, out string errorMessages) {
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
	}
}
