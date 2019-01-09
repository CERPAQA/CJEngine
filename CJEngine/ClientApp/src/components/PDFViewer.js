import React, { Component } from 'react';
export function PDFViewer(props) {
    return (
        <div id="pdfOne">
            <iframe id={props.id} src={"/Root/web/viewer.html?file=" + props.data} height='500em' width='500em'> </iframe>
        </div>
    );
}