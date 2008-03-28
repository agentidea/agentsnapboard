	function DecisionAreaContainer()
	{
		//data
		var _decisionAreaHeaders = new Object;
		this.DecisionAreaHeaders = _decisionAreaHeaders;
		var _dahCount = 0;
		this.dahCount = _dahCount;	

		//single subscriber.
		var _decisionAreaContainerREF = null;
		this.decisionAreaContainerREF = _decisionAreaContainerREF;
		
		this.Notify = notifyAllDecisionAreaSubscribers;
		
		//gui
		var oTable = document.createElement("TABLE");
		var oTBodyH = document.createElement("TBODY");
		var oTBody0 = document.createElement("TBODY");
		var oDAHrow = document.createElement("TR");

		this.table = oTable;
		this.bodyH = oTBodyH;
		this.body0 = oTBody0;
		this.dahrow = oDAHrow;
		this.Init = DecisionAreaContainerInit;
		this.AddHeader = AddDecisionHeader;
		this.Display = DecisionAreaContainerDisplay;
		this.AddDecChoice = AddDecChoice;
		
		this.getSequenceArray = getSequenceArray;
		
		this.GetDecisionAreaHeader = GetDecisionAreaHeader;
		this.GetDecisionAreaChoiceForHeader = GetDecisionAreaChoiceForHeader;
		this.GetDecisionChoice = GetDecisionChoice;
		
		this.AddAndDisplayDecisionArea = AddAndDisplayDecisionArea;
		this.DisplayAllDecisionAreas = DisplayAllDecisionAreas;
		this.UpdateDecisionAreaHeader = UpdateDecisionAreaHeader;
		this.UpdateDecisionChoice = UpdateDecisionChoice;
		this.AddAndDisplayDecisionChoice = AddAndDisplayDecisionChoice;
		this.ShowAllDecisionChoice = ShowAllDecisionChoice;
		
		this.DahHide		= DahHide;
		this.DahMoveRight	= DahMoveRight;
		this.DahMoveLeft	= DahMoveLeft;
		
		this.DcHide			= DcHide;
		this.DcMoveUp		= DcMoveUp;
		this.DcMoveDown		= DcMoveDown;
		this.DcMoveLeft		= DcMoveLeft;
		this.DcMoveRight	= DcMoveRight;
		
		this.ResetDecisionChoices = ResetDecisionChoices;
		
		this.getDecisionAreaJSON = getDecisionAreaJSON;
		this.setDecisionAreaJSON = setDecisionAreaJSON;
		
		var _strategyUtils = new StrategyUtils();
		this.utils = _strategyUtils;
	}

	function getDecisionAreaJSON()
	{
		return this.DecisionAreaHeaders.toJSONString();
	}
	
	function notifyAllDecisionAreaSubscribers()
	{
		this.decisionAreaContainerREF.Update( this );
	}
	
	function setDecisionAreaJSON(str)
	{
		eval("this.DecisionAreaHeaders = " + str + ";");
		//reflect over the dechision area headers to see how many items we have.
		var numDecs = 0;
		for (var i in this.DecisionAreaHeaders) 
		{
			numDecs++;   
		}
		this.dahCount = numDecs-1;
		TheCurrentEditor().Log("Loaded [" + this.dahCount + "] decision area headers from JSON");
	}
	
	function ResetDecisionChoices()
	{
		//reset all dcTextArea* input boxes
		var allElems = document.all;
		for(i = 0; i < document.all.length; i++)
		{
			if( document.all(i).id.indexOf("dcTextArea_") != -1 )
			{
				document.all(i).className = "decisionChoicePoppedOut";
				
				TheCurrentEditor().rectangleDimensions.decorateWidget(document.all(i));
				if( TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT )
				{
					//shave off a bit of the Decision choice boxes
					document.all(i).style.width = "138px";
				}
				
				
			}
		}
	}
	
	function DisplayAllDecisionAreas()
	{
		var numDecAreas = this.dahCount;
		var i = 0;
		var key = null; var tmpHeader = null;
		for(i=1;i<numDecAreas+1;i++)
		{
			key = "dahId_" + i;
			eval("tmpHeader = this.DecisionAreaHeaders." + key );
			tmpHeader.visible=true;
		}
		
		this.Display();
	}
	

	
	function DahHide(id)
	{
		eval("var dah = this.DecisionAreaHeaders." + id );
		dah.visible = false;
		
		//$audit
		TheCurrentEditor().Audit( "Deleted Decision Area Header (" + TheCurrentEditor().utils.decode64(dah.name) + ")"		,TheCurrentEditor().ACTION );
		
	}
	
	function DahMoveLeft(id)
	{
		eval("var dah = this.DecisionAreaHeaders." + id );
		if(dah.seq > 1)//can move left
		{
			var itemOnLeftSeq = dah.seq - 1;
			var key;
			var tmpHeader = null;
			var itemOnLeft = null;
			
			for(i=1;i<this.dahCount+1;i++)
			{
				key = "dahId_" + i;
				eval("tmpHeader = this.DecisionAreaHeaders." + key );
				if(tmpHeader.seq == itemOnLeftSeq)
				{
					itemOnLeft = tmpHeader;
					break;
				}
			}
			
			if(itemOnLeft != null)
			{
				itemOnLeft.seq = dah.seq;
				dah.seq = itemOnLeftSeq;
				//$audit
				
		TheCurrentEditor().Audit( "Decision Area Header [" + TheCurrentEditor().utils.decode64(dah.name) + "] moved West"		,TheCurrentEditor().ACTION );
				//$this.Display();
			}
		}
	}	
	
	function DahMoveRight(id)
	{
		eval("var dah = this.DecisionAreaHeaders." + id );
		if(dah.seq < this.dahCount) //can move right
		{
			var itemOnRightSeq = dah.seq + 1;
			var key;
			var tmpHeader = null;
			var itemOnRight = null;
			
			for(i=1;i<this.dahCount+1;i++)
			{
				key = "dahId_" + i;
				eval("tmpHeader = this.DecisionAreaHeaders." + key );
				if(tmpHeader.seq == itemOnRightSeq)
				{
					itemOnRight = tmpHeader;
					break;
				}
			}
			
			if(itemOnRight != null)
			{
				itemOnRight.seq = dah.seq;
				dah.seq = itemOnRightSeq;
				
				//$audit
		TheCurrentEditor().Audit( "Decision Area Header " + TheCurrentEditor().utils.decode64(dah.name) + " moved East"		,TheCurrentEditor().ACTION );
		
				//$$this.Display();
			}
		}
	}


	function DcHide(headerID,dcID)
	{
		
		var decisionChoice = st.decisionAreaContainer.GetDecisionAreaChoiceForHeader(headerID,dcID);
		decisionChoice.visible = false;
		//$audit
		TheCurrentEditor().Audit( "Deleted Decision Choice (" + TheCurrentEditor().utils.decode64(decisionChoice.name) + ")"		,TheCurrentEditor().ACTION );
		
		TheCurrentEditor().ClearCurrent();
	}
	
	function DcMoveUp(headerID,dcID)
	{
		//alert(headerID + " ::: " + dcID);
		//1 ::: 2
		var prevDC = null;
		var decisionHeader = st.decisionAreaContainer.GetDecisionAreaHeader(headerID);
		var decisionChoice = st.decisionAreaContainer.GetDecisionChoice(dcID,decisionHeader);

		var origSeq = decisionChoice.seq;
		var prevChoiceSeq = origSeq - 1;
		
		var i = 1;
		for(i = 1;i< decisionHeader.dcCount + 1; i++)
		{
			var dcTmp = st.decisionAreaContainer.GetDecisionChoice(i,decisionHeader);
			if(dcTmp.seq == prevChoiceSeq)
			{
				prevDC = dcTmp;
				break;
			}
		}
		
		if(prevDC != null)
		{
			decisionChoice.seq = prevDC.seq;
			prevDC.seq = origSeq;
			//$audit
		TheCurrentEditor().Audit( "Decision Choice " + TheCurrentEditor().utils.decode64(decisionChoice.name) + " moved North"		,TheCurrentEditor().ACTION );
		}	
	}
	
	function DcMoveDown(headerID,dcID)
	{
		var nextDC = null;
		var decisionHeader = st.decisionAreaContainer.GetDecisionAreaHeader(headerID);
		var decisionChoice = st.decisionAreaContainer.GetDecisionChoice(dcID,decisionHeader);

		var origSeq = decisionChoice.seq;
		var nextChoiceSeq = origSeq + 1;
		
		var i = 1;
		for(i = 1;i< decisionHeader.dcCount + 1; i++)
		{
			var dcTmp = st.decisionAreaContainer.GetDecisionChoice(i,decisionHeader);
			if(dcTmp.seq == nextChoiceSeq)
			{
				nextDC = dcTmp;
				break;
			}
		}
		
		if(nextDC != null)
		{
			decisionChoice.seq = nextDC.seq;
			nextDC.seq = origSeq;
			//$audit
		TheCurrentEditor().Audit( "Decision Choice " + TheCurrentEditor().utils.decode64(decisionChoice.name) + " moved South"		,TheCurrentEditor().ACTION );
		}
	}
	function DcMoveLeft(headerID,dcID)
	{
		this.DahMoveLeft("dahId_" + headerID);
		//$audit
		TheCurrentEditor().Audit( "Decision Choice [" + dcID + "] moved West"		,TheCurrentEditor().ACTION );
	}
	function DcMoveRight(headerID,dcID)
	{
		this.DahMoveRight("dahId_" + headerID);
		//$audit
		TheCurrentEditor().Audit( "Decision Choice [" + dcID + "] moved East"		,TheCurrentEditor().ACTION );
	}		
	
	function UpdateDecisionAreaHeader(id,val)
	{
		eval("var dah = this.DecisionAreaHeaders." + id );
		//alert("new val " + val);
		
		var origName = dah.name;  //base64
		//$64
		var newName =  TheCurrentEditor().utils.encode64( val );
		
		if(origName != newName)
		{
			//modify new value
			dah.name = newName;
			var oldVal = TheCurrentEditor().utils.decode64( origName );

			
			//$audit
		 TheCurrentEditor().Audit( "Modified Decision Header [" + id + "] from [" + oldVal + "] to new value [" + val + "]" ,
		 TheCurrentEditor().ACTION );
		 
		}
		
		this.Notify();
	}
	
	function UpdateDecisionChoice(id,val)
	{
		var bits = id.split('_');
		var dahKey = bits[1] + "_" + bits[2];
		var dcId =  bits[3] + "_" + bits[4];
		//$64
		var val64 = TheCurrentEditor().utils.encode64(val);
		
		eval("var dah = this.DecisionAreaHeaders." + dahKey );
		eval("var dc = dah.DecisionChoices." + dcId);
		
		
		if(dc.name != val64 )
		{
			//$audit change
			TheCurrentEditor().Audit( "Modified Decision Choice [" + id + "] from [" + TheCurrentEditor().utils.decode64( dc.name ) + "] to new value [" + val + "]" ,
			TheCurrentEditor().ACTION );
		 }
		
		dc.name = val64;

	}
	
	function AddAndDisplayDecisionChoice(name,dahKey)
	{
		var dcKey = this.AddDecChoice(name,dahKey);
		st.Display();
		
	}
	
	function ShowAllDecisionChoice(dahKey)
	{
		//alert("show all for " + dahKey);
		eval("var dah = this.DecisionAreaHeaders." + dahKey );

		var dcCount = dah.dcCount;
		var i = 0; var key = null;
		for(i=1;i<dcCount+1;i++)
		{
			key = "dcId_" + i;
			eval("var dc = dah.DecisionChoices." + key + ";");
			dc.visible = true;
		}
	
		this.Display();
	}
	
	function AddDecChoice(name,dahKey)
	{
		eval("var dah = this.DecisionAreaHeaders." + dahKey );
		dah.dcCount++;
		
		var key = "dcId_" + dah.dcCount;
		var dc = new DecisionAreaChoice();
		
		dc.key = key;
		dc.name = name;
		//dc.value = name;
		dc.seq = dah.dcCount;
		dc.parentHeaderID = dah.key;
		eval("dah.DecisionChoices." + key + " = dc");
		
		//$audit
		TheCurrentEditor().Audit( "Decision Choice " + dc.name + " Added"
		,TheCurrentEditor().ACTION );
		
		return key;
	}

	
	function AddAndDisplayDecisionArea(name)
	{
		this.AddHeader(name);
		//redraw
		st.Display();
	}
	
	function AddDecisionHeader(name)
	{
		this.dahCount++;
		
		var key = "dahId_" + this.dahCount;
		
		var dah = new DecisionAreaHeader();
		dah.key = key;
		dah.name = name;
		dah.value = name;
		dah.seq = this.dahCount;

		eval("this.DecisionAreaHeaders." + key + " = dah");
		
		//$audit
		TheCurrentEditor().Audit( "Added New Decision Header [" + key + "]",
		 TheCurrentEditor().ACTION );
		
	}

	function GetDecisionAreaHeader(index)
	{
		var key = "dahId_" + index;
		eval("var dahTmp = this.DecisionAreaHeaders." + key + ";");
		return dahTmp;
	}
	
	function GetDecisionAreaChoiceForHeader(headerIndex,choiceIndex)
	{
		var dah = this.GetDecisionAreaHeader(headerIndex);
		var dac = this.GetDecisionChoice(choiceIndex,dah);
		return dac;
	}
		
	function GetDecisionChoice(index,dah)
	{
		var key = "dcId_" + index;
		eval("var dcTmp = dah.DecisionChoices." + key + ";");
		return dcTmp;
	}
	
	function getSequenceArray()
	{
		var dsi = new Array();
		for (i=0; i < this.dahCount ; i++)
		{
			eval("var dahTmp = this.DecisionAreaHeaders.dahId_" + (i+1) );
			dsi[dahTmp.seq] = dahTmp;
		}
		return dsi;
	}
	
	function DecisionAreaContainerDisplay()
	{
		
		//clear out all children of the dah row!
		this.utils.clearChildren(this.dahrow);
		
		//create a sequence array 
		var dsi = this.getSequenceArray();
		
		//alert(dsi);
		//loop through headers as dictated by sequence array
		//create cells wherein the da column tables can reside
		var i = 0;
		var key;
		
		for(i=1;i<this.dahCount + 1;i++)
		{
			var dah = dsi[i];
			
			if(dah.visible == true)
			{
				var dacCell = this.utils.getCell();
				var dac = new DecisionAreaColumn();
				dac.Init(dah);
				dacCell.appendChild(dac.table);
				this.dahrow.appendChild(dacCell);
			}
		}
		
		this.Notify();
		
		
		if( TheCurrentEditor().mode == TheCurrentEditor().EDIT
		 || TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT )
		{
			//add the final column for the add dah controller.
			var addColCell = this.utils.getCell();

			
			var link = document.createElement("DIV");
			link.innerHTML = "<DIV class='clsFauxButton' onclick=\"st.decisionAreaContainer.AddAndDisplayDecisionArea('');\">add new area</DIV>";
			addColCell.appendChild(link);
			
			/*
			var link2 = document.createElement("DIV");
			link2.innerHTML = "<DIV class='clsFauxButton' onclick=\"st.decisionAreaContainer.DisplayAllDecisionAreas();\">show all</DIV>";
			addColCell.appendChild(link2);
			*/
			
			
			this.dahrow.appendChild(addColCell);
		}
		
		//TheCurrentEditor().RenderCurrentStrategy();	
		
		TheCurrentEditor().ColorInMultiStrategy();		
		

		
		
	}
	
	function DecisionAreaContainerInit(aDecisionAreaContainerREF)
	{
		this.table.appendChild(this.bodyH);
		this.table.appendChild(this.body0);
		this.table.border =0;
		
		this.decisionAreaContainerREF = aDecisionAreaContainerREF;
		
		var nnm = this.table.attributes;
		var namedItem = document.createAttribute("cellpadding");
		namedItem.value = 0;
		nnm.setNamedItem(namedItem);

		var namedItem2 = document.createAttribute("cellspacing");
		namedItem2.value = 0;
		nnm.setNamedItem(namedItem2);
		
		
		this.body0.appendChild(this.dahrow);

	}

//-------------------------------------------  end decision area container.
	
	////////////////////////////////////////////////
	//object representing a GUI DECISION AREA COLUMN
	function DecisionAreaColumn()
	{
		var _strategyUtils = new StrategyUtils();
		this.utils = _strategyUtils;
		
		var oTable = document.createElement("TABLE");
		var oTBody0 = document.createElement("TBODY");
		var oTBody1 = document.createElement("TBODY");
		
		this.table = oTable;
		this.body0 = oTBody0;
		this.body1 = oTBody1;
		this.Init = DecisionAreaColumnInit;
		
		this.getDahPanel = getDahPanel;
		this.getDcPanel = getDcPanel;
		this.dahController_right = dahController_right;
		this.dahController_left = dahController_left;
		this.dahController_remove = dahController_remove;
		
		this.dcController_up = dcController_up;
		this.dcController_down = dcController_down;
		this.dcController_hide = dcController_hide;
		this.dcController_addChoiceToCurrentStrategy = dcController_addChoiceToCurrentStrategy;
		
		this.dcInputBox_focus = dcInputBox_focus;
		this.dcInputBox_blur = dcInputBox_blur;
		
		this.renderStratCells = renderStratCells;
		this.getStratChoiceCountForCol = getStratChoiceCountForCol;
		this.getDcPanelReadOnly = getDcPanelReadOnly;
		
		this.nullEvent = nullEvent;
	}
	
	function nullEvent()
	{}
	
	function DecisionAreaColumnInit(dah)
	{
		//$64
		var dahName = TheCurrentEditor().utils.decode64(dah.name);
		var id = dah.key;
		
		this.table.appendChild(this.body0);
		this.table.appendChild(this.body1);
		
		
		if(TheCurrentEditor().mode == TheCurrentEditor().EDIT || 
		TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT)
		{
			this.table.border = 0;
			this.table.cellspacing = 0;
			this.table.cellpadding = 0;
		}
		else
		{
			this.table.border = 1;
			this.table.cellspacing = 0;
			this.table.cellpadding = 0;
		}
		
		var headRow = this.utils.getRow();
		headRow.className = "DecisionAreaHeader";
		var headCell = this.utils.getCell();
		headRow.appendChild(headCell);
		/*
		if ( TheCurrentEditor().mode == TheCurrentEditor().MATRIX )
		{
			var dahInputPanel = this.getDahPanel(dahName,"dac_" + id); 
			headCell.appendChild(dahInputPanel);
			
			this.body0.appendChild(headRow);
			
			this.renderStratCells(id);
			
		}
		else
		*/
		if ( TheCurrentEditor().mode == TheCurrentEditor().EDIT 
		|| TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT)
		{
			
			
			var dahInputPanel = this.getDahPanel(dahName,"dac_" + id); 
			headCell.appendChild(dahInputPanel);
			
			this.body0.appendChild(headRow);
			
			//are there any decision area choices?
			if( dah.dcCount > 0 )
			{
				var i = 1;
				var key = null;
				
				// generate a display sequence index
				var dsi = new Array();
				for(i=1;i<dah.dcCount+1;i++)
				{
					key = "dcId_" + i;
					eval("var dc = dah.DecisionChoices." + key );
					dsi[dc.seq] = dc;
				}
				
				for(i=1;i<dah.dcCount+1;i++)
				{
					var dc = dsi[i];
					if(dc.visible == true)
					{
						var dcPanel = this.getDcPanel(TheCurrentEditor().utils.decode64(dc.name),id + "_" + dc.key);
						this.body0.appendChild( dcPanel );
					}
				}
			}
			
			//add the ADD NEW CHOICE button.
			var addDcRow = this.utils.getRow();
			var addDcCell = this.utils.getCell();
			addDcRow.appendChild( addDcCell );
			var link = document.createElement("DIV");
			link.innerHTML = "<DIV class='clsFauxButton' title='add new choice' onclick=\"st.decisionAreaContainer.AddAndDisplayDecisionChoice('','"+dah.key+"');\">add new choice</DIV>";
			addDcCell.appendChild(link);
			

			this.body0.appendChild(addDcRow);
			
			//add the show all choices button.
			/*
			var addDcRow2 = this.utils.getRow();
			var addDcCell2 = this.utils.getCell();
			addDcRow2.appendChild( addDcCell2 );
			var link2 = document.createElement("DIV");
			link2.innerHTML = "<DIV class='clsFauxButton' onclick=\"st.decisionAreaContainer.ShowAllDecisionChoice('"+dah.key+"');\">show all</DIV>";
			addDcCell2.appendChild(link2);
			
			this.body0.appendChild(addDcRow2);
			*/
		}//~ end if
		
		

		
		

	}
	
	function renderStratCells(id)
	{
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
		for (i=1; i<st.strategyList.strategyCount+1; i++)
		{
			oRow = document.createElement("TR");
			oCell = document.createElement("TD");
			
			var strat2 = dsi[i];
			if(strat2.visible)
			{
				

				var totalCount = this.getStratChoiceCountForCol(strat2,id);
				
				if( totalCount == 0 )
				{

					var blankDC = this.getDcPanelReadOnly(null,null,strat2.color,null);
					oCell.appendChild(blankDC);
					oRow.appendChild(oCell);
					this.body0.appendChild(oRow);

				}
				
				
				//try to get the strategy choices for this column.
				var k = 0;
				for ( k=1;k<strat2.strategyChoicesCount+1;k++)
				{
					var sc = null;
					//TheCurrentEditor().Log( strat2.key );
					
					var indx = "item_" + k;
					eval("sc = strat2.strategyChoices." + indx + ";");
					if(sc!= null)
					{
						if(sc.visible == true)
						{
							var tmpSCkey = sc.choiceKey;
							var bits = tmpSCkey.split("~");
							var tmpHeadKey = bits[0];
							var tmpCellKey = bits[1];
							if(tmpHeadKey == id)
							{
								//item was part of strategy for this row.
								var decisionHeader = st.decisionAreaContainer.GetDecisionAreaHeader(tmpHeadKey.split("_")[1] );
								var decisionChoice = st.decisionAreaContainer.GetDecisionChoice(tmpCellKey.split("_")[1],decisionHeader);
								

								if( decisionChoice.visible == false ) break;
								
								var dcTextBoxReadOnly = this.getDcPanelReadOnly(TheCurrentEditor().utils.decode64(decisionChoice.name),null,strat2.color,TheCurrentEditor().utils,decode64(decisionChoice.name));
								
								
								//TheCurrentEditor().rectangleDimensions.decorateWidget(strategyInputBox);
								if(totalCount > 1)
								{
					var resizeFactor = ( TheCurrentEditor().rectangleDimensions.height / totalCount ) - 2;
					
									//alert("resize to " + resizeFactor + " from " + dcTextBoxReadOnly.style.height);
									//alert(dcTextBoxReadOnly.innerHTML);
									dcTextBoxReadOnly.style.height = resizeFactor
									dcTextBoxReadOnly.firstChild.style.height = resizeFactor;
									dcTextBoxReadOnly.firstChild.firstChild.style.height = resizeFactor;
									dcTextBoxReadOnly.firstChild.firstChild.firstChild.style.height = resizeFactor;
									dcTextBoxReadOnly.firstChild.firstChild.firstChild.firstChild.style.height = resizeFactor;
								}

								
								
								oCell.appendChild(dcTextBoxReadOnly);
								oRow.appendChild(oCell);
								this.body0.appendChild(oRow);
								
							}
						}
					}
				}
			}
		}	
	}
	
	function getStratChoiceCountForCol(strat2,id)
	{
		var count = 0;
		
		var k = 0;
				for ( k=1;k<strat2.strategyChoicesCount+1;k++)
				{
					var sc = null;
					//TheCurrentEditor().Log( strat2.key );
					
					var indx = "item_" + k;
					eval("sc = strat2.strategyChoices." + indx + ";");
					if(sc!= null)
					{
						if(sc.visible == true)
						{
							var tmpSCkey = sc.choiceKey;
							var bits = tmpSCkey.split("~");
							var tmpHeadKey = bits[0];
							var tmpCellKey = bits[1];
							if(tmpHeadKey == id)
							{
								//item was part of strategy for this row.
								
								count++;
								
							}
						}
					}
				}
				return count;
	}
	
	function getDcPanel(dcName,id)
	{
		
		var dcRow = this.utils.getRow();
		var dcCell = this.utils.getCell();
		dcCell.className = "clsDC_cell";
		dcRow.appendChild(dcCell);
		
		var dcController_MoveUp = this.utils.getButton("dcController_" + id,"move up", this.dcController_up,"buttonSmallWhite");
		var dcController_MoveDown = this.utils.getButton("dcController_" + id,"move down", this.dcController_down,"buttonSmallWhite");
		var dcController_Hide = this.utils.getButton("dcController_" + id,"hide", this.dcController_hide,"buttonSmallWhite");
		var dcController_AddToCurrentStrategy = this.utils.getButton("dcController_" + id,"add/remove to current strategy", this.dcController_addChoiceToCurrentStrategy,"buttonSmallWhite");
		var spacer = document.createElement("DIV");
		spacer.className = "clsSpacer";
		
		var dcControllerPanel = this.utils.getFormattingTable_5x1(dcController_MoveUp,dcController_Hide,dcController_MoveDown,spacer,dcController_AddToCurrentStrategy);
		
		var dcInputBoxID = "dcTextArea_" + id;
		var dcInputBox = this.utils.getTextArea(dcName,dcInputBoxID,dcInputBox_focus,dcInputBox_blur,"decisionChoicePoppedOut");
		

		var dcInputBoxPanel = null;
			
		if( TheCurrentEditor().mode == TheCurrentEditor().MULTI_STRAT )
		{
			var dcColorMatrix = new DecisionStrategyIndicator();
			dcColorMatrix.init(dcInputBoxID,st.strategyList.strategyCount);
			dcInputBoxPanel = this.utils.getFormattingTable_2x1(dcColorMatrix.rootDiv, dcInputBox);	
		}
		else
		{
			dcInputBoxPanel = this.utils.getFormattingTable_1x1(dcInputBox);
		}
		
		
		dcCell.appendChild(dcInputBoxPanel);
		
		return dcRow;
	}
	
	function getDcPanelReadOnly(dcName,id,color,title)
	{
		var dcDiv = this.utils.getDiv(id,dcName,"clsDcMatrix",title);	

		if(color != null)
		{
			alert(color);
			dcDiv.style.className = "cls_" + color + "";//.backgroundColor = color;
		//	
		}
			
			
		var dcPanel = this.utils.getFormattingTable_1x1(dcDiv,0);

		
		TheCurrentEditor().rectangleDimensions.decorateWidget(dcDiv);
		TheCurrentEditor().rectangleDimensions.decorateWidget(dcPanel);
		
		return dcPanel;
	
	}
	
	function getDahPanel(dahName,id)
	{
		var leftCursorButton  = this.utils.getButton("dahController_" + id,"move left", this.dahController_left);
		var dahInputBox = this.utils.getTextArea(dahName,"dahInputBox_" + id,dahInputBox_focus,dahInputBox_blur,"decisionAreaHeaderPoppedOut");

		TheCurrentEditor().rectangleDimensions.decorateWidget(dahInputBox);

		var hideButton   = this.utils.getButton("dahController_" + id,"hide", this.dahController_remove);
		var rightCursorButton   = this.utils.getButton("dahController_" + id,"move right", this.dahController_right);
		var panel = this.utils.getFormattingTable_3x1(leftCursorButton,hideButton,rightCursorButton);
		
		var panelOuter = null;
		if(TheCurrentEditor().showNavButtons)
			panelOuter = this.utils.getFormattingTable_1x2(panel,dahInputBox);	
		else
			panelOuter = this.utils.getFormattingTable_1x1(dahInputBox);
			
		
		
		return panelOuter;
	}
	
	
	
	function dcController_up()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
		var decisionChoice = st.decisionAreaContainer.DcMoveUp(bits[2],bits[4]);
		st.Display();
	}
	
	function dcController_down()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
		var decisionChoice = st.decisionAreaContainer.DcMoveDown(bits[2],bits[4]);
		st.Display();		
	}
	
	function dcController_hide()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
		var decisionChoice = st.decisionAreaContainer.DcHide(bits[2],bits[4]);
		st.Display();

	}
	
	function dcController_addChoiceToCurrentStrategy()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
		var dcTextAreaID = "dcTextArea_" + bits[1] + "_" + bits[2] + "_" + bits[3] + "_" + bits[4];
		
		var added = TheCurrentEditor().AddChoice(dcTextAreaID);
		var elem = document.all.item(dcTextAreaID);
		
		if(added)
		{
			elem.className = "cls_" + TheCurrentEditor().CurrentStrategy.color;
		}
		else
		{
			elem.className = "decisionChoicePoppedOut";
		}
		
		
		TheCurrentEditor().rectangleDimensions.decorateWidget(elem);
	
	}
	
	function dahInputBox_focus()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");
		var dh = st.decisionAreaContainer.GetDecisionAreaHeader(bits[3]);

		TheCurrentEditor().ClearCurrent();
		TheCurrentEditor().CurrentWidget = srcElem;
		TheCurrentEditor().CurrentHeader = dh;
		TheCurrentEditor().DecorateCurrentWidget();
	}
	
	function dahInputBox_blur()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		
		var inputBoxBits = srcElem.id.split('_');
		var dataID = inputBoxBits[2] + "_" + inputBoxBits[3];
	
		//$64
		st.decisionAreaContainer.UpdateDecisionAreaHeader( dataID, srcElem.value );
	}
	
	function dcInputBox_blur()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		st.decisionAreaContainer.UpdateDecisionChoice(srcElem.id,srcElem.value);
	}
	
	function dcInputBox_focus()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split("_");

		var dc = st.decisionAreaContainer.GetDecisionAreaChoiceForHeader(bits[2],bits[4]);
		TheCurrentEditor().ClearCurrent();
		TheCurrentEditor().CurrentWidget = srcElem;
		TheCurrentEditor().CurrentChoice = dc;
		TheCurrentEditor().DecorateCurrentWidget();
	}
	 
	
	
	
	function dahController_left()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var dahKey = bits[2] + "_" + bits[3];
		st.decisionAreaContainer.DahMoveLeft(dahKey);
		
		TheCurrentEditor().RenderCurrentStrategy();
	}
	
	function dahController_right()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var dahKey = bits[2] + "_" + bits[3];
		st.decisionAreaContainer.DahMoveRight(dahKey);
		
		TheCurrentEditor().RenderCurrentStrategy();
	}

	function dahController_remove()
	{
		var v = window.event;
		var srcElem = v.srcElement;
		var bits = srcElem.id.split('_');
		var dahKey = bits[2] + "_" + bits[3];
		st.decisionAreaContainer.DahHide(dahKey);

		TheCurrentEditor().RenderCurrentStrategy();	
	}
	
	//------------------------------------------ end decision area column
	
	
	
	
	function DecisionAreaHeaderHeader()
	{
		var _strategyUtils = new StrategyUtils();
		this.utils = _strategyUtils;

		var oTable = document.createElement("TABLE");
		var oTBody0 = document.createElement("TBODY");
		var oDAHrow = document.createElement("TR");

		this.table = oTable;
		this.body0 = oTBody0;
		this.dahrow = oDAHrow;
		
		this.Init = DecisionAreaHeaderHeaderInit;
		this.AddHeader = AddDecisionAreaHeaderHeader;
		this.Clear = ClearHeaderHeader;
		
		//for observer pattern
		this.Update = DecisionAreaHeaderHeaderUpdate;
	}
	
	function DecisionAreaHeaderHeaderUpdate(decisionAreaContainer)
	{
		///////////////////////////////////////////////////////////////////////
		//	
		//	callback that data has changed in the underlying model ( data )
		//
		///////////////////////////////////////////////////////////////////////

		this.Clear();
		
		var dsi = decisionAreaContainer.getSequenceArray();
		var i = 0;
		var key;
		
		for(i=1;i< decisionAreaContainer.dahCount + 1;i++)
		{
			var dah = dsi[i];
			
			if(dah.visible == true)
			{
				this.AddHeader(dah);
			}
		}
		
	}
	
	function DecisionAreaHeaderHeaderInit()
	{
		this.table.appendChild(this.body0);
		this.table.border = 0;
		
		var nnm = this.table.attributes;
		var namedItem = document.createAttribute("cellpadding");
		namedItem.value = 0;
		nnm.setNamedItem(namedItem);

		var namedItem2 = document.createAttribute("cellspacing");
		namedItem2.value = 0;
		nnm.setNamedItem(namedItem2);

		this.body0.appendChild(this.dahrow);
		
		
		
		
	}
	
	function ClearHeaderHeader()
	{
		//clear out all children of the dah row!
		var currentLength = this.dahrow.children.length;
		for (i=0; i<currentLength; i++)
		{
			this.dahrow.removeChild( this.dahrow.children[0] );
		}
	}
	
	function AddDecisionAreaHeaderHeader(header)
	{
		var dacCell = this.utils.getCell();
		var dahHeader = document.createElement("DIV");
		dahHeader.className = "cls_dahHeader";
		
		dahHeader.style.width = TheCurrentEditor().rectangleDimensions.width + 9;
		
		
		dahHeader.innerText = TheCurrentEditor().utils.decode64(header.name);
		dacCell.appendChild(dahHeader);
		this.dahrow.appendChild(dacCell);
		
		
		//add a little spacer cell.
		var spacerCell = this.utils.getCell();
		var spacerDiv = document.createElement("DIV");
		spacerDiv.style.width = "3px";
		spacerCell.appendChild(spacerDiv);
		this.dahrow.appendChild(spacerCell);
		
	}
	
	//--------------------------- end decision area header header
	
	
	
	
	
	/////////////////////////////////////////////
	//object representing a DECISION AREA HEADER.
	function DecisionAreaHeader()
	{
		var _decisionChoices = new Object;
		this.DecisionChoices = _decisionChoices;
		var _dcCount = 0;
		this.dcCount = _dcCount;
		this.count = _dcCount;
		
		var name = "";
		var key = "";
		var value = "";
		var seq = 0;
		var _visible = true;
		
		this.name = name;
		this.key = key;
		this.value = value;
		this.seq = seq;
		this.visible = _visible;

	}

	////////////////////////////////////////////
	//object representing a DECISION AREA CHOICE
	
	function DecisionAreaChoice()
	{
		var name = "";
		var key = "";
		var value = "";
		var seq = 0;
		var _visible = true;
		var _parentHeaderID = null;
		
		this.name = name;
		this.key = key;
		this.value = value;
		this.seq = seq;
		this.visible = _visible; 
		this.parentHeaderID = _parentHeaderID;
	}
	
