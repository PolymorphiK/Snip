namespace Snip.OSX {
	using AppKit;
	using Foundation;

	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {
		NSStatusItem item;

		public AppDelegate() { }

		public override void DidFinishLaunching(NSNotification notification) {
			// Insert code here to initialize your application
			var statusBar = NSStatusBar.SystemStatusBar;

			this.item = statusBar.CreateStatusItem(NSStatusItemLength.Variable);

			this.item.Button.Image = new NSImage("system-tray-icons/Snip.png");
			item.Action = new ObjCRuntime.Selector("itemClicked");
			item.Target = this;

			var snipMenu = new Views.SystemStatusbar.SnipMenu();

			this.item.Menu = snipMenu;
		}

		public override void WillTerminate(NSNotification notification) {
			// Insert code here to tear down your application
		}

		[Export("itemClicked")]
		private void itemClicked() {
			NSAlert.WithMessage("Hello, World!", "OK", "", "", "").RunModal();
			System.Diagnostics.Debug.WriteLine("Action!");
		}
	}
}
