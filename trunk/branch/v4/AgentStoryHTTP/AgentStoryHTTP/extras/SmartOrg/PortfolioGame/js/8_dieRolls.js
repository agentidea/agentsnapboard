function DieRolls(projectType) {
	this.projectType = projectType;
	if (this.projectType=="Oyster" || this.projectType == "White Elephant") {
		this.blackOrWhite = "WHITE";
	} else {
		this.blackOrWhite = "BLACK";
	}
	if (this.projectType=="Pearl" || this.projectType == "Oyster") {
		this.valueRange = 20;
	} else {
		this.valueRange = 6;
	}
	
	this.roll = roll;
	this.rollBlackDie = rollBlackDie;
	this.rollWhiteDie = rollWhiteDie;
	this.rollValueDie = rollValueDie;
	this.resultText = "";
	this.rollSuccess = 0;
	this.success = false;
	this.rollValue = 0;
	this.valueDieType = valueDieType;
	this.successDieType = successDieType;
}

function successDieType() {
	var successType = "";
	if (this.blackOrWhite == "BLACK") {
		successType = "4-SIDED-BLACK";
	} else {
		successType = "4-SIDED-WHITE";
	}
	return successType;
}

function valueDieType() {
	var dieType = "";
	if (this.valueRange == 6) {
		dieType = "6-SIDED";
	} else {
		dieType = "20-SIDED";
	}
	return dieType;
}

function roll() {
	if (this.blackOrWhite=="BLACK") {
		this.rollBlackDie(rollSuccessDie);
	} else {
		this.rollWhiteDie(rollSuccessDie);
	}
	this.rollValueDie();
}
	
function rollBlackDie(rollThis) {
	this.rollSuccess = rollThis();
	this.resultText = this.rollSuccess;
	if (this.rollSuccess > 1) {
		this.resultText += "(S)";
		this.success = true;
	} else {
		this.resultText += "(F)";
		this.success = false;
	}
}

function rollWhiteDie(rollThis) {
	this.rollSuccess = rollThis();
	this.resultText = this.rollSuccess;
	if (this.rollSuccess == 1) {
		this.resultText += "(S)";
		this.success = true;
	} else {
		this.resultText += "(F)";
		this.success = false;
	}		
}

function rollValueDie() {
	this.rollValue = Math.floor(Math.random()*this.valueRange)+1;
}

function rollSuccessDie() {
	var roll = Math.floor(Math.random()*4)+1;
	return roll;
}