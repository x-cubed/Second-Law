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

		public void LoadDeviceInformation() {
			BuildProperties = GetBuildProperties();
		}

		public SupportedDevice Metadata { get; private set; }
		public UsbDevice UsbDevice { get; private set; }

		public BuildProperties BuildProperties { get; private set; }

		public void WaitForDevice() {
			AdbDaemon.WaitForDevice();
		}

		private string RunADBCommand(string command, out string errorMessages) {
			// TODO: Ensure command is targeting the right device, with -s <serialNumber>
			string result = AdbDaemon.RunADBCommand(command, out errorMessages);
			Debug.Print("DeviceInstance.RunADBCommand(\"{0}\"):\r\n" + result + "\r\n" + errorMessages);
			return result;
		}

		public string RunADBCommandOrThrow(string command) {
			string errorMessages;
			string result = RunADBCommand(command, out errorMessages);
			if (!string.IsNullOrEmpty(errorMessages)) {
				throw new Exception(command + " failed: " + errorMessages);
			}
			return result;
		}

		private string GetTextFile(string fileName) {
			string errorMessages;
			string file = GetTextFiles(new[] { fileName }, out errorMessages);
			return String.IsNullOrEmpty(errorMessages) ? file : null;
		}

		private string GetTextFiles(string[] fileNames, out string errorMessages) {
			if (fileNames.Length == 0) {
				throw new ArgumentOutOfRangeException("fileNames");
			}
			WaitForDevice();
			return RunADBCommand("shell cat " + String.Join(" ", fileNames), out errorMessages);
		}

		private BuildProperties GetBuildProperties() {
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
			WaitForDevice();
			string errorMessages;
			RunADBCommand(command, out errorMessages);
			return errorMessages;
		}

		public void LaunchLogCat() {
			Process.Start(AdbDaemon.PathToADB, "logcat");
		}

		public string GetSerialNumber() {
			WaitForDevice();
			string errorMessages;
			string serial = RunADBCommand("get-serialno", out errorMessages);
			return (String.IsNullOrEmpty(errorMessages)) ? serial : null;
		}

		public void ChangeMode(int mode, string path) {
			WaitForDevice();
			RunADBCommandOrThrow("shell chmod " + mode + " \"" + path + "\"");			
		}

		public string PushFile(string sourcePath, string destinationPath) {
			WaitForDevice();
			string result;
			RunADBCommand("push \"" + sourcePath + "\" \"" + destinationPath + "\"", out result);
			return result;
		}

		public string InstallPackage(string filePath, out string errorMessages) {
			WaitForDevice();
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
