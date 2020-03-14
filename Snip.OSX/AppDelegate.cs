using AppKit;
using Foundation;

namespace Snip.OSX {
	using System.Runtime.CompilerServices;
	[Register("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {
		NSStatusItem item;
		bool iTunes;

		public AppDelegate() {

		}

		public override void DidFinishLaunching(NSNotification notification) {
			// Insert code here to initialize your application
			var statusBar = NSStatusBar.SystemStatusBar;

			this.item = statusBar.CreateStatusItem(NSStatusItemLength.Variable);

			this.item.Button.Image = new NSImage("Snip16.png");
			this.item.ToolTip = "CMD + Click to quit";
			item.Action = new ObjCRuntime.Selector("itemClicked");
			item.Target = this;

			NSMenu menu = new NSMenu("Title");

			menu.AddItem(new NSMenuItem("Item 1", new System.EventHandler((o, e) => { this.itemClicked(); })));

			this.item.Menu = menu;
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
