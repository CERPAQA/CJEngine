import React, { Component } from 'react';
export function CommentBox(props) {
    return (
        <div id="CommentBox" >
            {props.isHidden && <input type="text"></input>}
        </div>
    );
}