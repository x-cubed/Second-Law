using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace SecondLaw.Android {
	/// <summary>
	/// Handles adding new vendor IDs to adb_usb.ini, so that ADB recognises the
	/// vendor's driver as an ADB device.
	/// </summary>
	internal static class AdbUsb {
		private const string ADB_USB_PATH = @".android\adb_usb.ini";
		private const string HEX_SPECIFIER = "0x";

		public static void WriteVendorIds(IEnumerable<ushort> vendorIds) {
			// Create a list of vendor IDs, sorted in ascending order
			var idsToWrite = new List<ushort>(vendorIds);
			idsToWrite.Sort((a, b) => (a <= b) ? -1 : 1);

			// Determine where the INI file is
			var profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var adbUsbIni = new FileInfo(Path.Combine(profilePath, ADB_USB_PATH));

			bool endsOnNewLine = true;
			if (adbUsbIni.Exists) {
				// Determine which vendor IDs we already have
				using (var reader = adbUsbIni.OpenText()) {
					string line = "";
					while (!reader.EndOfStream) {
						// Read the line and ignore comments
						line = reader.ReadLine() ?? "";
						string content = line.Split('#')[0].Trim();
						if (content != "") {

							// Determine if the number is in hex or not (all should be hex)
							var style = NumberStyles.Integer;
							if (content.StartsWith(HEX_SPECIFIER)) {
								content = content.Substring(2);
								style = NumberStyles.AllowHexSpecifier;
							}

							// Parse the value, and remove it from the values we need to write out
							ushort vendorId;
							if (ushort.TryParse(content, style, null, out vendorId)) {
								idsToWrite.Remove(vendorId);
							}
						}
					}
					endsOnNewLine = (line == "");
				}
			}

			// If we have no IDs left, there's no point writing to the file
			if (idsToWrite.Count == 0) {
				Debug.Print("AdbUsb.WriteVendorIds(): adb_usb.ini is up-to-date");
				return;
			}

			// Write the new vendor IDs to the end of the file
			using (var writer = new StreamWriter(adbUsbIni.FullName, true, Encoding.UTF8)) {
				// Start a new line, if necessary
				if (!endsOnNewLine) {
					writer.WriteLine();
				}
				foreach (var vendorId in idsToWrite) {
					writer.WriteLine("{0}{1:X4}", HEX_SPECIFIER, vendorId);
				}
			}
		}
	}
}
