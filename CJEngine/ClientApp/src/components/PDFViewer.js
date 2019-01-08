function PDFViewer(props) {
    return (
        <div id="pdfOne">
            <iframe id={props.id} src={"/pdfjs-2.0.943-dist/web/viewer.html?file=" + props.data} height='500em' width='500em'> </iframe>
        </div>
    );
}