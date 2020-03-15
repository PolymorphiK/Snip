namespace Winter.OSX.Controllers {
	using Views.SystemStatusbar;
	using MediaPlayers;

	public class SnipController : Foundation.NSObject {
		private SnipMenu snipMenu = null;
		private MediaPlayer current = null;

		public SnipController(SnipMenu snipMenu) {
			this.snipMenu = snipMenu;

			this.snipMenu.ITunes.Action = new ObjCRuntime.Selector(SnipController.Exports.iTunesSelected);
			this.snipMenu.Spotify.Action = new ObjCRuntime.Selector(SnipController.Exports.SpotifySelected);
			this.snipMenu.VLC.Action = new ObjCRuntime.Selector(SnipController.Exports.VLCSelected);

			this.snipMenu.ITunes.Target = this.snipMenu.Spotify.Target = this.snipMenu.VLC.Target = this;
		}

		[Foundation.Export(Exports.iTunesSelected)]
		void OniTunesSelected() {
			this.current?.Unload();

			bool state = this.snipMenu.ITunes.State == AppKit.NSCellStateValue.On;

			if(state) {
				this.snipMenu.ITunes.State = AppKit.NSCellStateValue.Off;

				this.current = null;
			} else {
				this.snipMenu.ITunes.State = AppKit.NSCellStateValue.On;

				this.current = new iTunes();

				this.current.Load();
			}

			this.snipMenu.Spotify.State = this.snipMenu.VLC.State = AppKit.NSCellStateValue.Off;
		}

		[Foundation.Export(Exports.SpotifySelected)]
		void OnSpotifySelected() {
			this.snipMenu.Spotify.State = AppKit.NSCellStateValue.On;

			this.snipMenu.ITunes.State = this.snipMenu.VLC.State = AppKit.NSCellStateValue.Off;
		}

		[Foundation.Export(Exports.VLCSelected)]
		void OnVLCSelected() {
			this.snipMenu.VLC.State = AppKit.NSCellStateValue.On;

			this.snipMenu.Spotify.State = this.snipMenu.ITunes.State = AppKit.NSCellStateValue.Off;
		}

		static class Exports : System.Object {
			public const string iTunesSelected = "OniTunesSelected";
			public const string SpotifySelected = "OnSpotifySelected";
			public const string VLCSelected = "OnVLCSelected";
		}
	}
}
