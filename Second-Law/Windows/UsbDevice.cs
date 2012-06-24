using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SecondLaw.Windows {
	public class UsbDevice : Device {
		private static readonly Regex PRODUCT_ID = new Regex("PID_?([0-9A-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex REVISION = new Regex("REV_?([0-9A-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex VENDOR_ID = new Regex("VID_?([0-9A-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex INTERFACE_ID = new Regex("MI_?([0-9A-F]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		public UsbDevice(IntPtr enumeration, ref Hardware.SP_DEVINFO_DATA device)
			: base(enumeration, ref device) {
			ProductId = Find(PRODUCT_ID, 0);
			Revision = Find(REVISION, 0);
			VendorId = Find(VENDOR_ID, 0);
			InterfaceId = Find(INTERFACE_ID, 0xFFFF);
		}

		private ushort Find(Regex regex, ushort defaultValue) {
			var match = regex.Match(HardwareIds[0]);
			if (match.Success) {
				string number = match.Groups[1].Value;
				ushort result;
				return ushort.TryParse(number, NumberStyles.HexNumber, null, out result) ? result : defaultValue;
			}
			return defaultValue;
		}

		public ushort InterfaceId { get; private set; }
		public ushort ProductId { get; private set; }
		public ushort Revision { get; private set; }
		public ushort VendorId { get; private set; }
	}
}
