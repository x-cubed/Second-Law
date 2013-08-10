using System;

namespace SecondLaw {
	static class Conversions {
		public static ulong ParseSize(string size) {
			if (string.IsNullOrEmpty(size)) {
				throw new ArgumentOutOfRangeException("size");
			}

			decimal multiplier;
			char last = size[size.Length - 1];
			string number = size.Substring(0, size.Length - 1);
			switch (last) {
				case 'K':
					multiplier = 1024;
					break;
				case 'M':
					multiplier = 1024 * 1024;
					break;
				case 'G':
					multiplier = 1024 * 1024 * 1024;
					break;
				default:
					multiplier = 1;
					number = size;
					break;
			}
			return (ulong)(decimal.Parse(number) * multiplier);
		}

		public static string ToSize(ulong sizeInBytes) {
			decimal number = sizeInBytes;
			string suffix = "";
			char[] suffixes = {'K', 'M', 'G', 'T'};
			foreach (var suffixChar in suffixes) {
				if ((number/1024) > 1) {
					number = number/1024;
					suffix = suffixChar.ToString();
				}
			}
			return string.Format("{0:0.0}{1}", number, suffix);
		}
	}
}
