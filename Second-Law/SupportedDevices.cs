using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SecondLaw {
	class SupportedDevices : List<SupportedDevice> {
		public SupportedDevices() {
			var devicesFolder = new DirectoryInfo("..\\..\\Devices");
			foreach (var deviceFolder in devicesFolder.EnumerateDirectories()) {
				var deviceXml = new FileInfo(Path.Combine(deviceFolder.FullName, "device.xml"));
				var device = new SupportedDevice(deviceXml);
				Debug.Print("SupportedDevices(): {0}\r\n{1}", deviceXml.FullName, device);
				Add(device);
			}
		}

		public SupportedDevice GetDevice(ushort vendorId, ushort productId, ushort revision, ushort interfaceId) {
			Func<SupportedDevice, bool> vendorIdMatches = supportedDevice => supportedDevice.VendorId == vendorId;
			Func<SupportedDevice, bool> productIdMatches = supportedDevice => supportedDevice.ProductId == productId;
			Func<SupportedDevice, bool> revisionMatches = supportedDevice => !supportedDevice.Revision.HasValue || supportedDevice.Revision == revision;
			Func<SupportedDevice, bool> interfaceIdMatches = supportedDevice => !supportedDevice.InterfaceId.HasValue || supportedDevice.InterfaceId == interfaceId;
			return this.FirstOrDefault(supportedDevice =>
			                           vendorIdMatches(supportedDevice) && productIdMatches(supportedDevice) &&
			                           revisionMatches(supportedDevice) && interfaceIdMatches(supportedDevice));
		}
	}
}
