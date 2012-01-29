using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SecondLaw {
	public class Hardware {
		private const int WM_DEVICECHANGE = 0x219;
		private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		[Flags]
		public enum DiGetClassFlags : uint {
			Default = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE
			Present = 0x00000002,
			AllClasses = 0x00000004,
			Profile = 0x00000008,
			DeviceInterface = 0x00000010,
		}

		public enum DeviceEvent {
			DeviceNodesChanged = 0x0007,

			QueryConfigChanged = 0x0017,
			ConfigChanged = 0x0018,
			ConfigChangeCancelled = 0x0019,

			NoDiskSpace = 0x0047,
			LowDiskSpace = 0x0048,

			DeviceArrival = 0x8000,
			DeviceQueryRemove = 0x8001,
			DeviceQueryRemoveFailed = 0x8002,
			DeviceRemovePending = 0x8003,
			DeviceRemoveComplete = 0x8004,
			DeviceTypeSpecific = 0x8005,
			CustomEvent = 0x8006,

			UserDefined = 0xFFFF,
		}

		public enum DeviceType {
			OEM = 0x00000000,
			DeviceNode = 0x00000001,
			Volume = 0x00000002,
			Port = 0x00000003,
			Network = 0x00000004,
			DeviceInterface = 0x00000005,
			Handle = 0x00000006
		}

		[Flags]
		public enum VolumeFlags {
			Media = 1,
			Network = 2,
		}

		private const int DEVICE_NOTIFY_WINDOW_HANDLE = 0;
		private const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 4;

		[StructLayout(LayoutKind.Sequential)]
		internal class DEV_BROADCAST_HDR {
			internal Int32 dbch_size;
			internal Int32 dbch_devicetype;
			internal Int32 dbch_reserved;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal class DEV_BROADCAST_DEVICEINTERFACE {
			internal Int32 dbcc_size;
			internal Int32 dbcc_devicetype;
			internal Int32 dbcc_reserved;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 16)]
			internal Byte[] dbcc_classguid;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
			internal Char[] dbcc_name;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal class DEV_BROADCAST_VOLUME {
			internal Int32 dbcc_size;
			internal Int32 dbcc_devicetype;
			internal Int32 dbcc_reserved;
			internal Int32 dbcc_unitmask;
			internal Int32 dbcc_flags;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SP_DEVINFO_DATA {
			public uint cbSize;
			public Guid classGuid;
			public uint devInst;
			public IntPtr Reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SP_DEVICE_INTERFACE_DATA {
			public uint cbSize;
			public Guid interfaceClassGuid;
			public uint flags;
			public IntPtr reserved;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SP_DEVICE_INTERFACE_DETAIL_DATA {
			public UInt32 cbSize;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string devicePath;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr RegisterDeviceNotification(
			IntPtr hRecipient,
			IntPtr notificationFilter,
			Int32 flags
		);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetupDiGetClassDevs(
			IntPtr classId,
			[MarshalAs(UnmanagedType.LPTStr)] string enumerator,
			IntPtr hwndParent,
			DiGetClassFlags flags
		);

		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern bool SetupDiDestroyDeviceInfoList(
				 IntPtr deviceInfoSet
		);

		[DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern Boolean SetupDiGetDeviceInterfaceDetail(
			 IntPtr hDevInfo,
			 ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
			 ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
			 Int32 deviceInterfaceDetailDataSize,
			 out UInt32 requiredSize,
			 ref SP_DEVINFO_DATA deviceInfoData
		);

		[DllImport("setupapi.dll")]
		static extern int CM_Get_Parent(
			 out UInt32 pdnDevInst,
			 UInt32 dnDevInst,
			 int ulFlags
		);

		[DllImport("setupapi.dll", CharSet = CharSet.Auto)]
		static extern int CM_Get_Device_ID(
			 UInt32 dnDevInst,
			 IntPtr buffer,
			 int bufferLen,
			 int ulFlags
		);


		[DllImport("setupapi.dll", SetLastError = true)]
		static extern bool SetupDiEnumDeviceInfo(IntPtr deviceInfoSet, uint memberIndex, ref SP_DEVINFO_DATA deviceInfoData);

		public event EventHandler<DeviceInterfaceChangedArgs> DeviceInterfaceChanged;
		public event EventHandler<VolumeChangedArgs> VolumeChanged;

		private IntPtr _notificationHandle;

		public IEnumerable<UsbDevice> EnumerateUsbDevices() {
			Debug.Print("EnumerateUsbDevices()");
			const DiGetClassFlags flags = DiGetClassFlags.Present | DiGetClassFlags.AllClasses;
			IntPtr h = SetupDiGetClassDevs(IntPtr.Zero, "USB", IntPtr.Zero, flags);
			if (h != INVALID_HANDLE_VALUE) {
				bool success = true;
				uint i = 0;
				while (success) {
					// build a DevInfo Data structure
					SP_DEVINFO_DATA da = new SP_DEVINFO_DATA();
					da.cbSize = (uint)Marshal.SizeOf(da);

					// start the enumeration 
					success = SetupDiEnumDeviceInfo(h, i, ref da);
					i++;
					if (success) {
						yield return new UsbDevice(h, ref da);
					}
				}
			}
			SetupDiDestroyDeviceInfoList(h);
		}



		public void RegisterNotifications(Form form) {
			Debug.Print("Hardware.RegisterNotifications({0})", form.Text);
			RegisterNotifications(form.Handle);
		}

		private void RegisterNotifications(IntPtr hWnd) {
			var deviceInterface = new DEV_BROADCAST_DEVICEINTERFACE();
			deviceInterface.dbcc_size = Marshal.SizeOf(deviceInterface);
			deviceInterface.dbcc_devicetype = (int)DeviceType.DeviceInterface;
			deviceInterface.dbcc_reserved = 0;
			deviceInterface.dbcc_classguid = new byte[16];

			IntPtr pointer = Marshal.AllocHGlobal(deviceInterface.dbcc_size);
			Marshal.StructureToPtr(deviceInterface, pointer, true);
			const int flags = DEVICE_NOTIFY_WINDOW_HANDLE | DEVICE_NOTIFY_ALL_INTERFACE_CLASSES;
			_notificationHandle = RegisterDeviceNotification(hWnd, pointer, flags);
			Marshal.FreeHGlobal(pointer);
		}

		public void WndProc(ref Message message) {
			switch (message.Msg) {
				case WM_DEVICECHANGE:
					OnDeviceChange(message);
					break;
			}
		}

		private void OnDeviceChange(Message message) {
			var eventType = (DeviceEvent)message.WParam.ToInt32();
			if (message.LParam != IntPtr.Zero) {
				var broadcastHeader = new DEV_BROADCAST_HDR();
				Marshal.PtrToStructure(message.LParam, broadcastHeader);

				var deviceType = (DeviceType)broadcastHeader.dbch_devicetype;
				switch (deviceType) {
					case DeviceType.DeviceInterface:
						OnDeviceInterfaceChanged(eventType, message, broadcastHeader);
						break;

					case DeviceType.Volume:
						OnVolumeChanged(eventType, message);
						break;

					default:
						switch (eventType) {
							case DeviceEvent.DeviceArrival:
								Debug.Print("Hardware.OnDeviceChange(): Device arrived: {0}", deviceType);
								break;
							case DeviceEvent.DeviceRemoveComplete:
								Debug.Print("Hardware.OnDeviceChange(): Device removed: {0}", deviceType);
								break;
							default:
								Debug.Print("Hardware.OnDeviceChange(): {0}", eventType);
								break;
						}
						break;
				}
			}
		}

		private void OnDeviceInterfaceChanged(DeviceEvent eventType, Message message, DEV_BROADCAST_HDR broadcastHeader) {
			int stringSize = Convert.ToInt32((broadcastHeader.dbch_size - 32) / 2);

			var deviceInterface = new DEV_BROADCAST_DEVICEINTERFACE();
			Array.Resize(ref deviceInterface.dbcc_name, stringSize);
			Marshal.PtrToStructure(message.LParam, deviceInterface);
			var classId = new Guid(deviceInterface.dbcc_classguid);
			var deviceName = new string(deviceInterface.dbcc_name, 0, stringSize);
			Debug.Print("Hardware.OnDeviceInterfaceChanged(): {0} {2} {1}", eventType, deviceName, classId);

			var eh = DeviceInterfaceChanged;
			if (eh != null) {
				eh(this, new DeviceInterfaceChangedArgs(eventType, classId, deviceName));
			}
		}

		private void OnVolumeChanged(DeviceEvent eventType, Message message) {
			var volume = new DEV_BROADCAST_VOLUME();
			Marshal.PtrToStructure(message.LParam, volume);
			var flags = (VolumeFlags)volume.dbcc_flags;

			char driveLetter = 'A';
			int bitMask = 1;
			while (driveLetter <= 'Z') {
				if ((volume.dbcc_unitmask & bitMask) != 0) {
					OnVolumeChanged(eventType, driveLetter, flags);
				}
				bitMask = bitMask << 1;
				driveLetter++;
			}
		}

		private void OnVolumeChanged(DeviceEvent eventType, char driveLetter, VolumeFlags flags) {
			Debug.Print("Hardware.OnVolumeChanged(): {0} {1}: {2}", eventType, driveLetter, flags);
			var eh = VolumeChanged;
			if (eh != null) {
				var changeType = (eventType == DeviceEvent.DeviceArrival) ?
					VolumeChangedArgs.ChangeType.Added : VolumeChangedArgs.ChangeType.Removed;
				var args = new VolumeChangedArgs(driveLetter, changeType);
				eh(this, args);
			}
		}
	}

	public class DeviceInterfaceChangedArgs : EventArgs {
		public DeviceInterfaceChangedArgs(Hardware.DeviceEvent eventType, Guid classId, string deviceName) {
			EventType = eventType;
			ClassId = classId;
			DeviceName = deviceName;
		}

		public Hardware.DeviceEvent EventType { get; private set; }
		public Guid ClassId { get; private set; }
		public string DeviceName { get; private set; }
	}

	public class VolumeChangedArgs : EventArgs {
		public enum ChangeType {
			Added,
			Removed
		}

		public VolumeChangedArgs(char driveLetter, ChangeType changeType) {
			DriveLetter = driveLetter;
			Type = changeType;
		}

		public char DriveLetter { get; private set; }
		public ChangeType Type { get; private set; }
	}
}
