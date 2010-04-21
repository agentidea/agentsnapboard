//
//   Tack-Die Tree simulation Game
//


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
        TackDieGame.deleteData();
        location.href = location.href;
    }

}

var TackDieGame =
{

    code: "TackDie", callbackDiv: null,

    getTackUpDownPulldown: function(colName, dvToAdvanceTo) {

        this.callbackDiv = dvToAdvanceTo;


        var pulldown_change = function(ev) {
            //delegate ...
            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;
            var selValue = elem.options[elem.selectedIndex].value;
            var elemBits = elem.id.split('_');
            var colName = elemBits[1];





            updateColValue(colName, TackDieGame.code, selValue);
            storyView.log("Updated colum " + colName + " with a value of " + selValue);

            //set game data
            var s = "gameData." + colName + " = selValue;";
            eval(s);


        };


        var id = "sel_" + colName;
        var arrayValsPinUp = [-1, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10, 0];
        var arrayTxt = ["-- choose probability up/down --", "100% up 0% down", "90% up 10% down", "80% up 20% down", "70% up 30% down", "60% up 40% down", "50% up 50% down", "40% up 60% down", "30% up 70% down", "20% up 80% down", "10% up 90% down", "0% up 100% down"];

        var sel = TheUte().getSelectColor(id, arrayValsPinUp, arrayTxt, null, pulldown_change, 0, "clsSelect");
        return sel;
    },


    TheSimulator: function() {

        var dv = document.createElement("DIV");
        var img = document.createElement("IMG");
        img.style.display = "none";

        var roll = function(ev) {


            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;



            var max = 100;
            var diff = 30;

            var res = Math.floor(Math.random() * max + 1);

            var actualCall = 1; // 1 up 0 down
            var sActualCall = "";

            if (res < diff) {
                actualCall = 1;
                sActualCall = "UP";
            }
            else {
                actualCall = 0;
                sActualCall = "DOWN";
            }

            elem.value = sActualCall;
            elem.style.backgroundColor = "yellow";
            elem.disabled = true;
            elem.style.display = "none";

            gameData.ActualCall = actualCall;

            updateColValue("TackOrient", TackDieGame.code, actualCall);

            storyView.log("Outcome was " + sActualCall + " based of random[" + res + "]");

            img.src = "../extras/SmartOrg/TackDie/images/tack/tack_" + sActualCall + ".jpg";
            img.style.display = "block";


        };


        var cmdRoll = document.createElement("INPUT");
        cmdRoll.type = "button";
        cmdRoll.value = " R O L L ";
        cmdRoll.id = "cmdRoll";
        cmdRoll.className = "clsButtonAction2";
        cmdRoll.onclick = roll;


        dv.appendChild(cmdRoll);
        dv.appendChild(img);

        return dv;

    },
    getTackUpDownBinary: function(colName, dvToAdvanceTo) {

        this.callbackDiv = dvToAdvanceTo;


        var pulldown_change = function(ev) {
            //delegate ...
            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;
            var selValue = elem.options[elem.selectedIndex].value;
            var elemBits = elem.id.split('_');
            var colName = elemBits[1];





            updateColValue(colName, TackDieGame.code, selValue);
            storyView.log("Updated colum " + colName + " with a value of " + selValue);

            //set game data
            var s = "gameData." + colName + " = selValue;";
            eval(s);


        };


        var id = "sel_" + colName;
        var arrayValsPinUp = [-1, 1, 0];
        var arrayTxt = ["-- choose up or down --", "up", "down"];

        var sel = TheUte().getSelectColor(id, arrayValsPinUp, arrayTxt, null, pulldown_change, 0, "clsSelect");
        return sel;
    },

    refreshController: function(reveal) {
        try {

            var macroName = "extra" + this.code + "Controller";
            var extraController = newMacro(macroName);
            addParam(extraController, "targetDiv", "stratTable");
            gReveal = reveal;
            addParam(extraController, "reveal", reveal);
            addParam(extraController, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(extraController, "gameCode", this.code);
            addParam(extraController, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(extraController);
        }
        catch (e) {
            alert(macroName + " report error " + e.description);
        }
    },
    refreshReveal: function(reveal) {
        try {
            var macroName = "extra" + this.code + "Reveal";
            var extraReveal = newMacro(macroName);
            addParam(extraReveal, "targetDiv", "revealTable");
            gReveal = reveal;
            addParam(extraReveal, "reveal", reveal);
            addParam(extraReveal, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(extraReveal, "gameCode", this.code);
            addParam(extraReveal, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(extraReveal);
        }
        catch (e) {
            alert("reveal report error " + e.description);
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

    getInputScreen: function(tupleIndex) {


        var dv = document.createElement("DIV");
        dv.className = "clsPageNote";
        return dv;

    }

}


