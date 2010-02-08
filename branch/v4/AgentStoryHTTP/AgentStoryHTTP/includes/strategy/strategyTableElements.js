	/////////////////////////////////
	// Strategy Table Object
	
	function StrategyTable(aguid)
	{
		var _strategyUtils = new StrategyUtils();
		this.utils = _strategyUtils;
		
		var _outerContainer = null;
		var _strategyList = null;
		var _decisionAreaContainer = null;
		var _decisionAreaHeaderHeader = null;
		var _messageArea = null;
		var _body = null;
		this.body = _body;
		
		var _guid = aguid;
		
		this.getGUID = function() { return _guid; }
		
		
		var _printArea = null;
		this.printArea = _printArea;
	
		/*
			FUNDAMENTAL OBJECTS
			
				decision area container ( decisionAreaContainer )
				strategies ( strategyList )
		
		*/
		
		var _saveCallbackREF = null;
		
		this.setSaveCallback = function(rhs)
		{
		    _saveCallbackREF = rhs;
		}
		this.getSaveCallback = function()
		{
		    return _saveCallbackREF;
		}
		
		
		this.decisionAreaContainer = _decisionAreaContainer;
		this.strategyList = _strategyList;
		this.Init = StrategyTableInit;
		this.AddStrategy = StrategyTableAddStrategy;
		this.Display = StrategyTableDisplay;
		
		
		this.outerContainer = _outerContainer;
		
		this.messageArea = _messageArea;
		this.decisionAreaHeaderHeader = _decisionAreaHeaderHeader;
		
		this.AddDecisionHeader = StrategyTableAddDecisionHeader;
		this.CaptureKeys = CaptureKeys;
		
		this.updateScroll = updateScroll;
		this.updateStratScroll = updateStratScroll;
		
		var _decAreaScrollingHeaderDiv = null;
		this.decAreaScrollingHeaderDiv = _decAreaScrollingHeaderDiv;
		
		var _stratListScrollingDiv = null;
		this.stratListScrollingDiv = _stratListScrollingDiv;
		
		var _decAreaScrollingDiv = null;
		this.decAreaScrollingDiv = _decAreaScrollingDiv;
		
		this.GetPrintView = GetPrintView;
		this.setLog = setLog;

	}
	
	function StrategyTableInit(bDebug,aoContainer, asAuthor, asStategyName )
	{
		
		
		//alert("INIT::" + asStategyName);
		this.strategyList = new StrategyList();
		this.strategyList.Init();
		this.decisionAreaHeaderHeader = new DecisionAreaHeaderHeader();
		this.decisionAreaContainer = new DecisionAreaContainer();
		this.decisionAreaContainer.Init(this.decisionAreaHeaderHeader);  //passing the subscriber here as only one
		this.decisionAreaHeaderHeader.Init();
		
		this.messageArea = TheCurrentEditor();
		TheCurrentEditor().setDebug (bDebug);
		TheCurrentEditor().setAuthor ( asAuthor );
		
		this.stratListScrollingDiv = document.createElement("DIV");
		this.stratListScrollingDiv.className = "clsStratListScrollingDiv";
		this.stratListScrollingDiv.appendChild( this.strategyList.table );
		this.stratListScrollingDiv.onscroll = updateStratScroll;

		this.decAreaScrollingHeaderDiv = document.createElement("DIV");
		this.decAreaScrollingHeaderDiv.className = "clsDecisionContainerScrollingHeaderDiv";
		
		this.decAreaScrollingDiv = document.createElement("DIV");
		this.decAreaScrollingDiv.className = "clsDecisionContainerScrollingDiv";
		this.decAreaScrollingDiv.onscroll = this.updateScroll;
		
		this.decAreaScrollingDiv.appendChild( this.decisionAreaContainer.table );
		this.decAreaScrollingHeaderDiv.appendChild( this.decisionAreaHeaderHeader.table );


		
		var decAreaCompound = this.utils.getFormattingTable_1x2(this.decAreaScrollingHeaderDiv,this.decAreaScrollingDiv);

		this.body = this.utils.getFormattingTable_2x1(this.stratListScrollingDiv,decAreaCompound);
		
		var printStage = document.createElement("DIV");
		printStage.className = "clsPrintStage";
		
		this.printArea = printStage;
		
		var lblSTname = document.createElement("DIV");
		lblSTname.id = "stratName";
		lblSTname.className = "clsStratTitle";
		
		
		//hack
		var txtSTname = null;
		
		if(asStategyName != null)
		{
		    txtSTname = document.createTextNode( "Strategy Table - " + this.utils.decode64( asStategyName ) );
		}
		else
		{
		    txtSTname = document.createTextNode( "Strategy Table undefined ");
		}
		
		lblSTname.appendChild( txtSTname );
		
		this.outerContainer = this.utils.getFormattingTable_1x4(this.body,this.printArea,this.messageArea.table,lblSTname);
		
		TheCurrentEditor().Log("Started Strategy Table Editor version " + TheCurrentEditor().VERSION );
		
		//bind the table to it's container
		aoContainer.appendChild(this.outerContainer);
		

		
		
		
	}
	
	function setLog(str)
	{
		
		var tmpLog = null;
		var evalString = "tmpLog = " + str + ";";
		eval(evalString);
		var i = 0;
		var rpt = "";
		
		//rebuild the log line by line... as the assignment does not appear to work as well
		for(i=0;i < tmpLog.length; i++)
		{
			var tmpAuditLine = tmpLog[i];
			TheCurrentEditor().AuditWithAuditLine(tmpAuditLine);
		}
	}
	
	function StrategyTableDisplay()
	{

		if(TheCurrentEditor().mode == TheCurrentEditor().LOG)
		{
			this.utils.clearChildren ( this.printArea );
			
			var panel =	TheCurrentEditor().GetLogPanel(this.printArea );
			
			this.body.style.display = "none";
			this.printArea.style.display = "inline";
		}
		else
		{
			if(TheCurrentEditor().mode == TheCurrentEditor().PRINT)
			{
				this.utils.clearChildren ( this.printArea );
				var panel =	this.GetPrintView(this.printArea);
				//copy the panel HTML to the clipboard hack area.
				TheCurrentEditor().clipboardText.value = panel.parentElement.innerHTML;
				
				this.body.style.display = "none";
				this.printArea.style.display = "inline";
			}
			else
			{
				this.strategyList.DisplayStrategies();
				//alert("here");
				this.decisionAreaContainer.Display();
				//alert("here2");
				TheCurrentEditor().DecorateCurrentWidget();
				this.body.style.display = "inline";
				this.printArea.style.display = "none";
				
				this.decisionAreaContainer.ResetDecisionChoices();
				
				if(TheCurrentEditor().CurrentStrategy != null)
				{
					
					TheCurrentEditor().renderStratChoices();

					//color the strategy box
					var stratInputBox = document.all.item(TheCurrentEditor().CurrentStrategy.key);
					if(stratInputBox != null)
					{
						stratInputBox.className = "cls_" + TheCurrentEditor().CurrentStrategy.color + "";
						TheCurrentEditor().rectangleDimensions.decorateWidget(stratInputBox);
					}
					
				}
			}
			
		}
		
		//difficult to do
		//TheCurrentEditor().FocusOnCurrentWidget();
		
	
	}
	
	function updateScroll()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		
		//sync the scrolling areas with the header
		st.decAreaScrollingHeaderDiv.scrollLeft = srcElem.scrollLeft;
		
	//	if( TheCurrentEditor().mode == TheCurrentEditor().MATRIX ) 
	//		st.stratListScrollingDiv.scrollTop = srcElem.scrollTop;
	}
	
	function updateStratScroll()
	{
		
		var v = window.event;
		var srcElem = v.srcElement;
		
		//sync the strat area scrolls.
	//	if( TheCurrentEditor().mode == TheCurrentEditor().MATRIX )
	//		st.decAreaScrollingDiv.scrollTop = srcElem.scrollTop;
	
	}
	
	function Resize_Event()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		//alert(srcElem.id);
	}
	

	////////////////////////////////
	// central event for key capture
	////////////////////////////////
	function CaptureKeys()
	{
		//may need a semaphore here for the too eager shift keyer!
		var v = window.event;
		var keyCode = v.keyCode;
		
		//TheCurrentEditor().Log(" key pressed " + keyCode);
		
		if( keyCode == 17 )
		{
			if(TheCurrentEditor().AllowControlToggle)
				TheCurrentEditor().CurrentStrategy_ToggleChoice();
		}
	}



	
	function StrategyTableAddStrategy(name,color)
	{
		this.strategyList.AddStrategy (name,color);
	}
	
	function StrategyTableAddDecisionHeader(name)
	{
		this.decisionAreaContainer.AddHeader(name);
	}
	
	
	//upgrade function from v1 to v2
	//not in use, but here just in case
	function upgrade()
	{
		alert("upgrading from v1 to v2");
		
		//strat names
		for(var i = 1;i<st.strategyList.strategyCount+1; i++)
		{
		
			var strat = st.strategyList.GetStrategyByIndex(i);
			strat.name = st.utils.encode64(strat.name);
			
		}
		
		//dah
		for(var i = 1;i<st.decisionAreaContainer.dahCount+1; i++)
		{

			var dah = st.decisionAreaContainer.GetDecisionAreaHeader(i);
			dah.name = st.utils.encode64(dah.name);
			
			//dc
			for(var p = 1;p<dah.dcCount+1;p++)
			{
				var dc = st.decisionAreaContainer.GetDecisionChoice(p,dah);
				dc.name = st.utils.encode64(dc.name);
			}
		}
		
		//log
		for(i=0;i<TheCurrentEditor().auditLog.length;i++)
		{
			var tmpAuditLine = TheCurrentEditor().auditLog[i];
			tmpAuditLine.msg = st.utils.encode64(tmpAuditLine.msg);
		
		}
		
		alert("upgrade complete!");
		
		
		
	}
	
	function GetPrintView(printArea)
	{
		//get alt read only HTML TABLE
		//view of the strategy table in MATRIX style
		var rowIndex = 1;
		var colIndex = 1;

		//what are the dimensions of this table.
		var colNumber = TheCurrentEditor().CountCols();	
		var rowNumber = TheCurrentEditor().CountRows();	
		
		//TheCurrentEditor().Log( colNumber + " :: " + rowNumber );
		//bind grid EARLY to DOM as need it to set values correctly!.
		var grid = this.utils.getGrid(rowNumber + 1,colNumber + 1);
		var panel = this.utils.getFormattingTable_1x1(grid);
		printArea.appendChild( panel );
		
		//get the decision area headers.
		var dahSi = st.decisionAreaContainer.getSequenceArray();
		
		//loop through headers as dictated by sequence array
		//create cells wherein the da column tables can reside
		var dahIndex = 0;
		var key;
		var dahActualColNumber = 0;
		var ActualColMap = new Array();
		
		for(dahIndex=1;dahIndex < st.decisionAreaContainer.dahCount + 1;dahIndex++)
		{
			var dah = dahSi[dahIndex];
			if(dah.visible == true)
			{
				dahActualColNumber++;
				
				//TheCurrentEditor().Log( "about to set dah header name " + dah.name );
				var tmpHeadIndx = ( dahActualColNumber * 1) + 1;
				
				//map col index to header seq number for use in setting actual choice correctly.
				ActualColMap[tmpHeadIndx] = dah.seq;
				
				//$64
				var headWidget = this.utils.setGridCell(1,tmpHeadIndx,TheCurrentEditor().utils.decode64(dah.name) );
				if( headWidget != null)
				{
					//headWidget.parentElement.className="clsGridHead";
					headWidget.parentElement.style.backgroundColor = "#999999";
					headWidget.parentElement.style.color = "Black";
					headWidget.parentElement.style.fontWeight = "bold";
				}
			}
		}
		
		
		// generate a display sequence index
		var dsi = new Array();
		for (i=0; i < st.strategyList.strategyCount ; i++)
		{
			key = "stratId_" + ( i + 1) ;
			eval("var strat = st.strategyList.strategiesNew." + key);
			dsi[strat.seq] = strat;
		}

		
		// Create and insert rows and cells into the first body.
		// according to the sequence number of the strategy
		var actualRowIndex = 0;
		
		for (rowIndex=1; rowIndex<st.strategyList.strategyCount+1; rowIndex++)
		{
			oRow  = document.createElement("TR");
			oCell = document.createElement("TD");
			
			var strat2 = dsi[rowIndex];
			if(strat2.visible)
			{	
				colIndex =1;
				actualRowIndex++;
				//$64
				var stratWidget = this.utils.setGridCell(actualRowIndex + 1,colIndex, TheCurrentEditor().utils.decode64( strat2.name ) );
				
				if(stratWidget == null)
				{
					alert("unable to render " + strat2.name );
					continue;
				
				}
				
			
				//stratWidget.parentElement.parentElement.className = "cls_" + strat2.color + "";
				stratWidget.parentElement.parentElement.style.backgroundColor = strat2.color;
				stratWidget.parentElement.style.fontWeight = "bold";
				
				var scIndex = 1;
				var sc = null;
				//now look into the strategy choices
				if(strat2.strategyChoicesCount > 0)
				{		
					for ( scIndex = 1; scIndex < strat2.strategyChoicesCount+1; scIndex++)
					{
						var indx = "item_" + scIndex;
						eval("sc = strat2.strategyChoices." + indx + ";");
						
						//so now get the choice.
						var tmpSCkey = sc.choiceKey;
						var bits = tmpSCkey.split("~");
						var tmpHeadKey = bits[0];
						var tmpCellKey = bits[1];
						var headerID = tmpHeadKey.split("_")[1];
						var dcID = tmpCellKey.split("_")[1];
						
						var decisionHeader = st.decisionAreaContainer.GetDecisionAreaHeader(headerID);
						var decisionChoice = st.decisionAreaContainer.GetDecisionChoice(dcID,decisionHeader);
						
			
						if( decisionHeader.visible && decisionChoice.visible && sc.visible )
						{
							//need to map seq number with actual matrix hearder position
							//map is built when headers are built above
							var actualColPos = 
							this.utils.GetActualMatrixPosition( ActualColMap, decisionHeader.seq );
							
							var widget = 
							this.utils.setGridCell(actualRowIndex + 1, actualColPos, TheCurrentEditor().utils.decode64( decisionChoice.name ) );
							//$64
						}
					
					}
				}
			}
		}

		return panel;
	}
	
	
