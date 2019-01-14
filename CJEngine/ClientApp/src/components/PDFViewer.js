import React from 'react';
export function PDFViewer(props) {
    return (
        <div id="pdfOne">
            <iframe id={props.id} src={"/ClientApp/public/Root/pdfjs-2.0.943-dist/web/web/viewer.html?file="+ props.data} height='500em' width='500em'> </iframe>
        </div>
    );
}