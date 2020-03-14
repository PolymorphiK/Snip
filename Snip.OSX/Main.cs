using AppKit;

namespace Snip.OSX {
	static class MainClass {
		static void Main(string[] args) {
			NSApplication.Init();
			NSApplication.Main(args);
		}
	}
}
