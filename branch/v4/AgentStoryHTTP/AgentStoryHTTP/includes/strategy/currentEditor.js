//
// Current Editor
//

function rectangle()
{
	var _width = 150;
	var _height = 23;
	this.width = _width;
	this.height = _height;
	//slippery when wet for serializing!
	this.decorateWidget = decorateWidget;
}

function decorateWidget(widget)
{
	if(widget != null)
	{
		widget.style.height = this.height;
		widget.style.width = this.width;
	}
}

function getNewRectangle( sizeMode )
{
	var tmpRect = new rectangle();
	var STD_WIDTH = 158;
	switch (sizeMode)
	{

		case "1": 
			tmpRect.height = 23;
			tmpRect.width  = STD_WIDTH;
			break;
		case "2": 
			tmpRect.height = 40;
			tmpRect.width  = STD_WIDTH;
			break;
		case "3": 
			tmpRect.height = 57;
			tmpRect.width  = STD_WIDTH;
			break;
		case "4": 
			tmpRect.height = 80;
			tmpRect.width  = STD_WIDTH;
			break;
		case "5": 
			tmpRect.height = 103;
			tmpRect.width  = STD_WIDTH;
			break;	
		case "6": 
			tmpRect.height = 126;
			tmpRect.width  = STD_WIDTH;
			break;												
		default :
			tmpRect.height = 57;
			tmpRect.width  = STD_WIDTH;
	}
	return tmpRect;
}

//singleton method representing the single CurrentEditor or working area.
var _theCurrentEditor = null;
function TheCurrentEditor()
{
	if( _theCurrentEditor == null)
	{
		_theCurrentEditor = new CurrentEditor();
		_theCurrentEditor.Init();	
	}
	return _theCurrentEditor;
}

function CurrentEditor()
{
	
	
	//current editor
	var _VERSION = "1.1";
	
	var _EDIT = 0;
	var _MULTI_STRAT = 1;
	var _PRINT = 2;
	var _LOG = 3;
	
	this.EDIT = _EDIT;
	this.MULTI_STRAT = _MULTI_STRAT;
	this.PRINT = _PRINT;
	this.LOG = _LOG;
	
	this.VERSION = _VERSION;
		
	//MSG FLAGS
	var _COMMENT = 1;
	var _ACTION = 2;
	var _SAVEPOINT = 3;
	this.COMMENT = _COMMENT;
	this.ACTION = _ACTION;
	this.SAVEPOINT = _SAVEPOINT;
	
	var _mode = _EDIT; //DEFU
	
	
	var _rectangleDimensions = getNewRectangle(0);
	this.rectangleDimensions = _rectangleDimensions;
	
	var _AllowControlToggle = false;
	this.AllowControlToggle = _AllowControlToggle;
	
	var _currentStrategy = null;
	this.CurrentStrategy = _currentStrategy;
	
	var _currentChoice = null;
	this.CurrentChoice = _currentChoice;
	
	var _currentHeader = null;
	this.CurrentHeader = _currentHeader;
	
	var _clipboardText=null;
	this.clipboardText = _clipboardText;
	
	var _currentWidget = null;
	this.CurrentWidget = _currentWidget;
	this.IsDecisionHeaderCurrent	= IsDecisionHeaderCurrent;
	this.IsDecisionChoiceCurrent	= IsDecisionChoiceCurrent;
	this.IsStrategyCurrent			= IsStrategyCurrent;
	
	var _showNavButtons = false;
	this.showNavButtons = _showNavButtons;
	
	var	_isDirty = false;
	this.isDirty = _isDirty;
	var _input = null;
	var _origValue = null;
	
	var currentColor = null;
	var _colors = new Array;
	var _colorsIndex = 0;
	
	this.colors = _colors;
	this.currColor = currentColor;
	this.colorsIndex = _colorsIndex;
	
	this.getCurrentColor = getCurrentColor;
	this.getNextColor = getNextColor;
	
	this.ChangeCurrEditorMode = ChangeCurrEditorMode;
	var _modeSel = null;
	this.modeSel = _modeSel;
		
	var _strategyUtils = new StrategyUtils();
	this.utils = _strategyUtils;
	this.mode = _mode;
	
	var _msgArea = null;
	this.msgArea = _msgArea;
	
	var _logDiv = null;
	this.logDIV = _logDiv;
	var _debug = false;
	this.Debug = _debug;
	this.setDebug = setDebug;
	


	var oTable = null;
	this.table = oTable;
	
	var oContextSensitiveTable = null;
	this.contextSensitiveControlTable = oContextSensitiveTable;
	
	
	this.CurrentEditorInput = _input;
	this.Init = CurrentEditorInit;
	this.CurrentEditor_blur = CurrentEditor_blur;
	this.CurrentEditor_focus = CurrentEditor_focus;
	this.CurrentEditor_modeChange = CurrentEditor_modeChange;
	this.CurrentEditor_choiceSizeChange = CurrentEditor_choiceSizeChange;
	this.CurrentEditor_showHideNav = CurrentEditor_showHideNav;
	this.CurrentEditor_colWidth_Change = CurrentEditor_colWidth_Change;
	this.CurrentEditor_submitComment = CurrentEditor_submitComment;
	
	this.CopyGrid_ToClipboard = CopyGrid_ToClipboard;
	
	this.CurrentEditor_logFilterChange = CurrentEditor_logFilterChange;
	
	this.Log = CurrentEditorDisplay;
	this.Warn = Warn;
	
	var _auditLog = null;
	this.auditLog = _auditLog;
	this.Audit = Audit;
	this.GetLogPanel = GetLogPanel;
	this.AuditWithAuditLine = AuditWithAuditLine;
	
	var _author = null;
	this.Author = _author;
	
	this.setAuthor = setAuthor;
	
	this.SetFocus = SetFocus;
	this.SetBlur = SetBlur;
	this.OrigValue = _origValue;
	this.StartStrategyRecording = StartStrategyRecording;
	this.AddChoice = AddChoice;
	this.renderStratChoices = renderStratChoices;
	this.RenderCurrentStrategy = RenderCurrentStrategy;
	this.ClearAllRendering = ClearAllRendering;
	this.choiceSizeChange = choiceSizeChange;
	this.ColorInMultiStrategy = ColorInMultiStrategy;
	
	//functions moved from strategy object to allow for correct ser/deser
	this.AddNamedChoice = AddNamedChoiceToStrategy;
	this.NamedChoiceExists = NamedChoiceExists;
	
	this.CountCols = CountVisibleDecisionAreaHeaders;
	this.CountRows = CountVisibleStrategies;
	
	this.GetNavPanel = GetNavPanel;
	this.GetControlWidgets = GetControlWidgets;
	this.showLogDetails = showLogDetails;
	var _showDetail = false;
	this.showDetail = _showDetail;
	
	this.loadContextSensitiveControls = loadContextSensitiveControls;
	this.CurrentStrategy_ToggleChoice = CurrentStrategy_ToggleChoice;
	this.CurrentEditor_showHideDcStratIndicator = CurrentEditor_showHideDcStratIndicator;
	this.CurrentEditor_toggleControlKey = CurrentEditor_toggleControlKey;
	
	this.CurrentStrategy_Save = function()
	{
	    st.getSaveCallback()(st);
	}
	
	
	this.NavEvent = NavEvent;
	this.ShowStrategyColorPalette = ShowStrategyColorPalette;
	this.StrategyColorChanged = StrategyColorChanged;
	this.color_palette_cancel = color_palette_cancel;
	
	this.GetCurrentChoiceWidget = GetCurrentChoiceWidget;
	this.GetCurrentChoiceWidgetID = GetCurrentChoiceWidgetID;
	this.ClearCurrentChoice = ClearCurrentChoice;
	this.OutlineColorCurrentChoice = OutlineColorCurrentChoice;

	this.GetCurrentHeaderWidget = GetCurrentHeaderWidget;
	this.GetCurrentHeaderWidgetID = GetCurrentHeaderWidgetID;
	this.ClearCurrentHeader = ClearCurrentHeader;
	this.OutlineColorCurrentHeader = OutlineColorCurrentHeader;
	
	this.GetCurrentStrategyWidget		= GetCurrentStrategyWidget;
	this.GetCurrentStrategyWidgetID		= GetCurrentStrategyWidgetID;
	this.ClearCurrentStrategy			= ClearCurrentStrategy;
	this.OutlineColorCurrentStrategy	= OutlineColorCurrentStrategy;
	
	this.DecorateCurrentWidget = DecorateCurrentWidget;
	this.FocusOnCurrentWidget = FocusOnCurrentWidget;
	
	this.ClearCurrent = ClearCurrent;
	
	var _matrixViewSize = null;
	this.MatrixViewSize = _matrixViewSize;
	
	this.HEADER_OUTLINE_COLOR	= "yellow";
	this.CHOICE_OUTLINE_COLOR	= "yellow";
	this.STRATEGY_OUTLINE_COLOR = "yellow";
}



function GetLogPanel(printArea)
{
	var rptPage = document.createElement("DIV");
	rptPage.className = "clsReportPane";
	
	var panel = this.utils.getFormattingTable_1x1(rptPage);
	printArea.appendChild( panel );
	var i = 0;
	var rpt = "";
	
	for(i=0;i<this.auditLog.length;i++)
	{
		var tmpAuditLine = this.auditLog[i];
		
		var lsMsg =  TheCurrentEditor().utils.decode64(tmpAuditLine.msg);
		//alert("msg :: " + lsMsg);
		
		var AuditClassName = 'clsAuditRegularLine';
		
		if(tmpAuditLine.type == TheCurrentEditor().COMMENT )
		{
			AuditClassName = 'clsAuditCommentLine';
			rpt += "<div class='"+ AuditClassName +"'>" + tmpAuditLine.who + "> " + lsMsg + "</div>";
		
		}
		else
		{
			if(tmpAuditLine.type == TheCurrentEditor().SAVEPOINT )
			{
				AuditClassName = 'clsAuditSavePointLine';	
				rpt += "<div class='"+ AuditClassName +"'>" + tmpAuditLine.who + "> " + lsMsg + "</div>";
				
			}	
			else
			{
				//regular line
				if( TheCurrentEditor().showDetail == true )
				{
					rpt += "<div class='"+ AuditClassName +"'>" + tmpAuditLine.who + "> " + lsMsg + "</div>";
				}
			}
			
		}
			
		
		
		
	}
	
	rptPage.innerHTML = rpt;
	
	return panel;
}


function AuditLine()
{
	var _who = null;
	var _msg = null;
	var _type = null;
	
	this.who = _who;
	this.msg = _msg;
	this.type = _type;
}


function Audit( msg, type )
{
	//so if audit happens, probably the st is now dirty
	this.isDirty = true;
	
	var newAuditLine = new AuditLine();
	newAuditLine.who = this.Author;
	newAuditLine.msg = TheCurrentEditor().utils.encode64(msg);
	//alert("Audit() :: " + msg + " :: " + newAuditLine.msg);
	newAuditLine.type = type;
	
	this.auditLog.push ( newAuditLine );

}

function AuditWithAuditLine( auditLine )
{
	this.auditLog.push ( auditLine );
}

function setAuthor(who)
{
	this.Author = who;
}



function CountVisibleDecisionAreaHeaders()
{
	var strategyTable = st;
	var key = null;
	var dah = null;
	var count = 0;
	var i = 0;
	
	
	 for (i = 1;i< strategyTable.decisionAreaContainer.dahCount + 1;i++)
	 {
		eval("dah =  strategyTable.decisionAreaContainer.DecisionAreaHeaders.dahId_" + i + ";");
		if( dah.visible == true)
			count++;
	 
	 }
	 
	 return count;
}

function CountVisibleStrategies()
{
	
	var key = null;
	var strat = null;
	var count = 0;
	var i = 0;
	
	
	 for (i = 1;i< st.strategyList.strategyCount + 1;i++)
	 {
		key = "stratId_" + i  ;
		eval("strat = st.strategyList.strategiesNew." + key);	
		if( strat.visible == true)
			count++;		 
	 }

	 return count;
}

function setDebug(bDebug)
{
	TheCurrentEditor().Debug = bDebug;
	
	if(bDebug)
		this.logDIV.style.display = "inline";
	else
		this.logDIV.style.display = "none";
	
	
}




function ColorInMultiStrategy()
{
	/*
	
		color in the little squares in the legend in the multi strategy view.
	
	*/

			if( this.mode != this.MULTI_STRAT ) return;
			
			//color in the multi strats
			// generate a display sequence index
			var dsi = new Array();
			var i = 0;
			var key = null;
			for (i=0; i < st.strategyList.strategyCount ; i++)
			{
				key = "stratId_" + ( i + 1) ;
				eval("var strat = st.strategyList.strategiesNew." + key);
				dsi[strat.seq] = strat;
			}//~ for
			
			for (i=1; i<st.strategyList.strategyCount+1; i++)
			{
				var strat2 = dsi[i];
				if(strat2.visible)
				{
					//try to get the strategy choices for this column.
					var k = 0;
					for ( k=1;k<strat2.strategyChoicesCount+1;k++)
					{
						var sc = null;
						var indx = "item_" + k;
						eval("sc = strat2.strategyChoices." + indx + ";");
						if(sc!= null)
						{
							
								var tmpSCkey = sc.choiceKey;
								var bits = tmpSCkey.split("~");
								var tmpHeadKey = bits[0];
								var tmpCellKey = bits[1];
								var dcStratDivToColor = "dcStratDIV_";
								dcStratDivToColor += tmpHeadKey;
								dcStratDivToColor += "_";
								dcStratDivToColor += tmpCellKey;
								dcStratDivToColor += "_";
								dcStratDivToColor += strat2.key;
								
								var widget = document.all.item( dcStratDivToColor );
								if(widget != null)
								{
									if(sc.visible == true)
									{
										//widget.className = "cls_" + strat2.color + "";
										//strat2.color
										//widget.title = strat2.name;
										var colorToUse = strat2.color;
										if(strat2.color == "000000") //HACK
											colorToUse = "70FDA8";
										widget.style.backgroundColor = colorToUse;
										
									}
									else
									{
										widget.style.backgroundColor = "#E0E0E0";
										widget.title = "";
									}

								}

							
							
						}
					}
					
					
				}	//~if viz
					
				
			}//~ for

}


function ClearCurrent()
{
	this.ClearCurrentChoice();
	this.ClearCurrentHeader();
	this.ClearCurrentStrategy();
}



function FocusOnCurrentWidget()
{
	if( TheCurrentEditor().CurrentWidget == null)
	{
		TheCurrentEditor().Log( "no current widget to focus!");
		return;
	}
	
	if(TheCurrentEditor().IsDecisionHeaderCurrent() )
	{
		//alert( TheCurrentEditor().GetCurrentHeaderWidget() );
		TheCurrentEditor().GetCurrentHeaderWidget().focus();
	}
	else
	if(TheCurrentEditor().IsDecisionChoiceCurrent() )
	{
		//alert( TheCurrentEditor().GetCurrentChoiceWidget() );
		TheCurrentEditor().GetCurrentChoiceWidget().focus();
	}
	else
	if(TheCurrentEditor().IsStrategyCurrent() )
	{

	}


}


function DecorateCurrentWidget()
{
	if( TheCurrentEditor().CurrentWidget == null)
	{
		TheCurrentEditor().Log( "no current widget to decorate!");
		return;
	}
	
	
	if(TheCurrentEditor().IsDecisionHeaderCurrent() )
	{
		TheCurrentEditor().OutlineColorCurrentHeader(this.HEADER_OUTLINE_COLOR);
	}
	else
	if(TheCurrentEditor().IsDecisionChoiceCurrent() )
	{
		TheCurrentEditor().OutlineColorCurrentChoice(this.CHOICE_OUTLINE_COLOR);
	}
	else
	if(TheCurrentEditor().IsStrategyCurrent() )
	{
		TheCurrentEditor().OutlineColorCurrentStrategy(this.STRATEGY_OUTLINE_COLOR);
	}
}

function IsDecisionHeaderCurrent()
{
	var ret = false;
	if( this.CurrentWidget != null)
	{
		var key = this.CurrentWidget.id;
		if( key.split("_")[0] == "dahInputBox" )
		ret = true;
	}
	return ret;
}
function IsDecisionChoiceCurrent()
{
	var ret = false;
	if( this.CurrentWidget != null)
	{
		var key = this.CurrentWidget.id;
		if( key.split("_")[0] == "dcTextArea" )
		ret = true;
	}
	return ret;
}
function IsStrategyCurrent()
{
	var ret = false;
	if( this.CurrentWidget != null)
	{
		var key = this.CurrentWidget.id;
		if( key.split("_")[0] == "stratId" )
		ret = true;
	}
	return ret;
}

function OutlineColorCurrentChoice(color)
{
	if( this.GetCurrentChoiceWidget() != null)
		this.GetCurrentChoiceWidget().parentElement.parentElement.parentElement.parentElement.style.backgroundColor = color;
	
}
function ClearCurrentChoice()
{
		//clear out any border color selection
		if( this.CurrentChoice != null)
		{
			this.OutlineColorCurrentChoice("transparent");
			this.CurrentChoice = null;
		}
}

function ClearCurrentHeader()
{
	if( this.CurrentHeader != null)
	{
		this.OutlineColorCurrentHeader("transparent");
		this.CurrentHeader = null;
	}
}

function OutlineColorCurrentHeader(color)
{
	
	if(this.GetCurrentHeaderWidget() != null)
		this.GetCurrentHeaderWidget().parentElement.parentElement.parentElement.parentElement.style.backgroundColor = color;
	
}

function OutlineColorCurrentStrategy(color)
{
	if(this.GetCurrentStrategyWidget() != null)
	{
		try
		{
		this.GetCurrentStrategyWidget().parentElement.parentElement.parentElement.parentElement.style.backgroundColor = color;
		}
		catch(exp)
		{
			TheCurrentEditor().Log(exp.description);
		}
	}
	
}
function ClearCurrentStrategy()
{
		//clear out any border color selection
		if( this.CurrentStrategy != null)
		{
			this.OutlineColorCurrentStrategy("transparent");
			//keep the curr strat around
			//this.CurrentStrategy = null;
		}
}
function GetCurrentStrategyWidget()
{
	var Widget = document.all.item(this.GetCurrentStrategyWidgetID());
	return Widget;
}

function GetCurrentStrategyWidgetID()
{
	var widgetID = null;
	
	if( TheCurrentEditor().CurrentStrategy != null )
		widgetID = TheCurrentEditor().CurrentStrategy.key;
		
	return widgetID;
}



function GetCurrentHeaderWidget()
{
	var Widget = document.all.item(this.GetCurrentHeaderWidgetID());
	return Widget;
}

function GetCurrentHeaderWidgetID()
{
	//alert( "looking for " + TheCurrentEditor().CurrentHeader.key );
	var headerWidgetID = "dahInputBox_dac_" + TheCurrentEditor().CurrentHeader.key;
	return headerWidgetID;
}

function GetCurrentChoiceWidget()
{
	var currChoiceWidgetID = this.GetCurrentChoiceWidgetID();
	
	var choiceWidget = null;
	if(currChoiceWidgetID != null)
	{
		choiceWidget = document.all.item(this.GetCurrentChoiceWidgetID());
	}
	
	return choiceWidget;
}

function GetCurrentChoiceWidgetID()
{
	var choiceWidgetID = null;
	if( TheCurrentEditor().CurrentChoice != null)
	{
		choiceWidgetID = "dcTextArea_" + TheCurrentEditor().CurrentChoice.parentHeaderID + "_" + TheCurrentEditor().CurrentChoice.key;
	}
	return choiceWidgetID;
}		



function StartStrategyRecording(strat)
{
	
	var stratIndx = strat.split("_")[1];
	
	//this.Log("Starting to record strategy " + strat + " " + stratIndx );
	this.CurrentStrategy = st.strategyList.GetStrategyByIndex( stratIndx );
	
	if( TheCurrentEditor().mode == TheCurrentEditor().EDIT 	)
	{
		//this.ClearAllRendering();
		
			//clear out all previously decorated strategies's
			st.strategyList.ResetStrategySelection();
	}
	//clear out all previously decorated dc's
	st.decisionAreaContainer.ResetDecisionChoices();
		
	
		
	if( this.CurrentStrategy.strategyChoicesCount > 0 )
	{
		//existing
		this.renderStratChoices();
	}
	else
	{
		//first time in, record the color
		this.CurrentStrategy.color = TheCurrentEditor().getNextColor();
	}
	
	
	//color the strategy box
	var stratInputBox = document.all.item(this.CurrentStrategy.key);
	if(stratInputBox != null)
	{
		stratInputBox.className = "cls_" + this.CurrentStrategy.color + "";
		//this.rectangleDimensions.decorateWidget(stratInputBox);
	}
	
	
	
}



function ClearAllRendering()
{
	//clear out all previously decorated strategies's
	st.strategyList.ResetStrategySelection();
			
	//clear out all previously decorated dc's
	st.decisionAreaContainer.ResetDecisionChoices();
}

function RenderCurrentStrategy()
{
	if( this.CurrentStrategy != null )
	{
		this.StartStrategyRecording( this.CurrentStrategy.key );
	}
	else
	{
		this.ClearAllRendering();
	}
}

function renderStratChoices()
{
		//render the the decision choices
		//alert("here");
		if(this.CurrentStrategy == null)
		{
			alert("No strat selected, defaulting to first one");
			this.CurrentStrategy = st.strategyList.GetStrategyByIndex( 1 );
			
			var elem = document.all.item(this.CurrentStrategy.key);
			elem.className = "cls_" + this.CurrentStrategy.color;
		}
		
		
		var i = 1;
		for(i=1;i< this.CurrentStrategy.strategyChoicesCount+1; i++ )
		{
			
			var key = "item_" + i;
			var str = "var sc = this.CurrentStrategy.strategyChoices."+key;
			//alert("about to eval " + str);
			eval(str);
			
			if(sc != null)
			{
				if(sc.visible == true)
				{
					var choiceBits = sc.choiceKey.split("~");
					var elemName = "dcTextArea_" + choiceBits[0] + "_" +  choiceBits[1];
					var elem = document.all.item(elemName);
					
					if(elem != null)
						elem.className = "cls_" + this.CurrentStrategy.color;
				}
				if(sc.visible == false)
				{
					this.Log(sc.name + " -- choice was marked " + key + " as invisible");
				}
			}
			else
			{
				alert("sc was NULL");
			}
			
		}
}

	//look in a strategy object to see if a named choice exists
	function NamedChoiceExists(name,strategy)
	{
		var bExists = false;
		for(i=1;i<strategy.strategyChoicesCount+1;i++)
		{
			var val = null;
			eval("val = strategy.strategyChoices.item_" + i);
			
			if(val==null) 
			{
				alert ("no val for item_"  +i);
				return false;
			}
			
			if(val.choiceKey == name)
			{
				bExists = true;
				break;
			}
		
		}
		return bExists;
	}
	
	function AddNamedChoiceToStrategy(id,strategy)
	{
		var msg = "";
		var added = false;
		//TheCurrentEditor().Log("about to add/remove named choice ["+ id +"] to my strategy " + strategy.name);
		
		/*
			id is in form ::: 
			dcTextArea_dahId_3_dcId_3
		
		*/
		var bits = id.split("_");
		if(bits[0] != "dcTextArea")
		{
			TheCurrentEditor().Warn("please only click on a decision choice area");
			return;
		}
		
		var choiceKey = bits[1] + "_" + bits[2] + "~" + bits[3] + "_" + bits[4];
		
		var lChoice = st.decisionAreaContainer.GetDecisionAreaChoiceForHeader(bits[2],bits[4]);
		if( this.NamedChoiceExists(choiceKey,strategy) == true)
		{
			//item is already added
			//remove by setting the choice key to be null.
			//TheCurrentEditor().Log("about to toggle " + choiceKey);
			
			var k = 1;
			for(k=1;k < strategy.strategyChoicesCount+1; k++)
			{
				eval("var sc = strategy.strategyChoices.item_" + k + ";");	
				if(sc.choiceKey == choiceKey)
				{
					//toggle visible.
					if(sc.visible == true)
					{
						sc.visible = false;
						
						msg = 
						"Decision Choice (" + TheCurrentEditor().utils.decode64(lChoice.name) + ") has been REMOVED from strategy (" + TheCurrentEditor().utils.decode64(strategy.name) + ")";
						
						//TheCurrentEditor().Log(msg);
						
						//$audit
						TheCurrentEditor().Audit( msg ,TheCurrentEditor().ACTION );
								
						added = false;
					}
					else
					{
						sc.visible = true;
						
						msg = "Decision Choice (" + TheCurrentEditor().utils.decode64(lChoice.name) + ") has been ADDED to strategy (" + TheCurrentEditor().utils.decode64(strategy.name) + ")";
						
						//TheCurrentEditor().Log(msg);
						
						//$audit
						TheCurrentEditor().Audit( msg ,TheCurrentEditor().ACTION );
												
						added = true;
					}
					break;
				}
			}
		}
		else
		{
			//add item
			TheCurrentEditor().Log("about to add " + choiceKey);
			strategy.strategyChoicesCount++;
			var indx = "item_" + strategy.strategyChoicesCount;
			
			var sc = new StrategyChoiceItem();
			sc.choiceKey = choiceKey;
			sc.seq = this.strategyChoicesCount;
			
			var str = "strategy.strategyChoices." + indx + " = sc;";
			eval(str);	

			msg = "Decision Choice (" + TheCurrentEditor().utils.decode64(lChoice.name) + ") added to strategy (" + TheCurrentEditor().utils.decode64(strategy.name) + ")";
			
			//TheCurrentEditor().Log(msg);
			
			//$audit
			TheCurrentEditor().Audit( msg ,TheCurrentEditor().ACTION );
				
			added = true;	
		}
		
		return added;
	}


function AddChoice(choice)
{
	if(this.CurrentStrategy != null)
	{
		return this.AddNamedChoice(choice,this.CurrentStrategy);
	}
	else
	{
		TheCurrentEditor().Warn("Please select a strategy before attempting to add a choice");
		return false;
	}
}


function Warn(msg)
{
	//this.msgArea.value = msg;
	alert(msg);
}

function CurrentEditorDisplay(msg)
{
	if( TheCurrentEditor().Debug == true )
	{
		
		var ooption = document.createElement("OPTION");
		this.CurrentEditorInput.options.add(ooption);
		ooption.innerText = msg;
	}
}

function SetFocus(o)
{
	alert("Current Editor Setting Focus " + o.value);
	this.OrigValue = o.value;
}

function SetBlur(o)
{
	alert("Current Editor Setting Blur " + o.value);
	if( this.OrigValue != o.value)
	{
		alert("value has changed");
	}
}



function CurrentEditor_logFilterChange()
{
	alert("filter change");
}

function CurrentEditor_submitComment()
{
	
	var commentTextArea = document.all.item( "commentArea" );
	
	var msg = commentTextArea.value;
	if(msg == "") return;
	//$audit
	TheCurrentEditor().Audit( msg ,TheCurrentEditor().COMMENT );
	
	commentTextArea.value = "";
	
	st.Display();
}


function showLogDetails()
{
	st.utils.clearChildren ( st.printArea );
	TheCurrentEditor().showDetail = this.checked;
	TheCurrentEditor().GetLogPanel(st.printArea );
}


function GetControlWidgets( mode )
{

	
	var controlWidgets = null;
	if( mode == this.LOG )
	{

		var commentTextBox = this.utils.getTextArea("","commentArea",null,null,"clsCommentArea");
		var submitComment = this.utils.getButton("submitComment","Add Comment",this.CurrentEditor_submitComment,null,"clsButtonRegular");
		
		var commentWidget = this.utils.getFormattingTable_2x1(commentTextBox,submitComment);
		var loShowDetails = this.utils.getCheckbox("chkShowDetails","show details","check here for verbose log view",this.showLogDetails,"clsChk");
		controlWidgets = this.utils.getFormattingTable_1x2( commentWidget,loShowDetails );
	}
	else
	if( mode == this.EDIT || mode == this.MULTI_STRAT )
	{
	
		var choiceRowSize = this.utils.getSelect("select view size|1 row|2 rows|3 rows|4 rows|5 rows|6 rows",this.CurrentEditor_choiceSizeChange);
		var navController = this.GetNavPanel(); 
		
		controlWidgets = this.utils.getFormattingTable_2x1( choiceRowSize,navController );
		
	}
	else
	if ( mode == this.PRINT )
	{
		var txtClipboard = document.createElement("INPUT");
		txtClipboard.id = "txtClip";
		txtClipboard.style.width = "1pt";
		txtClipboard.style.borders ="none";
		txtClipboard.style.backgroundColor ="transparent";
		
		TheCurrentEditor().clipboardText = txtClipboard;

		var copyToExcel = this.utils.getButton2("cmdCopyGridToClipboard","Copy","Copy grid to clipboard, for pasting say into Excel or Word",this.CopyGrid_ToClipboard,"clsButtonRegular");
		
		
		var choiceColSize = this.utils.getSelect("select column width|100|200|300|400|500|best fit",this.CurrentEditor_colWidth_Change);
		
		controlWidgets = this.utils.getFormattingTable_3x1( choiceColSize,copyToExcel , txtClipboard );
	
	
	}
	
	controlWidgets.className = "clsControlWidgets";
	
	var divControlWidgets = document.createElement("DIV");
	divControlWidgets.className = "cls_divControlWidgets";
	divControlWidgets.appendChild(controlWidgets);
	
	return controlWidgets;


}




function loadContextSensitiveControls( mode )
{
	this.utils.clearChildren( this.contextSensitiveControlTable );
	var stuff = this.GetControlWidgets( mode );
	this.contextSensitiveControlTable.appendChild( stuff );
}

function CurrentEditorInit()
{

	this.CurrentEditorInput = this.utils.getList("currentEditorLog",8,"debugLog");
	this.logDIV = document.createElement("DIV");
	this.logDIV.style.display = "none";
	this.logDIV.appendChild( this.CurrentEditorInput );
	
	this.contextSensitiveControlTable = document.createElement("DIV");
	this.loadContextSensitiveControls( this.mode );
	
	this.modeSel = this.utils.getSelect("strategy editor|strategy table|strategy matrix|log",this.CurrentEditor_modeChange);
	var divAdjHeight  = document.createElement("DIV");
	//divAdjHeight.style.height = "1px";
	
	var modeSelTable = this.utils.getFormattingTable_1x2(divAdjHeight,this.modeSel);
	modeSelTable.cellSpacing = 0;
	modeSelTable.cellPadding = 2;
	
	var modeContextTable = this.utils.getFormattingTable_2x1(modeSelTable,this.contextSensitiveControlTable );
	modeContextTable.className = "clsControlWidgets";
	
	var containerTable = null;
	containerTable = this.utils.getFormattingTable_1x2( modeContextTable ,this.logDIV);
	
	this.table = containerTable;	
	
	this.auditLog = new Array;
	
	
	
	//add colors dynamically to the colors array
	this.colors.push("FFFFFF"); //IGNORED AS ONE BASED
	this.colors.push("A5CBFE");	// was CEFEFE
	this.colors.push("FE9ACE");
	this.colors.push("FECF9C");
	this.colors.push("FEFE9C");
	this.colors.push("CC99FE");
	this.colors.push("CCFECC");
	this.colors.push("000000"); //LAST ONE OUT :)

	this.currColor = this.colors[this.colorsIndex];
	
	
	this.MatrixViewSize = new rectangle();
	this.MatrixViewSize.width  = 100;   
	this.MatrixViewSize.height = 200;  

}

function CopyGrid_ToClipboard()
{
	try
	{
		TheCurrentEditor().clipboardText.focus();
		TheCurrentEditor().clipboardText.select();
		document.execCommand("Copy");
	}
	catch(e)
	{
		alert("Error copying to clipboard " + e.description);
	}
}	

function getCurrentColor()
{
	return this.colors[this.colorsIndex];
}

function getNextColor()
{
	//roll around the colors for now.
	if(this.colors.length == this.colorsIndex + 1)
	{
		this.colorsIndex = 1;
	}
	else
	{
		this.colorsIndex++;
	}
	
	var _currColor = this.getCurrentColor();
	

	return _currColor;
}

function CurrentEditor_showHideNav()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var checked = srcElem.checked;
	TheCurrentEditor().showNavButtons = checked;
	st.Display();
}

function CurrentEditor_showHideDcStratIndicator()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var checked = srcElem.checked;
	
	if(TheCurrentEditor().mode == TheCurrentEditor().EDIT )
	{
		if(checked)
		{
			TheCurrentEditor().mode = TheCurrentEditor().MULTI_STRAT; 
		}
	}
	else
	if(TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT )
	{
		if(checked == false)
		{
			TheCurrentEditor().mode = TheCurrentEditor().EDIT; 
		}
	}
	
	
	
	st.Display();
}

function CurrentEditor_modeChange()
{
	var v = window.event;
	var srcElem = v.srcElement;
	TheCurrentEditor().ChangeCurrEditorMode( srcElem.value );
}

function ChangeCurrEditorMode( mode )
{
//	alert("changing to mode " + mode);
	TheCurrentEditor().mode = mode;
	TheCurrentEditor().loadContextSensitiveControls( mode );
	
	st.Display();
	
	//if(mode == TheCurrentEditor().EDIT )
	//	TheCurrentEditor().ClearAllRendering();
}


function CurrentEditor_choiceSizeChange()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var val = srcElem.value;
	//TheCurrentEditor().Log("Choice Size changed to " + val);
	
	
	TheCurrentEditor().choiceSizeChange(val);
	
	srcElem.selectedIndex = 0;				//reset pulldown to first element.
	
	TheCurrentEditor().ClearAllRendering();
	//st.Display();
}

function CurrentEditor_colWidth_Change()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var val = srcElem.value;
	
	if( val == 6) //auto fit
	{
		
		TheCurrentEditor().MatrixViewSize.width = -1;
		TheCurrentEditor().MatrixViewSize.height = -1;
	}
	else
	if( val == 1 )
	{
		TheCurrentEditor().MatrixViewSize.width = 100;
		TheCurrentEditor().MatrixViewSize.height = 200;
	}
	else
	if( val == 2 )
	{
		TheCurrentEditor().MatrixViewSize.width = 200;
		TheCurrentEditor().MatrixViewSize.height = 300;
	}
	else
	if( val == 3 )
	{
		TheCurrentEditor().MatrixViewSize.width = 300;
		TheCurrentEditor().MatrixViewSize.height = 400;
	}
	else
	if( val == 4 )
	{
		TheCurrentEditor().MatrixViewSize.width = 400;
		TheCurrentEditor().MatrixViewSize.height = 500;
	}
	else
	if( val == 5 )
	{
		TheCurrentEditor().MatrixViewSize.width = 500;
		TheCurrentEditor().MatrixViewSize.height = 600;
	}
	
	srcElem.selectedIndex = 0;				//reset pulldown to first element.
	
	//refresh table.
	st.Display();
}



function ShowStrategyColorPalette()
{

	if( TheCurrentEditor().CurrentWidget == null)
	{
		TheCurrentEditor().Warn( "Please select a strategy to color");
		return;
	}
	//what element is currently been manipulated?
	//if(TheCurrentEditor().IsStrategyCurrent() )
	if( TheCurrentEditor().CurrentStrategy != null ) 
	{

		var paletteDiv = document.all.item("oPalette");
		//display the palette
		paletteDiv.style.display = "inline";
		//hide the regular stage.
		var stage = document.all.item("divStAttachPoint");
		stage.style.display = "none";

		//var currentStrategy = st.strategyList.getStrategy(TheCurrentEditor().CurrentWidget.id);
		var currentStrategy = TheCurrentEditor().CurrentStrategy;
		
		var colorPalette = new colorPickerPalette();
		colorPalette.init(TheCurrentEditor().StrategyColorChanged,currentStrategy);
		
		//colorPalette.table
		var cmdCancel =  TheCurrentEditor().utils.getButton2("cmdCancelColorPalette","cancel","return to strategy table",TheCurrentEditor().color_palette_cancel,"clsButtonRegular");
		
		var colorPaletteContainer = TheCurrentEditor().utils.getFormattingTable_1x2(colorPalette.table, cmdCancel );
		
		
		paletteDiv.appendChild( colorPaletteContainer );
		
	}
}

function color_palette_cancel()
{
	//remove color choice palette.
	var paletteDiv = document.all.item("oPalette");
	TheCurrentEditor().utils.clearChildren(paletteDiv);
	//hide it's div
	paletteDiv.style.display = "none";
	//show the stage
	var stage = document.all.item("divStAttachPoint");
	stage.style.display = "inline";
}

function StrategyColorChanged()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var id = srcElem.id;
	var bits = id.split("_");
	var currentStrategy = st.strategyList.getStrategy( bits[2] + "_" + bits[3] );


	//$audit
	var msg = "";
	msg += " Strategy ("+ currentStrategy.name +") COLOR CHANGED from " + currentStrategy.color + " to " + bits[1];
	TheCurrentEditor().Audit( msg ,TheCurrentEditor().ACTION );

	//change color
	currentStrategy.color = bits[1];

	TheCurrentEditor().color_palette_cancel();
	
	st.Display();
	TheCurrentEditor().StartStrategyRecording( currentStrategy.key );
}

function GetNavPanel()
{
	
	
	
	var saveStrategyCMD = this.utils.getButton2("cmdSaveStrat","save","save",this.CurrentStrategy_Save,"clsButtonRegular");
	
	var addToCurrentStrategyCMD = this.utils.getButton2("cmdAddToCurrStrat","add/remove to/from strategy","add/remove choice to the currently selected strategy",this.CurrentStrategy_ToggleChoice,"clsButtonRegular");
	
	var toggleShiftKey = this.utils.getCheckbox("toggleCtrlKey","ctrl on/off","click control key to add or remove from strategy",this.CurrentEditor_toggleControlKey,"clsChk");	
	
	var removeWidgetCMD = this.utils.getButton2("Hide","Delete","delete selection",this.NavEvent,"clsButtonRegular");
	var moveWidgetUpCMD = this.utils.getButton2("MoveUp","^","move selection up",this.NavEvent,"clsButtonRegular");
	var moveWidgetDownCMD = this.utils.getButton2("MoveDown","v","move selection down",this.NavEvent,"clsButtonRegular");
	var moveWidgetLeftCMD = this.utils.getButton2("MoveLeft","<","move selection left",this.NavEvent,"clsButtonRegular");
	var moveWidgetRightCMD = this.utils.getButton2("MoveRight",">","move selection right",this.NavEvent,"clsButtonRegular");	
		
	var colorStratCMD = this.utils.getButton2("ColorStrategy","Color strategy","color strategy",this.ShowStrategyColorPalette,"clsButtonRegular");	
	
		
		

	
	var panel = this.utils.getFormattingTable_9x1(toggleShiftKey, saveStrategyCMD,addToCurrentStrategyCMD,removeWidgetCMD,moveWidgetUpCMD,moveWidgetDownCMD,moveWidgetLeftCMD,moveWidgetRightCMD,colorStratCMD);
	return panel;
}

function NavEvent()
{
	var v = window.event;
	var srcElem = v.srcElement;
	var id = srcElem.id;
	TheCurrentEditor().Log(id);
	
	if( TheCurrentEditor().CurrentWidget == null)
	{
		TheCurrentEditor().Warn( "Please select an item to move");
		return;
	}
	
	var bits = TheCurrentEditor().CurrentWidget.id.split('_');
	
	//what element is currently been manipulated?
	if(TheCurrentEditor().IsDecisionHeaderCurrent() )
	{
		var dahKey = bits[2] + "_" + bits[3];
		command = "st.decisionAreaContainer.Dah"+ id + "(dahKey);";
	}
	else
	if(TheCurrentEditor().IsDecisionChoiceCurrent() )
	{
		command = "st.decisionAreaContainer.Dc"+ id + "(bits[2],bits[4]);";
	}
	else
	if(TheCurrentEditor().IsStrategyCurrent() )
	{
		command = "st.strategyList.Strat"+ id +"(bits[1],'stratId');";
	}
	
	try
	{	
		if(command != null)
		{
			eval(command);
			st.Display();
		}
	}
	catch(ex)
	{
		TheCurrentEditor().Log( ex.description );
	}
	

}

function CurrentEditor_toggleControlKey()
{
	if( TheCurrentEditor().AllowControlToggle == true)
		TheCurrentEditor().AllowControlToggle = false;
	else
		TheCurrentEditor().AllowControlToggle = true;
		
	TheCurrentEditor().Log("Toggle Ctrl Key set to : " + TheCurrentEditor().AllowControlToggle );
	

}

function CurrentStrategy_ToggleChoice()
{


	if( TheCurrentEditor().CurrentStrategy == null)
	{
		TheCurrentEditor().Warn("Please select a strategy.");
		return;
	}
	
	if( TheCurrentEditor().CurrentChoice == null)
	{
		TheCurrentEditor().Warn("Please select a choice to add to / remove from the current strategy '" + TheCurrentEditor().CurrentStrategy.name + "'");
		return;
	}


	if(TheCurrentEditor().AddChoice(TheCurrentEditor().GetCurrentChoiceWidgetID()) == true )
	{
		TheCurrentEditor().GetCurrentChoiceWidget().className = "cls_" + TheCurrentEditor().CurrentStrategy.color;
		
		
	}
	else
	{
		TheCurrentEditor().GetCurrentChoiceWidget().className = "decisionChoicePoppedOut";
	}
	
	TheCurrentEditor().ColorInMultiStrategy();
		
	

}

function choiceSizeChange(val)
{
	if(val > 0)
	{
		TheCurrentEditor().rectangleDimensions = getNewRectangle(val);
		st.Display();
	}
}

function CurrentEditor_focus()
{
	var v = window.event;
	var srcElem = v.srcElement;
	//srcElem.className = "pushedIn";
}

function CurrentEditor_blur()
{
	var v = window.event;
	var srcElem = v.srcElement;
	//srcElem.className = "poppedOut";
}

////////////////////////////////////////////////////////////////////////////////////
//object representing a strip indicator representing strategies for a decsion choice
function DecisionStrategyIndicator()
{
	var _rootDiv = null;
	this.rootDiv = _rootDiv;
	this.init = DecisionStrategyIndicatorInit;
	var _strategyUtils = new StrategyUtils();
	this.utils = _strategyUtils;
	this.reportDiv = reportDiv;
}

function DecisionStrategyIndicatorInit( dcID, numberOfStrategies )
{
	//alert(numberOfStrategies);
	var bits = dcID.split("_");
	var dcKey = "dcStratDIV_" + bits[1] + "_" + bits[2] + "_" + bits[3] + "_" + bits[4];
	//TheCurrentEditor().Log( " dcKey " + dcKey );
	this.rootDiv = this.utils.getDiv("root_" + dcKey,null,null,null);


	var k = 1;
	var i = 0;
	var stratKey = null;
	var strat = null;
	var key = null;
		
	//create a legend panel for the strategies been used.
	//make sure the legend panel is correctly ordered.
	
	// generate a display sequence index
	var dsi = new Array();
	for (i=0; i < numberOfStrategies ; i++)
	{
		key = "stratId_" + ( i + 1) ;
		eval("strat = strat = st.strategyList.strategiesNew." + key);
		dsi[strat.seq] = strat;
	}
	
	for(k=1;k<numberOfStrategies+1;k++)
	{	
		
		var strat2 = dsi[k];
		
		if(strat2.visible == true)
		{
			var tmpKey = dcKey + "_" + strat2.key;
			var dcStratDIV = this.utils.getDiv(tmpKey,null,"cls_dcStratDIV",null);
			dcStratDIV.ondblclick = this.reportDiv;
			//$64
			dcStratDIV.title = "double click here to add/remove to strategy '" + TheCurrentEditor().utils.decode64(strat2.name) + "'";
			
			//HACK !!!
			//var colorToUse = strat.color;
			//if(strat.color == "000000")
			//	colorToUse = "70FDA8";
				
			//dcStratDIV.style.backgroundColor = colorToUse;
			//dcStratDIV.className = "cls_" + strat.color;
			this.rootDiv.appendChild( dcStratDIV );
		}
	}

}

function reportDiv()
{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
	
		var strat = st.strategyList.GetStrategyByIndex( bits[6] );
		
		//need key in this format:
		//	dcTextArea_dahId_3_dcId_3
		
		var key = "dcTextArea_dahId_";
		key += bits[2];
		key += "_dcId_";
		key += bits[4];

	    TheCurrentEditor().AddNamedChoice(key, strat);
	    		
		//redraw
		st.Display();
		
}


function colorPickerPalette()
{
	var _colors = new Array("A5CBFE","FE9ACE","FECF9C","FEFE9C","CC99FE","CCFECC");
	
// excel palette
// FE99CC 
// FECC99
// FEFE99
// CCFECC
// CCFEFE	
// 99CCFE
// CC99FE

	this.colors = _colors;
	
	var _palette = null;
	this.palette = _palette;
	
	var oTable = document.createElement("TABLE");
	var oTBody0 = document.createElement("TBODY");
	var oCaption = document.createElement("CAPTION");
	
	this.table = oTable;
	this.body = oTBody0;
	this.caption = oCaption;
	this.init = initColorPalette;
}

function initColorPalette(colorCallback,currentStrat)
{
	
	this.table.appendChild(this.body);
	this.table.appendChild(this.caption);
	this.table.className = "clsPaletteBackground";
	this.table.width = "200px";
	
	var i = 0;
	for(i=0;i<this.colors.length;i++)
	{
		var oRow,oCell,oCell2;
		oRow = document.createElement("TR");
		oCell = document.createElement("TD");
		oCell2 = document.createElement("TD");
		var oDiv = document.createElement("DIV");
		oDiv.id = "color_" + this.colors[i] + "_" +  currentStrat.key;
		oDiv.onclick = colorCallback;
		oDiv.style.cursor = "hand";
		oDiv.style.backgroundColor = this.colors[i];
		oDiv.style.width = "35px";
		//oDiv.title =;
		
		oCell.appendChild(oDiv);
		
		//oCell2.innerText = this.colors[i];
		oRow.appendChild(oCell);
		//oRow.appendChild(oCell2);
		this.body.appendChild(oRow);
		
		
	
	}
	
	this.caption.innerText = "Strategy (" + currentStrat.name + ") is currently this color, to change it, please click on a color below.";
		this.caption.style.fontSize = "10";
		this.caption.align = "top";
		this.caption.style.backgroundColor = currentStrat.color;

}

