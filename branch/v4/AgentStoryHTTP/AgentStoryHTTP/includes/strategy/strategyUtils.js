	//////////////////
	// utils
	function StrategyUtils()
	{
	
		this.getGrid = getGrid;
		this.setGridCell = setGridCell;
		this.GetActualMatrixPosition = GetActualMatrixPosition;
		
		
													   //cols x rows
		this.getFormattingTable_1x1 = getFormattingTable_1x1;
		this.getFormattingTable_1x2 = getFormattingTable_1x2;
		this.getFormattingTable_1x3 = getFormattingTable_1x3;
		this.getFormattingTable_1x4 = getFormattingTable_1x4;
		
		
		this.getFormattingTable_2x1 = getFormattingTable_2x1;
		this.getFormattingTable_3x1 = getFormattingTable_3x1;
		this.getFormattingTable_4x1 = getFormattingTable_4x1;
		this.getFormattingTable_5x1 = getFormattingTable_5x1;
		this.getFormattingTable_6x1 = getFormattingTable_6x1;
		this.getFormattingTable_7x1 = getFormattingTable_7x1;
		this.getFormattingTable_8x1 = getFormattingTable_8x1;
		this.getFormattingTable_9x1 = getFormattingTable_9x1;
		
		this.getCell = getCell;
		this.getRow = getRow;
		this.getButton = getButton;
		this.getButton2 = getButton2;
		this.getInputBox = getInputBox;
		this.getSimpleInputBox = getSimpleInputBox;
		this.getTextArea = getTextArea;
		this.getSelect = getSelect;
		this.getList = getList;
		this.getCheckbox = getCheckbox;
		this.getDiv = getDiv;
		
		this.clearChildren = clearChildren;
		this.findElement = findElement;
		
		var _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
		this.keyStr = _keyStr;
		this.encode64 = encode64;
		this.decode64 = decode64;
	}
	



function encode64(input) {
// Base64 code from Tyler Akins -- http://rumkin.com
   var output = "";
   
   if(input == "") return "";
   
   var lsKeyStr = this.keyStr;
   var chr1, chr2, chr3;
   var enc1, enc2, enc3, enc4;
   var i = 0;

   do {
      chr1 = input.charCodeAt(i++);
      chr2 = input.charCodeAt(i++);
      chr3 = input.charCodeAt(i++);

      enc1 = chr1 >> 2;
      enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
      enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
      enc4 = chr3 & 63;

      if (isNaN(chr2)) {
         enc3 = enc4 = 64;
      } else if (isNaN(chr3)) {
         enc4 = 64;
      }

      output = output + lsKeyStr.charAt(enc1) + lsKeyStr.charAt(enc2) + 
         lsKeyStr.charAt(enc3) + lsKeyStr.charAt(enc4);
   } while (i < input.length);
   
   return output;
}

function decode64(input) {
   var output = "";
   
   if(input == "") return "";
   
   var lsKeyStr = this.keyStr;
   var chr1, chr2, chr3;
   var enc1, enc2, enc3, enc4;
   var i = 0;

   // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
   input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

   do {
      enc1 = lsKeyStr.indexOf(input.charAt(i++));
      enc2 = lsKeyStr.indexOf(input.charAt(i++));
      enc3 = lsKeyStr.indexOf(input.charAt(i++));
      enc4 = lsKeyStr.indexOf(input.charAt(i++));

      chr1 = (enc1 << 2) | (enc2 >> 4);
      chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
      chr3 = ((enc3 & 3) << 6) | enc4;

      output = output + String.fromCharCode(chr1);

      if (enc3 != 64) {
         output = output + String.fromCharCode(chr2);
      }
      if (enc4 != 64) {
         output = output + String.fromCharCode(chr3);
      }
   } while (i < input.length);

   return output;
}

	function findElement(key,elementTagName)
    {
        var tags = document.getElementsByTagName(elementTagName);
        
        for(var i=0;i<tags.length;i++)
        {
           //alert("found divs " + tags[i].id);
            if(tags[i].id == key)
            {
                return tags[i];
            }
        }
    }
	
	//GET position of a col map that maps seq number to an actual col position
	function GetActualMatrixPosition(aActualColMap,seqNum)
	{
	
		var lenMap = aActualColMap.length;
		var ret = -1;
		
		var i = 0;
		for(i=0;i<lenMap;i++)
		{
			if(seqNum == aActualColMap[i])
			{
				return i;
			}	
		
		}
		
		alert("CRITICAL MATRIX ERROR: no matrixPos found for seqNum " + seqNum);
		return ret;
	}
	
	function setGridCell( row, col, val )
	{
	
		//so grid is accessible via the named divs
		//ie divGrid_1_1 for first cell.
		// row first then col
		var key = "divGrid_" + row + "_" + col;
		
		var widget = document.all.item(key);
		if(widget != null)
		{
			
			if(widget.innerHTML != "")
			{
				//alert( widget.innerHTML );
				var prevHTML = widget.innerHTML;
				if(prevHTML == "&nbsp;")
				{
					widget.innerHTML = "<div>" + val + "</div>";
				}
				else
				{
				
					widget.innerHTML = prevHTML + " " + "<div>" + val + "</div>";
				}
			}
			else
			{
				//widget.innerHTML = "<div>zzzz" + val + "</div>";
			}
				
			//TheCurrentEditor().Log( "set widget's innerText to " + widget.innerText);
		}
		else
		{
			TheCurrentEditor().Log("could not find grid cell/widget by way of key :: " + key + " for row:" + row + " col:" + col + " for val:" + val );
		}
	
		return widget;
	}
	
	
	function getGrid(rows,cols)
	{
		
		TheCurrentEditor().Log("About to create a grid  " + rows + "X" + cols + " rowsXcols for a col width of " + 
		TheCurrentEditor().MatrixViewSize.width );
		
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		table.appendChild(body);
		table.border = 1;
	
		if(TheCurrentEditor().MatrixViewSize.width > 0 )
		{
			table.width = 	( TheCurrentEditor().MatrixViewSize.width * cols);
		}

		table.cellSpacing = 0;
		table.cellPadding = 2;
		
		for (i = 1;i<rows + 1;i++)
		{
			var tr = this.getRow();
			var k = 1;
			
			
			for(k = 1;k<cols + 1;k++)
			{
				var tc = this.getCell();
				
				if(TheCurrentEditor().MatrixViewSize.width > 0 )
				{
					tc.style.width  = 	TheCurrentEditor().MatrixViewSize.width;
				}
				
				var divContent = document.createElement("DIV");
				divContent.className = "clsGridCell";
				divContent.id = "divGrid_" + i + "_" + k;
				divContent.innerHTML = "&nbsp;";
				divContent.title = "" + i + ":" + k ;
				tc.appendChild(divContent);
				tr.appendChild( tc );
			}
			
			body.appendChild(tr);
		
		
		}
		return table;
	}
	
	function getDiv(id,text,className,title)
	{
		var div = document.createElement("DIV");
		
		if(id!=null)
			div.id = id;
			
		if(text != null)
			div.innerText = text;
			
		if(title != null) 
			div.title = title;
			
		if(className != null)
			div.className = className;
			
		return div;
	
	}
	
	function clearChildren( body )
	{
		var i = 0;
		var currentLength = body.children.length;
		
		for (i=0; i<currentLength; i++)
		{
			body.removeChild( body.children[0] );
		}
	
	}
	
	function getCheckbox(id,label,title,clickHandler,className)
	{
		//alert(clickHandler);
		var chk = document.createElement("INPUT");
		chk.type = "checkbox";
		chk.id = id;
		chk.className = className;
		chk.onclick = clickHandler;
		chk.title = title;
		
		var lbl = document.createElement("DIV");
		lbl.innerText = label;
		lbl.className = className;
		lbl.title = title;
		
		var layout = this.getFormattingTable_2x1(chk,lbl);
		return layout;
	}
	
	
	function getList(id,size,className)
	{
		var listbox = document.createElement("SELECT");
		listbox.id = id;
		listbox.size = size;
		listbox.className = className;
		return listbox;
	
	}
	
	
	function getSelect(pipeDelimetedVals, callback)
	{
		var sel = document.createElement("SELECT");
		var items = pipeDelimetedVals.split("|");
		
		sel.onchange = callback;

		//alert(items);

		for(i=0;i<items.length;i++)
		{
			//alert(items[i]);
			var ooption = document.createElement("OPTION");
			sel.options.add(ooption);
			ooption.innerText = items[i];
			ooption.value = i;
		}
		
		
		return sel;
	}
	function getTextArea(val,id,focusHandler,blurHandler,className)
	{
			var textArea = document.createElement("TEXTAREA");
			textArea.value = val;
			textArea.title = val;
			textArea.id = id;
			
			if(focusHandler != null)
				textArea.onfocus = focusHandler;
			if(blurHandler != null)
				textArea.onblur = blurHandler;
				
			textArea.className = className;
			textArea.style.overflow = "auto";
			return textArea;
	}
	
	function getInputBox(val,id,focusHandler,blurHandler,className)
	{
			var inputBox = document.createElement("INPUT");
			inputBox.value = val;
			inputBox.title = val;
			inputBox.id = id;
			if(focusHandler != null)
				inputBox.onfocus = focusHandler;
				
			if(blurHandler != null)
				inputBox.onblur = blurHandler;
			
			inputBox.className = className;
			return inputBox;
	}
	
	function getSimpleInputBox(val,id,className)
	{
			var inputBox = document.createElement("INPUT");
			inputBox.value = val;
			inputBox.id = id;
			inputBox.className = className;
			return inputBox;
	}
	
	//one col one rows
	function getFormattingTable_1x1(content1,cellpad)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1;
		table.appendChild(body);
		table.border = 0;
		
		if(cellpad != null)
		{
			table.cellspacing = 0;
			table.cellpadding = 0;
		}
		
		row1 = this.getRow();
		cell1 = this.getCell();
		row1.appendChild(cell1);
		body.appendChild(row1);
		cell1.appendChild(content1);
		return table;
	}
	
	//one col two rows
	function getFormattingTable_1x2(content1,content2)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,row2,cell2;
		
		table.appendChild(body);
		table.border = 0;
		
		row1 = this.getRow();
		cell1 = this.getCell();
		row2 = this.getRow();
		cell2 = this.getCell();
		
		row1.appendChild(cell1);
		body.appendChild(row1);

		row2.appendChild(cell2);
		body.appendChild(row2);		

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		
		return table;
	}	
	//three cols one row
	function getFormattingTable_3x1(content1,content2,content3)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		
		return table;
	
	}
	
	//five cols one row
	function getFormattingTable_5x1(content1,content2,content3,content4,content5)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4,cell5;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		cell5 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		row1.appendChild(cell5);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		cell5.appendChild(content5);
		
		return table;
	
	}
	
	
	function getFormattingTable_8x1(content1,content2,content3,content4,content5,content6,content7,content8)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4,cell5,cell6,cell7,cell8;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		cell5 = this.getCell();
		cell6 = this.getCell();
		cell7 = this.getCell();
		cell8 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		row1.appendChild(cell5);
		row1.appendChild(cell6);
		row1.appendChild(cell7);
		row1.appendChild(cell8);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		cell5.appendChild(content5);
		cell6.appendChild(content6);
		cell7.appendChild(content7);
		cell8.appendChild(content8);
		
		return table;
	
	}

function getFormattingTable_9x1(content1,content2,content3,content4,content5,content6,content7,content8,content9)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4,cell5,cell6,cell7,cell8,cell9;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		cell5 = this.getCell();
		cell6 = this.getCell();
		cell7 = this.getCell();
		cell8 = this.getCell();
		cell9 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		row1.appendChild(cell5);
		row1.appendChild(cell6);
		row1.appendChild(cell7);
		row1.appendChild(cell8);
		row1.appendChild(cell9);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		cell5.appendChild(content5);
		cell6.appendChild(content6);
		cell7.appendChild(content7);
		cell8.appendChild(content8);
		cell9.appendChild(content9);
		
		return table;
	
	}




function getFormattingTable_7x1(content1,content2,content3,content4,content5,content6,content7)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4,cell5,cell6;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		cell5 = this.getCell();
		cell6 = this.getCell();
		cell7 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		row1.appendChild(cell5);
		row1.appendChild(cell6);
		row1.appendChild(cell7);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		cell5.appendChild(content5);
		cell6.appendChild(content6);
		cell7.appendChild(content7);
		
		return table;
	
	}
		
	//six cols one row
	function getFormattingTable_6x1(content1,content2,content3,content4,content5,content6)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4,cell5,cell6;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		cell5 = this.getCell();
		cell6 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		row1.appendChild(cell5);
		row1.appendChild(cell6);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		cell5.appendChild(content5);
		cell6.appendChild(content6);
		
		return table;
	
	}
	
	//four cols one row
	function getFormattingTable_4x1(content1,content2,content3,content4)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2,cell3,cell4;
		
		table.appendChild(body);
		table.border = 0;
		
		row1  = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		cell3 = this.getCell();
		cell4 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		row1.appendChild(cell3);
		row1.appendChild(cell4);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		
		return table;
	
	}
	
	//two cols one row
	function getFormattingTable_2x1(content1,content2)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,cell2;
		
		table.appendChild(body);
		table.border = 0;
		
		row1 = this.getRow();
		cell1 = this.getCell();
		cell2 = this.getCell();
		
		row1.appendChild(cell1);
		row1.appendChild(cell2);
		body.appendChild(row1);

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		
		return table;
	}
	
	
	
	//one col three rows
	function getFormattingTable_1x3(content1,content2,content3)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,row2,cell2,row3,cell3;
		
		table.appendChild(body);
		table.border = 0;
		
		row1 = this.getRow();
		cell1 = this.getCell();
		row2 = this.getRow();
		cell2 = this.getCell();
		row3 = this.getRow();
		cell3 = this.getCell();
		
		row1.appendChild(cell1);
		body.appendChild(row1);

		row2.appendChild(cell2);
		body.appendChild(row2);	
		
		row3.appendChild(cell3);
		body.appendChild(row3);			

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		
		return table;
	}
	
	function getFormattingTable_1x4(content1,content2,content3,content4)
	{
		var table = document.createElement("TABLE");
		var body = document.createElement("TBODY");
		var row1, cell1,row2,cell2,row3,row4, cell3,cell4;
		
		table.appendChild(body);
		table.border = 0;
		
		row1 = this.getRow();
		cell1 = this.getCell();
		row2 = this.getRow();
		cell2 = this.getCell();
		row3 = this.getRow();
		cell3 = this.getCell();
		row4= this.getRow();
		cell4 = this.getCell();
		
		row1.appendChild(cell1);
		body.appendChild(row1);

		row2.appendChild(cell2);
		body.appendChild(row2);	
		
		row3.appendChild(cell3);
		body.appendChild(row3);		
		
		row4.appendChild(cell4);
		body.appendChild(row4);			

		cell1.appendChild(content1);
		cell2.appendChild(content2);
		cell3.appendChild(content3);
		cell4.appendChild(content4);
		
		return table;
	}
	
	function getCell()
	{
		var	cell = document.createElement("TD");
		var nnm = cell.attributes;
		var namedItem = document.createAttribute("valign");
		namedItem.value = "top";
		nnm.setNamedItem(namedItem);
		return cell;
	}
	
	function getRow()
	{
		var row = document.createElement("TR");
		return row;
	}
	
	function getButton(id,val,e,backgroundImage,buttonStyle)
	{
		var button = document.createElement("INPUT");
		button.type = "button";
		button.value = val;
		button.title = val;
		button.id = id;
		if(e!=null)
			button.onclick = e;
		
		
		if(buttonStyle == null) buttonStyle = "buttonSmall";
		button.className = buttonStyle;
		return button;
	}
	
	function getButton2(id,val,title,e,buttonStyle)
	{
		var button = document.createElement("INPUT");
		button.type = "button";
		button.value = val;
		button.title = title;
		button.id = id;
		if(e!= null)
			button.onclick = e;
		
		
		if(buttonStyle == null) buttonStyle = "buttonSmall";
		button.className = buttonStyle;
		return button;
	}
	
	function getButtonImage(id,val,e,backgroundImage,buttonStyle)
	{
		var button = document.createElement("IMG");
		//button.type = "button";
		button.value = val;
		button.title = val;
		button.id = id;
		button.onclick = e;
		button.src = "images/up.gif";

		
		if(buttonStyle == null) buttonStyle = "buttonSmall";
		button.className = buttonStyle;
		/*
		if(backgroundImage == null) backgroundImage = "url(images/up.gif)";
		button.style.backgroundImage = backgroundImage;
		*/	
		return button;
	}
	
	// end UTILS
	////////////////////////
	
