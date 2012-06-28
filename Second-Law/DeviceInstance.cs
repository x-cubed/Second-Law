using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SecondLaw.Android;
using SecondLaw.Windows;

namespace SecondLaw {
	public class DeviceInstance {
		private const string PLATFORM_DEVICES = "/sys/devices/platform/";
		private const string BATTERY_CAPACITY = "/power_supply/battery/capacity";
		private const string BATTERY_STATUS = "/power_supply/battery/status";

		private string _batteryDevicePath;

		internal DeviceInstance(SupportedDevice metadata, UsbDevice usbDevice) {
			Metadata = metadata;
			UsbDevice = usbDevice;
		}

		public void LoadDeviceInformation() {
			BuildProperties = GetBuildProperties();

			// Find the battery device
			IEnumerable<string> devices = RunShellCommand("ls -1 " + PLATFORM_DEVICES);
			string battery = devices.FirstOrDefault(d => d.EndsWith("_battery"));
			if (battery != null) {
				_batteryDevicePath = PLATFORM_DEVICES + battery;
			}
		}

		public SupportedDevice Metadata { get; private set; }
		public UsbDevice UsbDevice { get; private set; }

		public BuildProperties BuildProperties { get; private set; }

		public void WaitForDevice() {
			AdbDaemon.WaitForDevice();
		}

		public byte? BatteryChargePercentage {
			get {
				if (_batteryDevicePath == null) {
					return null;
				}

				string capacity = RunShellCommand("cat " + _batteryDevicePath + BATTERY_CAPACITY).FirstOrDefault();
				byte percentage;
				return (byte.TryParse(capacity, out percentage)) ? percentage : (byte?)null;
			}
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

		public void ChangeOwner(string owner, string path) {
			if (owner.Contains("\"") || owner.Contains(" ")) {
				throw new ArgumentOutOfRangeException("owner", "Can't contain quotes");
			}
			if (path.Contains("\"")) {
				throw new ArgumentOutOfRangeException("path", "Can't contain quotes");
			}

			WaitForDevice();
			RunADBCommandReturnString("shell chown " + owner + " \"" + path + "\"", true);
		}

		public void CopyFile(string sourcePath, string destinationPath) {
			if (sourcePath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("sourcePath", "Can't contain quotes");
			}
			if (destinationPath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("destinationPath", "Can't contain quotes");
			}

			WaitForDevice();
			RunADBCommandReturnString("shell dd if=\"" + sourcePath + "\" of=\"" + destinationPath + "\"", true);
		}

		public void CreateSymbolicLink(string targetPath, string name) {
			if (targetPath.Contains("\"")) {
				throw new ArgumentOutOfRangeException("targetPath", "Can't contain quotes");
			}
			if (name.Contains("\"")) {
				throw new ArgumentOutOfRangeException("name", "Can't contain quotes");
			}

			WaitForDevice();
			RunADBCommandReturnString("shell ln -s \"" + targetPath + "\" \"" + name + "\"", true);
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

		public void RemoveFile(string path) {
			if (path.Contains("\"")) {
				throw new ArgumentOutOfRangeException("path", "Can't contain quotes");
			}

			WaitForDevice();
			RunADBCommandReturnString("shell rm -r \"" + path + "\"", false);
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
