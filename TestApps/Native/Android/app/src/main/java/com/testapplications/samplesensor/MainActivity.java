    package com.testapplications.samplesensor;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;

public class MainActivity extends AppCompatActivity implements SensorEventListener {
    SensorManager mSensorManager;
    Sensor mSensor;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Initialise the sensor manager
        mSensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        // Initialise the sensor
        mSensor = mSensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        // Register sensor manager with the event listener, sensor and the delay
        mSensorManager.registerListener(this, mSensor, SensorManager.SENSOR_DELAY_UI);


    }


    @Override
    protected void onStop() {
        super.onStop();
        // un register the listener once the work is done
        mSensorManager.unregisterListener(this);
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        // get the sensor events here, which can be used for further functionality
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {
        // get the accuracy of the sensor data here,
        // if the sensor data needs to be accurate then this event should be used,
        // which gives the accuracy of the data, 3 being very accurate and 0 being unreliable.
    }
}
