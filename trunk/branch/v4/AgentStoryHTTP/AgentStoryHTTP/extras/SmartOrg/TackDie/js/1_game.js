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

    code: "TackDie",
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


