var artefactList = [];
var judgeList = [];

//add Artefact to expArtefacts
$(".addArtefactButton").click(function (event) {
    event.preventDefault(); 
    var $item = $(this).closest("tr")
    var artefactName = $item.find("td:nth-child(1)").text();
    artefactName = artefactName.trim();
    var inList = artefactList.includes(artefactName);
    if (inList === false) {
        artefactList.push(artefactName);
        var node = document.createElement("li");
        var textNode = document.createTextNode(artefactName);
        var buttonNode = document.createElement("button");
        buttonNode.setAttribute("class", "remove");
        buttonNode.setAttribute("id", "artefact" + artefactName)
        buttonNode.setAttribute("type", "button");
        buttonNode.addEventListener("click", function () {
            var inList = artefactList.includes(artefactName);
            if (inList === false) {
                alert("Artefact not in List");
            } else {
                var index = artefactList.indexOf(artefactName);
                artefactList.splice(index, 1);
                node.remove();
            }
        });
        buttonNode.textContent = "Remove";
        node.appendChild(textNode);
        node.appendChild(buttonNode);
        addHidden(node, 'expArtefacts', artefactName);
        document.getElementById("selectedArtefacts").appendChild(node);
    } else {
        alert("Artefact Already Added");
    }
});

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selectedArtefacts").appendChild(input);
}

//add judge to expJudges
$(".addJudgeButton").click(function (event) {
    event.preventDefault();
    var $item = $(this).closest("tr")
    var judgeName = $item.find("td:nth-child(1)").text();
    judgeName = judgeName.trim();
    var k = judgeList.includes(judgeName);
    if (k === false) {
        judgeList.push(judgeName);
        var node = document.createElement("li");
        var textNode = document.createTextNode(judgeName);
        var buttonNode = document.createElement("button");
        buttonNode.setAttribute("class", "remove");
        buttonNode.setAttribute("id", "artefact" + judgeName)
        buttonNode.setAttribute("type", "button");
        buttonNode.addEventListener("click", function () {
            var inList = judgeList.includes(judgeName);
            if (inList === false) {
                alert("Judge not in List");
            } else {
                var index = judgeList.indexOf(judgeName);
                judgeList.splice(index, 1);
                node.remove();
            }
        });
        buttonNode.textContent = "Remove";
        node.appendChild(textNode);
        node.appendChild(buttonNode);
        addHidden(node, 'expJudges', judgeName);
        document.getElementById("selectedJudges").appendChild(node);
    } else {
        alert("Judge Already Added");
    }
});

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selectedJudges").appendChild(input);
}

//used for edit experiment page
window.onload = function () {
    var tempLsArtefact = [];
    var tempLsJudge = [];
    tempLsArtefact = document.getElementById("selectedArtefacts")
        .getElementsByTagName("li");
    for (i = 0; i < tempLsArtefact.length; i++) {
        var name = $(tempLsArtefact[i]).text().split("Remove")[0].trim(); //not efficient enough, needs fixing
        artefactList.push(name);
    }
    tempLsJudge = document.getElementById("selectedJudges")
        .getElementsByTagName("li");
    for (i = 0; i < tempLsJudge.length; i++) {
        var name = $(tempLsJudge[i]).text().split("Remove")[0].trim(); //not efficient enough, needs fixing
        judgeList.push(name);
    }

    var remove = document.getElementsByClassName("removeArtefact");
    for (var i = 0; i < remove.length; i++) {
        remove[i].addEventListener("click", function () {
            var item = $(this).closest("li")
            var artefactName = item.text().split("Remove")[0].trim(); //not efficient enough, needs fixing
            var inList = artefactList.includes(artefactName);
            if (inList === false) {
                alert("Artefact not in List");
            } else {
                var index = artefactList.indexOf(artefactName);
                artefactList.splice(index, 1);
                item.remove();
            }
        });
    }

    var remove = document.getElementsByClassName("removeJudge");
    for (var i = 0; i < remove.length; i++) {
        remove[i].addEventListener("click", function () {
            var item = $(this).closest("li")
            var judgeName = item.text().split("Remove")[0].trim(); //not efficient enough, needs fixing
            var inList = judgeList.includes(judgeName);
            if (inList === false) {
                alert("Judge not in List");
            } else {
                var index = judgeList.indexOf(judgeName);
                judgeList.splice(index, 1);
                item.remove();
            }
        });
    }
}

//saves the chosen parameters (API method)
function createParams() {
    var description = document.getElementById("description").value;
    var showTitle = document.getElementById("checktitle").checked;
    var showTimer = document.getElementById("checkTime").checked;
    var addComment = document.getElementById("checkAddComment").checked;
    var timer = document.getElementById("checkTimer").value;

    //reconfigure fetch method
    fetch('/api/ExperimentParametersAPI/CreateParams', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ description, showTimer, showTitle, addComment, timer })
    }).then(function (response) {
        if (response.status !== 200) {
            console.log('fetch returned not ok' + response.status);
        }

        response.json().then(function (data) {
            console.log('fetch returned ok');
            console.log(data);
        });
    })
        .catch(function (err) {
            console.log(`error: ${err}`);
        });
    var select = document.getElementById("parametersList");
    select.options[select.options.length] = new Option(description, description);
    select.value = description;
}

function CreateAlgorithm() {
    var file = document.getElementById("algorithmFile").value;
    var description = document.getElementById("algorithmDescription").value;
    var formData = new FormData();
    formData.append("Filename", file);
    formData.append("Description", description);
    const options = {
        method: 'POST',
        body: formData
    }
    fetch('/api/AlgorithmAPI/AddAlgorithm', options)
        .then(function (response) {
        if (response.status !== 200) {
            console.log('fetch returned not ok' + response.status);
        }

        response.json().then(function (data) {
            console.log('fetch returned ok');
            console.log(data);
        });
    })
        .catch(function (err) {
            console.log(`error: ${err}`);
        });
}