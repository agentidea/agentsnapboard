var projectSelection = {
	addSelectionToPortfolio: function(src,target,limit) {
		dirtyFlag = "on";
		i = 0;
		var count = target.options.length;
		var overflow = false;
		while (i<src.options.length && !overflow) {
				if (src.options[i].selected) {
	  				count++;
					if (count>limit) {
						this.sortAndCreateFundedListFrom(target);
						alert("You tried adding more than "+limit+" items.");
						overflow = true;
					} else {
						var newOption = document.createElement('option');
						newOption.text = src.options[i].text;
						newOption.value = src.options[i].value;
						try {
							target.add(newOption);
						}
						catch(ex) {
							target.add(newOption, null);
						}
						src.remove(i);
					}
				} else {
				i++;
			}
		}
		this.createUnfundedListFrom(src);
		this.sortAndCreateFundedListFrom(target);
	},

	createUnfundedListFrom: function(src) {
		var html = "<table bgcolor='#000000'><tr><td><table cellspacing = 2 cellpadding = 5 bgcolor='#FFFFFF'><tr><td><B><U>Project Type</U></B></td><td align=center><B><U>Die Roll</U></B></td><td align=center><B><U>Result</U></B></td><td align=center><B><U>Value</U></B></td>";	
		var itemText, buttonText, lineToAdd;
		for(var j=0; j<src.options.length; j++)  {
			itemText = "<tr><td>"+src.options[j].text+"</td>";
			imgTag = "<span id='images_unfunded"+j+"'><img name='rotating_picture"+j+"_tetra_unfunded' src='images/blank.png' width='50' height='50'>";
			imgTag += "<img name='rotating_picture"+j+"_unfunded' src='images/blank.png' width='50' height='50'></span>";
			buttonText = "<td align=center>"+imgTag+"<span id='roll"+j+"_unfunded'> <input type=button value='roll' onclick='rollUnfunded(";
			buttonText += j+',"'+src.options[j].text+'"';
			buttonText += ");'";
			lineToAdd = itemText+buttonText+">";
			lineToAdd += "</span><br/><span id='roll"+j+"_unfunded_text'></span></td>";
			lineToAdd += "<td align=center><span id='roll"+j+"_unfunded_success'>&nbsp;</td><td align=center><span id='roll"+j+"_unfunded_value'>&nbsp;</td>";
			lineToAdd += "</tr>";
			html += lineToAdd;
		}
		html += "<tr><td colspan=4><hr/></td></tr>";
		html += "<tr><td>&nbsp;</td><td>&nbsp;</td><td align=center><span id='numberOfSuccesses_unfunded'><i>Total Successes</i></span></td><td align=center><span id='totalValueUnfunded'><i>Total Value</i></span></td></tr>";
		html += "</table></td></tr></table>";
		var el = document.getElementById("unfundedProjectList");
		el.innerHTML = html;
		for(var j=0; j<src.options.length; j++)  {
			$("#images_unfunded"+j).hide();
		}
	},

	sortAndCreateFundedListFrom: function(target) {
		var arrTexts = new Array();
		var j = 0;
		while (target.length>0)  {
			arrTexts[j] = target.options[0];
			j++;
			target.remove(0);
		}
		arrTexts.sort(this.compareOptions);
		portfolio.clear();
		var html = "<table bgcolor='#000000'><tr><td><table cellspacing = 2 cellpadding = 5 bgcolor='#FFFFFF'><tr><td><B><U>Project Type</U></B></td><td align=center><B><U>Die Roll</U></B></td><td align=center><B><U>Result</U></B></td><td align=center><B><U>Value</U></B></td>";
		for(j=0; j<arrTexts.length; j++)  {
			try {
				target.add(arrTexts[j]);
			}
			catch (ex) {
				target.add(arrTexts[j], null);
			}
			portfolio.addProject(arrTexts[j].text);
			itemText = "<tr><td>"+arrTexts[j].text+"</td>";
			imgTag = "<span id='images_funded"+j+"'><img name='rotating_picture"+j+"_tetra_funded' src='images/blank.png' width='50' height='50'>";
			imgTag += "<img name='rotating_picture"+j+"_funded' src='images/blank.png' width='50' height='50'></span>";
			buttonText = "<td align=center>"+imgTag+"<span id='roll"+j+"_funded'> <input type=button value='roll' onclick='rollFunded(";
			buttonText += j+',"'+arrTexts[j].text+'"';
			buttonText += ");'";
			lineToAdd = itemText+buttonText+"></span><br/><span id='roll"+j+"_funded_text'></span></td>";
			lineToAdd += "<td align=center><span id='roll"+j+"_funded_success'>&nbsp;</td><td align=center><span id='roll"+j+"_funded_value'>&nbsp;</td>";
			lineToAdd += "</tr>";
			html += lineToAdd;
		}	
		html += "<tr><td colspan=4><hr/></td></tr>";
		html += "<tr><td>&nbsp;</td><td>&nbsp;</td><td align=center><span id='numberOfSuccesses_funded'>0</span></td><td align=center><span id='totalValueFunded'>0</span></td></tr>";
		html += "</table></td></tr></table>";
		var el = document.getElementById("fundedProjectList");
		el.innerHTML = html;
		for(var j=0; j<arrTexts.length; j++)  {
			$("#images_funded"+j).hide();
		}
	},

	compareOptions: function(optionA, optionB) {
		if (optionA.value < optionB.value) {
			return -1; 
		} else {
			return 1;
		}
	}

}