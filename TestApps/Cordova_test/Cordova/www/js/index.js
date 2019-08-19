/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
var mRecordButton=document.getElementById("recordData");
var app = {
    // Application Constructor
    initialize: function() {
        this.bindEvents();
        console.log("I am here initialize");
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function() {
        console.log("I am here bindEvents");
        mRecordButton.addEventListener("click", Start);
        /*function getAcceleration() {
            navigator.accelerometer.getCurrentAcceleration(accelerometerSuccess, accelerometerError);

            function accelerometerSuccess(acceleration) {
                console.log("I am here");
                var checkBox = document.getElementById("printData");
                if (checkBox.checked === true) {
                    document.getElementById("accX").innerHTML = "Acceleration X:"  + acceleration.x
                    document.getElementById("accY").innerHTML = "Acceleration Y:"  + acceleration.y
                    document.getElementById("accZ").innerHTML = "Acceleration Z:"  + acceleration.z
                }

            }

            function accelerometerError() {
                alert('onError!');
            }
        }*/

    },

    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady: function() {
        app.receivedEvent();
        console.log("I am here onDeviceReady");

    },
    // Update DOM on a Received Event
    receivedEvent: function(id) {

    }



};
var isRecording=false;
function processEvent(event) {
    // process the event object

            document.getElementById("accX").innerHTML = "Acceleration X:"  + event.acceleration.x.toFixed(5)
            document.getElementById("accY").innerHTML = "Acceleration Y:"  + event.acceleration.y.toFixed(5)
            document.getElementById("accZ").innerHTML = "Acceleration Z:"  + event.acceleration.z.toFixed(5)
            isRecording=true;


}
function getAcceleration(){


        window.addEventListener("compassneedscalibration",function(event) {
            // ask user to wave device in a figure-eight motion .
            event.preventDefault();
        }, true);
        options = {frequency: 20000};
        window.addEventListener("devicemotion",processEvent, false, options);






}
function Start(){
    var checkBox = document.getElementById("printData");
    if (!isRecording){
        if (checkBox.checked === true) {

        console.log("Started");
        mRecordButton.removeEventListener("click", Start);
        mRecordButton.addEventListener("click", Stop);
        getAcceleration();
        mRecordButton.innerHTML = "Stop Record Data";

        }
    } else {
        isRecording=false;
    }

}

function Stop(){
    console.log("Stopped");
    window.removeEventListener("devicemotion",processEvent,false);
    mRecordButton.removeEventListener("click", Stop);
    mRecordButton.addEventListener("click", Start);
    mRecordButton.innerHTML = "Start Record Data";
}