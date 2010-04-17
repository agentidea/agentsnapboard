//when leave a text box this gets called ...
function processBlur(ev) {

    ev = ev || window.event;
    var elem = ev.srcElement;
    var gameCode = raiffaGame.code;
    var elemBits = elem.id.split('_');

    var lowElemID = elemBits[0] + "_LOW";
    var highElemID = elemBits[0] + "_HIGH";

    var lowElem = document.getElementById(lowElemID);
    var highElem = document.getElementById(highElemID);

    var lowValueToUpdate = lowElem.value;
    var highValueToUpdate = highElem.value;
    

    lowElem.style.backgroundColor = "#FFFFFF";
    highElem.style.backgroundColor = "#FFFFFF";    
    storyView.log("");
    
    //validate entries
    if (lowValueToUpdate.trim().length == 0 || isNaN(lowValueToUpdate)) {
        lowElem.style.backgroundColor = "#FF6666";
        storyView.log("Please enter a valid number for this field");
        lowElem.focus();
        return;
    }
    if (highValueToUpdate.trim().length == 0 || isNaN(highValueToUpdate)) {
        highElem.style.backgroundColor = "#FF6666";
        storyView.log("Please enter a valid number for this field");
        highElem.focus();
        return;
    }

//    if (lowValueToUpdate.indexOf("E") != -1 || highValueToUpdate.indexOf("E") != -1) {
//        storyView.log("Exponential numbers are not supported");
//        return;
//    }
    
    //convert numeric
    lowValueToUpdate = lowValueToUpdate *1;
    highValueToUpdate = highValueToUpdate *1;
    
    if( highValueToUpdate <= lowValueToUpdate)
    {
        lowElem.style.backgroundColor = "#FF6666";
        highElem.style.backgroundColor = "#FF6666";
        lowElem.focus();
        storyView.log("Please review this range.  HIGH values must be greater than LOW values");
        return;
    }

    var upperIntLimit = 2147483647;
    var lowerIntLimit = -2147483647;

    //test for limits ...
    if (highValueToUpdate > upperIntLimit || lowValueToUpdate < lowerIntLimit) {
        lowElem.style.backgroundColor = "#FF6666";
        highElem.style.backgroundColor = "#FF6666";
        lowElem.focus();
        storyView.log("This range is out of bounds.  Numbers should fall within the range of [ " + lowerIntLimit + " to " + upperIntLimit + "]");
        return;

    }


    updateColValue(lowElemID, gameCode, lowValueToUpdate);
    updateColValue(highElemID, gameCode, highValueToUpdate);
    raiffaGame.inputsProcessed += 2; //pairwise increment

}

function updateColValue(colToUpdate,gameCode,valueToUpdate)
{
 try {
        var extraSetColVal2 = newMacro("extraSetColVal2");
        addParam(extraSetColVal2, "colName", colToUpdate);
        addParam(extraSetColVal2, "gameCode", gameCode);
        addParam(extraSetColVal2, "colDoubleValue", valueToUpdate);
        addParam(extraSetColVal2, "StoryID", storyView.StoryController.CurrentStory.ID);
        addParam(extraSetColVal2, "tx_id64", TheUte().encode64(gUserCurrentTxID));

        processRequest(extraSetColVal2);

        
    }
    catch (e) {
        storyView.log("col setting2 setting error " + e.description);
    }

}

function removeGameData() {

    var c = confirm("Are you sure you want to do this? \r\n\t This action cannot be undone!");
    if (c) {
        raiffaGame.deleteData();
        location.href = location.href;
    }

}



var raiffaGame =
{
    inputsProcessed: 0, totalInputs: 0,
    processedBlurs: false,
    code: "raiffa",
    refreshController: function(reveal) {
        try {
            var extraRaiffaController = newMacro("extraRaiffaController");
            addParam(extraRaiffaController, "targetDiv", "stratTable");
            gReveal = reveal;
            addParam(extraRaiffaController, "reveal", reveal);
            addParam(extraRaiffaController, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(extraRaiffaController, "gameCode", this.code);
            addParam(extraRaiffaController, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(extraRaiffaController);
        }
        catch (e) {
            alert("controller report error " + e.description);
        }
    },
    refreshReveal: function(reveal) {
        try {
            var extraRaiffaReveal = newMacro("extraRaiffaReveal");
            addParam(extraRaiffaReveal, "targetDiv", "revealTable");
            gReveal = reveal;
            addParam(extraRaiffaReveal, "reveal", reveal);
            addParam(extraRaiffaReveal, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(extraRaiffaReveal, "gameCode", this.code);
            addParam(extraRaiffaReveal, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(extraRaiffaReveal);
        }
        catch (e) {
            alert("reveal report error " + e.description);
        }
    },
    compat: function() {
        //game uses blur events not handled by Gecko
        var isIE = storyView.StoryController.CurrentUser.isIE;

        if (isIE == false) {
            alert("unsupported browser");
        }

    },

    deleteData: function() {
    
        try {
            var extraDeleteGameData2 = newMacro("extraDeleteGameData2");
            addParam(extraDeleteGameData2, "gameCode", this.code);
            addParam(extraDeleteGameData2, "tx_id64", TheUte().encode64(gUserCurrentTxID));

            processRequest(extraDeleteGameData2);


        }
        catch (e) {
            alert("game data deletion error " + e.description);
        }

    },
    
    getInputScreen: function() {

        var storyTuples = storyView.StoryController.CurrentStory.storyTuples;
        var dv = document.createElement("DIV");
        var numAxioms = storyTuples.length;
        var values = new Array();
        var rows = numAxioms;
        this.totalInputs = numAxioms * 2;
        var cols = 3;
        var aGUID = "XYZ";
        var border = 1;
        dv.style.width = "900px";

        for (var i = 0; i < numAxioms; i++) {

            var axe = storyTuples[i];
            var txtQuestion = TheUte().decode64(axe.description) + " (" + axe.units + ")";
            var tmpQuestion = document.createTextNode(txtQuestion);

            values.push(tmpQuestion);
            var val = "";
            var tmpTitle = axe.name + " (" + axe.units + ")";
            var tmpTxtBoxLOW = TheUte().getInputBox(val, axe.code + "_LOW", null, null, null, null);
            values.push(tmpTxtBoxLOW);

            var tmpTxtBoxHIGH = TheUte().getInputBox(val, axe.code + "_HIGH", null, processBlur, null, null);
            values.push(tmpTxtBoxHIGH);

        }

        var g = newGrid2("grdInput", rows, cols, values, border, aGUID);
        g.setHeaderRow("Uncertain Quantity | LOW | HIGH");
        initializeGrid(g);


        dv.appendChild(g.gridTable);

        return dv;

    }

}


//    axioms: [
//        {
//            ID: 1,
//            fact: "Attila the Hun died",
//            code: "AttilaHunDied",
//            description: "The year in which Attila the Hun died",
//            units: "positive for A.D. negative for B.C.",
//            value: "453"
//        },
//        {
//            ID: 2,
//            fact: "US Auto Thefts",
//            code: "USAutoThefts",
//            description: "Number of auto thefts in the U.S. in 1996 ",
//            units: "thousands",
//            value: "1394"
//        },
//        {
//            ID: 3,
//            fact: "US Beef Consumption",
//            code: "USBeefConsumption",
//            description: "U.S. consumption of beef in 1997 ",
//            units: "millions of pounds",
//            value: "25609"
//        },
//        {
//            ID: 4,
//            code: "PlutoOrbit",
//            fact: "Pluto Orbit Duration",
//            description: "Number of years it takes Pluto to circumnavigate the sun ",
//            units: "Earth years",
//            value: "247.7"
//        },
//        {
//            ID: 5,
//            fact: "Beijing to Moscow",
//            code: "BeijingToMoscow",
//            description: "Airline distance from Beijing, China to Moscow, Russia ",
//            units: "miles",
//            value: "3607"
//        }
//    ],