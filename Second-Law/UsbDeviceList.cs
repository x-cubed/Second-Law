using System.Drawing;
using System.Windows.Forms;

namespace SecondLaw {
	public partial class UsbDeviceList : Form {
		private Hardware _hardware;

		public UsbDeviceList(Hardware hardware) {
			InitializeComponent();

			_hardware = hardware;
			_hardware.DeviceInterfaceChanged += Hardware_DeviceInterfaceChanged;

			RefreshList();
		}

		private void Hardware_DeviceInterfaceChanged(object sender, DeviceInterfaceChangedArgs e) {
			RefreshList();
		}

		private void RefreshList() {
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
				//LoadImage(imageKey, usbDevice.LargeIcon, imlLarge);
				LoadImage(imageKey, usbDevice.SmallIcon, imlSmall);
				deviceItem.ImageKey = imageKey;

				lsvDevices.Items.Add(deviceItem);
			}
		}

		private static void LoadImage(string key, Bitmap image, ImageList imageList) {
			if (!imageList.Images.ContainsKey(key) && (image != null)) {
				imageList.Images.Add(key, image);
			}
		}

		private void UsbDeviceList_FormClosed(object sender, FormClosedEventArgs e) {
			_hardware.DeviceInterfaceChanged -= Hardware_DeviceInterfaceChanged;
			_hardware = null;
		}
	}
}
