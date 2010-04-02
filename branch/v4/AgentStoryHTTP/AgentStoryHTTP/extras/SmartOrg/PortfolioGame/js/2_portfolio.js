var portfolio = {
	pearl : 0,
	oyster : 0,
	breadAndButter : 0,
	whiteElephant : 0,
	
	addProject : function(type) {
		if (type == "Pearl") {
			this.pearl++;
		}
		else if (type == "Oyster") {
			this.oyster++;
		}
		else if (type == "Bread and Butter") {
			this.breadAndButter++;
		} else {
			this.whiteElephant++;
		}
	},

	clear : function() {
		this.pearl = 0;
		this.oyster = 0;
		this.breadAndButter = 0;
		this.whiteElephant = 0;	
		this.successes = new Array();
		this.unfundedPoints = 0;
		this.unfundedSuccess = 0;
		this.fundedPoints = 0;
		this.fundedSuccess = 0;
		this.fiveBestPoints = 0;
		this.fiveBestSuccess = 0;
		this.successCount = 0;
		this.fundedRoll = 0;
		this.unfundedRoll = 0;
		this.fundedRollsConcluded = 0;
		this.unfundedRollsConcluded = 0;
	},
	clearResults : function() {
		this.successes = [];
		this.unfundedPoints = 0;
		this.unfundedSuccess = 0;
		this.fundedPoints = 0;
		this.fundedSuccess = 0;
		this.fiveBestPoints = 0;
		this.fiveBestSuccess = 0;
		this.fundedRoll = 0;
		this.unfundedRoll = 0;
		this.fundedRollsConcluded = 0;
		this.unfundedRollsConcluded = 0;
	},
		
	addSuccess: function(type, value) {
		this.addConcludedRoll(type, value, "SUCCESS");
	},

	addFailure: function(type) {
		this.addConcludedRoll(type, 0, "FAILURE");
	},
	
	addConcludedRoll : function(type, value, result) {
		if (result=="SUCCESS") {
			this.successes[this.successCount] = value;
			this.successCount++;
		}
		if (type=="FUNDED") {
			if (result=="SUCCESS") {
				this.fundedSuccess++;
				this.fundedPoints += value;
			}
			this.fundedRollsConcluded++;
		}
		
		if (type=="UNFUNDED") {
			if (result=="SUCCESS") {
				this.unfundedSuccess++;
				this.unfundedPoints += value;
			}
			this.unfundedRollsConcluded++;
		} 
	},
	count : 0,
	fundedRoll : 0,
	unfundedRoll : 0,
	fundedRollsConcluded: 0,
	unfundedRollsConcluded: 0,
	findFiveBestSuccesses : function() {
		this.successes.sort(sortDescending);
		this.fiveBestPoints = 0;
		var text = "";
		for (i=0;i<this.successes.length;i++) {
			text += this.successes[i]+",";
		}
		if (this.successes.length > 5) {
			this.fiveBestSuccess = 5;
		} else {
			this.fiveBestSuccess = this.successes.length;
		}
		
		for (i=0;i<this.fiveBestSuccess;i++) {
			this.fiveBestPoints += this.successes[i];
		}
	},

	incrementUnfundedRoll : function() {
		this.unfundedRoll++;
	},
	
	incrementFundedRoll : function() {
		this.fundedRoll++;
	}
}

function sortDescending(a, b) {
	return (parseInt(a)>parseInt(b) ? -1:1);
}