using System.Collections.Generic;
using System.Diagnostics;

namespace SecondLaw.Android {
	public class BuildProperties : Dictionary<string, string> {
		public const string PATH = "/system/build.prop";

		private const string BUILD_VERSION_SDK = "ro.build.version.sdk";
		private const string PRODUCT_MANUFACTURER = "ro.product.manufacturer";
		private const string PRODUCT_MODEL = "ro.product.model";

		private static readonly Dictionary<byte, string> SdkVersions = new Dictionary<byte, string> {
			{ 1, "1.0"}, // Base
			{ 2, "1.1"}, // Base
			{ 3, "1.5"}, // Cupcake
			{ 4, "1.6"}, // Donut
			{ 5, "2.0"},  // Eclair
			{ 6, "2.0.1"}, // Eclair
			{ 7, "2.1"}, // Eclair MR1
			{ 8, "2.2"}, // Froyo
			{ 9, "2.3"}, // Gingerbread
			{ 10, "2.3"}, // Gingerbread MR1
			{ 11, "3.0"}, // Honeycomb
			{ 12, "3.1"}, // Honeycomb
			{ 13, "3.2"}, // Honeycomb
			{ 14, "4.0"}, // Ice Cream Sandwich
			{ 15, "4.0.3"}, // Ice Cream Sandwich MR1
			{ 16, "4.1"}, // Jellybean
		};

		public BuildProperties(string buildProp) {
			var lines = buildProp.Split('\r', '\n');
			foreach (string line in lines) {
				string cleanLine = line.Trim();
				if ((cleanLine != "") && (cleanLine[0] != '#')) {
					string[] kvp = cleanLine.Split(new[] { '=' }, 2);
					if (kvp.Length == 1) {
						Debug.WriteLine("Ignoring invalid build property: \"{0}\"", kvp[0]);
					} else {
						this[kvp[0]] = kvp[1];
					}
				}
			}
		}

		public string ProductManufacturer {
			get {
				string manufacturer;
				return TryGetValue(PRODUCT_MANUFACTURER, out manufacturer) ? manufacturer : null;
			}
		}

		public string ProductModel {
			get {
				string model;
				return TryGetValue(PRODUCT_MODEL, out model) ? model : null;
			}
		}

		public byte? SdkVersion {
			get {
				string buildVersion;
				TryGetValue(BUILD_VERSION_SDK, out buildVersion);

				byte versionNumber;
				if (!byte.TryParse(buildVersion, out versionNumber)) {
					return null;
				}
				return versionNumber;
			}
		}

		public string SystemVersion {
			get {
				byte? versionNumber = SdkVersion;
				if (versionNumber == null) {
					return null;
				}

				string version;
				if (!SdkVersions.TryGetValue(versionNumber.Value, out version)) {
					return null;
				}
				return version;
			}
		}
	}
}
