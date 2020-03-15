namespace Winter.OSX.Views.SystemStatusbar {
	using AppKit;

	/// <summary>
	/// This is the menu that will be displayed via the SystemStatusbar Tray.
	/// </summary>
	public partial class SnipMenu : NSMenu {
		const string Location = "system-tray-icons";

		private NSMenuItem iTunes = new NSMenuItem("iTunes");
		private NSMenuItem spotify = new NSMenuItem("Spotify");
		private NSMenuItem vlc = new NSMenuItem("VLC");
		private NSMenuItem settings = null;
		private NSMenuItem quit = null;

		public SnipMenu() : base(typeof(SnipMenu).Name) {
			this.iTunes.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "iTunes.png"));
			this.spotify.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "Spotify.png"));
			this.vlc.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "VLC.png"));

			this.AddItem(this.iTunes);
			this.AddItem(this.spotify);
			this.AddItem(this.vlc);

			//this.quit = new NSMenuItem("Quit", new System.EventHandler(
			//	(o, e) => {
			//		//NSApplication.SharedApplication.Terminate(this);
			//	}));

			this.quit = new NSMenuItem("Quit");

			this.AddItem(this.quit);

			this.quit.Action = new ObjCRuntime.Selector("OnQuit");
			this.quit.Target = this;
		}

		public NSMenuItem ITunes {
			get {
				return this.iTunes;
			}
		}

		public NSMenuItem Spotify {
			get {
				return this.spotify;
			}
		}

		public NSMenuItem VLC {
			get {
				return this.vlc;
			}
		}

		public NSMenuItem Quit {
			get {
				return this.quit;
			}
		}

		[Foundation.Export("OnQuit")]
		protected virtual void OnQuit() {
			var alert = NSAlert.WithMessage("Snip", "OK", "Cancel", "", "Are you sure you want to quit?");

			var result = alert.RunModal();

			if(result == 1) {
				NSApplication.SharedApplication.Terminate(this);
			}
		}
	}
}
