using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Hardware;
using Android.Content;
using System;
using Java.Interop;
using Android.Views;

namespace Proximity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ISensorEventListener
    {
        private SensorManager sensorService = null;
        private Sensor lightSensor = null;
        private Boolean isRecording = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Hide();
            // Get a SensorManager
            sensorService = (SensorManager)GetSystemService(Context.SensorService);

            // Get a Light Sensor
            lightSensor = sensorService.GetDefaultSensor(SensorType.Proximity);
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
                isRecording = true;

                sensorService.RegisterListener(this, lightSensor, SensorDelay.Normal);
            }
            else
            {
                isRecording = false;
                sensorService.UnregisterListener(this, lightSensor);
            }

        }


        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
           
        }

        public void OnSensorChanged(SensorEvent e)
        {
            if (FindViewById<CheckBox>(Resource.Id.printData).Checked == true)
            {
                (FindViewById<TextView>(Resource.Id.proxValue)).Text = (String.Format("{0:F5}", e.Values[0]));
            }



        }

    }
}