import React, { Component } from 'react';
export class Clock extends React.Component {
    constructor(props) {
        super(props);
        this.state = { showTimer: false };
    }
    componentDidMount() {
        var stringExpNum = document.URL.split("/")[4];
        var expNum = parseInt(stringExpNum, 10);
        fetch("api/Pairings/GetParams/?id=" + expNum)
            .then(response => response.json())
            .then(params => {
                this.setState({ showTimer: params["showTimer"]})
            });
    }
    render() {
        return (
            <div id="ElapsedTimer">
                {<Duration isHidden={this.state.showTimer} />}
            </div>
            );
    }
}

function Duration(props) {
    return (
        <div id="timer" >
        {
           props.isHidden && <h2>Elapsed Time: {updateClock()}</h2>
        }
        </div>
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
