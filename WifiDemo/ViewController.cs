using System;
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

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			string[] configuredSsids = await NEHotspotConfigurationManager.SharedManager.GetConfiguredSsidsAsync();
			foreach (var ssid in configuredSsids)
			{
				Console.WriteLine($"Found SSID: {ssid}");
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
