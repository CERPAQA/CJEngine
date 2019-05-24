import React from 'react';
//TODO: need to make the pdfs render larger, there is also the issue of screen resolution
export function PDFViewer(props) {
    return (
        <div id="pdfOne">
            <iframe id={props.id} src={props.fileNames} height='500em' width='500em'> </iframe>
        </div>
    );
}