import React, { Component } from 'react';
export function ElapsedTimer(props) {
    return (
        <div id="currentTime" >
            {props.isHidden && <h2>{new Date().toLocaleTimeString()}</h2>}
        </div>
    );
}