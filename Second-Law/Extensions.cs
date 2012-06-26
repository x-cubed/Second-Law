using System.Linq;
using System.Reflection;

namespace SecondLaw {
	static class Extensions {
		public static TAttribute GetCustomAttribute<TAttribute>(this Assembly assembly, bool inherit = false) {
			return assembly.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().FirstOrDefault();
		}

		public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, bool inherit = false) {
			return member.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>().FirstOrDefault();
		}
	}
}
