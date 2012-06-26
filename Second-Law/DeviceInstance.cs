using System;
using System.Collections.Generic;
using System.Diagnostics;
using SecondLaw.Android;
using SecondLaw.Windows;

namespace SecondLaw {
	public class DeviceInstance {
		internal DeviceInstance(SupportedDevice metadata, UsbDevice usbDevice) {
			Metadata = metadata;
			UsbDevice = usbDevice;
		}

		public void LoadDeviceInformation() {
			BuildProperties = GetBuildProperties();
		}

		public SupportedDevice Metadata { get; private set; }
		public UsbDevice UsbDevice { get; private set; }

		public BuildProperties BuildProperties { get; private set; }

		public void WaitForDevice() {
			AdbDaemon.WaitForDevice();
		}

		private IEnumerable<string> RunADBCommand(string command, bool throwOnError) {
			// TODO: Ensure command is targeting the right device, with -s <serialNumber>
			Debug.Print("DeviceInstance.RunADBCommand(\"{0}\")", command);
			return AdbDaemon.RunADBCommand(command, throwOnError);
		}

		private string RunADBCommandReturnString(string command, bool throwOnError) {
			string result = string.Join("\r\n", RunADBCommand(command, throwOnError));
			Debug.Print(result);
			return result;
		}

		public IEnumerable<string> RunShellCommand(string command) {
			if (command.Contains("\"")) {
				throw new ArgumentOutOfRangeException(command, "Can't contain quotes");
			}
			return RunADBCommand("shell \"" + command + "\"", true);
		}

		private string GetTextFile(string fileName) {
			return GetTextFiles(new[] { fileName });
		}

		private string GetTextFiles(string[] fileNames) {
			if (fileNames.Length == 0) {
				throw new ArgumentOutOfRangeException("fileNames");
			}
			WaitForDevice();
			return RunADBCommandReturnString("shell cat " + String.Join(" ", fileNames), true);
		}

		private BuildProperties GetBuildProperties() {
			var text = GetTextFile(BuildProperties.PATH);
			return (text == null) ? null : new BuildProperties(text);
		}

		public string GetSystemInformation() {
			return GetTextFiles(new[] { "/proc/cpuinfo", "/proc/meminfo" });
		}

		public void Reboot(AdbDaemon.RebootMode mode = AdbDaemon.RebootMode.Normal) {
			string command;
			switch (mode) {
				case AdbDaemon.RebootMode.Recovery:
					command = "reboot recovery";
					break;
				case AdbDaemon.RebootMode.Bootloader:
					command = "reboot bootloader";
					break;
				default:
					command = "reboot";
					break;
			}
			WaitForDevice();
			RunADBCommand(command, true);
		}

		public void LaunchLogCat() {
			Process.Start(AdbDaemon.PathToADB, "logcat");
		}

		public string GetSerialNumber() {
			WaitForDevice();
			return RunADBCommandReturnString("get-serialno", true);
		}

		public void ChangeMode(int mode, string path) {
			if (path.Contains("\"")) {
				throw new ArgumentOutOfRangeException("path", "Can't contain quotes");
			}

			WaitForDevice();
			RunADBCommandReturnString("shell chmod " + mode + " \"" + path + "\"", true);
		}

		public string PushFile(string sourcePath, string destinationPath) {
			if (sourcePath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("sourcePath", "Can't contain quotes");
			}
			if (destinationPath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("destinationPath", "Can't contain quotes");
			}

			WaitForDevice();
			return RunADBCommandReturnString("push \"" + sourcePath + "\" \"" + destinationPath + "\"", false);
		}

		public string InstallPackage(string filePath) {
			if (filePath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("filePath", "Can't contain quotes");
			}

			WaitForDevice();
			return RunADBCommandReturnString("install \"" + filePath + "\"", true);
		}
	}
}
