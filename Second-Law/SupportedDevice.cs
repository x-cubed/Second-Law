using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SecondLaw {
	class SupportedDevice {
		public SupportedDevice(FileInfo deviceXml) {
			var xml = new XmlDocument();
			xml.Load(deviceXml.FullName);

			DeviceName = xml.SelectSingleNode("/device/name/text()").Value;

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

		public string DeviceName { get; private set; }

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
