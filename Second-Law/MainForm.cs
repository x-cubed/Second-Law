﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Windows.Forms;
using SecondLaw.Android;
using SecondLaw.PowerShell;
using SecondLaw.Windows;
using Timer = System.Windows.Forms.Timer;

namespace SecondLaw {
	public partial class MainForm : Form {
		private const string DEFAULT_TASK_ICON = "cmd.ico";

		private readonly BackgroundWorker _scanner = new BackgroundWorker();
		private readonly Hardware _hardware = new Hardware();
		private readonly SupportedDevices _supportedDevices = new SupportedDevices();
		private readonly Timer _scanForDevices = new Timer();
		private readonly Host _psHost;

		private DeviceInstance _currentDevice;

		public MainForm() {
			InitializeComponent();
			lsvScripts.Items.Clear();

			// Update adb_usb.ini then kill off any existing ADB instance
			var vendorIds = _supportedDevices.Select(d => d.VendorId).Distinct().ToArray();
			Debug.Print("MainForm.ctor(): Writing adb_usb.ini file for {0} vendors", vendorIds.Length);
			AdbUsb.WriteVendorIds(vendorIds);
			AdbDaemon.KillServer();

			// Create the host UI for PowerShell scripts
			_psHost = new Host(this);
			
			// Create the scanner for finding devices and reading device properties
			_scanner.DoWork += Scanner_DoWork;
			_scanner.RunWorkerCompleted += Scanner_RunWorkerCompleted;

			// Listen for hardware change notifications
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
			if (_scanner.IsBusy) {
				return;
			}

			tslStatus.Text = "Scanning...";
			mnuDevice.Enabled = false;
			pnlScanning.Visible = true;
			pnlDevice.Visible = false;
			prgScanning.Visible = true;
			_scanner.RunWorkerAsync();
		}

		private void Scanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			_currentDevice = (DeviceInstance)e.Result;

			prgScanning.Visible = false;
			DisplayCurrentDevice();
		}

		private void Scanner_DoWork(object sender, DoWorkEventArgs e) {
			DeviceInstance currentDevice = null;
			var devices = _hardware.EnumerateUsbDevices();
			foreach (var usbDevice in devices) {			
				SupportedDevice supportedDevice = _supportedDevices.GetDevice(usbDevice.VendorId, usbDevice.ProductId,
																																			usbDevice.Revision, usbDevice.InterfaceId);
				if (supportedDevice != null) {
					currentDevice = new DeviceInstance(supportedDevice, usbDevice);
					currentDevice.LoadDeviceInformation();
					break;
				}
			}
			e.Result = currentDevice;
		}

		private void DisplayCurrentDevice() {
			if (_currentDevice != null) {
				tslStatus.Text = "Found a " + _currentDevice.Metadata.ProductName;

				// Load the device icons
				string imageKey = _currentDevice.UsbDevice.PhysicalDeviceObjectName;
				LoadImage(imageKey, _currentDevice.UsbDevice.LargeIcon, imlLargeIcons);
				LoadImage(imageKey, _currentDevice.UsbDevice.SmallIcon, imlSmallIcons);

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
			var props = device.BuildProperties;
			SetLink(lnkDeviceName, device.Metadata.DeviceName ?? props.ProductModel, device.Metadata.ProductPage);
			lblSerialNumber.Text = device.GetSerialNumber() ?? "(Unknown)";
			lblSystemVersion.Text = props.SystemVersion ?? "(Unknown)";
			SetLink(lnkVendor, device.Metadata.VendorName, device.Metadata.SupportPage);
			SetLink(lnkManufacturer, device.Metadata.ManufacturerName ?? props.ProductManufacturer, device.Metadata.ManufacturerPage);
			picDevice.Image = device.Metadata.DeviceImage;

			byte? batteryCharge = device.BatteryChargePercentage;
			if (batteryCharge == null) {
				lbdBatteryCharge.Visible = false;
				prgBatteryCharge.Visible = false;
			} else {
				lbdBatteryCharge.Visible = true;
				prgBatteryCharge.Visible = true;
				prgBatteryCharge.Value = batteryCharge.Value;
				toolTip.SetToolTip(prgBatteryCharge, batteryCharge.Value + "% charged");
			}

			Volume volume = device.Volumes.FirstOrDefault();
			if (volume == null) {
				lbdDiskUsage.Visible = false;
				prgDiskUsage.Visible = false;
			} else {
				lbdDiskUsage.Visible = true;
				prgDiskUsage.Visible = true;
				prgDiskUsage.Value = (int)volume.PercentUsed;
				toolTip.SetToolTip(prgDiskUsage, string.Format(
					"{0} of {1}",
					Conversions.ToSize(volume.SizeUsedBytes),
					Conversions.ToSize(volume.SizeTotalBytes)
				));
			}
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
			TryReboot(AdbDaemon.RebootMode.Normal);
		}

		private void recoveryToolStripMenuItem_Click(object sender, EventArgs e) {
			TryReboot(AdbDaemon.RebootMode.Recovery);
		}

		private void bootloaderToolStripMenuItem_Click(object sender, EventArgs e) {
			TryReboot(AdbDaemon.RebootMode.Bootloader);
		}

		private void TryReboot(AdbDaemon.RebootMode mode = AdbDaemon.RebootMode.Normal) {
			string output = "Device is rebooting";
			try {
				_currentDevice.Reboot(mode);
			} catch (Exception x) {
				output = x.Message;
			}
			MessageBox.Show(output, "Reboot - " + mode.ToString());
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

			string oldDirectory = Environment.CurrentDirectory;
			Action<PipelineStateInfo> completed = null;
			completed = state => {
				if (InvokeRequired) {
					Invoke(new Action(() => completed(state)));
					return;
				}

				Environment.CurrentDirectory = oldDirectory;
			  lsvScripts.Enabled = true;

			  // Update the device information and available task list
			  DisplayCurrentDevice();
			};

			// Run the script
			Environment.CurrentDirectory = task.Folder.FullName;
			_psHost.RunAsync(task.Name, task.ScriptFile, completed,
				new KeyValuePair<string, object>("Device", device),
				new KeyValuePair<string, object>("UI", new ExposedUI(_psHost.UI, _psHost.WindowHandle))
			);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {			
			_psHost.Dispose();
		}
	}
}
