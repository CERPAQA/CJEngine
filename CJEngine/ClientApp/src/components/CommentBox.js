import React, { Component } from 'react';
export function CommentBox(props) {
    return (
        <div id="CommentBox" >
            {props.isHidden && <input id="CommentText" type="text"></input>}
        </div>
    );
}