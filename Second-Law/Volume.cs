
namespace SecondLaw {
	public class Volume {
		public Volume(string path) {
			Path = path;
		}

		public string Path { get; private set; }

		public decimal PercentUsed {
			get {
				if (SizeTotalBytes == 0) {
					return 0;
				}
				return (decimal)100 * SizeUsedBytes / SizeTotalBytes;
			}
		}

		public ulong SizeTotalBytes { get; set; }
		public ulong SizeUsedBytes { get; set; }

		public ulong BlockSizeBytes { get; set; }
	}
}
