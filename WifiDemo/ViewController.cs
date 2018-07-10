using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using NetworkExtension;
using UIKit;

namespace WifiDemo
{
	public partial class ViewController : UIViewController
	{

		// Related documentation and notes:
		// This app will work with iOS11+. Prior to iOS11 either private API has to be used or
		// some advanced admin configuration is required - I did not do any research because iOS11
		// should be on the majority of devices.
		//
		// https://developer.apple.com/documentation/networkextension/nehotspotconfigurationmanager
		// https://developer.apple.com/documentation/networkextension/nehotspotconfiguration
		//
		// To use the NEHotspotConfigurationManager class, you must enable the Hotspot Configuration capability 
		// in Entitlements.plist
		// 
		// Discussion of "iOS Wi-Fi Management APIs"
		// https://developer.apple.com/library/archive/qa/qa1942/_index.html
		// Request access to network management APIs: https://developer.apple.com//contact/request/network-extension/

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			string[] configuredSsids = await NEHotspotConfigurationManager.SharedManager.GetConfiguredSsidsAsync();

			if (configuredSsids != null)
			{
				foreach (var ssid in configuredSsids)
				{
					Console.WriteLine($"Found SSID: {ssid}");
				}
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		async partial void OnConnectClicked(UIButton sender)
		{
			var ssid = TxtSsid.Text.Trim();
			var password = TxtPassword.Text.Trim();

			if (string.IsNullOrWhiteSpace(ssid))
			{
				var alert = new UIAlertController
				{
					Title = "No SSID set",
					Message = "You need do specify an SSID"
				};
				alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
				PresentViewController(alert, true, null);
				return;
			}

			var config = new NEHotspotConfiguration(ssid, password, isWep: false);
			// See: https://developer.apple.com/documentation/networkextension/nehotspotconfiguration/2887518-joinonce
			config.JoinOnce = false;

			var tcs = new TaskCompletionSource<NSError>();
			NEHotspotConfigurationManager.SharedManager.ApplyConfiguration(config, err => tcs.SetResult(err));

			var error = await tcs.Task;
			if (error != null)
			{
				var alert = new UIAlertController
				{
					Title = "Error",
					Message = error.Description
				};
				alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
				PresentViewController(alert, true, null);
				return;
			}
		}
	}
}
