namespace Winter.OSX.MediaPlayers {
	using iTunesLibrary;
	using AppKit;
	using NotificationCenter;
	using System.Diagnostics;
	using Foundation;

	/// <summary>
	/// iTunes wrapper to get contents of iTunes Media Player Application. For more information
	/// see <seealso cref="https://developer.apple.com/documentation/ituneslibrary"/>.
	/// </summary>
	public class iTunes : MediaPlayer {
		/// <summary>
		/// Version of the API to use with iTunes.
		///
		/// This is set to '1.0' to use (i am assuming) the latest version.
		///
		/// <seealso cref="https://developer.apple.com/documentation/ituneslibrary/itlibrary/1809375-init"/>
		/// </summary>
		const string ApiVersion = "1.0";

		private ITLibrary library = null;
		private NSObject observer = null;

		public override void Load() {
			NSError error;

			this.library = new ITLibrary(iTunes.ApiVersion, ITLibInitOptions.None, out error);

			if(error == null) {
				Debug.WriteLine(
					string.Format("iTunes initialized with Api Version {0}", iTunes.ApiVersion));
			} else {
				Debug.WriteLine(
					string.Format("iTunes failed to initialize. {0}", error.Description));
			}

			NSDistributedNotificationCenter center = (NSDistributedNotificationCenter)NSDistributedNotificationCenter.DefaultCenter;

			this.observer = center.AddObserver(new NSString("com.apple.iTunes.playerInfo"), this.OnNotificationReceived);
		}

		public override void Unload() {
			NSDistributedNotificationCenter center = (NSDistributedNotificationCenter)NSDistributedNotificationCenter.DefaultCenter;

			center.RemoveObserver(this.observer);

			this.library.Dispose();

			this.library = null;

			Debug.WriteLine("iTunes unloaded...");
		}

		[Export("OnNotificationReceived")]
		protected virtual void OnNotificationReceived(NSNotification notification) {
			NSDictionary dictionary = notification.UserInfo;

			Debug.WriteLine("Notification Received....");

			for(int i = 0; i < dictionary.Keys.Length; ++i) {
				var message = string.Format(
					"{0} - {1}",
					dictionary.Keys[i], dictionary[dictionary.Keys[i]]);

				Debug.WriteLine(message);
			}
		}
	}
}
