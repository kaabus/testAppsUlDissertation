using System;

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace GPS
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ILocationListener
    {
        private Boolean recording = false;
        private LocationManager mLocationManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Hide();
            mLocationManager =(LocationManager) this.GetSystemService(Context.LocationService);
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        [Export("recordData")]
        public void button_OnClick(View v)
        {
            record();

        }

        private void record()
        {
            if (!recording)
            {
                try
                {
                    mLocationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
                }
                catch(Exception e)
                {
                }

               
            }
            else
            {
                mLocationManager.RemoveUpdates(this);
            }
            recording = !recording;
        }

        public void OnLocationChanged(Location location)
        {
            if ((FindViewById<CheckBox>(Resource.Id.printData)).Checked==true)
            {
                (FindViewById<TextView>(Resource.Id.latitude)).Text=location.Latitude.ToString();
                (FindViewById<TextView>(Resource.Id.longitude)).Text = location.Longitude.ToString();
            }
            /*Log.Debug("Checking", "Lat:" + location.Latitude.ToString() + "Lon:" + location.Longitude + "");*/
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}