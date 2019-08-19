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
var recording = false;
var watchID = -1;
var lastTimestamp = 0;

var app = {
    // Application Constructor
    initialize: function() {
        this.bindEvents();
    },
    // Bind Event Listeners
    //
    // Bind any events that are required on startup. Common events are:
    // 'load', 'deviceready', 'offline', and 'online'.
    bindEvents: function() {
        document.addEventListener('deviceready', this.onDeviceReady, false);
    },
    // deviceready Event Handler
    //
    // The scope of 'this' is the event. In order to call the 'receivedEvent'
    // function, we must explicitly call 'app.receivedEvent(...);'
    onDeviceReady: function() {
        app.receivedEvent('deviceready');
    },
    // Update DOM on a Received Event
    receivedEvent: function(id) {

        console.log('Received Event: ' + id);
    }
};

function startStopRecordCompass() {
    if (recording) {
        recording = false; lastTimestamp = 0;
        navigator.compass.clearWatch(watchID);
    }
    else {
        recording = true;
        var select = document.getElementById("selectInterval");
        var selectValue = select.options[select.selectedIndex].value;
        var options = {frequency: Number(selectValue)};
        watchID = navigator.compass.watchHeading(handleHeadingData, errorHandler, options);
    }
}

function handleHeadingData(heading) {

    if (document.getElementById("printData").checked) {
        document.getElementById("valueHeading").innerHTML = Number(heading.magneticHeading).toFixed(5);
    }
    if (document.getElementById("printInterval") &&
        document.getElementById("printInterval").checked) {
        if (lastTimestamp != 0) {
            document.getElementById("interval").innerHTML = Number(heading.timestamp - lastTimestamp).toFixed();
        }
        lastTimestamp = heading.timestamp;
    }
}

function errorHandler(e) {
    console.log(e);
}