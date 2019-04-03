import React, { Component } from 'react';
export function Header(props) {
    return (
        <div id="header" >
            {props.isHidden && <h1 id="headerText" align="center"> CJ ENGINE </h1>}
        </div>
    );
}
//TODO: add experiment name to header instead of CJ engine.