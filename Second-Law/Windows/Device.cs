using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SecondLaw.Windows {
	public class Device {
		public enum RegistryDataType : uint {
			REG_SZ = 1,
			REG_EXPAND_SZ = 2,
			REG_BINARY = 3,
			REG_DWORD = 4,
			REG_MULTI_SZ = 7
		}

		public enum InstallStates {
			Installed = 0,
			NeedsReinstall = 1,
			FailedInstall = 2,
			FinishInstall = 3
		}

		[Flags]
		public enum ConfigFlags {
			Disabled = 0x00000001, // Set if disabled
			Removed = 0x00000002, // Set if a present hardware enum device deleted
			ManualInstall = 0x00000004, // Set if the devnode was manually installed
			IgnoreBootConfig = 0x00000008, // Set if skip the boot config
			NetBoot = 0x00000010, // Load this devnode when in net boot
			Reinstall = 0x00000020, // Redo install
			FailedInstall = 0x00000040, // Failed the install
			CannotStopAChild = 0x00000080, // Can't stop/remove a single child
			OkToRemoveROM = 0x00000100, // Can remove even if rom.
			DoNotRemoveOnExit = 0x00000200, // Don't remove at exit.
			FinishInstall = 0x00000400, // Complete install for devnode running 'raw'
			ForcedConfig = 0x00000800 // This devnode requires a forced config
		}

		/// <summary>
		/// Flags for SetupDiGetDeviceRegistryProperty().
		/// </summary>
		private enum RegistryProperty : uint {
			DeviceDescription = 0x00000000, // DeviceDesc (R/W)
			HardwareId = 0x00000001, // HardwareID (R/W)
			CompatibleIds = 0x00000002, // CompatibleIDs (R/W)
			Unused0 = 0x00000003, // unused
			Service = 0x00000004, // Service (R/W)
			Unused1 = 0x00000005, // unused
			Unused2 = 0x00000006, // unused
			Class = 0x00000007, // Class (R--tied to ClassGUID)
			ClassGuid = 0x00000008, // ClassGUID (R/W)
			Driver = 0x00000009, // Driver (R/W)
			ConfigFlags = 0x0000000A, // ConfigFlags (R/W)
			Manufacturer = 0x0000000B, // Mfg (R/W)
			FriendlyName = 0x0000000C, // FriendlyName (R/W)
			LocationInformation = 0x0000000D, // LocationInformation (R/W)
			PhysicalDeviceObjectName = 0x0000000E, // PhysicalDeviceObjectName (R)
			Capabilities = 0x0000000F, // Capabilities (R)
			UiNumber = 0x00000010, // UiNumber (R)
			UpperFilters = 0x00000011, // UpperFilters (R/W)
			LowerFilters = 0x00000012, // LowerFilters (R/W)
			BusTypeGuid = 0x00000013, // BusTypeGUID (R)
			LegacyBysType = 0x00000014, // LegacyBusType (R)
			BusNumber = 0x00000015, // BusNumber (R)
			EnumeratorName = 0x00000016, // Enumerator Name (R)
			Security = 0x00000017, // Security (R/W, binary form)
			SecuritySDS = 0x00000018, // Security (W, SDS form)
			DeviceType = 0x00000019, // Device Type (R/W)
			ExclusiveAccess = 0x0000001A, // Device is exclusive-access (R/W)
			Characteristics = 0x0000001B, // Device Characteristics (R/W)
			Address = 0x0000001C, // Device Address (R)
			UiNumberDescFormat = 0X0000001D, // UiNumberDescFormat (R/W)
			DevicePowerData = 0x0000001E, // Device Power Data (R)
			RemovalPolicy = 0x0000001F, // Removal Policy (R)
			RemovcalPolicyHardwareDefault = 0x00000020, // Hardware Removal Policy (R)
			RemovalPolicyOverride = 0x00000021, // Removal Policy Override (RW)
			InstallState = 0x00000022, // Device Install State (R)
			LocationPaths = 0x00000023, // Device Location Paths (R)
			BaseContainerId = 0x00000024  // Base ContainerID (R)
		}


		/// <summary>
		/// The SetupDiGetDeviceRegistryProperty function retrieves the specified device property.
		/// This handle is typically returned by the SetupDiGetClassDevs or SetupDiGetClassDevsEx function.
		/// </summary>
		/// <param Name="DeviceInfoSet">Handle to the device information set that contains the interface and its underlying device.</param>
		/// <param Name="DeviceInfoData">Pointer to an SP_DEVINFO_DATA structure that defines the device instance.</param>
		/// <param Name="Property">Device property to be retrieved. SEE MSDN</param>
		/// <param Name="PropertyRegDataType">Pointer to a variable that receives the registry data Type. This parameter can be NULL.</param>
		/// <param Name="PropertyBuffer">Pointer to a buffer that receives the requested device property.</param>
		/// <param Name="PropertyBufferSize">Size of the buffer, in bytes.</param>
		/// <param Name="RequiredSize">Pointer to a variable that receives the required buffer size, in bytes. This parameter can be NULL.</param>
		/// <returns>If the function succeeds, the return value is nonzero.</returns>
		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiGetDeviceRegistryProperty(
			IntPtr deviceInfoSet,
			ref Hardware.SP_DEVINFO_DATA deviceInfoData,
			Device.RegistryProperty property,
			out RegistryDataType propertyRegDataType,
			IntPtr propertyBuffer,
			uint propertyBufferSize,
			out UInt32 requiredSize
		);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool SetupDiLoadDeviceIcon(
			IntPtr deviceInfoSet,
			ref Hardware.SP_DEVINFO_DATA deviceInfoData,
			uint cXIcon,
			uint cYIcon,
			uint flags,
			out IntPtr hIcon
		);

		protected Device(IntPtr enumeration, ref Hardware.SP_DEVINFO_DATA device) {
			HardwareIds = (string[])GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.HardwareId);
			Manufacturer = (string)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.Manufacturer);
			DeviceDescription = (string)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.DeviceDescription);
			FriendlyName = (string)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.FriendlyName);
			LocationInformation = (string)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.LocationInformation);
			PhysicalDeviceObjectName = (string)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.PhysicalDeviceObjectName);

			Configuration = (ConfigFlags)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.ConfigFlags);
			InstallState = (InstallStates)GetDeviceRegistryProperty(enumeration, ref device, RegistryProperty.InstallState);

			LargeIcon = LoadDeviceIcon(enumeration, ref device, 48);
			SmallIcon = LoadDeviceIcon(enumeration, ref device, 16);
		}

		private Bitmap LoadDeviceIcon(IntPtr enumeration, ref Hardware.SP_DEVINFO_DATA device, uint size) {
			IntPtr hIcon;
			return (SetupDiLoadDeviceIcon(enumeration, ref device, size, size, 0, out hIcon)) ? Bitmap.FromHicon(hIcon) : null;
		}

		public ConfigFlags Configuration { get; private set; }
		public InstallStates InstallState { get; private set; }

		public string[] HardwareIds { get; private set; }
		public string Manufacturer { get; private set; }
		public string DeviceDescription { get; private set; }
		public string FriendlyName { get; private set; }
		public string LocationInformation { get; private set; }
		public string PhysicalDeviceObjectName { get; private set; }

		public Bitmap LargeIcon { get; private set; }
		public Bitmap SmallIcon { get; private set; }

		public override string ToString() {
			var result = new StringBuilder();
			result.AppendFormat("Manufacturer: {0}", Manufacturer);
			result.AppendFormat("Description:  {0}", DeviceDescription);
			result.AppendFormat("Hardware ID:  {0}", string.Join("\r\n              ", HardwareIds));
			return result.ToString();
		}

		private static object GetDeviceRegistryProperty(IntPtr h, ref Hardware.SP_DEVINFO_DATA devInfo, RegistryProperty property) {
			RegistryDataType dataType;
			uint bufferLength;
			SetupDiGetDeviceRegistryProperty(h, ref devInfo, property, out dataType, IntPtr.Zero, 0, out bufferLength);

			object result = null;
			IntPtr buffer = Marshal.AllocHGlobal((int)bufferLength);
			if (SetupDiGetDeviceRegistryProperty(h, ref devInfo, property, out dataType, buffer, bufferLength, out bufferLength)) {
				switch (dataType) {
					case RegistryDataType.REG_SZ:
						result = Marshal.PtrToStringAuto(buffer);
						break;

					case RegistryDataType.REG_MULTI_SZ:
						var bytes = PtrToByteArray(buffer, bufferLength);
						var text = Encoding.Unicode.GetString(bytes);
						result = text.TrimEnd('\0').Split('\0');
						break;

					case RegistryDataType.REG_BINARY:
						result = PtrToByteArray(buffer, bufferLength);
						break;

					case RegistryDataType.REG_DWORD:
						result = Marshal.ReadInt32(buffer);
						break;

					default:
						throw new Exception(string.Format("Unsupported data type: {0}", dataType));
				}
			}
			Marshal.FreeHGlobal(buffer);
			return result;
		}

		private static byte[] PtrToByteArray(IntPtr pointer, uint length) {
			var bytes = new byte[length];
			Marshal.Copy(pointer, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}
