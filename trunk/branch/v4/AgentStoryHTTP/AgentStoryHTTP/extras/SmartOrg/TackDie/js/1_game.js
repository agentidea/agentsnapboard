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

    testFire: function() {
        createFirework(25, 187, 5, 1, null, null, null, null, false, true);
    },

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
            storyView.log("your choice was recorded.");

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
        var dvMsg = document.createElement("DIV");
        dv.appendChild(dvMsg);

        var img = document.createElement("IMG");
        img.style.display = "none";

        var spin = function(ev) {


            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;



            var max = 100;
            var diff = 30;

            var res = Math.floor(Math.random() * max + 1);

            var actualCall = 1;                                 // 1 up 0 down
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


            gameData.Outcome = "WRONG";
            if (gameData.ActualCall == gameData.YourCall)
                gameData.Outcome = "CORRECT";

            updateColValue("TackOrient", TackDieGame.code, actualCall);

            storyView.log(gameData.Outcome + "!!!");

            dvMsg.innerHTML = "Your Result: Your tack ended PIN " + sActualCall + " <br/> and your call was <B>" + gameData.Outcome + "</B>" + "<BR/>";

            img.src = "../extras/SmartOrg/TackDie/images/tack/tack_" + sActualCall + ".jpg";
            img.alt = gameData.Outcome;
            img.style.display = "block";


        };




        var cmdRoll = document.createElement("INPUT");
        cmdRoll.type = "button";
        cmdRoll.value = " Reveal your Tack ";
        cmdRoll.id = "cmdRoll";
        cmdRoll.className = "clsButtonAction2";
        cmdRoll.onclick = spin;


        dv.appendChild(cmdRoll);
        dv.appendChild(img);

        return dv;

    },


    TheDieSimulator: function() {

        var dv = document.createElement("DIV");
        var img = document.createElement("IMG");
        img.style.display = "none";

        var roll = function(ev) {


            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;



            var max = 6;

            /*
            "First to Market", "Second to Market" and "Other" (with probability 1/3, 1/2 and 1/6
            */

            //   if (typeof (soundManagerInit) != 'undefined') soundManagerInit();

            var res = Math.floor(Math.random() * max + 1);

            var simRollOutCome = -1;                                 // 1,2,3
            var sROC = "";

            if (res == 1 || res == 2) {
                simRollOutCome = 1;
                sROC = "First to Market";

                //fireworks ...
                createFirework(25, 187, 5, 1, null, null, null, null, false, true);
                // var r = 4 + parseInt(Math.random() * 16); for (var i = r; i--; ) { setTimeout('createFirework(8,14,2,null,null,null,null,null,Math.random()>0.5,true)', (i + 1) * (1 + parseInt(Math.random() * 1000))); }



            }
            if (res == 3 || res == 4 || res == 5) {
                simRollOutCome = 2;
                sROC = "Second to Market";

                //fireworks ...

                //createFirework(25, 187, 5, 1, null, null, null, null, false, true);

            }
            if (res == 6) {
                simRollOutCome = 3;
                sROC = "Other";

                //fireworks ...

                // createFirework(38, 128, 2, null, 49, 2, 50, 100, false, true);
            }

            elem.style.display = "none";  //hide the roller button


            //DieOrient

            gameData.DieOrient = simRollOutCome;
            gameData.CommercialOutcome = sROC;

            //only record if allowed to
            //alert(gameData.WillInvestFurther + " :: " + gameData.Outcome);

            if (gameData.WillInvestFurther == null || gameData.Outcome == "WRONG") {
                storyView.log("Now you can know what would have happened if you had guessed correctly.");
            }
            else {

                updateColValue("DieOrient", TackDieGame.code, simRollOutCome);
                //this is the order to market ...

                storyView.log("recorded outcome of commercial simulation");
            }

            img.src = "../extras/SmartOrg/TackDie/images/die/" + simRollOutCome + ".jpg";
            img.alt = gameData.CommercialOutcome;
            img.style.display = "block";  //show the outcome of the die


        };


        var cmdRoll = document.createElement("INPUT");
        cmdRoll.type = "button";
        cmdRoll.value = "simulate commercial result";
        cmdRoll.id = "cmdRoll";
        cmdRoll.className = "clsButtonAction2";
        cmdRoll.onclick = roll;


        dv.appendChild(cmdRoll);
        dv.appendChild(img);

        return dv;

    },
    getTackUpDownBinary: function(colName) {

        // this.callbackDiv = dvToAdvanceTo;


        var pulldown_change = function(ev) {
            //delegate ...
            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;
            var selValue = elem.options[elem.selectedIndex].value;
            var elemBits = elem.id.split('_');
            var colName = elemBits[1];

            updateColValue(colName, TackDieGame.code, selValue);
            // storyView.log("Updated colum " + colName + " with a value of " + selValue);
            storyView.log("your choice was recorded.");

            //set game data
            var s = "gameData." + colName + " = selValue;";
            eval(s);


        };


        var id = "sel_" + colName;
        var arrayValsPinUp = [-1, 1, 0];
        var arrayTxt = ["-- choose up or down --", "up", "down"];


        var sel = TheUte().getSelectColor(id, arrayValsPinUp, arrayTxt, null, pulldown_change, 0, "clsSelect");

        var txtContainer = document.createElement("DIV");
        
        var txtNode = document.createTextNode("Please make your call");

        txtContainer.className = "clsActionRequired";

        txtContainer.appendChild(txtNode);
        var vals = new Array();

        vals.push(txtContainer);
        vals.push(sel);

        var oInvestGrid = newGrid2("InvestGrid", 2, 2, vals, 1);
        oInvestGrid.init(oInvestGrid);

        return oInvestGrid.gridTable;


    },
    getInvestFurtherBinary: function(colName) {

        var pulldown_change = function(ev) {
            //delegate ...
            ev = ev || window.event;
            var elem = ev.srcElement || ev.currentTarget;
            var selValue = elem.options[elem.selectedIndex].value;
            var elemBits = elem.id.split('_');
            var colName = elemBits[1];

            updateColValue(colName, TackDieGame.code, selValue);
            //storyView.log("Updated colum " + colName + " with a value of " + selValue);
            storyView.log("your choice was recorded.");


            selValue = selValue * 1;

            if (selValue == 1) {
                //Yes
                //go to next page

            }
            else {
                //NO
                storyView.log("You win $20, for a net win of $15");

            }

            /*
           

If they say "no", display "You win $20, for a net win of $15" and skip the next page. 
            (Or say "You have won $0 for a net of -$5 if they were wrong).



  
            */




            //set game data
            var s = "gameData." + colName + " = selValue;";
            eval(s);


        };


        var id = "sel_" + colName;
        var arrayValsPinUp = [-1, 1, 0];
        var arrayTxt = ["-- choose --", "yes", "no"];

        var sel = TheUte().getSelectColor(id, arrayValsPinUp, arrayTxt, null, pulldown_change, 0, "clsSelect");

        var vals = new Array();
        var txtContainer = document.createElement("DIV");


        var s = "Do you wish to invest further?";
        if (gameData.Outcome == "CORRECT") {
            //can invest further

        }
        else {
            //game over
            s = "You have won $0 for a net of -$5";
            gameData.winnings = s;
            sel.style.display = "none";
            var img = document.createElement("IMG");
            img.src = "./../extras/SmartOrg/TackDie/images/gameOver.jpg";
            txtContainer.appendChild(img);
        }

        var txtNode = document.createTextNode(s);

        txtContainer.className = "clsActionRequired";

        txtContainer.appendChild(txtNode);

        vals.push(txtContainer);
        vals.push(sel);

        var oInvestGrid = newGrid2("InvestGrid", 2, 2, vals, 1);
        oInvestGrid.init(oInvestGrid);

        return oInvestGrid.gridTable;

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


