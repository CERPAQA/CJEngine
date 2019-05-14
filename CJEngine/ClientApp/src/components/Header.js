import React, { Component } from 'react';
export function Header(props) {
    return (
        <div id="header" >
            {props.isHidden && <h1 id="headerText" align="center">{props.text}</h1>}
        </div>
    );
}