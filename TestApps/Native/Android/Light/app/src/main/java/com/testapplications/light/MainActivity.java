package com.testapplications.light;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.CheckBox;
import android.widget.Spinner;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity implements SensorEventListener, AdapterView.OnItemSelectedListener {

    private boolean isRecording = false;
    private SensorManager mSensorManager = null;
    private Sensor mSensorLight = null;
    private long lastTimestamp = 0;
    private int interval;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        getSupportActionBar().hide();

        mSensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        mSensorLight = mSensorManager.getDefaultSensor(Sensor.TYPE_LIGHT);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.intervals,
                android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        ((Spinner)findViewById(R.id.interval)).setAdapter(adapter);

        ((Spinner)findViewById(R.id.interval)).setOnItemSelectedListener(this);
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    public void buttonRecordData(View view) {
        if (!isRecording) {
            isRecording = true;
            Log.d("INTERVAL_REGISTERED", Integer.toString(interval));
            mSensorManager.registerListener(this, mSensorLight, interval);
        }
        else {
            isRecording = false;
            mSensorManager.unregisterListener(this, mSensorLight);
        }
    }


    @Override
    public void onAccuracyChanged(Sensor arg0, int arg1) {
        // TODO Auto-generated method stub

    }

    @Override
    public void onSensorChanged(SensorEvent event) {

        CheckBox printData = findViewById(R.id.printData);
        CheckBox printInterval = findViewById(R.id.printTime);
        if (printData.isChecked()) {
            ((TextView)findViewById(R.id.lightValue)).setText(String.format("%.5f",event.values[0]));
        }
        if (printInterval.isChecked()) {
            if (lastTimestamp != 0) {
                ((TextView)findViewById(R.id.delta)).setText(
                        Float.toString((event.timestamp - lastTimestamp) / 1000));
            }
            lastTimestamp = event.timestamp;
        }

    }

    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int pos,
                               long id) {
        // TODO Auto-generated method stub
        String selected = parent.getItemAtPosition(pos).toString();
        interval = Integer.parseInt(selected) * 1000;
        Log.d("INTERVAL", Integer.toString(interval));
    }

    @Override
    public void onNothingSelected(AdapterView<?> arg0) {
        // TODO Auto-generated method stub

    }

}
