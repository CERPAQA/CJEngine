var artefactList = [];
var x = document.getElementById("parametersList").value;

$(".addArtefactButton").click(function (e) {
    e.preventDefault(); //this line prevents the form from refreshing each time
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    var k = artefactList.includes(x);
    if (k === false) {
        artefactList.push(x);
        var node = document.createElement("li");
        var textNode = document.createTextNode(x);
        var buttonNode = document.createElement("a");
        buttonNode.setAttribute("class", "remove");
        buttonNode.setAttribute("asp-route-id", "@item.Id")
        buttonNode.setAttribute("href", "")
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

$(".removeArtefactButton").click(function (e) {
    e.preventDefault();
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    var k = artefactList.includes(x);
    if (k === false) {
        alert("Artefact not in List");
    } else {
        //$("li:contains(''" + x + "'')").remove();
        $('.remove').on('click', function () {
            $(this).parent().remove();
        });
        console.log();
    }
});

$("#tblJudge td").click(function (e) {
    e.preventDefault(); //this line prevents the form from refreshing each time
    var tbl = document.getElementById("tblJudge");
    if (tbl.innerHTML != null) {
        var row_num = parseInt($(this).parent().index()) + 1;
        var column_num = parseInt($(this).index()) - 1;
        var x = tbl.rows[row_num].cells[column_num].innerHTML;
        var node = document.createElement("li");
        var textNode = document.createTextNode(x);
        node.appendChild(textNode);
        addHidden(node, 'expJudges', x);
        document.getElementById("selctedJudges").appendChild(node);
    } else {
        alert("The Table is empty")
    }
});

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selctedJudges").appendChild(input);
}