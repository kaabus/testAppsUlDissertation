using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;
using System;
using XE = Xamarin.Essentials;
using Android.Util;
using Java.Interop;
using Android.Views;

namespace Accelerometer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity

    {
        private Boolean isRecording=false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.activity_main);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           
            this.SupportActionBar.Hide();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        [Export("buttonRecordData")]
        public void button_OnClick(View v)
        {
            if (!isRecording)
            {
                XE.Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                XE.Accelerometer.Start(SensorSpeed.UI);
                
                isRecording = true;
            }
            else
            {
                XE.Accelerometer.Stop();
                isRecording = false;
            }
            
        }
       

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            CheckBox printData = FindViewById<CheckBox>(Resource.Id.printData);
            if (printData.Checked==true)
            {
                (FindViewById<TextView>(Resource.Id.accX)).Text = (String.Format("{0:F5}", data.Acceleration.X));

            (FindViewById<TextView>(Resource.Id.accY)).Text=(String.Format("{0:F5}", data.Acceleration.Y));

            (FindViewById<TextView>(Resource.Id.accZ)).Text=(String.Format("{0:F5}", data.Acceleration.Z));
        }
		
        /*Log.Debug("Checking",$"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}");*/
            // Process Acceleration X, Y, and Z
        }
    }
}