using System;
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

			tslStatus.Text = "Waiting for device...";
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
			tslStatus.Text = "Scanning...";
			pnlScanning.Visible = true;
			pnlDevice.Visible = false;
			prgScanning.Visible = true;

			SupportedDevice supportedDevice = null;
			var devices = _hardware.EnumerateUsbDevices();
			foreach (var usbDevice in devices) {
				// Load the device icons
				string imageKey = usbDevice.PhysicalDeviceObjectName;
				LoadImage(imageKey, usbDevice.LargeIcon, imlLargeIcons);
				LoadImage(imageKey, usbDevice.SmallIcon, imlSmallIcons);

				supportedDevice = _supportedDevices.GetDevice(usbDevice.VendorId, usbDevice.ProductId, usbDevice.Revision, usbDevice.InterfaceId);
				if (supportedDevice != null) {
					break;
				}
			}

			prgScanning.Visible = false;
			if (supportedDevice != null) {
				tslStatus.Text = "Found a " + supportedDevice.ProductName;
				DisplayDeviceInformation(supportedDevice);
				pnlScanning.Visible = false;
				pnlDevice.Visible = true;
			} else {
				tslStatus.Text = "Waiting for device...";
			}
		}

		private void DisplayDeviceInformation(SupportedDevice device) {
			SetLink(lnkDeviceName, device.DeviceName, device.ProductPage);
			SetLink(lnkVendor, device.VendorName, device.SupportPage);
			SetLink(lnkManufacturer, device.ManufacturerName, device.ManufacturerPage);
			picDevice.Image = device.DeviceImage;
		}

		private void SetLink(LinkLabel link, string caption, Uri uri) {
			link.Text = caption;
			link.Links.Clear();
			if ((caption != null) && (uri != null)) {
				link.Links.Add(0, caption.Length, uri);
				toolTip.SetToolTip(link, uri.ToString());
				link.Enabled = true;
			} else {
				toolTip.SetToolTip(link, null);
				link.Enabled = false;
			}
		}

		private static void LoadImage(string key, Bitmap image, ImageList imageList) {
			if (!imageList.Images.ContainsKey(key) && (image != null)) {
				imageList.Images.Add(key, image);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}

		private void viewlogToolStripMenuItem_Click(object sender, EventArgs e) {
			Process.Start(_pathToADB, "logcat");
		}

		private void systemInformationToolStripMenuItem_Click(object sender, EventArgs e) {
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

		private void Link_Clicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var url = (Uri) e.Link.LinkData;
			if (url != null) {
				Process.Start(url.ToString());
			}
		}
	}
}
