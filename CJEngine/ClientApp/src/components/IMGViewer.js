import React, { Component } from 'react';

export function IMGViewer(props) {
    return (
        <div id="imgOne">
            <img id={props.id} src={props.data} height='500em' width='500em' ></img>
        </div>
    );
}