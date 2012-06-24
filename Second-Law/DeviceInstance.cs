using System;
using System.Diagnostics;
using SecondLaw.Android;
using SecondLaw.Windows;

namespace SecondLaw {
	public class DeviceInstance {
		internal DeviceInstance(SupportedDevice metadata, UsbDevice usbDevice) {
			Metadata = metadata;
			UsbDevice = usbDevice;
		}

		public SupportedDevice Metadata { get; private set; }
		public UsbDevice UsbDevice { get; private set; }

		private static string RunADBCommand(string command, out string errorMessages) {
			// TODO: Ensure command is targeting the right device, with -s <serialNumber>
			return AdbDaemon.RunADBCommand(command, out errorMessages);
		}

		private static string GetTextFile(string fileName) {
			string errorMessages;
			string file = GetTextFiles(new[] { fileName }, out errorMessages);
			return String.IsNullOrEmpty(errorMessages) ? file : null;
		}

		private static string GetTextFiles(string[] fileNames, out string errorMessages) {
			if (fileNames.Length == 0) {
				throw new ArgumentOutOfRangeException("fileNames");
			}
			return AdbDaemon.RunADBCommand("shell cat " + String.Join(" ", fileNames), out errorMessages);
		}

		public static BuildProperties GetBuildProperties() {
			var text = GetTextFile(BuildProperties.PATH);
			return (text == null) ? null : new BuildProperties(text);
		}

		public string GetSystemInformation() {
			string errorMessages;
			string output = GetTextFiles(new[] { "/proc/cpuinfo", "/proc/meminfo" }, out errorMessages);
			output += Environment.NewLine + errorMessages;
			return output;
		}

		public string Reboot(AdbDaemon.RebootMode mode = AdbDaemon.RebootMode.Normal) {
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
			string errorMessages;
			AdbDaemon.RunADBCommand(command, out errorMessages);
			return errorMessages;
		}

		public void LaunchLogCat() {
			Process.Start(AdbDaemon.PathToADB, "logcat");
		}

		public string GetSerialNumber() {
			string errorMessages;
			string serial = AdbDaemon.RunADBCommand("get-serialno", out errorMessages);
			return (String.IsNullOrEmpty(errorMessages)) ? serial : null;
		}

		public string InstallPackage(string filePath, out string errorMessages) {
			return RunADBCommand("install \"" + filePath + "\"", out errorMessages);
		}

		public string InstallPackage(string filePath) {
			string errorMessages;
			string result = InstallPackage(filePath, out errorMessages);
			if (String.IsNullOrEmpty(result)) {
				return errorMessages;
			} else if (String.IsNullOrEmpty(errorMessages)) {
				return result;
			} else {
				return result + "\r\n" + errorMessages;
			}
		}
	}
}
