namespace Snip.OSX.Views.SystemStatusbar {
	using AppKit;

	/// <summary>
	/// This is the menu that will be displayed via the SystemStatusbar Tray.
	/// </summary>
	public partial class SnipMenu : NSMenu {
		const string Location = "system-tray-icons";

		private NSMenuItem iTunes = new NSMenuItem("iTunes");
		private NSMenuItem spotify = new NSMenuItem("Spotify");
		private NSMenuItem vlc = new NSMenuItem("VLC");
		private NSMenuItem quit = null;

		public SnipMenu() : base(typeof(SnipMenu).Name) {
			this.iTunes.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "iTunes.png"));
			this.spotify.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "Spotify.png"));
			this.vlc.Image = new NSImage(string.Format("{0}/{1}", SnipMenu.Location, "VLC.png"));

			this.AddItem(this.iTunes);
			this.AddItem(this.spotify);
			this.AddItem(this.vlc);

			this.quit = new NSMenuItem("Quit", new System.EventHandler(
				(o, e) => {
					NSApplication.SharedApplication.Terminate(this);
				}));

			this.AddItem(this.quit);
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
	}
}
