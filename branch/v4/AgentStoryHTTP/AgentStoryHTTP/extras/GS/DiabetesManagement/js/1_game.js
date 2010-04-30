//
//   Diabetes Management util
//

function  procNum(s) {

        if (s.trim().length == 0)
            return 0;
        else
            return s;

    }


    var DM =
{
    code: "DiabetesManager",
    checkNumeric: function(ev) {

        ev = ev || window.event;
        var elem = ev.srcElement;
        var val = elem.value;
        elem.className = "clsInput";

        if (val.trim().length == 0 || isNaN(val) == false) {
            //do nothing
        } else {
            elem.value = "";
            elem.className = "clsInputValidationError";
            elem.focus();
        }

    },

    localTime: function() {


        var now = new Date();
        var hour = now.getHours();
        var minutes = now.getMinutes();
        var sec = now.getSeconds();
        var year = now.getFullYear();
        var day = now.getDate();
        var month = now.getMonth() + 1; //0 based

        return month + '/' + day + '/' + year + ' ' + hour + ':' + minutes + ':' + sec;



    },
    
    getReport: function(userID,dv) {

    try {
        var extraDMreport = newMacro("extraDMreport");
        addParam(extraDMreport, "targetDiv", dv);
        addParam(extraDMreport, "reportForUserID", userID);
        processRequest(extraDMreport);


    }
    catch (e) {
        storyView.log("report retrieval error:: " + e.description);
    }

    },
    
    getInputScreen: function() {


        var dv = document.createElement("DIV");
        dv.className = "clsDM";
        dv.appendChild(document.createTextNode("Diabetes Management"));

        var _grid = null;

        var clsInput = "clsInput";
        var _sugar = TheUte().getInputBox("", 'txtSugar', null, this.checkNumeric, clsInput, 'sugar level');
        var _InsulinA = TheUte().getInputBox("", 'txtInsulinA', null, this.checkNumeric, clsInput, 'insulin taken');
        var _InsulinB = TheUte().getInputBox("", 'txtInsulinB', null, this.checkNumeric, clsInput, 'insulin taken');
        var _carbs = TheUte().getInputBox("", 'txtCarbs', null, this.checkNumeric, clsInput, 'food carbs taken');
        var _tim = TheUte().getInputBox(this.localTime(), 'txtCarbs', null, null, clsInput, 'local time stamp');
        var _comment = TheUte().getTextArea("", "txtComment", null, null, 'clsTupleTextArea');
        var _submit = TheUte().getButton("cmdSubmit", "Save", null, null, 'clsButtonAction2LGE');
        var _reset = TheUte().getButton("cmdReset", "Reset", null, null, 'clsButtonLGE');

        _submit.onclick = function() {

            //save event in db

            try {
                var extraSaveDM = newMacro("extraSetDMdata");

                addParam(extraSaveDM, "localtime64", TheUte().encode64(_tim.value));


                addParam(extraSaveDM, "sugar", procNum(_sugar.value));
                addParam(extraSaveDM, "insulinA", procNum(_InsulinA.value));
                addParam(extraSaveDM, "insulinB", procNum(_InsulinB.value));
                addParam(extraSaveDM, "carbs", procNum(_carbs.value));
                addParam(extraSaveDM, "comment64", TheUte().encode64(_comment.value));

                addParam(extraSaveDM, "StoryID", storyView.StoryController.CurrentStory.ID);
                addParam(extraSaveDM, "tx_id64", TheUte().encode64(gUserCurrentTxID));

                processRequest(extraSaveDM);


            }
            catch (e) {
                storyView.log("save error::  " + e.description);

            }

        };

        _reset.onclick = function() {

            _sugar.value = "";
            _InsulinA.value = "";
            _InsulinB.value = "";
            _carbs.value = "";
            _comment.value = "";

        };

        var vals = new Array();
        vals.push(document.createTextNode("blood glucose"));
        vals.push(_sugar);
        vals.push(document.createTextNode("Insulin A"));
        vals.push(_InsulinA);
        vals.push(document.createTextNode("Insulin B"));
        vals.push(_InsulinB);
        vals.push(document.createTextNode("Carbs"));
        vals.push(_carbs);
        vals.push(document.createTextNode("Comment"));
        vals.push(_comment);
        vals.push(document.createTextNode("Time"));
        vals.push(_tim);
        vals.push(_submit);
        vals.push(_reset);

        _grid = newGrid2("diabetesEntry", vals.length, 2, vals, 1);
        _grid.init(_grid);

        dv.appendChild(_grid.gridTable);
        return dv;

    }

}
