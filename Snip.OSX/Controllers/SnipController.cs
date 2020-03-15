namespace Winter.OSX.Controllers {
	using System.Diagnostics;
	using Views.SystemStatusbar;

	public class SnipController : Foundation.NSObject {
		private SnipMenu snipMenu;

		public SnipController(SnipMenu snipMenu) {
			this.snipMenu = snipMenu;

			this.snipMenu.ITunes.Action = new ObjCRuntime.Selector(SnipController.Exports.iTunesSelected);
			this.snipMenu.Spotify.Action = new ObjCRuntime.Selector(SnipController.Exports.SpotifySelected);
			this.snipMenu.VLC.Action = new ObjCRuntime.Selector(SnipController.Exports.VLCSelected);

			this.snipMenu.ITunes.Target = this.snipMenu.Spotify.Target = this.snipMenu.VLC.Target = this;
		}

		[Foundation.Export("OniTunesSelected")]
		void OniTunesSelected() {
			Debug.WriteLine("OniTunesSelected");

			this.snipMenu.ITunes.State = AppKit.NSCellStateValue.On;

			this.snipMenu.Spotify.State = this.snipMenu.VLC.State = AppKit.NSCellStateValue.Off;
		}

		[Foundation.Export("OnSpotifySelected")]
		void OnSpotifySelected() {
			Debug.WriteLine("OnSpotifySelected");

			this.snipMenu.Spotify.State = AppKit.NSCellStateValue.On;

			this.snipMenu.ITunes.State = this.snipMenu.VLC.State = AppKit.NSCellStateValue.Off;
		}

		[Foundation.Export("OnVLCSelected")]
		void OnVLCSelected() {
			Debug.WriteLine("OnVLCSelected");

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
