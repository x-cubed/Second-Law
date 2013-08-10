using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace SecondLaw.PowerShell {
	class ExposedUI : IWin32Window {
		private const int FILE_MONITOR_SOURCE = 0;
		private const int FILE_MONITOR_ACTIVITY = 100;

		private static readonly TimeSpan CHECK_INTERVAL = TimeSpan.FromMilliseconds(500);

		private readonly PSHostUserInterface _ui;

		private Timer _sizeCheckTimer;
		private ulong _finalSize;
		private string _activity;
		private string _statusDescription;
		private int _startPercent;
		private int _endPercent;
		private int _lastPercent;

		public ExposedUI(PSHostUserInterface ui, IntPtr handle) {
			_ui = ui;
			Handle = handle;
		}

		private void WriteFileSizeAsProgress(FileInfo file) {
			decimal fileSize = file.Length;
			if (fileSize > _finalSize) {
				fileSize = _finalSize;
			}

			decimal scale = _endPercent - _startPercent;
			if (scale == 0) {
				scale = 1;
			}
			decimal percentComplete = 100 * fileSize / _finalSize;
			if (percentComplete != _lastPercent) {
				_lastPercent = (int)percentComplete;
				var progress = new ProgressRecord(FILE_MONITOR_ACTIVITY, _activity, _statusDescription + " (" + percentComplete + "%)") {
					PercentComplete = (int)(_startPercent + (percentComplete / scale))
				};
				_ui.WriteProgress(FILE_MONITOR_SOURCE, progress);
			}
		}

		public void StartUsingFileSizeForProgress(string fileName, ulong finalSize, string activity, string status, int startPercent = 0, int endPercent = 100) {
			_finalSize = finalSize;
			_activity = activity;
			_statusDescription = status;
			_startPercent = startPercent;
			_endPercent = endPercent;
			_lastPercent = -1;

			var file = new FileInfo(fileName);
			_sizeCheckTimer = new Timer(SizeCheck_Callback, file, CHECK_INTERVAL, CHECK_INTERVAL);
		}

		private void SizeCheck_Callback(object state) {
			var file = (FileInfo)state;
			file.Refresh();
			if (file.Exists && file.Length != 0) {
				WriteFileSizeAsProgress(file);
			}
		}

		public void StopUsingFileSizeForProgress() {
			if (_sizeCheckTimer == null) {
				return;
			}

			_sizeCheckTimer.Dispose();
			_sizeCheckTimer = null;
		}

		public IntPtr Handle { get; set; }
	}
}
