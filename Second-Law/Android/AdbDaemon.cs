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

		public static void WaitForDevice() {
			var process = new Process {
				StartInfo = {
					FileName = PathToADB,
					Arguments = "wait-for-device",
					UseShellExecute = false,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden
				}
			};
			process.Start();
			process.WaitForExit();
		}

		public static string RunADBCommand(string adbArguments, out string errorMessages) {
			var process = new Process{
				StartInfo = {
					FileName = PathToADB,
					Arguments = adbArguments,
					RedirectStandardError = true,
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Hidden
				}
			};
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
