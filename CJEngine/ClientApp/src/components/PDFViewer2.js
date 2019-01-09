import React from 'react';
import PDF from 'react-pdf-js';

export class PDFViewer2 extends React.Component {
    constructor(props) {
        super(props);
    }
    state = { page: 1 };

    onDocumentComplete = (pages) => {
        this.setState({ page: 1, pages });
    }

    handlePrevious = () => {
        this.setState({ page: this.state.page - 1 });
    }

    handleNext = () => {
        this.setState({ page: this.state.page + 1 });
    }

    renderPagination = (page, pages) => {
        let previousButton = (
            
            <button onClick={this.handlePrevious} className="btn btn-dark">
                Previous pg
            </button>
        );
        if (page === 1) {
            previousButton = (
                <button className="btn btn-dark">
                    Previous pg
                </button>

            );
        }
        let nextButton = (
            <button onClick={this.handleNext} className="btn btn-dark">
                Next pg
            </button>

        );
        if (page === pages) {
            nextButton = (
               
                <button className="btn btn-dark">
                    Next pg
                </button>
                
            );
        }
        return (
            <nav>
                {previousButton}
                {nextButton}
            </nav>
        );
    }

    render() {
        let pagination = null;
        if (this.state.pages) {
            pagination = this.renderPagination(this.state.page, this.state.pages);
        }
        return (
            <div>
                <PDF file={this.props.data} onDocumentComplete={this.onDocumentComplete} page={this.state.page} />
                {pagination}
            </div>
        );
    }
}