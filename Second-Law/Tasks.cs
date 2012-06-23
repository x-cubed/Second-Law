using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SecondLaw {
	class Tasks : ICollection<Task> {
		private readonly List<Task> _list;

		public Tasks() {
			_list = new List<Task>();
		}

		public Tasks(IEnumerable<Task> tasks) {
			_list = new List<Task>(tasks);
		}

		public IEnumerator<Task> GetEnumerator() {
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void Add(Task task) {
			_list.Add(task);
		}

		public void Clear() {
			_list.Clear();
		}

		public bool Contains(Task task) {
			return _list.Contains(task);
		}

		public void CopyTo(Task[] array, int arrayIndex) {
			_list.CopyTo(array, arrayIndex);
		}

		public bool Remove(Task task) {
			return _list.Remove(task);
		}

		public int Count {
			get { return _list.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public static Tasks LoadCompatibleTasksFor(DeviceInstance device) {
			var tasks = new Tasks();
			var folder = new DirectoryInfo("..\\..\\Tasks");
			if (folder.Exists) {
				foreach (var subfolder in folder.GetDirectories()) {
					var task = Task.Load(subfolder);
					if (task != null) {
						tasks.Add(task);
					}
				}
			}
			return tasks;
		}
	}
}
