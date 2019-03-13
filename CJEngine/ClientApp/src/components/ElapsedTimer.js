import React, { Component } from 'react';
export function ElapsedTimer(props) {
    return (
        <div id="ElapsedTimer" >
            {props.isHidden && <Duration/>}
        </div>
    );
}

function Duration() {
    return (
        <h2>Elapsed Time: {updateClock()}</h2>
    );
}

function updateClock() {
    var currDate = new Date();
    var diff = currDate - markDate;
    return (format(diff / 1000));

}

function format(seconds) {
    var numhours = parseInt(Math.floor(((seconds % 31536000) % 86400) / 3600), 10);
    var numminutes = parseInt(Math.floor((((seconds % 31536000) % 86400) % 3600) / 60), 10);
    var numseconds = parseInt((((seconds % 31536000) % 86400) % 3600) % 60, 10);
    return ((numhours < 10) ? "0" + numhours : numhours)
        + ":" + ((numminutes < 10) ? "0" + numminutes : numminutes)
        + ":" + ((numseconds < 10) ? "0" + numseconds : numseconds);
}

var markDate = new Date();