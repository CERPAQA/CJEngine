import React, { Component } from 'react';
export class Clock extends React.Component {
    constructor(props) {
        super(props);
        this.state = { isHidden: false };
    }

    toggleHidden() {
        this.setState({ isHidden: !this.state.isHidden })
    }

    render() {
        return (
            <br/>
            );
    }
}

function currentTime() {
    return (
        <div>
            <h2>{new Date().toLocaleTimeString()}</h2>
        </div>
    );
}

/*<div >
    {!this.state.isHidden && <Tick />}
    <button class="btn btn-dark" onClick={this.toggleHidden.bind(this)} >
        Hide Time
				</button>
</div>*/