function TotalScripts(props) {
    return (
        <div id="totalScripts">
            <p>Total pairings: {props.data}</p>
            <p>Scripts Judged: {props.score}</p>
            <p>Leading Script: {props.top}</p>
        </div>
    );
}