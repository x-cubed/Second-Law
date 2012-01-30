using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml;

namespace SecondLaw {
	class SupportedDevice {
		private readonly DirectoryInfo _deviceFolder;

		public SupportedDevice(DirectoryInfo deviceFolder) {
			_deviceFolder = deviceFolder;
			ParseDeviceXml(Path.Combine(deviceFolder.FullName, "device.xml"));
		}

		private void ParseDeviceXml(string deviceXmlPath) {
			var xml = new XmlDocument();
			xml.Load(deviceXmlPath);

			DeviceName = GetOptionalXmlText(xml, "/device/name/text()");
			VendorName = GetOptionalXmlText(xml, "/device/vendor/text()");
			ManufacturerName = GetOptionalXmlText(xml, "/device/manufacturer/text()");

			ProductPage = GetOptionalXmlUri(xml, "/device/links/product-page/text()");
			SupportPage = GetOptionalXmlUri(xml, "/device/links/support-page/text()");

			IEnumerable<XmlNode> usbDevices = (xml.SelectNodes("/device/usb-devices/*") ?? (IEnumerable)new XmlNode[0]).Cast<XmlNode>();
			foreach (var usbDevice in usbDevices) {
				string type = usbDevice.Attributes["type"].Value;
				if (type == "debug-bridge") {
					VendorId = ParseAttributeAsUShort(usbDevice.Attributes["vendorId"], 0);
					ProductId = ParseAttributeAsUShort(usbDevice.Attributes["productId"], 0);
					Revision = ParseAttributeAsNullableUShort(usbDevice.Attributes["revision"]);
					InterfaceId = ParseAttributeAsNullableUShort(usbDevice.Attributes["mi"]);
				}
			}
		}

		private static Uri GetOptionalXmlUri(XmlDocument xml, string xPath) {
			string text = GetOptionalXmlText(xml, xPath);
			Uri uri;
			return (Uri.TryCreate(text, UriKind.Absolute, out uri)) ? uri : null;
		}

		private static string GetOptionalXmlText(XmlDocument xml, string xPath) {
			XmlNode node = xml.SelectSingleNode(xPath);
			return (node == null) ? null : node.Value;
		}

		public string ProductName {
			get { return (VendorName ?? ManufacturerName) + " " + DeviceName; }
		}

		public Image DeviceImage {
			get {
				var imageFile = _deviceFolder.GetFiles("device.jpg").FirstOrDefault();
				return (imageFile == null) ? null : Image.FromFile(imageFile.FullName);
			}
		}

		public string DeviceName { get; private set; }
		public Uri ProductPage { get; private set; }

		public string VendorName { get; private set; }
		public Uri SupportPage { get; private set; }

		public string ManufacturerName { get; private set; }
		public Uri ManufacturerPage { get; private set; }

		public ushort VendorId { get; private set; }
		public ushort ProductId { get; private set; }
		public ushort? Revision { get; private set; }
		public ushort? InterfaceId { get; private set; }

		public override string ToString() {
			var result = new StringBuilder();
			result.AppendFormat("\"{0}\" VID=0x{1:X4} PID=0x{2:X4} REV=0x{3:}{4}",
				DeviceName, VendorId, ProductId,
				(!Revision.HasValue) ? "????" : Revision.Value.ToString("X4"),
				(!InterfaceId.HasValue) ? "" : " MI=0x" + InterfaceId.Value.ToString("X2"));
			return result.ToString();
		}

		private static ushort ParseAttributeAsUShort(XmlAttribute attribute, ushort defaultValue) {
			ushort? value = ParseAttributeAsNullableUShort(attribute);
			return (!value.HasValue) ? defaultValue : value.Value;
		}

		private static ushort? ParseAttributeAsNullableUShort(XmlAttribute attribute) {
			if (attribute == null) {
				return null;
			}

			string value = attribute.Value;
			if (value.StartsWith("0x")) {
				return ushort.Parse(value.Substring(2), NumberStyles.HexNumber);
			}
			return ushort.Parse(value);
		}
	}
}
