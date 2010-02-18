var imgCount = 0;
function DelayTable() {
	this.numRules = 0;
	this.addRule = addRule;
	var rules = new Array();
	this.rules = rules;
	var cumes = new Array();
	this.cumes = cumes;
	this.calcCumes = calcCumes;
	this.delayFor = delayFor;
}

function calcCumes() {
	var cume = 0;
	for (i=0;i<this.rules.length;i++) {
		cume += this.rules[i].countThreshold;
		this.cumes[i] = new DelayRule(this.rules[i].delay, cume);
	}
}

function delayFor(count) {
	var i =0;
	while (i<this.rules.length && this.cumes[i].countThreshold<count) {
		i++;
	}	
	return this.cumes[i].delay;
}

function addRule(delay, threshold) {
	var newRule = new DelayRule(delay, threshold);
	this.rules[this.numRules] = newRule;
	this.numRules++;
	this.calcCumes();
}

function DelayRule(delay, threshold) {
	this.delay = delay;
	this.countThreshold = threshold;
}

function PictureShower(gameController) {
	this.gameController = gameController;
	this.pictures = new Array();
	this.loadPictures = loadPictures;	
	this.loadRules = loadRules;
	this.findDelayFor = findDelayFor;
}

function findDelayFor(count) {
	return this.delayTable.delayFor(count);	
}

//function loadRules() {
//	this.delayTable = new DelayTable();
//	this.delayTable.addRule(10,100);
//	this.delayTable.addRule(100,10);
//	this.delayTable.addRule(250,4);
//}
function loadRules() {
    this.delayTable = new DelayTable();
    this.delayTable.addRule(10, 100);
    this.delayTable.addRule(50, 10);
    this.delayTable.addRule(100, 4);
}

function loadPictures(typeOfDie) {
	var num=0;
	this.currentPicture = new Image();
	var die = this.gameController.dieForType(typeOfDie);
	
	var i = 0;
	while (die.hasNextImage()) {
		this.pictures[i] = die.nextImage();
		this.currentPicture.src = this.pictures[i];
		i++;
	}	
}

function AnimatedDice(itemNumber, loc,roller,picSuffix, showSummaryMethod, gameController) {
	this.locToDisplay = loc;
	this.summaryMethod = showSummaryMethod;
	this.itemNo = itemNumber;
	this.roller = roller;
	this.imgCount = 0;
	this.valuePictureShower = new PictureShower(gameController);
	this.valuePictureShower.loadPictures(roller.valueDieType());
	this.valuePictureShower.loadRules();
	
	this.successPictureShower = new PictureShower(gameController);
	this.successPictureShower.loadPictures(roller.successDieType());
	this.successPictureShower.loadRules();
	
	this.valuePictureLabel = "rotating_picture"+itemNumber+picSuffix;
	this.successPictureLabel = "rotating_picture"+itemNumber+"_tetra"+picSuffix;
	
}

function showDiceAnimation(animatedDice) {
	animatedDice.roller.roll();
	setCurrentImagesFrom(animatedDice);
	animatedDice.imgCount++;
	if (animatedDice.imgCount<114) {
		var timeoutFunc = function () { 
			this.showDiceAnimation(animatedDice) 
		};
		setTimeout(timeoutFunc, animatedDice.valuePictureShower.findDelayFor(animatedDice.imgCount));
	} else {
		setCurrentImagesFrom(animatedDice);
		displayFinalResult(animatedDice);
	}
}

function setCurrentImagesFrom(animatedDice) {
	document[animatedDice.valuePictureLabel].src = animatedDice.valuePictureShower.pictures[animatedDice.roller.rollValue-1];
	document[animatedDice.successPictureLabel].src = animatedDice.successPictureShower.pictures[animatedDice.roller.rollSuccess-1];
}

function displayFinalResult(animatedDice) {
	var x=document.getElementById(animatedDice.locToDisplay);
	x.style.visibility = 'hidden';
	var y=document.getElementById(animatedDice.locToDisplay+"_text");
	y.innerHTML = animatedDice.roller.resultText+"&nbsp;&nbsp;"+animatedDice.roller.rollValue;
	animatedDice.summaryMethod(animatedDice.roller, animatedDice.itemNo);
}