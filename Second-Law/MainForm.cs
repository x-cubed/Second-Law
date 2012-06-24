using System;
using System.Diagnostics;
using System.Drawing;
using System.Management.Automation;
using System.Windows.Forms;
using SecondLaw.Windows;

namespace SecondLaw {
	public partial class MainForm : Form {
		private const string DEFAULT_TASK_ICON = "cmd.ico";

		private readonly Hardware _hardware = new Hardware();
		private readonly SupportedDevices _supportedDevices = new SupportedDevices();
		private readonly Timer _scanForDevices = new Timer();

		private DeviceInstance _currentDevice;

		public MainForm() {
			InitializeComponent();
			lsvScripts.Items.Clear();

			_hardware.DeviceInterfaceChanged += Hardware_DeviceInterfaceChanged;
			_hardware.RegisterNotifications(this);

			tslStatus.Text = "Waiting for device...";
			_scanForDevices.Interval = 500;
			_scanForDevices.Tick += ScanForDevices_Tick;
			_scanForDevices.Enabled = true;
		}

		private void ScanForDevices_Tick(object sender, EventArgs e) {
			_scanForDevices.Enabled = false;
			ScanForUsbDevices();
		}

		private void Hardware_DeviceInterfaceChanged(object sender, DeviceInterfaceChangedArgs e) {
			_scanForDevices.Enabled = true;
		}

		protected override void WndProc(ref Message m) {
			base.WndProc(ref m);
			_hardware.WndProc(ref m);
		}

		private void mniFileScanForDevices_Click(object sender, EventArgs e) {
			ScanForUsbDevices();
		}

		private void ScanForUsbDevices() {
			tslStatus.Text = "Scanning...";
			mnuDevice.Enabled = false;
			pnlScanning.Visible = true;
			pnlDevice.Visible = false;
			prgScanning.Visible = true;

			var devices = _hardware.EnumerateUsbDevices();
			foreach (var usbDevice in devices) {
				// Load the device icons
				string imageKey = usbDevice.PhysicalDeviceObjectName;
				LoadImage(imageKey, usbDevice.LargeIcon, imlLargeIcons);
				LoadImage(imageKey, usbDevice.SmallIcon, imlSmallIcons);

				SupportedDevice supportedDevice = _supportedDevices.GetDevice(usbDevice.VendorId, usbDevice.ProductId,
																																			usbDevice.Revision, usbDevice.InterfaceId);
				if (supportedDevice != null) {
					_currentDevice = new DeviceInstance(supportedDevice, usbDevice);
					break;
				}
			}

			prgScanning.Visible = false;
			DisplayCurrentDevice();
		}

		private void DisplayCurrentDevice() {
			if (_currentDevice != null) {
				tslStatus.Text = "Found a " + _currentDevice.Metadata.ProductName;
				mnuDevice.Enabled = true;
				DisplayDeviceInformation(_currentDevice);
				DisplayTasksForDevice(_currentDevice);
				pnlScanning.Visible = false;
				pnlDevice.Visible = true;
			} else {
				tslStatus.Text = "Waiting for device...";
				mnuDevice.Enabled = false;
			}
		}

		private void DisplayDeviceInformation(DeviceInstance device) {
			var props = DeviceInstance.GetBuildProperties();
			if (props == null) {
				return;
			}

			SetLink(lnkDeviceName, device.Metadata.DeviceName ?? props.ProductModel, device.Metadata.ProductPage);
			lblSerialNumber.Text = device.GetSerialNumber() ?? "(Unknown)";
			lblSystemVersion.Text = props.SystemVersion ?? "(Unknown)";
			SetLink(lnkVendor, device.Metadata.VendorName, device.Metadata.SupportPage);
			SetLink(lnkManufacturer, device.Metadata.ManufacturerName ?? props.ProductManufacturer, device.Metadata.ManufacturerPage);
			picDevice.Image = device.Metadata.DeviceImage;
		}

		private void DisplayTasksForDevice(DeviceInstance device) {
			lsvScripts.Items.Clear();

			var tasks = Tasks.LoadCompatibleTasksFor(device);
			foreach (var task in tasks) {
				var icon = task.Icon;
				var iconKey = (icon == null) ? DEFAULT_TASK_ICON : task.Folder.FullName;
				var item = new ListViewItem(task.Name, iconKey) { Tag = task };
				if ((icon != null) && !imlLargeIcons.Images.ContainsKey(iconKey)) {
					imlLargeIcons.Images.Add(iconKey, icon);
				}
				lsvScripts.Items.Add(item);
			}
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
			_currentDevice.LaunchLogCat();
		}

		private void systemInformationToolStripMenuItem_Click(object sender, EventArgs e) {
			MessageBox.Show(_currentDevice.GetSystemInformation(), "System Information");
		}

		private void normalToolStripMenuItem_Click(object sender, EventArgs e) {
			var output = _currentDevice.Reboot() ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Normal");
		}

		private void recoveryToolStripMenuItem_Click(object sender, EventArgs e) {
			var output = _currentDevice.Reboot(AdbDaemon.RebootMode.Recovery) ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Recovery");
		}

		private void bootloaderToolStripMenuItem_Click(object sender, EventArgs e) {
			var output = _currentDevice.Reboot(AdbDaemon.RebootMode.Bootloader) ?? "Device is rebooting";
			MessageBox.Show(output, "Reboot - Bootloader");
		}

		private void Link_Clicked(object sender, LinkLabelLinkClickedEventArgs e) {
			var url = (Uri)e.Link.LinkData;
			if (url != null) {
				Process.Start(url.ToString());
			}
		}

		private void lsvScripts_MouseDoubleClick(object sender, MouseEventArgs e) {
			var hitTest = lsvScripts.HitTest(e.Location);
			if ((hitTest.Item != null) && (_currentDevice != null)) {
				var task = (Task)hitTest.Item.Tag;
				RunTask(task, _currentDevice);
			}
		}

		private void RunTask(Task task, DeviceInstance device) {
			tslStatus.Text = string.Format("Running task \"{0}\" for the \"{1}\"...", task.Name, device.Metadata.ProductName);
			lsvScripts.Enabled = false;
			try {
				// Run the script
				string result = task.Run(device);
				if (!string.IsNullOrEmpty(result)) {
					MessageBox.Show(this, result, task.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			} catch (ParseException px) {
				// Script was unable to be parsed
				string message = string.Format(
					"Unable to parse PowerShell script:\r\n" +
					"\r\n" +
					"File: {0}\r\n" +
					"\r\n" +
					"Error: {1}{2}",
					task.ScriptFile.FullName, px.Message, px.ErrorRecord.InvocationInfo.PositionMessage);
				MessageBox.Show(this, message, task.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.Print(px.ToString());

			} catch (RuntimeException rx) {
				// Script failed to run
				string message = string.Format(
					"Failed to run PowerShell script:\r\n" +
					"\r\n" +
					"File: {0}\r\n" +
					"\r\n" +
					"Error: {1}{2}",
					task.ScriptFile.FullName, rx.Message, rx.ErrorRecord.InvocationInfo.PositionMessage);
				MessageBox.Show(this, message, task.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.Print(rx.ToString());
				
			} catch (Exception x) {
				// Task failed
				MessageBox.Show(this, x.ToString(), task.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Debug.Print(x.ToString());
			}
			lsvScripts.Enabled = true;

			// Update the device information and available task list
			DisplayCurrentDevice();
		}
	}
}
