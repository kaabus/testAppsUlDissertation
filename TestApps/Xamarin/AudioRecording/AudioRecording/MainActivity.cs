using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using System.IO;
using System;
using Java.Interop;
using Android.Views;
using Android.Util;

namespace AudioRecording
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private string filepath;
        private string filename;
        private MediaRecorder recorder = null;
        private Boolean isRecording = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.Hide();
            filepath =(string) Android.OS.Environment.ExternalStorageDirectory;
            filename = Path.Combine(filepath, "audiorecordtest.3gp");
            Log.Debug("checking", filename);
            
        }
        [Export("buttonRecordData")]
        public void button_OnClick(View v)
        {
            Log.Debug("checking", filename);
            if (!isRecording)
            {
                StartRecorder();
                isRecording = true;
            }
            else
            {
                StopRecorder();
                isRecording = false;
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void StartRecorder()
        {
            try
            {
                //Java.IO.File sdDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic);
                //filePath = sdDir + "/" + "testAudio.mp3";
                if (File.Exists(filename))
                    File.Delete(filename);

                //Java.IO.File myFile = new Java.IO.File(filePath);
                //myFile.CreateNewFile();

                if (recorder == null)
                    recorder = new MediaRecorder(); // Initial state.
                else
                    recorder.Reset();

                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                recorder.SetOutputFile(filename); // DataSourceConfigured state.
                recorder.SetAudioEncoder(AudioEncoder.AmrNb); // Initialized state.
                recorder.Prepare(); // Prepared state
                recorder.Start(); // Recording state.

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopRecorder()
        {
            if (recorder != null)
            {
                recorder.Stop();
                recorder.Release();
                recorder = null;
            }
        }
    }
}