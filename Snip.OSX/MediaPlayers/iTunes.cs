namespace Winter.OSX.MediaPlayers {
	using System.IO;
	using iTunesLibrary;
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
		const string NotificationIdentifier = "com.apple.iTunes.playerInfo";

		private ITLibrary library = null;
		private NSObject observer = null;
		private ITLibMediaItem current = null;

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

			/* Reference: https://stackoverflow.com/a/30027772 */
			NSDistributedNotificationCenter center = (NSDistributedNotificationCenter)NSDistributedNotificationCenter.DefaultCenter;

			this.observer = center.AddObserver(new NSString(iTunes.NotificationIdentifier), this.OnNotificationReceived);
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

			// this region is good for inspecting the layout
			// of the notification details.

			//Debug.WriteLine("Notification Received....");

			for(int i = 0; i < dictionary.Keys.Length; ++i) {
				var message = string.Format(
					"{0} - {1}",
					dictionary.Keys[i], dictionary[dictionary.Keys[i]]);

				Debug.WriteLine(message);
			}

			long value;

			var pid = dictionary["PersistentID"].Description;

			Debug.WriteLine(pid);

			if(long.TryParse(pid, out value)) {
				this.current = System.Array.Find(this.library.AllMediaItems, i => i.PersistentId.Int64Value == value);

				var fName = "Snip.txt";

				var current = Directory.GetCurrentDirectory();

				var desktop = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

				var path = string.Format("{0}{1}{2}", desktop, Path.DirectorySeparatorChar, fName);

				using(var stream = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite)) {
					using(var writer = new StreamWriter(stream)) {
						var output = "{0} - {1} - {2}...";

						writer.Write(string.Format(output, this.current.Title, this.current.Artist.Name, this.current.Album.Title));

						writer.Flush();
					}
				}
			} else {
				Debug.WriteLine("Cannot parse to int");
			}
		}
	}
}
