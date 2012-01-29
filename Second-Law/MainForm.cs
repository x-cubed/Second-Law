using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SecondLaw.Properties;

namespace SecondLaw {
	public partial class MainForm : Form {
		private readonly Hardware _hardware = new Hardware();
		private readonly SupportedDevices _supportedDevices = new SupportedDevices();
		private readonly string _pathToADB;

		public MainForm() {
			InitializeComponent();

			_pathToADB = Settings.Default.PathToADK + "\\platform-tools\\adb.exe";

			_hardware.DeviceInterfaceChanged += Hardware_DeviceInterfaceChanged;
			_hardware.RegisterNotifications(this);

			ScanForUsbDevices();
		}

		private void Hardware_DeviceInterfaceChanged(object sender, DeviceInterfaceChangedArgs e) {
			ScanForUsbDevices();
		}

		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);
			_hardware.WndProc(ref m);
		}

		private void mniFileScanForDevices_Click(object sender, System.EventArgs e) {
			ScanForUsbDevices();
		}

		private void ScanForUsbDevices() {
			var devices = _hardware.EnumerateUsbDevices();
			lsvDevices.Items.Clear();
			lsvDevices.Columns.Clear();
			lsvDevices.Columns.Add("Name", 250);
			lsvDevices.Columns.Add("Manufacturer", 250);
			lsvDevices.Columns.Add("Configuration", 100);
			lsvDevices.Columns.Add("Vendor ID", 100);
			lsvDevices.Columns.Add("Product ID", 100);
			lsvDevices.Columns.Add("Revision", 100);
			lsvDevices.Columns.Add("Interface ID", 100);
			lsvDevices.Columns.Add("Location Information", 250);
			lsvDevices.Columns.Add("Physical Device Object Name", 500);
			lsvDevices.Columns.Add("Hardware IDs", 500);

			tslStatus.Text = "Ready";
			foreach (var usbDevice in devices) {
				var deviceItem = new ListViewItem(new[] {
				  usbDevice.DeviceDescription,
					usbDevice.Manufacturer,
					usbDevice.Configuration.ToString(),
					usbDevice.VendorId.ToString("X4"),
					usbDevice.ProductId.ToString("X4"),
					usbDevice.Revision.ToString("X4"),
					(usbDevice.InterfaceId == 0xFFFF) ? "" : usbDevice.InterfaceId.ToString("X2"),
					usbDevice.LocationInformation,
					usbDevice.PhysicalDeviceObjectName,
					string.Join(", ", usbDevice.HardwareIds)
				});

				// Load the device icons
				string imageKey = usbDevice.PhysicalDeviceObjectName;
				LoadImage(imageKey, usbDevice.LargeIcon, imlLargeIcons);
				LoadImage(imageKey, usbDevice.SmallIcon, imlSmallIcons);
				deviceItem.ImageKey = imageKey;

				var supportedDevice = _supportedDevices.GetDevice(usbDevice.VendorId, usbDevice.ProductId, usbDevice.Revision, usbDevice.InterfaceId);
				if (supportedDevice != null) {
					deviceItem.BackColor = Color.Gold;
					tslStatus.Text = supportedDevice.DeviceName;
				}

				lsvDevices.Items.Add(deviceItem);
			}
		}

		private static void LoadImage(string key, Bitmap image, ImageList imageList) {
			if (!imageList.Images.ContainsKey(key) && (image != null)) {
				imageList.Images.Add(key, image);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, System.EventArgs e) {
			Application.Exit();
		}

		private void viewlogToolStripMenuItem_Click(object sender, System.EventArgs e) {
			Process.Start(_pathToADB, "logcat");
		}

		private void systemInformationToolStripMenuItem_Click(object sender, System.EventArgs e) {
			var output = RunADBCommand("shell cat /proc/cpuinfo;cat /proc/meminfo");
			MessageBox.Show(output, "System Information");
		}

		private string RunADBCommand(string adbArguments) {
			var process = new Process();
			process.StartInfo.FileName = _pathToADB;
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
					result += error.ReadToEnd();
				}
			}
			result = result.Replace("\r\r", "\r");
			result = result.Trim();
			return (result == "") ? null : result;
		}

		private void normalToolStripMenuItem_Click(object sender, System.EventArgs e) {
			var output = RunADBCommand("reboot") ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Normal");
		}

		private void recoveryToolStripMenuItem_Click(object sender, System.EventArgs e) {
			var output = RunADBCommand("reboot recovery") ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Recovery");
		}

		private void bootloaderToolStripMenuItem_Click(object sender, System.EventArgs e) {
			var output = RunADBCommand("reboot bootloader") ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Bootloader");
		}
	}
}
