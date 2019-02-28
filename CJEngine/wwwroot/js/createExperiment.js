var artefactList = [];
var judgeList = [];
var x = document.getElementById("parametersList").value;

$(".addArtefactButton").click(function (e) {
    e.preventDefault(); 
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    x = x.trim();
    var k = artefactList.includes(x);
    if (k === false) {
        artefactList.push(x);
        var node = document.createElement("li");
        var textNode = document.createTextNode(x);
        var buttonNode = document.createElement("button");
        buttonNode.setAttribute("class", "remove");
        buttonNode.setAttribute("id", "artefact"+x)
        buttonNode.setAttribute("type", "button");
        buttonNode.addEventListener("click", function () {
            var k = artefactList.includes(x);
            if (k === false) {
                alert("Artefact not in List");
            } else {
                var index = artefactList.indexOf(x);
                artefactList.splice(index, 1);
                node.remove();
            }
        });
        buttonNode.textContent = "Remove";
        node.appendChild(textNode);
        node.appendChild(buttonNode);
        addHidden(node, 'expArtefacts', x);
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

$(".addJudgeButton").click(function (e) {
    e.preventDefault();
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    x = x.trim();
    var k = judgeList.includes(x);
    if (k === false) {
        judgeList.push(x);
        var node = document.createElement("li");
        var textNode = document.createTextNode(x);
        var buttonNode = document.createElement("button");
        buttonNode.setAttribute("class", "remove");
        buttonNode.setAttribute("id", "artefact" + x)
        buttonNode.setAttribute("type", "button");
        buttonNode.addEventListener("click", function () {
            var k = judgeList.includes(x);
            if (k === false) {
                alert("Judge not in List");
            } else {
                var index = judgeList.indexOf(x);
                judgeList.splice(index, 1);
                node.remove();
            }
        });
        buttonNode.textContent = "Remove";
        node.appendChild(textNode);
        node.appendChild(buttonNode);
        addHidden(node, 'expJudges', x);
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