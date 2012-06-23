using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SecondLaw {
	class Task {
		private const string TASK_ICON = "icon.png";
		private const string TASK_SCRIPT = "task.ps1";

		private readonly FileInfo _icon;

		public Task(DirectoryInfo folder) {
			Folder = folder;
			_icon = Folder.GetFiles(TASK_ICON).FirstOrDefault();
			
			ScriptFile = Folder.GetFiles(TASK_SCRIPT).FirstOrDefault();
			if (ScriptFile == null) {
				string path = Path.Combine(Folder.FullName, TASK_SCRIPT);
				throw new FileNotFoundException("Unable to locate task script", path);
			}
		}

		public DirectoryInfo Folder { get; private set; }

		public string Name {
			get { return Folder.Name; }
		}

		public Image Icon {
			get { return (_icon == null) ? null : Image.FromFile(_icon.FullName); }
		}

		public FileInfo ScriptFile { get; private set; }

		public string Run(DeviceInstance device) {
			return PowerShell.Run(ScriptFile, new KeyValuePair<string, object>("Device", device));
		}

		public static Task Load(DirectoryInfo folder) {
			try {
				var task = new Task(folder);
				return task;
			} catch (Exception x) {
				Debug.Print(x.ToString());
				return null;
			}
		}
	}
}
