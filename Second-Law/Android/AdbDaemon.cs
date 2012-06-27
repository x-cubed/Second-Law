using System;
using System.Collections.Generic;
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
			Debug.Print("AdbDaemon.WaitForDevice(): Waiting...");
			process.Start();
			process.WaitForExit();
			Debug.Print("AdbDaemon.WaitForDevice(): Device ready");
		}

		public static IEnumerable<string> RunADBCommand(string adbArguments, bool throwOnError = false) {
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

			using (var output = process.StandardOutput) {
				using (var error = process.StandardError) {
					while (!output.EndOfStream) {
						string line = output.ReadLine();
						if (!string.IsNullOrEmpty(line)) {
							yield return line;
						}
					}

					if (throwOnError) {
						string errors = error.ReadToEnd();
						if (!string.IsNullOrEmpty(errors)) {
							throw new Exception(errors);
						}
						yield break;
					}

					while (!error.EndOfStream) {
						string line = error.ReadLine();
						if (!string.IsNullOrEmpty(line)) {
							yield return line;
						}
					}
				}
			}
		}
	}
}
