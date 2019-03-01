var artefactList = [];
var judgeList = [];
var x = document.getElementById("parametersList").value;

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
        document.getElementById("selctedArtefacts").appendChild(node);
    } else {
        alert("Artefact Already Added");
    }
});

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selctedArtefacts").appendChild(input);
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
        document.getElementById("selctedJudges").appendChild(node);
    } else {
        alert("Judge Already Added");
    }
});

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selctedJudges").appendChild(input);
}

//saves the chosen parameters (API method)
function createParams() {
    var description = document.getElementById("description").value;
    var showTitle = document.getElementById("checktitle").checked;
    var showTimer = document.getElementById("checkTime").checked;

    //reconfigure fetch method
    fetch('/api/ExperimentParametersAPI/CreateParams', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ description, showTimer, showTitle })
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