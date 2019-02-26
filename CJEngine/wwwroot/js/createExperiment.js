﻿var ls = [];

var x = document.getElementById("parametersList").value;

function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selctedArtefacts").appendChild(input);
}

$(".addArtefactButton").click(function (e) {
    e.preventDefault(); //this line prevents the form from refreshing each time
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    var k = ls.includes(x);
    if (k === false) {
        ls.push(x);
        var node = document.createElement("li");
        var textNode = document.createTextNode(x);
        node.appendChild(textNode);
        addHidden(node, 'expArtefacts', x);
        node.appendChild(textNode);
        addHidden(node, 'expArtefacts', x);
        document.getElementById("selctedArtefacts").appendChild(node);
    } else {
        alert("Artefact Already Added");
    }
});

$(".removeArtefactButton").click(function (e) {
    var $item = $(this).closest("tr")
    var x = $item.find("td:nth-child(1)").text();
    var k = ls.includes(x);
    if (k === false) {
        alert("Artefact not in List");
    } else {

    }
});


function addHidden(Li, key, value) {
    var input = document.createElement('input');
    input.type = 'hidden';
    input.name = key; 'name-as-seen-at-the-server';
    input.value = value;
    document.getElementById("selctedJudges").appendChild(input);
}

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