import React from 'react';
import PDF from 'react-pdf-js';
export function PDFViewer(props) {
    return (
        <div id="pdfOne">
        <PDF
            file={props.data}
            />
        </div>
    );
}