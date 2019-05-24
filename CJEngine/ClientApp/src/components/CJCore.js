import React, { Component } from 'react';
import { GetFileType } from './GetFileType';
import { Header } from './Header';
import { IMGViewer } from './IMGViewer';
import { JudgedScripts } from './JudgedScripts';
import { PDFViewer } from './PDFViewer';
//import { PDFViewer2 } from './PDFViewer2';
import { ElapsedTimer } from './ElapsedTimer';
import { CommentBox } from './CommentBox';

export class CJCore extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            fileNames: [], expID: 0, expTitle: "",  showTitle: false, addComment: false, timer: 0, index: 0, isHidden: false, counter: 0, score: 0, time: new Date(), judgeID: 0, winList: [], topPick: "" };
        this.nextFileButton = this.nextFileButton.bind(this);
        this.prevFileButton = this.prevFileButton.bind(this);
        this.judgePairOneButton = this.judgePairOneButton.bind(this);
        this.judgePairTwoButton = this.judgePairTwoButton.bind(this);
        this.judgePair = this.judgePair.bind(this);
        this.send = this.send.bind(this);
        this.judgeScore = this.judgeScore.bind(this);
        this.getNextFiles = this.getNextFiles.bind(this);
        this.getJudgeID = this.getJudgeID.bind(this);
        this.getLeadingScript = this.getLeadingScript.bind(this);
        this.randomClick = this.randomClick.bind(this);
        this.Judge = this.Judge.bind(this);
        this.timeOut = setTimeout(null, 1000000000000);
        this.tempDisableItemButtons = this.tempDisableItemButtons.bind(this);
        this.enableItemButtons = this.enableItemButtons.bind(this);
    }
    //TODO: next and previous buttons should bepart of params, needs to be hidden by default
    componentWillMount() {
        var stringExpNum = document.URL.split("/")[4];
        var expNum = parseInt(stringExpNum, 10);
        this.setState({ expID: expNum });

        fetch("api/Pairings/CreatePairings/?id=" + expNum)
            .then(response => response.json())
            .then(fileNames => {
                this.setState({ fileNames: fileNames })
            });

        fetch("api/Pairings/GetParams/?id=" + expNum)
            .then(response => response.json())
            .then(params => {
                this.setState({ showTitle: params["showTitle"], addComment: params["addComment"], expTitle: params["expTitle"] })
            });
    }

    componentDidMount() {
        var stringExpNum = document.URL.split("/")[document.URL.split("/").length-1];
        var expNum = parseInt(stringExpNum, 10);
        fetch("api/Pairings/IsTimerSet/?id=" + expNum)
            .then(response => response.json())
            .then(timerLength => {
                this.Judge(timerLength);
                this.setState({ timer: timerLength });
            });
    }
    //TODO: Timed judgements crash at the end, needs to be fixed
    randomClick(item) {
        //document.getElementById(item).click();
        document.getElementById("itemOne").click();
    }

    Judge(timerLength) {
        if (timerLength > 0) {
            clearTimeout(this.timeOut);
            var interval = timerLength * 1000;
            var itemLs = ["itemOne", "itemTwo"];
            //TODO: fix random choice of items issue(STRETCH GOAL)
            var randChoice = itemLs[Math.floor(Math.random() * itemLs.length)];
            this.timeOut = setTimeout(this.randomClick, interval);
        } else {
            console.log("no timer");
        }
    }

    toggleHidden() {
        this.setState({ isHidden: !this.state.isHidden });
    }

    getNextFiles() {
        var newindex = this.state.index + 1;
        var newcounter = this.state.counter;
        if(this.state.index === this.state.counter)
            newcounter++;
        var Score = this.judgeScore();
        this.getLeadingScript();
        this.setState({ index: newindex, counter: newcounter, score: Score, time: new Date() });
    }

    nextFileButton() {
        if (this.state.index < this.state.counter) {
            this.getNextFiles();
        }
    }

    prevFileButton() {
        var newindex = this.state.index - 1;
        if (newindex <= 0) {
            newindex = 0;
        }
        this.setState({ index: newindex });
    }

    judgePairOneButton() {
        this.judgePair("item1");
    }

    judgePairTwoButton() {
        this.judgePair("item2");
    }

    //This handles pressing either the item 1 or 2 button 
    judgePair(itemNumber) {
        this.tempDisableItemButtons();
        setTimeout(this.enableItemButtons, 5000);
        var item = this.state.fileNames[this.state.index][itemNumber];
        var timeJudged = this.setTime();
        var elapsed = this.elapsedTime();
        this.getNextFiles();
        this.send(this.state.fileNames[this.state.index], item, timeJudged, elapsed);
        this.Judge(this.state.timer);
    }

    setTime() {
        var now = new Date();
        var x = now.toLocaleString();
        return x;
    }

    elapsedTime() {
        var start = this.state.time;
        var end = new Date();
        var timeDiff = end - start;
        timeDiff /= 1000;

        var seconds = Math.round(timeDiff);
        var elapsed = seconds;
        return elapsed;
    }

    judgeScore(score) {
        score = this.state.score;
        score += 2;
        return score;
    }

    getJudgeID() {
        fetch('api/Pairings/GenerateID')
            .then(response => response.json())
            .then(id => {
                this.setState({ judgeID: id});
            });

        var id = this.state.judgeID;
        return id;
    }

    getLeadingScript() {
        fetch('api/Pairings/GetLeadingScript')
            .then(response => response.text())
            .then(script => {
                this.setState({ topPick: script });
            });
        var script = this.state.topPick;
        return script;
    }

    tempDisableItemButtons() {
        document.getElementById("itemOne").disabled = true;
        document.getElementById("itemTwo").disabled = true;
    }

    enableItemButtons() {
        document.getElementById("itemOne").disabled = false;
        document.getElementById("itemTwo").disabled = false;
    }

    send(pair, winner, timeJ, elapsed) {
        var commentEnabled = this.state.addComment;
        if (commentEnabled === true) {
            var comment = document.getElementById("CommentText").value;
        } else {
            comment = "";
        }
        fetch("api/Pairings/GetWinners?id=" + this.state.expID, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ Winner: winner, ArtefactPairings: pair, TimeOfPairing: timeJ, ElapsedTime: elapsed, Comment: comment, judgeID: this.state.judgeID })
        }).then(function (response) {
            if (response.status !== 200) {
                console.log('fetch returned not ok' + response.status);
            }
        })
    }

    render() {
        let viewLeft;
        let viewRight;
        var endOfPairs = this.state.counter;

        if (this.state.fileNames.length > 0) {
            if (endOfPairs >= this.state.fileNames.length) {
                viewLeft = <EndOfPairs align="left" />;
            } else {
                var currentFileLeft = this.state.fileNames[this.state.index]["item1"];
                var currentFileRight = this.state.fileNames[this.state.index]["item2"];
                var x = GetFileType(currentFileLeft);
                var y = GetFileType(currentFileRight);
                if (x === true) {
                    viewLeft = <PDFViewer id="left" fileNames={currentFileLeft} />;

                } else {
                    viewLeft = <IMGViewer id="left" fileNames={currentFileLeft} />;
                }
                if (y === true) {
                    viewRight = <PDFViewer id="right" fileNames={currentFileRight} />;
                } else {

                    viewRight = <IMGViewer id="right" fileNames={currentFileRight} />;
                }
            }
        }
        return (
            <div>
                {<Header isHidden={this.state.showTitle} text={this.state.expTitle}/>}
                <div class="itemDisplay">
                    <button id="prevFileButton" className="btn btn-dark" align="right" onClick={this.prevFileButton}>Previous File</button>
                    {viewLeft}
                    {viewRight}
                    <button id="nextFileButton" className="btn btn-dark" align="left" onClick={this.nextFileButton}>Next File</button>
                </div>

                <div class="judgeChoice">
                    <button id="itemOne" class="btn btn-dark" onClick={this.judgePairOneButton}>Item One</button>
                    <button id="itemTwo" class="btn btn-dark" onClick={this.judgePairTwoButton}>Item Two</button>
                </div>
                <JudgedScripts fileNames={this.state.fileNames.length} score={this.state.score} top={this.state.topPick} />
            </div>
        );
    }
}

function EndOfPairs(props) {
    return (
        <img src="finished.jpg" width='40%' align={props.align}></img>
    );
}