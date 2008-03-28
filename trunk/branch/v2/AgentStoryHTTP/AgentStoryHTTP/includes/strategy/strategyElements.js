    //////////////////
	// Strategy List
	
	function StrategyList()
	{
		///////////////
		//strategy DATA
		var _stragegiesNew = new Object;
		this.strategiesNew = _stragegiesNew;
		var _strategyCount = 0;
		//$to do: deprecate, use count instead.
		this.strategyCount = _strategyCount;
		//this.count = _strategyCount;
		this.name = "StrategyList";

		var _strategyUtils = new StrategyUtils();
		this.utils = _strategyUtils;
		
		////////////////////////////////
		//elements for strategy list GUI
		var oTable = document.createElement("TABLE");
		var oTHead = document.createElement("THEAD");
		var oTBody0 = document.createElement("TBODY");
		var oTBody1 = document.createElement("TBODY");
		var oTFoot = document.createElement("TFOOT");
		var oCaption = document.createElement("CAPTION");
		this.table = oTable;
		this.head = oTHead;
		this.body0 = oTBody0;
		this.body1 = oTBody1;
		this.foot = oTFoot;
		this.caption = oCaption;
		this.getStrategy = getStrategy;
		this.GetStrategyByIndex = GetStrategyByIndex;
		this.AddStrategy = AddStrategy;
		this.DisplayStrategies = DisplayStrategies;
		this.Init = Init;
		this.inputBox_focus = inputBox_focus;
		this.inputBox_blur = inputBox_blur;
		this.inputBoxController_up = inputBoxController_up;
		this.inputBoxController_down = inputBoxController_down;
		this.inputBoxController_remove = inputBoxController_remove;
		this.setCaption = setCaption;
		this.setHeader = setHeader;
		this.getInputBoxPanel = getInputBoxPanel;
		

		
		this.StratHide = StratHide;
		this.StratMoveUp = StratMoveUp;
		this.StratMoveDown = StratMoveDown;
		this.AddAndDisplayStrategy = AddAndDisplayStrategy;
		
		this.decorateAllStrategyBoxes = decorateAllStrategyBoxes;
		this.unhideAllStrategies = unhideAllStrategies;
		this.ResetStrategySelection = ResetStrategySelection;
		
		this.getStrategyJSON = getStrategyJSON;
		this.setStrategyJSON = setStrategyJSON;
		this.getLogJSON = getLogJSON;
		
		this.syncStratData = syncStratData;
	
	}
	
	function Init()
	{
		// Insert the created elements into oTable.
		this.table.appendChild(this.head);
		this.table.appendChild(this.body0);
		this.table.appendChild(this.body1);
		this.table.appendChild(this.foot);
		this.table.appendChild(this.caption);
		// Set the table's border width and colors.
		this.table.border=0;
		this.table.className = "stratList";
		
		//set header
		this.setHeader();
	}
	 
	 function setHeader()
	 {
		var oRow,oCell;
		oRow = document.createElement("TR");
		this.head.appendChild(oRow);
		this.head.className = "stratListHead";
		
		// Create and insert cells into the header row.
		for (i=0; i<1; i++)
		{
			oCell = document.createElement("TH");
			oDiv = document.createElement("DIV");
			oDiv.className = "StratLabel";
			oDiv.innerText = "Alternatives";
			oDiv.style.height = TheCurrentEditor().rectangleDimensions.height + 45;
			TheCurrentEditor().Log( "height :: " + TheCurrentEditor().rectangleDimensions.height );
			oCell.appendChild(oDiv);
			oRow.appendChild(oCell);
		}
		
	 
	 
	 }
	
	//add a new strategy
	function AddStrategy(name,color)
	{
		this.strategyCount++;
		var key = "stratId_" + this.strategyCount;
		var strategy = new Strategy();
		strategy.key = key;
		strategy.name = name;
		strategy.seq = this.strategyCount;
		strategy.color = color;
		eval("this.strategiesNew." + key + " = strategy");
		
		//$audit
		TheCurrentEditor().Audit( "Added new Strategy " + name + " [" + key + "] ",
		TheCurrentEditor().ACTION );
	}
	
	function GetStrategyByIndex(index)
	{
		var key  = "stratId_" + index;
		eval(" var strat = this.strategiesNew." + key);
		return strat;
	}
	
	function AddAndDisplayStrategy(s)
	{
		this.AddStrategy(s);
		//redraw
		st.Display();
		
		//this.DisplayStrategies();
		//TheCurrentEditor().RenderCurrentStrategy();
	}
	
	function DisplayStrategies()
	{
		var oRow,oCell,i,key;
		
		//clear header row.
		this.utils.clearChildren(this.head);
		
		//redraw header row
		this.setHeader();
		
		//first clear out any existing rows in the strategy column in the table
		this.utils.clearChildren(this.body0);
		//and the footer
		this.utils.clearChildren(this.body1);
		
		// generate a display sequence index
		var dsi = new Array();
		for (i=0; i < this.strategyCount ; i++)
		{
			key = "stratId_" + ( i + 1) ;
			eval("var strat = this.strategiesNew." + key);
			dsi[strat.seq] = strat;
		}

		
		// Create and insert rows and cells into the first body.
		// according to the sequence number of the strategy
		for (i=1; i<this.strategyCount+1; i++)
		{
			oRow = document.createElement("TR");
			oCell = document.createElement("TD");
			
			var strat2 = dsi[i];
			if(strat2.visible)
			{
				//$64
				var strategyInputBox = this.getInputBoxPanel( TheCurrentEditor().utils.decode64( strat2.name ), strat2.key, this.inputBox_focus, this.inputBox_blur, "decisionAreaHeaderPoppedOut",strat2);
				
				TheCurrentEditor().rectangleDimensions.decorateWidget(strategyInputBox);

				oCell.appendChild(strategyInputBox);
				oRow.appendChild(oCell);
				this.body0.appendChild(oRow);
				//alert(":");
			}
		}
		
		
		if(TheCurrentEditor().mode == TheCurrentEditor().EDIT ||
		 TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT)
		{
			var bottomRow = document.createElement("TR");
			var bottomCell = document.createElement("TD");
			bottomRow.appendChild(bottomCell);
			
			var link = document.createElement("DIV");
			link.innerHTML = "<DIV class='clsFauxButton' title='add new strategy' onclick=\"st.strategyList.AddAndDisplayStrategy('');\">add new strategy</DIV>";
			
			bottomCell.appendChild(link);
			
			/*
			var unhideAllLink = document.createElement("DIV");
			unhideAllLink.innerHTML = "<DIV class='clsFauxButton' onclick=\"st.strategyList.unhideAllStrategies();\">show all</DIV>";
			
			bottomCell.appendChild(unhideAllLink);
			*/
			
			this.body1.appendChild(bottomRow);
		}
		
		
		
	}
	
	
	function decorateAllStrategyBoxes(className)
	{
		var i,key;
		var tmpStrat = null;
	
		
		for (i=1; i < this.strategyCount + 1 ; i++)
		{
			key = "stratId_" + i;
			eval("tmpStrat = this.strategiesNew." + key + ";");

			var elem = document.all.item(tmpStrat.key);
			
			if(elem != null && tmpStrat.visible == true)
			{
				elem.className = className;
				TheCurrentEditor().rectangleDimensions.decorateWidget(elem);
				//alert("decorated " + elem.id);
			}
		}
	}
	
	function unhideAllStrategies()
	{
		var i,key;
		var tmpStrat = null;
	
		for (i=1; i < this.strategyCount + 1 ; i++)
		{
			key = "stratId_" + i;
			eval("tmpStrat = this.strategiesNew." + key + ";");
			tmpStrat.visible = true;
			
			//$audit
		TheCurrentEditor().Audit( "Undo, Revealed Strategy " + tmpStrat.name + ""		,TheCurrentEditor().ACTION );
		
		}	
		this.DisplayStrategies();
		TheCurrentEditor().RenderCurrentStrategy();
	}
	
	function ResetStrategySelection()
	{
		this.decorateAllStrategyBoxes("decisionAreaHeaderPoppedOut");
	}
	
	function StratMoveDown(pk,prefix)
	{
		var currStrategy = this.getStrategy(prefix + "_" + pk);
		var currStrategySeq = currStrategy.seq;
		var nextStrategy = null;
		var i,key;
		var tmpStrat = null;
		var nextStratSeq = currStrategySeq  + 1;
				
		for (i=1; i < this.strategyCount + 1 ; i++)
		{
			key = prefix + "_" + i;
			eval("tmpStrat = this.strategiesNew." + key + ";");
			if(tmpStrat.seq == nextStratSeq)
			{
				nextStrategy = tmpStrat;
				break;
			}
		}

		if(nextStrategy != null)
		{
			var nextStrategySeq = nextStrategy.seq;
			//swap sequence numbers around
			currStrategy.seq = nextStrategySeq;
			nextStrategy.seq = currStrategySeq;
		
			//$audit
		TheCurrentEditor().Audit( "Moved Strategy " + TheCurrentEditor().utils.decode64(currStrategy.name) + " South"		,TheCurrentEditor().ACTION );
		}

	}
	
	
	function StratMoveUp(pk,prefix)
	{
		var currStrategy = this.getStrategy(prefix + "_" + pk);
		var currStrategySeq = currStrategy.seq;
		var prevStrategy = null;
		var i,key;
		var tmpStrat = null;
		var prevStratSeq = currStrategySeq -1;
				
		for (i=1; i < this.strategyCount + 1 ; i++)
		{
			key = prefix + "_" + i;
			eval("tmpStrat = this.strategiesNew." + key + ";");
			if(tmpStrat.seq == prevStratSeq)
			{
				prevStrategy = tmpStrat;
				break;
			}
		}

		if(prevStrategy != null)
		{
			var prevStrategySeq = prevStrategy.seq;
			//swap sequence numbers around
			currStrategy.seq = prevStrategySeq;
			prevStrategy.seq = currStrategySeq;
			
			//$audit
		TheCurrentEditor().Audit( "Moved Strategy " + TheCurrentEditor().utils.decode64(currStrategy.name) + " North"		,TheCurrentEditor().ACTION );
		
			//this.DisplayStrategies();
		}

	}
	
	function StratHide(pk,prefix)
	{
		var currStrategy = this.getStrategy(prefix + "_" + pk);
		currStrategy.visible = false;
		
		//$audit
		TheCurrentEditor().Audit( "Deleted Strategy (" + TheCurrentEditor().utils.decode64(currStrategy.name) + ")"		,TheCurrentEditor().ACTION );
		
		TheCurrentEditor().CurrentStrategy = null;
		TheCurrentEditor().ClearCurrent();
	}
	
	function getStrategy(id)
	{
		var strat = null;
		eval("strat = this.strategiesNew." + id + ";");
		return strat;
	}
	
	function syncStratData(id,val)
	{
		var strat = this.getStrategy(id);
		var origStratName = strat.name;  //base64 
		var origStratNameH = TheCurrentEditor().utils.decode64(origStratName);
		//$64
		var val64 = TheCurrentEditor().utils.encode64(val);
		
		//alert(val);
		//alert(val64);
	//	alert(origStratNameH);
	//	alert(origStratName);
		
		strat.name = val64;
		
		if(origStratName != val64)
		{
			//$audit change
			TheCurrentEditor().Audit( "Renamed Strategy [" + id + "] from ["+ origStratNameH +"] to ["+ val +"]" ,		 TheCurrentEditor().ACTION );
		}
		
	}
	
	
	function getInputBoxPanel(val,id,focusHandler,blurHandler,className,strat2)
	{
		var inputBox = this.utils.getTextArea(val,id,focusHandler,blurHandler,className);
		
		//color the boxes in.
		if(  TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT )
		{
					inputBox.className = "cls_" + strat2.color;
					
		}
					
		
		var inputBoxPanel = this.utils.getFormattingTable_1x1(inputBox);
		return inputBoxPanel;
	}

	
		
	
	function setCaption(s)
	{
		// Set the innerText of the caption and position it at the bottom of the table.
		this.caption.innerText = s;
		this.caption.style.fontSize = "10";
		this.caption.align = "bottom";
	}
	

	
	
	function inputBox_focus()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		TheCurrentEditor().CurrentWidget = srcElem;
		TheCurrentEditor().ClearCurrent();
		TheCurrentEditor().StartStrategyRecording(srcElem.id);
		TheCurrentEditor().DecorateCurrentWidget();
	}
	
	function inputBox_blur()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		//$64
		st.strategyList.syncStratData(srcElem.id,srcElem.value);
	}
	
	function inputBoxController_down()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var strat_pk = bits[2];
		st.strategyList.StratMoveDown(strat_pk,bits[1]);
		TheCurrentEditor().RenderCurrentStrategy();
	}
	
	function inputBoxController_up()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var strat_pk = bits[2];
		st.strategyList.StratMoveUp(strat_pk,bits[1]);
		TheCurrentEditor().RenderCurrentStrategy();
	}
	
	function inputBoxController_remove()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var strat_pk = bits[2];
		st.strategyList.StratHide(strat_pk,bits[1]);
		TheCurrentEditor().RenderCurrentStrategy();
	
	}
	
	
	function setStrategyJSON(str)
	{

		eval("this.strategiesNew = " + str + ";");
		//reflect over the strategies to see how many items we have.
		var numStrats = 0;
		for (var i in this.strategiesNew) 
		{
			numStrats++;   
		}
		this.strategyCount = numStrats-1;
		TheCurrentEditor().Log("Loaded [" + this.strategyCount + "] strategies from JSON");
	}
	
	function getStrategyJSON()
	{
		return this.strategiesNew.toJSONString();
	}
	
	function getLogJSON()
	{
		return TheCurrentEditor().auditLog.toJSONString();
	}
	
	
	// END STRATEGY LIST
	//////////////////////
	



	//////////////////////////////////
	// object representing a STRATEGY 
	function Strategy()
	{
		var _strategyChoices = new Object; 
		this.strategyChoices = _strategyChoices;
		
		var _strategyChoicesCount = 0;
		this.strategyChoicesCount = _strategyChoicesCount;
		
		var name = "";
		var key = "";
		var value = "";
		var seq = 0;
		var _visible = true;
		var _color = null;
		
		this.name = name;
		this.key = key;
		this.value = value;
		this.seq = seq;
		this.visible = _visible;
		this.color = _color;

	}
	
	/////////////////////////////////////////////
	//object representing a STRATEGY CHOICE item
	function StrategyChoiceItem()
	{
		var _visible = true;
		var _seq = -1;
		this.seq = _seq;
		this.visible = _visible;
		var _choiceKey = null;
		this.choiceKey = _choiceKey;
	}
