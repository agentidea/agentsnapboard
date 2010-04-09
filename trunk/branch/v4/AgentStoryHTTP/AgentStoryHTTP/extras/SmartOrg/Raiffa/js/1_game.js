﻿


function processBlur(ev) {

    ev = ev || window.event;

    // TheUte().reflect(ev);
    var elem = ev.srcElement;
    var o = document.getElementById(elem.id);



    var colToUpdate = o.id;
    var tblToUpdate = "RaiffaGameData";
    var valueToUpdate = o.value;
    if (o.value.trim().length == 0) return;

    try {
        var extraSetColVal2 = newMacro("extraSetColVal2");
        addParam(extraSetColVal2, "colName", colToUpdate);
        addParam(extraSetColVal2, "tblName", tblToUpdate);
        addParam(extraSetColVal2, "colDoubleValue", valueToUpdate);
        addParam(extraSetColVal2, "StoryID", storyView.StoryController.CurrentStory.ID);
        addParam(extraSetColVal2, "tx_id64", TheUte().encode64(gUserCurrentTxID));

        processRequest(extraSetColVal2);

        raiffaGame.processedBlurs = true;
    }
    catch (e) {
        storyView.log("col setting2 setting error " + e.description);
    }


    //storyView.log("UPDATE TABLE - " + o.id + " " + o.value + " " + gUserCurrentTxID);


}

var raiffaGame =
{
    processedBlurs: false,
   
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




    compat: function() {
        //game uses blur events not handled by Gecko
        var isIE = storyView.StoryController.CurrentUser.isIE;

        if (isIE == false) {
            alert("unsupported browser");
        }

    },

    getInputScreen: function() {


      
       var storyTuples = storyView.StoryController.CurrentStory.storyTuples;
           
        var dv = document.createElement("DIV");
        var numAxioms = storyTuples.length;

        var values = new Array();
        var rows = numAxioms;
        var cols = 3;
        var aGUID = "XYZ";
        var border = 1;


        dv.style.width = "900px";

        for (var i = 0; i < numAxioms; i++) {

            var axe = storyTuples[i];

            var tmpQuestion = document.createTextNode(TheUte().decode64(axe.description));

            values.push(tmpQuestion);
            var val = "";
            var tmpTitle = axe.name + " (" + axe.units + ")";

            var tmpTxtBoxLOW = TheUte().getInputBox(val, axe.code + "_LOW", null, processBlur, null, tmpTitle + " LOW value");
            values.push(tmpTxtBoxLOW);

            var tmpTxtBoxHIGH = TheUte().getInputBox(val, axe.code + "_HIGH", null, processBlur, null, tmpTitle + " HIGH value");
            values.push(tmpTxtBoxHIGH);

        }

        var g = newGrid2("grdInput", rows, cols, values, border, aGUID);
        g.setHeaderRow("Uncertain Quantity | LOW | HIGH");
        initializeGrid(g);


        dv.appendChild(g.gridTable);
      
        return dv;

    }

}

