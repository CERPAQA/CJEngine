import React, { Component } from 'react';

function importAll(r) {
    let images = {};
    r.keys().map((item, index) => { images[item.replace('./', '')] = r(item); });
    return images;
}

const images = importAll(require.context('../public', false, '/\.jpg/'));
export function IMGViewer(props) {
    return (
        <div id="imgOne">
            <img id={props.id} src={images[props.data]} height='500em' width='500em' ></img>
        </div>
    );
}