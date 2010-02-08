

function StrategyChoiceList()
{
	var _stratChoices = new Object;
	var _numStratChoices = 0;
	
	this.count = _numStratChoices;
	this.strategyChoices = _stratChoices;
	this.add = addStrategyChoice;

}

function addStrategyChoice(stratDataElem)
{
	this.count++;
	var evalStr = "this.strategyChoices.item_" + this.count + "=stratDataElem;";
	alert(evalStr);
	eval(evalStr);
}


function strategyDataElement()
{
	var _name = "name";
	var _value = "value";
	this.name = _name;
	this.value = _value;

}

