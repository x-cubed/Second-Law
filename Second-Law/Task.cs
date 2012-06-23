using System.Drawing;
using System.IO;
using System.Linq;

namespace SecondLaw {
	class Task {
		public Task(DirectoryInfo folder) {
			Folder = folder;
		}

		public DirectoryInfo Folder { get; private set; }

		public string Name {
			get { return Folder.Name; }
		}

		public Image Icon {
			get {
				var iconFile = Folder.GetFiles("icon.png").FirstOrDefault();
				return (iconFile == null) ? null : Image.FromFile(iconFile.FullName);
			}
		}

		public string Run() {
			var taskFile = Folder.GetFiles("task.ps1").FirstOrDefault();
			string result = PowerShell.Run(taskFile);
			return result;
		}

		public static Task Load(DirectoryInfo folder) {
			var task = new Task(folder);
			return task;
		}
	}
}
