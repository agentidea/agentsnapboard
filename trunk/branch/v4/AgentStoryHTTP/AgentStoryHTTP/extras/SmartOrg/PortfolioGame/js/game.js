var dirtyFlag = "on";
portfolio.clear();
debugLevel = "ON";
$(document).ready(function() {
		$("#portfolioSelectionStep").show();
    	$("#rollDiceForUnfundedStep").hide();
    	$("#rollDiceForFundedStep").hide();
    	$("#selectFiveBestStep").hide();
    	gameController.loadImagesIntoBrowserCache();
 	});

// save col values by way of Macro framework.
 	function saveColVal(colName, colIntVal) {
 	    try {
 	        var extraSetColVal = newMacro("extraSetColVal");
 	        addParam(extraSetColVal, "colName", colName);
 	        addParam(extraSetColVal, "colIntVal", colIntVal);
 	        addParam(extraSetColVal, "StoryID", storyView.StoryController.CurrentStory.ID);
 	        addParam(extraSetColVal, "tx_id64", TheUte().encode64(gUserCurrentTxID));

 	        processRequest(extraSetColVal);
 	    }
 	    catch (e) {
 	        alert("col setting setting error " + e.description);
 	    }

 	}
 	
    function removeGameData()
    {

        var conf = confirm("Are you sure you want to delete all game data?");
        if (conf == false) {
            alert("no game data was deleted");
            return;
        }


        try {
            var extraDeleteGameData = newMacro("extraDeleteGameData");
            addParam(extraDeleteGameData, "tx_id64", TheUte().encode64(gUserCurrentTxID));

            processRequest(extraDeleteGameData);
        }
        catch (e) {
            alert("data delete error " + e.description);
        }
    }

   

function nextStepToRollDiceForUnfunded(selected) {

    var msg = null;
    
	if (dirtyFlag == "on") {
		resetPastResults();
		
	}
	if (selected.options.length < 5) {
		msg =  "You should have selected exactly 5 projects. Please try again.";
		
	}

	return msg;
	
}

function resetPastResults() {
	portfolio.clearResults();
	showUnfundedRollSummary();
	showFundedRollSummary();
	//var allProjectHtml = document.getElementById("allProjectList");
	//allProjectHtml.innerHTML = "";
}

function disableRollFundedButton() {
	//document.getElementById("rollFundedBtn").disabled = true;
}

function enableRollFundedButton() {
	//document.getElementById("rollFundedBtn").disabled = false;
}

function disablePickFiveButton() {
	//document.getElementById("selectFiveBestBtn").disabled = true;
}

function enablePickFiveButton() {
	//document.getElementById("selectFiveBestBtn").disabled = false;
}

function nextStep(showThis, hideThis) {
	$(showThis).show();
	$(hideThis).hide();
}

function nextStepToRollFunded() {

   
    var _unfundedListHTML = document.getElementById("unfundedProjectList");
    projectSelection.unfundedListHTML = _unfundedListHTML.innerHTML;
	disablePickFiveButton();
}

function nextStepToSelectFiveBest() {
    var _fundedListHTML = document.getElementById("fundedProjectList");
    projectSelection.fundedListHTML = _fundedListHTML.innerHTML;
  
	pickFive.createAllProjectList();
}


function rollUnfunded(itemNumber,itemType) {
	$("#images_unfunded"+itemNumber).show();
	$("#roll"+itemNumber+"_unfunded").hide();
	dieRolls = new DieRolls(itemType);
	var locToDisplay = "roll"+itemNumber+"_unfunded";
	rollAnimatedDiceUnfunded(itemNumber, locToDisplay, dieRolls, showUnfundedRollSummary);
	
	portfolio.incrementUnfundedRoll();
	if (portfolio.unfundedRoll==5) {
		enableRollFundedButton();
	}
}

function rollAnimatedDiceUnfunded(itemNumber, locToDisplay, dieRolls, showSummaryMethod) {
	dice = new AnimatedDice(itemNumber, locToDisplay, dieRolls, "_unfunded", showSummaryMethod, gameController);
	showDiceAnimation(dice);
}

function showUnfundedRollSummary(myRoller, itemNumber) {
	if (myRoller != undefined) {
		if (myRoller.success) {
			portfolio.addSuccess("UNFUNDED", myRoller.rollValue);
			successText = "Success";
			pointsText = myRoller.rollValue;
		} else {
			successText = "Failure";
			pointsText = "0";
		}
		document.getElementById("roll"+itemNumber+"_unfunded_success").innerHTML = successText;
		document.getElementById("roll"+itemNumber+"_unfunded_value").innerHTML = pointsText;
}
try {
    document.getElementById("numberOfSuccesses_unfunded").innerHTML = "<i>Successes: " + portfolio.unfundedSuccess + "</i>";
    document.getElementById("totalValueUnfunded").innerHTML = "<i>Points: " + portfolio.unfundedPoints + "</i>";
} catch (Errorrrr) {
//poor design flaw here
}

}

function rollFunded(itemNumber,itemType) {
	$("#images_funded"+itemNumber).show();
	$("#roll"+itemNumber+"_funded").hide();
	dieRolls = new DieRolls(itemType);
	dieRolls.roll();
	var locToDisplay = "roll"+itemNumber+"_funded";
	rollAnimatedDiceFunded(itemNumber, locToDisplay, dieRolls,showFundedRollSummary);
	portfolio.incrementFundedRoll();
}

function showFundedRollSummary(myRoller, itemNumber) {
	if (myRoller != undefined) {
		if (myRoller.success) {
			portfolio.addSuccess("FUNDED", myRoller.rollValue);
			successText = "Success";
			pointsText = myRoller.rollValue;
		} else {
			portfolio.addFailure("FUNDED");
			successText = "Failure";
			pointsText = "0";
		}
		document.getElementById("roll"+itemNumber+"_funded_success").innerHTML = successText;
		document.getElementById("roll"+itemNumber+"_funded_value").innerHTML = pointsText;
	}
	try {
	    document.getElementById("numberOfSuccesses_funded").innerHTML = "<i>Successes: " + portfolio.fundedSuccess + "</i>";
	    document.getElementById("totalValueFunded").innerHTML = "<i>Points: " + portfolio.fundedPoints + "</i>";
	} catch (Errorrrrrrr) { 
	//more poor design here
	}
	if (portfolio.fundedRollsConcluded==5) {
		dirtyFlag = "off";
		enablePickFiveButton();
	}
}

function rollAnimatedDiceFunded(itemNumber, locToDisplay, dieRolls, showSummaryMethod) {
	dice = new AnimatedDice(itemNumber, locToDisplay, dieRolls, "_funded",showSummaryMethod, gameController);
	showDiceAnimation(dice);
}

function set(id, value) {
	var x = document.getElementById(id);
	x.innerHTML = value;
}