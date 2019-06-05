import React from 'react';
export function PDFViewer(props) {
    return (
        <div id="pdfOne">
            <iframe id={props.id} src={props.fileNames} height='500em' width='500em'></iframe>
        </div>
    );
}