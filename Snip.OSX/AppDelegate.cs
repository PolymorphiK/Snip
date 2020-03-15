namespace Winter.OSX {
	using AppKit;
	using Foundation;
	using Controllers;
	using Views.SystemStatusbar;

	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {
		NSStatusItem item;

		public AppDelegate() { }

		public override void DidFinishLaunching(NSNotification notification) {
			// Insert code here to initialize your application
			var statusBar = NSStatusBar.SystemStatusBar;

			this.item = statusBar.CreateStatusItem(NSStatusItemLength.Variable);

			this.item.Button.Image = new NSImage("system-tray-icons/Snip.png");

			var snipMenu = new SnipMenu();

			this.item.Menu = snipMenu;

			new SnipController(snipMenu);
		}

		public override void WillTerminate(NSNotification notification) {
			// Insert code here to tear down your application
		}
	}
}
